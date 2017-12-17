using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;

namespace RESTClient
{
    public class SharpPoint3D
    {
        public long nMask;
        public double x;
        public double y;
        public double z;

        public SharpPoint3D()
        {
            nMask = 0;
            x = y = z = 0;
        }

        public SharpPoint3D(double px, double py, double pz)
        {
            nMask = 0xE0;
            x = px;
            y = py;
            z = pz;
        }
    }

    public class DrillHole
    {
        public SharpPoint3D _Dir;
        public SharpPoint3D _EntryPoint;

        public string _HolePlane;
        public long _ToolNumber;
        public double _Diameter;
        public double _Deep;

        public DrillHole()
        {
            _ToolNumber = 0;
            _Diameter = 0;
            _Deep = 0;
            _HolePlane = "";

            _Dir = new SharpPoint3D();
            _EntryPoint = new SharpPoint3D();
        }
    }

    public class ProcessBoard
    {
        public SharpPoint3D _MinCorner;
        public SharpPoint3D _MaxCorner;

        public List<SharpPoint3D> _Contour;
        public List<DrillHole> _ALlHoles;

        public ProcessBoard()
        {
            _MinCorner = new SharpPoint3D();
            _MaxCorner = new SharpPoint3D();

            _ALlHoles = new List<DrillHole>();
            _Contour = new List<SharpPoint3D>();
        }
    }

    public partial class HoleLayoutPreview
    {
        private static ProcessBoard ParseProcessBoard(string xmlFile)
        {
            ProcessBoard board = new ProcessBoard();
            try
            {
                XmlDocument doc = null;
                try
                {
                    doc = new XmlDocument();
                    doc.Load(xmlFile);
                }
                catch
                {
                    return null;
                }

                board._MinCorner = new SharpPoint3D(double.MaxValue, double.MaxValue, double.MaxValue);
                board._MaxCorner = new SharpPoint3D(-1 * double.MaxValue, -1 * double.MaxValue, -1 * double.MaxValue);

                //工件轮廓信息
                XmlNode nodeContour = doc.SelectSingleNode("/process/contour");
                if (nodeContour != null)
                {
                    return ParseProcessContour(xmlFile);
                }
                else
                {
                    nodeContour = doc.SelectSingleNode("/part_forms/part/process");
                    if (nodeContour != null)
                        return ParsePartFormsProcess(xmlFile);
                }
            }
            catch
            {
            }

            return board;
        }

