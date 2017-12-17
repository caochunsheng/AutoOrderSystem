using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoOrderSystem.Common
{
    public static class ExcelHelper
    {
        public static bool ReadExcel(string excelFilePath, out DataSet ds)
        {
            if (!File.Exists(excelFilePath))
            {
                ds = null;
                return false;
            }
            else
            {
                ds = new DataSet(Path.GetFileNameWithoutExtension(excelFilePath));
                using (FileStream fs = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))//文件占用也能读取
                {
                    IWorkbook workbook = WorkbookFactory.Create(fs);

                    for (int i = 0; i < workbook.NumberOfSheets; i++)
                    {
                        DataTable dt = GetDataTableBySheetName(workbook.GetSheetName(i), workbook,true);
                        ds.Tables.Add(dt);
                    }
                }
                return true;
            }
        }
        public static bool ReadExcel(string excelFilePath, string sheetName, out DataTable dt)
        {
            if (!File.Exists(excelFilePath))
            {
                dt = null;
                return false;
            }
            else
            {
                using (FileStream fs = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))//文件占用也能读取
                {
                    IWorkbook workbook = WorkbookFactory.Create(fs);
                    dt = GetDataTableBySheetName(sheetName, workbook, true);
                }
                return true;
            }
        }
        public static List<string> GetExcelSheetNames(string excelFilePath)
        {
            if (File.Exists(excelFilePath))
            {
                List<string> nameList = new List<string>();

                using (FileStream fs = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    IWorkbook workbook = WorkbookFactory.Create(fs);
                    for (int i = 0; i < workbook.NumberOfSheets; i++)
                    {
                        nameList.Add(workbook.GetSheetName(i));
                    }
                }

                return nameList;
            }
            else
            {
                return null;
            }
        }
        public static bool WriteExcel(DataSet ds, string outFilePath)
        {
            if (ds == null)
            {

                return false;
            }
            else
            {

                IWorkbook workbook = new HSSFWorkbook();
                foreach (DataTable dt in ds.Tables)
                {
                    ISheet sheet = workbook.CreateSheet(dt.TableName);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet.CreateRow(i);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            ICell cell = row.CreateCell(j);
                            cell.SetCellValue(dt.Rows[i][j].ToString());
                        }
                    }

                    //自动列宽
                    for (int i = 0; i <= dt.Columns.Count; i++)
                        sheet.AutoSizeColumn(i, true);
                }
                using (FileStream fs = new FileStream(outFilePath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }

                return true;
            }

        }
        public static bool WriteExcel(DataTable dt, string outFilePath)
        {
            if (dt == null)
            {
                return false;
            }
            else
            {
                IWorkbook workbook = new HSSFWorkbook();
                ISheet sheet = workbook.CreateSheet(dt.TableName);

                IRow row_header = sheet.CreateRow(0);

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ICell cell = row_header.CreateCell(i);
                    cell.SetCellValue(dt.Columns[i].ColumnName);
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        cell.SetCellValue(dt.Rows[i][j].ToString());
                    }
                }
                //自动列宽
                for (int i = 0; i <= dt.Columns.Count; i++)
                    sheet.AutoSizeColumn(i, true);
                using (FileStream fs = new FileStream(outFilePath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
                return true;
            }

        }
        private static DataTable GetDataTableBySheetName(string sheetName, IWorkbook workbook,bool firstRowIsColumnName)
        {
            DataTable dt = new DataTable(sheetName);

            ISheet sheet = workbook.GetSheet(sheetName);

            //总行数
            int rowNum = sheet.PhysicalNumberOfRows;
            //总列数
            int columnNumMax = 0;
            for (int i = 0; i < rowNum; i++)
            {
                if (sheet.GetRow(i) != null)
                {
                    if (sheet.GetRow(i).Cells.Count > columnNumMax)
                    {
                        columnNumMax = sheet.GetRow(i).Cells.Count;
                    }
                }
            }


            if (firstRowIsColumnName)
            {
                IRow row = sheet.GetRow(0);
                for (int i = 0; i < columnNumMax; i++)
                {
                    ICell cell = row.GetCell(i);
       
                    if (dt.Columns.Contains(cell.ToString()))
                    {
                        dt.Columns.Add(cell.ToString()+"1");
                    }
                    else
                    {
                        dt.Columns.Add(cell.ToString());
                    }               
                }


                for (int i = 1; i < rowNum; i++)
                {

                    if (sheet.GetRow(i) != null)
                    {
                        row = sheet.GetRow(i);

                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < row.PhysicalNumberOfCells; j++)
                        {

                            if (row.Cells[j] != null)
                            {
                                ICell cell = row.Cells[j];
                                dr[j] = cell.ToString();
                            }

                        }
                        dt.Rows.Add(dr);
                    }

                }

            }
            else
            {
                for (int i = 0; i < columnNumMax; i++)
                {
                    dt.Columns.Add($"column{i}");
                }

                for (int i = 0; i < rowNum; i++)
                {

                    if (sheet.GetRow(i) != null)
                    {
                        IRow row = sheet.GetRow(i);

                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < row.PhysicalNumberOfCells; j++)
                        {

                            if (row.Cells[j] != null)
                            {
                                ICell cell = row.Cells[j];
                                dr[j] = cell.ToString();
                            }

                        }
                        dt.Rows.Add(dr);
                    }

                }
            }



            //System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            ////获取最长的一行的
            //int Max = 0;
            //for (int i = 0; i < sheet.PhysicalNumberOfRows; i++)
            //{
            //    if (sheet.GetRow(i) != null)
            //    {
            //        if (sheet.GetRow(i).Cells.Count > Max)
            //        {
            //            Max = sheet.GetRow(i).Cells.Count;
            //        }
            //    }
            //}
            //for (int i = 0; i < Max; i++)
            //{
            //    ICell cell = sheet.GetRow(0).GetCell(i);
            //    //string columnname = String.Format("column{0}", i);
            //    string columnname = cell.ToString();
            //    int j = 1;
            //    while(dt.Columns.Contains(columnname))
            //    {
            //        columnname += j.ToString();
            //    }
            //    dt.Columns.Add(columnname);
            //}
            //while (rows.MoveNext())
            //{
            //    IRow row;
            //    try
            //    {
            //        row = (HSSFRow)rows.Current;
            //    }
            //    catch (Exception)
            //    {
            //        row = (XSSFRow)rows.Current;
            //    }
            //    DataRow dr = dt.NewRow();
            //    for (int i = 0; i < row.LastCellNum; i++)
            //    {
            //        if (i >= dt.Columns.Count)
            //        {
            //            continue;
            //        }
            //        ICell cell = row.GetCell(i);
            //        if ((i == 0) && cell == null)//每行第一个cell为空,break
            //        {
            //            break;
            //        }
            //        if (cell == null || cell.ToString() == "")
            //        {
            //            dr[i] = null;
            //        }
            //        else
            //        {
            //            //dr[i] = cell.ToString();
            //            if (cell.CellType == CellType.Formula)
            //            {
            //                cell.SetCellType(CellType.String);
            //                dr[i] = cell.StringCellValue.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
            //            }
            //            else
            //            {
            //                dr[i] = cell.ToString().Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
            //            }
            //        }
            //    }
            //    bool isRowNull = false;
            //    for (int i = 0; i < Max; i++)
            //    {
            //        if (dr[i].ToString() == "")
            //        {
            //            isRowNull = true;
            //            break;
            //        }
            //    }
            //    if (!isRowNull)
            //    {
            //        dt.Rows.Add(dr);
            //    }
            //}
            ////移除第一行的列头
            //dt.Rows.RemoveAt(0);
            return dt;
        }

    }
}
