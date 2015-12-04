using System;
using System.Collections.Generic;
using System.IO;

namespace ArTerm
{
    public class Database
    {
        private List<string> dataBaseCollection;
        private List<string> datesCollection;
        private List<double> weightsCollection;
        private ExportToExcel saveToExcel;
        public Database()
        {
            dataBaseCollection = new List<string>(); // container of data
            datesCollection = new List<string>(); // container for dates
            weightsCollection = new List<double>(); // container for weights
        }
        public void AddToDatabase(string value) // add to DB
        {
            if (!value.Equals(""))
            {
                dataBaseCollection.Add(value);
            }
        }
        public void CleanDataBaseCollection() // clear DB
        {
            dataBaseCollection.Clear();
        }

        public void RefactorDataBase() // convert recived data into to arrays of dates and weights - prepearing export to file
        {
            if (dataBaseCollection != null)
            {
                string date;
                string weight;
                foreach (string item in dataBaseCollection)
                {
                    date = item.Substring(0, 11); // get date form data
                    if (System.Text.RegularExpressions.Regex.IsMatch(item.Substring(12, 2), "^\\d{2}$")) // if weight has 2 decimals before dot
                    {
                        if (System.Text.RegularExpressions.Regex.IsMatch(item.Substring(15, 3), "^\\d{3}$")) // if weight has 3 decimals after dot - do staff
                        {
                            datesCollection.Add(date); // add date
                            weight = item.Substring(12, 2) + "," + item.Substring(15, 3); // convert weight to proper format
                            weightsCollection.Add(Convert.ToDouble(weight)); // add weight to collection
                        }
                    }
                }
            }
        }

        public void SaveToExcel(string filePath) // save to excel file 
        {
            RefactorDataBase(); // firstly refactor recived data
            string fileExtension = Path.GetExtension(filePath); // get extension of the file to determine which Excel file to create
            saveToExcel = new ExportToExcel(fileExtension); // create export class instance 
            if (datesCollection != null && weightsCollection != null) // check if dates and weights are in collections
            {
                saveToExcel.SetCellsValue(datesCollection, weightsCollection);//    set data to cells
                saveToExcel.SaveFile(filePath);     // save file
            }
        }
    }
}
