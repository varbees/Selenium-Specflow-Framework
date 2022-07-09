using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Specflow.Automation.Utilities
{
    public class ExcelHelper
    {
        private static Dictionary<string, List<Datacollection>> _dataColList = new Dictionary<string, List<Datacollection>>();
        public static List<Datacollection> PopulateInCollection(DataTable table)
        {
            List<Datacollection> dataCol = new List<Datacollection>();

            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    Datacollection dtTable = new Datacollection()
                    {
                        rowNumber = row,
                        colName = table.Columns[col].ColumnName,
                        colValue = table.Rows[row - 1][col].ToString()
                    };
                    dataCol.Add(dtTable);
                }
            }
            return dataCol;
        }

        //get excel file and convert to dataTable
        public static void excelToDataTable(string fileName)
        {
            try
            {
                using (var Stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    //using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(Stream))
                    //{  
                        var reader = ExcelReaderFactory.CreateReader(Stream);
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });

                        DataTableCollection table = result.Tables;

                        for (int i = 0; i < table.Count; i++)
                        {
                            //store in DataTable
                            DataTable resultTable = table[i];
                            string tableName = table[i].TableName;
                            List<Datacollection> datacol = PopulateInCollection(resultTable);
                            _dataColList.Add(tableName, datacol);
                        }
                    //}
                }
            }
            catch (Exception e)
            {
                //handle datatable conversion fail

            }
        }

        //get By rowData
        public static Dictionary<string, string> getRowData(string sheetName, string colName, string title)
        {
            Dictionary<string, string> rowData = new Dictionary<string, string>();

            try
            {
                int row = 0;
                List<Datacollection> dataCol = _dataColList[sheetName];
                foreach (var data in dataCol)
                {
                    if (data.colName.Equals(colName) && data.colValue.Equals(title))
                        row = data.rowNumber;
                    if (row != 0 && data.rowNumber.Equals(row))
                        rowData.Add(data.colName, data.colValue);
                }
                return rowData;
            }
            catch (Exception e)
            {
                //handle row data not found
                return null;
            }
        }

        public class Datacollection
        {
            public int rowNumber { get; set; }
            public string colName { get; set; }
            public string colValue { get; set; }
        }
    }
}

