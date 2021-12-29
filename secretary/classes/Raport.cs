using Microsoft.Win32;
using secretary.dbHelper;
using System;
 
using System.Data;
using System.IO;

using System.Windows;
 
 
 

public static class Raport
    {
        public static void GenerateRaport(DataTable raportData)
    {
        string raportDataString = "";
        foreach (DataRow row in raportData.Rows)
            {
                for (int i = 0; i < raportData.Columns.Count; i++)
                {
                    raportDataString += "|" + (row[i].ToString());
                    raportDataString += (i == raportData.Columns.Count - 1 ? "\n" : "|");
                }
                raportDataString += "| \n";
            }

            SaveFileDialog saveDialog = new SaveFileDialog();

            if (saveDialog.ShowDialog() == true)
                File.WriteAllText(saveDialog.FileName + ".txt", raportDataString);

            MessageBox.Show("Your raport has already been saved" + raportDataString);
        }
    }

