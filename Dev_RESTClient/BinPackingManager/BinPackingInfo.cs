using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BinPacking
{
    public class Part
    {
        public long  _SortSequence;

        public string _OrderId;
        public string _PartId;
        public string _OrderNumber;
        public string _PartNumber;
        public string _Model;
        public string _OtherParams;
        public string _Extensions;

        public double _OffsetX;
        public double _OffsetY;
        public double _CutLengthX;
        public double _CutWidthY;

        public double _LengthX;
        public double _WidthY;

        public Boolean _Rotate;
        public long _PartSequence;
        public long _PartTotalCount;

        public Part()
        {
            _SortSequence = -1;

            _OrderId = "";
            _PartId = "";
            _OrderNumber = "";
            _PartNumber = "";
            _Model = "";
            _OtherParams = "";
            _Extensions = "";

            _OffsetX = 0;
            _OffsetY = 0;
            _LengthX = 0;
            _WidthY = 0;

            _Rotate = false;
            _PartSequence = 0;
            _PartTotalCount =0;
        }
    }

    public class MDF
    {
        public long _PageNo;
        public string _JobName;
        public string _PathFile;

        public double _Length;
        public double _Width;
        public double _Thickness;

        public double _LayerDeep;
        public double _ToolLift;

        public long _Algorithm;
        public long _RotateEnable;
        public long _ColinearMode;

        public double _MinRect;
        public double _PlungeAngle;

        public double _TravelSpeed;
        public double _PlungeSpeed;
        public double _FeedSpeed;
        public double _AvoidOvercut;

        public long _ToolNumber;
        public string _ToolDesc;

        public double _Margin;
        public double _ToolRadius;

        public List<Part> _Parts;

        public MDF()
        {
            _PageNo = 0;
            _JobName = "";
            _PathFile = "";

            _Length = 0;
            _Width = 0;
            _Thickness = 0;

            _LayerDeep = 0;
            _ToolLift = 40;

            _Algorithm = 0;
            _RotateEnable = 1;
            _ColinearMode = 1;

            _MinRect = 0;
            _PlungeAngle = 20;

            _TravelSpeed = 0;
            _PlungeSpeed = 0;
            _FeedSpeed = 0;
            _AvoidOvercut = 0;

            _ToolNumber = 1;
            _ToolDesc = "";

            _Margin = 0;
            _ToolRadius = 0;

            _Parts = new List<Part>();
        }

        public string GetPackingConditions()
        {
            StringBuilder conditions = new StringBuilder();

            conditions.Append(String.Format("BinLength={0};", _Length));
            conditions.Append(String.Format("BinWidth={0};", _Width));
            conditions.Append(String.Format("Thickness={0};", _Thickness));

            conditions.Append(String.Format("LayerDeep={0};", _LayerDeep));
            conditions.Append(String.Format("ToolLift={0};", _ToolLift));

            conditions.Append(String.Format("Algorithm={0};", _Algorithm));
            conditions.Append(String.Format("Rotate={0};", _RotateEnable));
            conditions.Append(String.Format("ColinearMode={0};", _ColinearMode));

            conditions.Append(String.Format("MinRect={0};", _MinRect));
            conditions.Append(String.Format("PlungeAngle={0};", _PlungeAngle));

            conditions.Append(String.Format("TravelSpeed={0};", _TravelSpeed));
            conditions.Append(String.Format("PlungeSpeed={0};", _PlungeSpeed));
            conditions.Append(String.Format("FeedSpeed={0};", _FeedSpeed));
            conditions.Append(String.Format("AvoidOvercut={0};", _AvoidOvercut));

            conditions.Append(String.Format("ToolNumber={0};", _ToolNumber));
            conditions.Append(String.Format("ToolDesc={0};", _ToolDesc));

            conditions.Append(String.Format("Margin={0};", _Margin));
            conditions.Append(String.Format("ToolRadius={0};", _ToolRadius));

            return conditions.ToString();
        }
    }

    public class BinPackingInfo
    {
        public static List<MDF> LoadFromXml(string xmlResultFile)
        {
            List<MDF> allMDF = new List<MDF>();
            try
            {
                XmlDocument doc = null;
                try
                {
                    doc = new XmlDocument();
                    doc.Load(xmlResultFile);
                }
                catch (Exception e)
                {
                    return allMDF;
                }

                //工件轮廓信息
                XmlNode nodeRoot = doc.SelectSingleNode("/BinPackingResults");
                if (nodeRoot == null)
                    return allMDF;

                for (int iNode = 0; iNode < nodeRoot.ChildNodes.Count; iNode++)
                {
                    XmlNode nodeMDF = nodeRoot.ChildNodes[iNode];
                    if (nodeMDF == null)
                        continue;

                    if (nodeMDF.Name.Equals("MDF", StringComparison.InvariantCultureIgnoreCase))
                    {
                        MDF newMDF = new MDF();
                        if (nodeMDF.Attributes != null)
                        {
                            foreach (XmlAttribute attribute in nodeMDF.Attributes)
                            {
                                if (attribute.Name.Equals("Page", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    if (!String.IsNullOrEmpty(attribute.Value))
                                    {
                                        newMDF._PageNo = Convert.ToInt32(attribute.Value);
                                    }
                                }
                            }
                        }

                        for (int iProperty = 0; iProperty < nodeMDF.ChildNodes.Count; iProperty++)
                        {
                            XmlNode property = nodeMDF.ChildNodes[iProperty];
                            if (property == null)
                                continue;
                            if (property.Name.Equals("PathFile", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._PathFile = property.InnerText;
                            }
                            else if (property.Name.Equals("JobName", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._JobName = property.InnerText;
                            }
                            else if (property.Name.Equals("MaterialLength", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._Length = Convert.ToDouble(property.InnerText);
                            }
                            else if (property.Name.Equals("MaterialWidth", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._Width = Convert.ToDouble(property.InnerText);
                            }
                            else if (property.Name.Equals("MaterialThickness", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._Thickness = Convert.ToDouble(property.InnerText);
                            }
                            else if (property.Name.Equals("MaterialMargin", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._Margin = Convert.ToDouble(property.InnerText);
                            }
                            else if (property.Name.Equals("ToolRadius", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._ToolRadius = Convert.ToDouble(property.InnerText);
                            }
                            else if (property.Name.Equals("LayerDeep", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._LayerDeep = Convert.ToDouble(property.InnerText);
                            }
                            else if (property.Name.Equals("ToolLift", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._ToolLift = Convert.ToDouble(property.InnerText);
                            }
                            else if (property.Name.Equals("Algorithm", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._Algorithm = Convert.ToInt32(property.InnerText);
                            }
                            else if (property.Name.Equals("Rotate", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._RotateEnable = Convert.ToInt32(property.InnerText);
                            }
                            else if (property.Name.Equals("ColinearMode", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._ColinearMode = Convert.ToInt32(property.InnerText);
                            }
                            else if (property.Name.Equals("MinRect", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._MinRect = Convert.ToDouble(property.InnerText);
                            }
                            else if (property.Name.Equals("PlungeAngle", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._PlungeAngle = Convert.ToDouble(property.InnerText);
                            }
                            else if (property.Name.Equals("TravelSpeed", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._TravelSpeed = Convert.ToDouble(property.InnerText);
                            }
                            else if (property.Name.Equals("PlungeSpeed", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._PlungeSpeed = Convert.ToDouble(property.InnerText);
                            }
                            else if (property.Name.Equals("FeedSpeed", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._FeedSpeed = Convert.ToDouble(property.InnerText);
                            }
                            else if (property.Name.Equals("AvoidOvercut", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._AvoidOvercut = Convert.ToDouble(property.InnerText);
                            }
                            else if (property.Name.Equals("ToolNumber", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._ToolNumber = Convert.ToInt32(property.InnerText);
                            }
                            else if (property.Name.Equals("ToolDesc", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!String.IsNullOrEmpty(property.InnerText))
                                    newMDF._ToolDesc = (property.InnerText);
                            }
                            else if (property.Name.Equals("Rect", StringComparison.InvariantCultureIgnoreCase))
                            {
                                Part newPart = new Part();
                                for (int iRect = 0; iRect < property.ChildNodes.Count; iRect++)
                                {
                                    XmlNode rectProperty = property.ChildNodes[iRect];
                                    if (rectProperty == null)
                                        continue;

                                    if (rectProperty.Name.Equals("SortSequence", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(rectProperty.InnerText))
                                            newPart._SortSequence = Convert.ToInt32(rectProperty.InnerText);
                                    }
                                    else if (rectProperty.Name.Equals("OrderIdent", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(rectProperty.InnerText))
                                            newPart._OrderId = rectProperty.InnerText;
                                    }
                                    else if (rectProperty.Name.Equals("PartIdent", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(rectProperty.InnerText))
                                            newPart._PartId = rectProperty.InnerText;
                                    }
                                    else if (rectProperty.Name.Equals("OrderNo", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(rectProperty.InnerText))
                                            newPart._OrderNumber = rectProperty.InnerText;
                                    }
                                    else if (rectProperty.Name.Equals("PartNo", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(rectProperty.InnerText))
                                            newPart._PartNumber = rectProperty.InnerText;
                                    }
                                    else if (rectProperty.Name.Equals("Model", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(rectProperty.InnerText))
                                            newPart._Model = rectProperty.InnerText;
                                    }
                                    else if (rectProperty.Name.Equals("OtherParams", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(rectProperty.InnerText))
                                            newPart._OtherParams = rectProperty.InnerText;
                                    }
                                    else if (rectProperty.Name.Equals("Extensions", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(rectProperty.InnerText))
                                            newPart._Extensions = rectProperty.InnerText;
                                    }
                                    else if (rectProperty.Name.Equals("Offset_x", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(rectProperty.InnerText))
                                            newPart._OffsetX = Convert.ToDouble(rectProperty.InnerText);
                                    }
                                    else if (rectProperty.Name.Equals("Offset_y", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(rectProperty.InnerText))
                                            newPart._OffsetY = Convert.ToDouble(rectProperty.InnerText);
                                    }
                                    else if (rectProperty.Name.Equals("Cut_Length_x", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(rectProperty.InnerText))
                                            newPart._CutLengthX = Convert.ToDouble(rectProperty.InnerText);
                                    }
                                    else if (rectProperty.Name.Equals("Cut_Width_y", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(rectProperty.InnerText))
                                            newPart._CutWidthY = Convert.ToDouble(rectProperty.InnerText);
                                    }
                                    else if (rectProperty.Name.Equals("Length_x", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(rectProperty.InnerText))
                                            newPart._LengthX = Convert.ToDouble(rectProperty.InnerText);
                                    }
                                    else if (rectProperty.Name.Equals("Width_y", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(rectProperty.InnerText))
                                            newPart._WidthY = Convert.ToDouble(rectProperty.InnerText);
                                    }
                                    else if (rectProperty.Name.Equals("Rotate", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(rectProperty.InnerText))
                                            newPart._Rotate = (Convert.ToInt32(rectProperty.InnerText) > 0);
                                    }
                                    else if (rectProperty.Name.Equals("Sequence", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(rectProperty.InnerText))
                                            newPart._PartSequence = Convert.ToInt32(rectProperty.InnerText);
                                    }
                                    else if (rectProperty.Name.Equals("Total", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!String.IsNullOrEmpty(rectProperty.InnerText))
                                            newPart._PartTotalCount = Convert.ToInt32(rectProperty.InnerText);
                                    }
                                }
                                newMDF._Parts.Add(newPart);
                            }
                        }

                        if (newMDF._Parts.Count > 0)
                            allMDF.Add(newMDF);
                    }
                }
            }
            catch (Exception e)
            { 
            }
            return allMDF;
        }
    }
}
