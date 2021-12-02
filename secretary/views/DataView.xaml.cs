using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using secretary.dbHelper;
namespace secretary.views
{
    /// <summary>
    /// Logika interakcji dla klasy DataView.xaml
    /// </summary>
    public partial class DataView : UserControl
    {
        public DataView()
        {
            InitializeComponent();
        }

        private void saveDataBtn_Click(object sender, RoutedEventArgs e)
        {
            string dbDump = DbHelper.dumpDbToJson();
            SaveFileDialog saveDialog = new SaveFileDialog();
            if (saveDialog.ShowDialog() == true)
                File.WriteAllText(saveDialog.FileName,dbDump);
        }
    }
}