        private static ProcessBoard ParseProcessContour(string xmlFile)
        {
            ProcessBoard board = new ProcessBoard();
            try
            {
                XmlDocument doc = null;
                try
                {
                    doc = new XmlDocument();
                    doc.Load(xmlFile);
                }
                catch
                {
                    return null;
                }

                board._MinCorner = new SharpPoint3D(double.MaxValue, double.MaxValue, double.MaxValue);
                board._MaxCorner = new SharpPoint3D(-1 * double.MaxValue, -1 * double.MaxValue, -1 * double.MaxValue);

                //工件轮廓信息
                XmlNode nodeContour = doc.SelectSingleNode("/process/contour");
                if (nodeContour == null)
                    return null;

                for (int iNode = 0; iNode < nodeContour.ChildNodes.Count; iNode++)
                {
                    XmlNode node = nodeContour.ChildNodes[iNode];
                    if (node == null)
                        continue;

                    SharpPoint3D pt = new SharpPoint3D();
                    if (node.Name.Equals("point", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (node.Attributes != null)
                            foreach (XmlAttribute attribute in node.Attributes)
                            {
                                if (attribute.Name.Equals("x", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    if (!String.IsNullOrEmpty(attribute.Value))
                                    {
                                        double value = Convert.ToDouble(attribute.Value);
                                        board._MinCorner.x = Math.Min(board._MinCorner.x, value);
                                        board._MaxCorner.x = Math.Max(board._MaxCorner.x, value);
                                        pt.x = value;
                                        pt.nMask |= 0x80;
                                    }
                                }
                                else if (attribute.Name.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    if (!String.IsNullOrEmpty(attribute.Value))
                                    {
                                        double value = Convert.ToDouble(attribute.Value);
                                        board._MinCorner.y = Math.Min(board._MinCorner.y, value);
                                        board._MaxCorner.y = Math.Max(board._MaxCorner.y, value);
                                        pt.y = value;
                                        pt.nMask |= 0x40;
                                    }
                                }
                                else if (attribute.Name.Equals("z", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    if (!String.IsNullOrEmpty(attribute.Value))
                                    {
                                        double value = Convert.ToDouble(attribute.Value);
                                        board._MinCorner.z = Math.Min(board._MinCorner.z, value);
                                        board._MaxCorner.z = Math.Max(board._MaxCorner.z, value);
                                        pt.z = value;
                                        pt.nMask |= 0x20;
                                    }
                                }
                            }
                    }
                    if (pt.nMask == 0xE0)
                        board._Contour.Add(pt);
                }
                if (board._Contour.Count == 0)
                    return null;

                //工件孔位信息
                long nToolNumber = -1;
                string holePlane = "";
                SharpPoint3D ptDir = new SharpPoint3D();

                XmlNode nodeRoot = doc.SelectSingleNode("/process");
                if (nodeRoot == null)
                    return null;

                for (int iNode = 0; iNode < nodeRoot.ChildNodes.Count; iNode++)
                {
                    XmlNode nodeSide = nodeRoot.ChildNodes[iNode];
                    if (nodeSide != null)
                        if ((nodeSide.Name.Equals("sideHole", StringComparison.InvariantCultureIgnoreCase)) ||
                            (nodeSide.Name.Equals("frontHole", StringComparison.InvariantCultureIgnoreCase)) ||
                            (nodeSide.Name.Equals("backHole", StringComparison.InvariantCultureIgnoreCase)))
                        {
                            holePlane = nodeSide.Name;
                            if (nodeSide.Attributes != null)
                                foreach (XmlAttribute attribute in nodeSide.Attributes)
                                {
                                    if (attribute.Name.Equals("dirx", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(attribute.Value))
                                        {
                                            ptDir.x = Convert.ToDouble(attribute.Value);
                                            ptDir.nMask |= 0x80;
                                        }
                                    }
                                    else if (attribute.Name.Equals("diry", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(attribute.Value))
                                        {
                                            ptDir.y = Convert.ToDouble(attribute.Value);
                                            ptDir.nMask |= 0x40;
                                        }
                                    }
                                    else if (attribute.Name.Equals("dirz", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(attribute.Value))
                                        {
                                            ptDir.z = Convert.ToDouble(attribute.Value);
                                            ptDir.nMask |= 0x20;
                                        }
                                    }
                                }

                            for (int iTool = 0; iTool < nodeSide.ChildNodes.Count; iTool++)
                            {
                                XmlNode nodeTool = nodeSide.ChildNodes[iTool];
                                if (nodeTool != null)
                                    if (nodeTool.Name.Equals("tool", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (nodeTool.Attributes != null)
                                            foreach (XmlAttribute attribute in nodeTool.Attributes)
                                            {
                                                if (attribute.Name.Equals("toolno", StringComparison.InvariantCultureIgnoreCase))
                                                {
                                                    if (!String.IsNullOrEmpty(attribute.Value))
                                                    {
                                                        nToolNumber = Convert.ToInt32(attribute.Value);
                                                    }
                                                }
                                            }

                                        for (int iHole = 0; iHole < nodeTool.ChildNodes.Count; iHole++)
                                        {
                                            XmlNode nodeHole = nodeTool.ChildNodes[iHole];
                                            if (nodeHole != null)
                                                if (nodeHole.Name.Equals("hole", StringComparison.InvariantCultureIgnoreCase))
                                                {
                                                    DrillHole drillHole = new DrillHole();
                                                    drillHole._Dir = new SharpPoint3D(ptDir.x, ptDir.y, ptDir.z);
                                                    drillHole._ToolNumber = nToolNumber;
                                                    drillHole._HolePlane = holePlane;

                                                    if (nodeHole.Attributes != null)
                                                        foreach (XmlAttribute attribute in nodeHole.Attributes)
                                                        {
                                                            if (attribute.Name.Equals("x", StringComparison.InvariantCultureIgnoreCase))
                                                            {
                                                                if (!String.IsNullOrEmpty(attribute.Value))
                                                                {
                                                                    drillHole._EntryPoint.x = Convert.ToDouble(attribute.Value);
                                                                    drillHole._EntryPoint.nMask |= 0x80;
                                                                }
                                                            }
                                                            else if (attribute.Name.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                                                            {
                                                                if (!String.IsNullOrEmpty(attribute.Value))
                                                                {
                                                                    drillHole._EntryPoint.y = Convert.ToDouble(attribute.Value);
                                                                    drillHole._EntryPoint.nMask |= 0x40;
                                                                }
                                                            }
                                                            else if (attribute.Name.Equals("z", StringComparison.InvariantCultureIgnoreCase))
                                                            {
                                                                if (!String.IsNullOrEmpty(attribute.Value))
                                                                {
                                                                    drillHole._EntryPoint.z = Convert.ToDouble(attribute.Value);
                                                                    drillHole._EntryPoint.nMask |= 0x20;
                                                                }
                                                            }
                                                            else if (attribute.Name.Equals("diameter", StringComparison.InvariantCultureIgnoreCase))
                                                            {
                                                                if (!String.IsNullOrEmpty(attribute.Value))
                                                                    drillHole._Diameter = Convert.ToDouble(attribute.Value);
                                                            }
                                                            else if (attribute.Name.Equals("deep", StringComparison.InvariantCultureIgnoreCase))
                                                            {
                                                                if (!String.IsNullOrEmpty(attribute.Value))
                                                                    drillHole._Deep = Convert.ToDouble(attribute.Value);
                                                            }
                                                        }
                                                    board._ALlHoles.Add(drillHole);
                                                }
                                        }
                                    }
                            }
                        }
                }
            }
            catch
            {
            }

            return board;
        }

        public static Bitmap CreateHoleImage(string xmlFile, Boolean bTop = true, Boolean bBottom = true, Boolean bSide = true, double dotsPerMm = 2)
        {
            try
            {
                ProcessBoard board = ParseProcessBoard(xmlFile);
                if (board == null)
                    return null;

                float angle = 30;
                double dblRatio = dotsPerMm;

                Font font = new Font("Terminal", (int)(6 * dblRatio));
                int margin = (int)(50 * (dblRatio + 0.5));

                try
                {
                    Bitmap bmpTest = new Bitmap(1024, 768);
                    bmpTest.MakeTransparent();
                    Graphics gTest = Graphics.FromImage(bmpTest);
                    SizeF testStringSize = gTest.MeasureString("D00.L00.(000.000,000.000)", font);
                    margin = (int)(testStringSize.Width * Math.Cos(Math.PI * angle / 180.0));
                }
                catch
                { }

                int length = Convert.ToInt32(dblRatio * (board._MaxCorner.x - board._MinCorner.x)) + 2 * margin;
                int width = Convert.ToInt32(dblRatio * (board._MaxCorner.y - board._MinCorner.y)) + 2 * margin;

                Bitmap bmp = new Bitmap(length, width);
                bmp.MakeTransparent();

                Graphics g = Graphics.FromImage(bmp);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                //g.TranslateTransform(0, width);
                //g.ScaleTransform(1, -1);

                float[] dashValues = { 16, 4, 2, 3 };
                Pen blackDashPen = new Pen(Color.Gray, 1);
                blackDashPen.DashPattern = dashValues;

                Brush blackBrush = Brushes.Black;
                Pen blackPen = new Pen(Color.Black, 1);
                Pen holePen = new Pen(Color.Black, 1);

                g.FillRectangle(Brushes.White, 0, 0, length, width);
                g.FillRectangle(new SolidBrush(Color.FromArgb(245, 245, 245)), margin, margin, length - 2 * margin, width - 2 * margin);

                if (board._Contour.Count > 0)
                {
                    Point[] points = new Point[board._Contour.Count];
                    for (int i = 0; i < board._Contour.Count; i++)
                    {
                        points[i].X = margin + Convert.ToInt32(dblRatio * board._Contour[i].x);
                        points[i].Y = width - (margin + Convert.ToInt32(dblRatio * board._Contour[i].y));
                    }
                    g.DrawLines(new Pen(Color.Gray, 1), points);
                }

                string labelString = "";
                PointF labelPos = new PointF();
                string labelSide = "";

                if (board._ALlHoles.Count > 0)
                {
                    Point from = new Point();
                    Point to = new Point();
                    for (int i = 0; i < board._ALlHoles.Count; i++)
                    {
                        labelString = "";

                        DrillHole hole = board._ALlHoles[i];

                        from.X = margin + Convert.ToInt32(dblRatio * (hole._EntryPoint.x));
                        from.Y = width - (margin + Convert.ToInt32(dblRatio * (hole._EntryPoint.y)));

                        if (bSide && hole._HolePlane.Equals("sideHole", StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (Math.Abs(hole._Dir.x) > 0.5)
                            {
                                to.Y = from.Y;
                                if (hole._Dir.x < 0)
                                {
                                    to.X = from.X - Convert.ToInt32(dblRatio * hole._Deep); //右侧孔
                                    g.DrawRectangle(holePen,
                                        to.X,
                                        to.Y - Convert.ToInt32(dblRatio * hole._Diameter / 2),
                                        Convert.ToInt32(dblRatio * hole._Deep),
                                        Convert.ToInt32(dblRatio * hole._Diameter));
                                }
                                else
                                {
                                    to.X = from.X + Convert.ToInt32(dblRatio * hole._Deep); //左侧孔
                                    g.DrawRectangle(holePen,
                                        from.X,
                                        from.Y - Convert.ToInt32(dblRatio * hole._Diameter / 2),
                                        Convert.ToInt32(dblRatio * hole._Deep),
                                        Convert.ToInt32(dblRatio * hole._Diameter));
                                }
                                g.DrawLine(blackDashPen, from, to);

                                labelString = String.Format("D{0}.L{1}.Y{2}", hole._Diameter, hole._Deep, hole._EntryPoint.y);
                                if (hole._Dir.x < 0)
                                    labelSide = ("Right");
                                else
                                    labelSide = ("Left");
                                labelPos = (new PointF(from.X, from.Y));
                            }
                            else if (Math.Abs(hole._Dir.y) > 0.5)
                            {
                                to.X = from.X;
                                if (hole._Dir.y < 0)
                                {
                                    to.Y = from.Y + Convert.ToInt32(dblRatio * hole._Deep); //上边孔
                                    g.DrawRectangle(holePen,
                                        from.X - Convert.ToInt32(dblRatio * hole._Diameter / 2),
                                        from.Y,
                                        Convert.ToInt32(dblRatio * hole._Diameter),
                                        Convert.ToInt32(dblRatio * hole._Deep));
                                }
                                else
                                {
                                    to.Y = from.Y - Convert.ToInt32(dblRatio * hole._Deep); //下边孔
                                    g.DrawRectangle(holePen,
                                        to.X - Convert.ToInt32(dblRatio * hole._Diameter / 2),
                                        to.Y,
                                        Convert.ToInt32(dblRatio * hole._Diameter),
                                        Convert.ToInt32(dblRatio * hole._Deep));
                                }

                                g.DrawLine(blackDashPen, from, to);

                                labelString = String.Format("D{0}.L{1}.X{2}", hole._Diameter, hole._Deep, hole._EntryPoint.x);
                                if (hole._Dir.y < 0)
                                    labelSide = ("Top");
                                else
                                    labelSide = ("Bottom");
                                labelPos = (new PointF(from.X, from.Y));
                            }
                        }

                        if (bTop && hole._HolePlane.Equals("frontHole", StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (Math.Abs(hole._Dir.z) > 0.5)
                            {
                                double dblToLeft = Math.Abs(hole._EntryPoint.x - board._MinCorner.x);
                                double dblToRight = Math.Abs(hole._EntryPoint.x - board._MaxCorner.x);

                                double offset = dblRatio * hole._Diameter / 2;

                                if (hole._Dir.z < 0)
                                {
                                }
                                else
                                {
                                    g.DrawEllipse(holePen,
                                        from.X - Convert.ToInt32(dblRatio * hole._Diameter / 2),
                                        from.Y - Convert.ToInt32(dblRatio * hole._Diameter / 2),
                                        Convert.ToInt32(dblRatio * hole._Diameter),
                                        Convert.ToInt32(dblRatio * hole._Diameter));

                                    //g.DrawLine(holePen, new PointF(from.X - Convert.ToInt32(dblRatio * hole._Diameter), from.Y),
                                    //                     new PointF(from.X + Convert.ToInt32(dblRatio * hole._Diameter), from.Y));
                                    //g.DrawLine(holePen, new PointF(from.X, from.Y - Convert.ToInt32(dblRatio * hole._Diameter)),
                                    //                     new PointF(from.X, from.Y + Convert.ToInt32(dblRatio * hole._Diameter)));
                                }
                                labelString = String.Format("D{0}.L{1}.({2},{3})", hole._Diameter, hole._Deep, hole._EntryPoint.x, hole._EntryPoint.y);
                                labelSide = ("Right");
                                labelPos = (new PointF(from.X + Convert.ToInt32(dblRatio * hole._Diameter / 2), from.Y));
                            }
                        }

                        if (bBottom && hole._HolePlane.Equals("backHole", StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (Math.Abs(hole._Dir.z) > 0.5)
                            {
                                double dblToLeft = Math.Abs(hole._EntryPoint.x - board._MinCorner.x);
                                double dblToRight = Math.Abs(hole._EntryPoint.x - board._MaxCorner.x);

                                double offset = dblRatio * hole._Diameter / 2;

                                if (hole._Dir.z < 0)
                                {
                                }
                                else
                                {
                                    g.DrawEllipse(holePen,
                                        from.X - Convert.ToInt32(dblRatio * hole._Diameter / 2),
                                        from.Y - Convert.ToInt32(dblRatio * hole._Diameter / 2),
                                        Convert.ToInt32(dblRatio * hole._Diameter),
                                        Convert.ToInt32(dblRatio * hole._Diameter));

                                    g.DrawLine(holePen, new PointF(from.X - Convert.ToInt32(dblRatio * hole._Diameter), from.Y),
                                                         new PointF(from.X + Convert.ToInt32(dblRatio * hole._Diameter), from.Y));
                                    g.DrawLine(holePen, new PointF(from.X, from.Y - Convert.ToInt32(dblRatio * hole._Diameter)),
                                                         new PointF(from.X, from.Y + Convert.ToInt32(dblRatio * hole._Diameter)));
                                }
                                labelString = String.Format("D{0}.L{1}.({2},{3})", hole._Diameter, hole._Deep, hole._EntryPoint.x, hole._EntryPoint.y);
                                labelSide = ("Right");
                                labelPos = (new PointF(from.X + Convert.ToInt32(dblRatio * hole._Diameter / 2), from.Y));
                            }
                        }

                        if (!String.IsNullOrEmpty(labelString))
                        {
                            SizeF stringSize = new SizeF();

                            double x = labelPos.X;
                            double y = labelPos.Y;
                            stringSize = g.MeasureString(labelString, font);

                            g.RotateTransform(-angle);
                            if (labelSide.Equals("Right", StringComparison.InvariantCultureIgnoreCase))
                            {
                                g.TranslateTransform(Convert.ToSingle(x), Convert.ToSingle(y - (stringSize.Height / 2)), MatrixOrder.Append);
                            }
                            else if (labelSide.Equals("Left", StringComparison.InvariantCultureIgnoreCase))
                            {
                                double nx = x - stringSize.Width * Math.Cos(Math.PI * (angle) / 180.0);
                                double ny = y + stringSize.Width * Math.Sin(Math.PI * (angle) / 180.0);
                                g.TranslateTransform(Convert.ToSingle(nx - (stringSize.Height / 2)), Convert.ToSingle(ny - (stringSize.Height / 2)), MatrixOrder.Append);
                            }
                            else if (labelSide.Equals("Top", StringComparison.InvariantCultureIgnoreCase))
                            {
                                g.TranslateTransform(Convert.ToSingle(x - (hole._Diameter / 2)), Convert.ToSingle(y - (3 * hole._Diameter / 2)), MatrixOrder.Append);
                            }
                            else if (labelSide.Equals("Bottom", StringComparison.InvariantCultureIgnoreCase))
                            {
                                double nx = x - stringSize.Width * Math.Cos(Math.PI * (angle) / 180.0);
                                double ny = y + stringSize.Width * Math.Sin(Math.PI * (angle) / 180.0);
                                g.TranslateTransform(Convert.ToSingle(nx + (hole._Diameter / 2)), Convert.ToSingle(ny), MatrixOrder.Append);
                            }

                            g.DrawString(labelString, font, blackBrush, 0, 0);
                            g.ResetTransform();
                        }
                    }
                }
                g.Flush();
                g.Dispose();
                return bmp;
            }
            catch
            {
            }
            return null;
        }

        private static ProcessBoard ParsePartFormsProcess(string xmlFile)
        {
            ProcessBoard board = new ProcessBoard();
            try
            {
                XmlDocument doc = null;
                try
                {
                    doc = new XmlDocument();
                    doc.Load(xmlFile);
                }
                catch
                {
                    return null;
                }

                board._MinCorner = new SharpPoint3D(double.MaxValue, double.MaxValue, double.MaxValue);
                board._MaxCorner = new SharpPoint3D(-1 * double.MaxValue, -1 * double.MaxValue, -1 * double.MaxValue);

                //工件轮廓信息
                XmlNode nodeContour = doc.SelectSingleNode("/part_forms");
                if (nodeContour == null)
                    return null;

                for (int iNode = 0; iNode < nodeContour.ChildNodes.Count; iNode++)
                {
                    XmlNode node = nodeContour.ChildNodes[iNode];
                    if (node == null)
                        continue;

                    SharpPoint3D pt = new SharpPoint3D();
                    if (node.Name.Equals("part", StringComparison.InvariantCultureIgnoreCase))
                    {
                        for (int item = 0; item < node.ChildNodes.Count; item++)
                        {
                            if (node.ChildNodes[item].Name.Equals("curvelist", StringComparison.InvariantCultureIgnoreCase))
                            {
                                XmlNode nodeLine = node.ChildNodes[item];
                                for (int iLine = 0; iLine < nodeLine.ChildNodes.Count; iLine++)
                                {
                                    if (nodeLine.ChildNodes[iLine].Name.Equals("line", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        SharpPoint3D ptStart = new SharpPoint3D();
                                        SharpPoint3D ptEnd = new SharpPoint3D();

                                        XmlNode nodePoint = nodeLine.ChildNodes[iLine];
                                        for (int iPoint = 0; iPoint < nodePoint.ChildNodes.Count; iPoint++)
                                        {
                                            if (nodePoint.ChildNodes[iPoint].Name.Equals("startpt", StringComparison.InvariantCultureIgnoreCase))
                                            {
                                                XmlNode nodePT = nodePoint.ChildNodes[iPoint];
                                                for (int iPt = 0; iPt < nodePT.ChildNodes.Count; iPt++)
                                                {
                                                    if (nodePT.ChildNodes[iPt].Name.Equals("point", StringComparison.InvariantCultureIgnoreCase))
                                                    {
                                                        if (nodePT.ChildNodes[iPt].Attributes != null)
                                                        {
                                                            foreach (XmlAttribute attribute in nodePT.ChildNodes[iPt].Attributes)
                                                            {
                                                                if (attribute.Name.Equals("x", StringComparison.InvariantCultureIgnoreCase))
                                                                {
                                                                    if (!String.IsNullOrEmpty(attribute.Value))
                                                                        ptStart.x = Convert.ToDouble(attribute.Value);
                                                                }
                                                                else if (attribute.Name.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                                                                {
                                                                    if (!String.IsNullOrEmpty(attribute.Value))
                                                                        ptStart.y = Convert.ToDouble(attribute.Value);
                                                                }
                                                                else if (attribute.Name.Equals("z", StringComparison.InvariantCultureIgnoreCase))
                                                                {
                                                                    if (!String.IsNullOrEmpty(attribute.Value))
                                                                        ptStart.z = Convert.ToDouble(attribute.Value);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else if (nodePoint.ChildNodes[iPoint].Name.Equals("endpt", StringComparison.InvariantCultureIgnoreCase))
                                            {
                                                XmlNode nodePT = nodePoint.ChildNodes[iPoint];
                                                for (int iPt = 0; iPt < nodePT.ChildNodes.Count; iPt++)
                                                {
                                                    if (nodePT.ChildNodes[iPt].Name.Equals("point", StringComparison.InvariantCultureIgnoreCase))
                                                    {
                                                        if (nodePT.ChildNodes[iPt].Attributes != null)
                                                        {
                                                            foreach (XmlAttribute attribute in nodePT.ChildNodes[iPt].Attributes)
                                                            {
                                                                if (attribute.Name.Equals("x", StringComparison.InvariantCultureIgnoreCase))
                                                                {
                                                                    if (!String.IsNullOrEmpty(attribute.Value))
                                                                        ptEnd.x = Convert.ToDouble(attribute.Value);
                                                                }
                                                                else if (attribute.Name.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                                                                {
                                                                    if (!String.IsNullOrEmpty(attribute.Value))
                                                                        ptEnd.y = Convert.ToDouble(attribute.Value);
                                                                }
                                                                else if (attribute.Name.Equals("z", StringComparison.InvariantCultureIgnoreCase))
                                                                {
                                                                    if (!String.IsNullOrEmpty(attribute.Value))
                                                                        ptEnd.z = Convert.ToDouble(attribute.Value);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (node.ChildNodes[item].Name.Equals("process", StringComparison.InvariantCultureIgnoreCase))
                            {
                                XmlNode nodeLine = node.ChildNodes[item];
                                for (int iLine = 0; iLine < nodeLine.ChildNodes.Count; iLine++)
                                {
                                    if (nodeLine.ChildNodes[item].Name.Equals("hole", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        XmlNode nodeHole = nodeLine.ChildNodes[item];

                                        DrillHole drillHole = new DrillHole();
                                        drillHole._Dir = new SharpPoint3D(0, 0, 0);
                                        drillHole._ToolNumber = 0;
                                        drillHole._HolePlane = "";

                                        if (nodeHole.Attributes != null)
                                        {
                                            foreach (XmlAttribute attribute in nodeHole.Attributes)
                                            {
                                                if (attribute.Name.Equals("faceid", StringComparison.InvariantCultureIgnoreCase))
                                                {
                                                    if (!String.IsNullOrEmpty(attribute.Value))
                                                        drillHole._HolePlane = attribute.Value;
                                                }
                                                else if (attribute.Name.Equals("dir", StringComparison.InvariantCultureIgnoreCase))
                                                {
                                                    if (!String.IsNullOrEmpty(attribute.Value))
                                                    {
                                                        int dr = Convert.ToInt32(attribute.Value);
                                                    }
                                                }
                                                else if (attribute.Name.Equals("diameter", StringComparison.InvariantCultureIgnoreCase))
                                                {
                                                    if (!String.IsNullOrEmpty(attribute.Value))
                                                        drillHole._Diameter = Convert.ToDouble(attribute.Value);
                                                }
                                                else if (attribute.Name.Equals("deep", StringComparison.InvariantCultureIgnoreCase))
                                                {
                                                    if (!String.IsNullOrEmpty(attribute.Value))
                                                        drillHole._Deep = Convert.ToDouble(attribute.Value);
                                                }
                                            }
                                        }

                                        for (int iHole = 0; iHole < nodeHole.ChildNodes.Count; iHole ++)
                                        {
                                            if (nodeHole.ChildNodes[iHole].Name.Equals("point", StringComparison.InvariantCultureIgnoreCase))
                                            {
                                                if (nodeHole.ChildNodes[iHole].Attributes != null)
                                                {
                                                    foreach (XmlAttribute attribute in nodeHole.ChildNodes[iHole].Attributes)
                                                    {
                                                        if (attribute.Name.Equals("x", StringComparison.InvariantCultureIgnoreCase))
                                                        {
                                                            if (!String.IsNullOrEmpty(attribute.Value))
                                                            {
                                                                drillHole._EntryPoint.x = Convert.ToDouble(attribute.Value);
                                                                drillHole._EntryPoint.nMask |= 0x80;
                                                            }
                                                        }
                                                        else if (attribute.Name.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                                                        {
                                                            if (!String.IsNullOrEmpty(attribute.Value))
                                                            {
                                                                drillHole._EntryPoint.y = Convert.ToDouble(attribute.Value);
                                                                drillHole._EntryPoint.nMask |= 0x40;
                                                            }
                                                        }
                                                        else if (attribute.Name.Equals("z", StringComparison.InvariantCultureIgnoreCase))
                                                        {
                                                            if (!String.IsNullOrEmpty(attribute.Value))
                                                            {
                                                                drillHole._EntryPoint.z = Convert.ToDouble(attribute.Value);
                                                                drillHole._EntryPoint.nMask |= 0x20;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        board._ALlHoles.Add(drillHole);
                                    }
                                }
                            }
                        }
                    }
                    if (pt.nMask == 0xE0)
                        board._Contour.Add(pt);
                }
                if (board._Contour.Count == 0)
                    return null;
            }
            catch
            {
            }

            return board;
        }
    }
}