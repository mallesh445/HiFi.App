using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Office.Interop.Excel;
//using Excel;
//using ExcelDataReader;
using OfficeOpenXml;
using System.Reflection;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace HiFi.Common
{
    public class ExcelHelper
    {
        public static string FileServerPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Saving the excel file project directory
        /// </summary>
        /// <param name="postedExcelFile"></param>
        /// <returns></returns>
        public static string SavePathForThePostedFile(IFormFile postedExcelFile)
        {
            var fileName = Path.GetFileName(postedExcelFile.FileName);
            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(postedExcelFile.FileName);
            var fileExt = Path.GetExtension(postedExcelFile.FileName);

            var uploadPath = Path.Combine(FileServerPath + "ExcelImport", DateTime.Now.ToString("yyyy/MM"));
            bool isExists = Directory.Exists(uploadPath);
            if (!isExists)
                Directory.CreateDirectory(uploadPath);

            var path = Path.Combine(uploadPath, fileName);

            //Add number to file name incase the file already exists. 
            bool condition = true;
            int i = 1;
            while (condition)
            {
                if (!System.IO.File.Exists(path))
                    condition = false;
                else
                {
                    fileName = fileNameWithoutExt + "_" + "UserIdToBeUsed" + "_" + i + fileExt;
                    path = Path.Combine(uploadPath, fileName);
                    i++;
                }
            }
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                postedExcelFile.CopyToAsync(fileStream);
            }
            //postedExcelFile.CopyTo("Your-File-Path");
            //postedExcelFile.SaveAs(path);
            return path;
        }
        public static IList<T> ReadSheet<T>(string filePath, bool hasHeader, int maxHeaderAmount = 0, List<string> colNames = null, bool isOrderImport = false)
        {
            try
            {
                //FileStream fStream = File.OpenRead(filePath);
                System.Data.DataTable dataTable = new System.Data.DataTable();
                DataRow dr = null;
                FileInfo existingFile = new FileInfo(filePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    //get the first worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count
                    for (int row = 1; row <= rowCount; row++)
                    {
                        dr = dataTable.NewRow(); // have new row on each iteration
                        for (int col = 1; col <= colCount; col++)
                        {
                            if (row == 1)
                            {
                                dataTable.Columns.Add(worksheet.Cells[1, col].Value.ToString().Trim());
                                continue;
                            }
                            string a = worksheet.Cells[1, col].Value.ToString().Trim();
                            string rowValue = "";
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(worksheet.Cells[row, col].Value)))
                            {
                                rowValue = worksheet.Cells[row, col].Value.ToString().Trim();
                                dr[col - 1] = rowValue == null ? null : rowValue;
                            }
                            dr[a] = rowValue;
                        }
                        if (row != 1)
                        {
                            dataTable.Rows.Add(dr);
                        }
                    }
                }
                
                //var dt = result.Tables[0];
                var dt = dataTable;

                dt.Rows.Cast<DataRow>().ToList().FindAll(row => string.IsNullOrEmpty(string.Join("", row.ItemArray))).ForEach(row => { dt.Rows.Remove(row); });
              
                var dc = dt.Columns;
                //Trim column names for matching below
                foreach (DataColumn col in dt.Columns)
                {
                    col.ColumnName = col.ColumnName.Trim();
                }
                List<T> list = Activator.CreateInstance<List<T>>();
                T minstance = Activator.CreateInstance<T>();
                var propq = minstance.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
                foreach (DataRow drr in dt.Rows)
                {
                    T ins = Activator.CreateInstance<T>();
                    foreach (var p in propq)
                    {
                        try
                        {
                            p.SetValue(ins, drr[p.Name], null);
                        }
                        catch { }
                    }
                    list.Add(ins);
                }
                return list;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("InValidZipCode"))
                {
                    throw new InvalidDataException(ex.Message);
                }
                else
                {
                    throw new InvalidDataException("We were unable to process the file. Please contact customer support.");
                }
            }
        }

        private static void GetDataTableFromExcel(string filePath)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                //using (var stream = File.OpenRead(path))
                //{
                //    pck.Load(stream);
                //}
                //var ws = pck.Workbook.Worksheets[1];
                //DataTable tbl = new DataTable();
                //foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                //{
                //    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                //}
                //var startRow = hasHeader ? 2 : 1;
                //for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                //{
                //    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                //    DataRow row = tbl.Rows.Add();
                //    foreach (var cell in wsRow)
                //    {
                //        row[cell.Start.Column - 1] = cell.Text;
                //    }
                //}
                //return tbl;
            }
        }

        //public static IList<T> ReadSheetold<T>(string filePath, bool hasHeader, int maxHeaderAmount = 0, List<string> colNames = null, bool isOrderImport = false)
        //{
        //    try
        //    {
        //        List<T> excelList = new List<T>();
        //        //allows read access from many theads at once
        //        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        //        GetDataTableFromExcel(filePath);
        //        MyExcel.Application application = new MyExcel.Application();
        //        MyExcel.Workbook workbook = application.Workbooks.Open(filePath);
        //        MyExcel.Worksheet worksheet = workbook.ActiveSheet;
        //        MyExcel.Range range = worksheet.UsedRange;
        //        List<ShoppingCart.Utilities.ExcelModel.CategoryImportExcel> records = new List<ExcelModel.CategoryImportExcel>();
        //        for (int row = 2; row < range.Rows.Count; row++)
        //        {
        //            ExcelModel.CategoryImportExcel categoryImportExcel = new ExcelModel.CategoryImportExcel();
        //            categoryImportExcel.CategoryName = ((MyExcel.Range)range.Cells[row, 1]).Text;
        //            categoryImportExcel.CreatedByUser = ((MyExcel.Range)range.Cells[row, 2]).Text;
        //            categoryImportExcel.UpdatedByUser = ((MyExcel.Range)range.Cells[row, 3]).Text;
        //            categoryImportExcel.Active = ((MyExcel.Range)range.Cells[row, 6]).Text;
        //            records.Add(categoryImportExcel);
        //        }
        //        //ExcelDataReader.IExcelDataReader excelReader = ExcelDataReader.ExcelReaderFactory.CreateOpenXmlReader(stream);
        //        ExcelDataReader.IExcelDataReader excelReader = null;
        //        if (filePath.EndsWith(".xls"))
        //        {
        //            excelReader = ExcelDataReader.ExcelReaderFactory.CreateBinaryReader(stream);
        //        }
        //        else if (filePath.EndsWith(".xlsx"))
        //        {
        //            excelReader = ExcelDataReader.ExcelReaderFactory.CreateOpenXmlReader(stream);
        //        }

        //        //excelReader.IsFirstRowAsColumnNames = hasHeader;
        //        DataSet result = excelReader.AsDataSet();

        //        if (result.Tables.Count == 0)
        //        {
        //            throw new InvalidDataException("The provided excel contains no sheets");
        //        }

        //        var dt = result.Tables[0];

        //        dt.Rows.Cast<DataRow>().ToList().FindAll(row => string.IsNullOrEmpty(string.Join("", row.ItemArray))).ForEach(row => { dt.Rows.Remove(row); });
        //        if (isOrderImport)
        //        {
        //            var zipCodeColumnPresent = ((DataColumn)dt.Columns[5]) != null && !string.IsNullOrEmpty(((DataColumn)dt.Columns[5]).ColumnName);
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                if (zipCodeColumnPresent && ((DataColumn)dt.Columns[5]).ColumnName.Equals("zipcode", StringComparison.OrdinalIgnoreCase) && ((DataRow)dt.Rows[i]).ItemArray[5].GetType() != typeof(string))
        //                {
        //                    throw new InvalidDataException($"InValidZipCode: {((DataRow)dt.Rows[i]).ItemArray[5]}");
        //                }
        //            }
        //        }

        //        var dc = dt.Columns;
        //        //Trim column names for matching below
        //        foreach (DataColumn col in dt.Columns)
        //        {
        //            col.ColumnName = col.ColumnName.Trim();
        //        }
        //        //Check for more headers in file than excpected
        //        if (maxHeaderAmount != 0 && dc.Count > maxHeaderAmount)
        //        {
        //            throw new InvalidDataException("There are too many columns in this file");
        //        }
        //        //Check for column name match from template
        //        if (colNames != null)
        //        {
        //            foreach (DataColumn col in dt.Columns)
        //            {
        //                if (colNames != null && !colNames.Contains(col.ColumnName))
        //                {
        //                    throw new InvalidDataException("The column names do not match what is expected.");
        //                }
        //            }

        //        }
        //        //Converting Datable to Object List using reflection
        //        foreach (var row in dt.Rows)
        //        {
        //            T instance = (T)Activator.CreateInstance(typeof(T));
        //            foreach (var prop in typeof(T).GetProperties())
        //            {
        //                DataRow datarow = (DataRow)row;
        //                if (datarow.Table.Columns.Contains(prop.Name))
        //                {
        //                    // Set the value of the given property on the given instance
        //                    if (!Object.ReferenceEquals(datarow[prop.Name, DataRowVersion.Original], DBNull.Value))
        //                    {
        //                        try
        //                        {
        //                            prop.SetValue(instance, datarow[prop.Name, DataRowVersion.Original], null);
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            var convertedValue = Convert.ChangeType(datarow[prop.Name, DataRowVersion.Original], prop.PropertyType);
        //                            prop.SetValue(instance, convertedValue, null);
        //                        }
        //                    }
        //                }
        //            }
        //            excelList.Add(instance);
        //        }

        //        return excelList;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Contains("InValidZipCode"))
        //        {
        //            throw new InvalidDataException(ex.Message);
        //        }
        //        else
        //        {
        //            throw new InvalidDataException("We were unable to process the file. Please contact customer support.");
        //        }
        //    }
        //}

    }
}
