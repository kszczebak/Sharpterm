using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.POIFS.FileSystem;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using NPOI.XSSF.Util;
using NPOI.XSSF.UserModel;
using NPOI.HPSF;
using System.IO;

namespace ArTerm
{
    public class ExportToExcel
    {
        private string filePath;
        private static IWorkbook workBook;
        public ExportToExcel(string format)
        {
            if (format.Equals("xls"))
            {
                workBook = new HSSFWorkbook(); // create workbook
            }
            else
            {
                workBook = new XSSFWorkbook();
            }
        }

        public void SetCellsValue(List<string> date, List<double> weight)
        {
            if (workBook != null)
            {
                var sheet = workBook.CreateSheet("Nowy"); // create sheet
                for (int i = 0; i < date.Count; i++)
                {
                    var row = sheet.CreateRow(i); // create a row
                    for (int j = 0; j < 2; j++)
                    {
                        switch (j)
                        {
                            case 0: row.CreateCell(j).SetCellValue(date.ElementAt(i)); // create cell in current row
                                break;
                            case 1: row.CreateCell(j).SetCellValue(weight.ElementAt(i)); // create second cell in current row
                                break;
                        }
                    }
                }
            }
        }

        public void SetFilePath(string filePath)
        {
            this.filePath = filePath;
        }

        public void SaveFile(string filePath)
        {
            var fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            workBook.Write(fs);
            fs.Close();
        }
    }
}