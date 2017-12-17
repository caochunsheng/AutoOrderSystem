using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RESTClient
{
    public class Coordinate
    {
        public double x;
        public double y;
        public double z;

        public Coordinate()
        {
            x = 0; y = 0; z = 0;
        }
    }

    public class WorkOrder
    {
        public String BatchCode;

        public double Length;
        public double Width;
        public double Height;

        public String Texture;

        public String Coordinate;

        public int HoleCount;

        public String ParameterCode;

        public int Total;

        public WorkOrder()
        {
            BatchCode = "20160607207";

            Length = 4182;
            Width = 450;
            Height = 100;

            Texture = "E";

            Coordinate = "(0,1396),(450,1396)(0,2792),(450,2792)";

            HoleCount = 3;

            ParameterCode = "E 450 100";

            Total = 17;
        }

        public List<Coordinate> GetCoordinates()
        {
            List<Coordinate> Coordinates = new List<Coordinate>();

            String sCoordinates = Coordinate.Replace(")(", ",");
            sCoordinates = sCoordinates.Replace(")", "");
            sCoordinates = sCoordinates.Replace("(", "");

            String[] Axis = sCoordinates.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < Axis.Length / 2; i++)
            {
                Coordinate coord = new Coordinate();

                coord.x = Convert.ToDouble(Axis[2 * i]);
                coord.y = Convert.ToDouble(Axis[2 * i + 1]);

                Coordinates.Add(coord);
            }
            return Coordinates;
        }

        public String GetPath(double sawDiameter, double sawDepth, double lift)
        {
            StringBuilder path = new StringBuilder();
            List<Coordinate> coord = GetCoordinates();

            path.Append(String.Format("G00 Z{0}\r\n", lift));
            int pt = 0;
            for (int i = 0; i < coord.Count / 2; i++)
            {
                path.Append(String.Format("G00 X{0} Y{1}\r\n", coord[pt].x, coord[pt].y)); pt++;
                path.Append(String.Format("G01 Z{0}\r\n", sawDepth));
                path.Append(String.Format("G01 X{0} Y{1}\r\n", coord[pt].x, coord[pt].y)); pt++;
                path.Append(String.Format("G00 Z{0}\r\n", lift));
            }
            return path.ToString();
        }
    }

    public class MESaw
    {
        public String GetPath(DataTable workOrder, double sawDiameter, double sawDepth, double lift)
        {
            StringBuilder path = new StringBuilder();
            List<Coordinate> coord = new List<Coordinate>();
            if ((workOrder != null) && (workOrder.Rows.Count > 0))
            {
                WorkOrder order = new WorkOrder();

                order.BatchCode = Convert.ToString(workOrder.Rows[0]["BatchCode"]).Trim();
                order.Length = Convert.ToDouble(workOrder.Rows[0]["Length"]);
                order.Width = Convert.ToDouble(workOrder.Rows[0]["Width"]);
                order.Height = Convert.ToDouble(workOrder.Rows[0]["Height"]);

                order.Texture = Convert.ToString(workOrder.Rows[0]["Texture"]);
                order.Coordinate = Convert.ToString(workOrder.Rows[0]["Coordinate"]).Trim();

                order.HoleCount = Convert.ToInt32(workOrder.Rows[0]["HoleCount"]);
                order.ParameterCode = Convert.ToString(workOrder.Rows[0]["ParameterCode"]).Trim();

                order.Total = Convert.ToInt32(workOrder.Rows[0]["Total"]);

                coord = order.GetCoordinates();

                path.Append(order.GetPath(sawDiameter, sawDepth, lift));
            }
            return path.ToString();
        }
    }
}
