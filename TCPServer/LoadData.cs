using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    internal class LoadData
    {
        public static List<double> displacementData = null;
        public static List<double> loadData = null;

        public static void ReadExcelFile()
        {
           
            string path = "C:\\Users\\Sayyabkhan\\Downloads\\final-batch-ld-1-1.xls";

            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                
                
                HSSFWorkbook workbook = new HSSFWorkbook(file);
                ISheet sheet = workbook.GetSheetAt(0);


                LoadData.displacementData = new List<double>();
                LoadData.loadData = new List<double>();

                
                for (int row = 5; row <= sheet.LastRowNum; row++)
                {
                    IRow dataRow = sheet.GetRow(row);

                    // Get data from the second last column 
                    ICell displacementCell = dataRow.GetCell(dataRow.LastCellNum - 2);
                    double displacementCellValue = GetCellValueAsDouble(displacementCell);

                    // Get data from the last column 
                    ICell loadCell = dataRow.GetCell(dataRow.LastCellNum - 1);
                    double loadCellValue = GetCellValueAsDouble(loadCell);

                    // Add data to the respective lists
                    displacementData.Add(displacementCellValue);
                    loadData.Add(loadCellValue);

                }


            }
        }

        // Helper method to get cell value as a double
        private static double GetCellValueAsDouble(ICell cell)
        {
            if (cell == null)
            {
                return 0.0; 
            }

            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return cell.NumericCellValue;
                default:
                    return 0.0;
            }
        }

    }
}
