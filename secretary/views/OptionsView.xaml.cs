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
using secretary.dbHelper;
using System.IO;
using secretary;
using Secretary;
using System.Data.SQLite;
namespace secretary.views
{
    /// <summary>
    /// Logika interakcji dla klasy OptionsView.xaml
    /// </summary>
    public partial class OptionsView : UserControl
    {
        string path = Environment.CurrentDirectory + "/assets/binds.txt";
        public OptionsView()
        {
            InitializeComponent();
            int counter = 0;
           
            string text = File.ReadAllText(path);
            string[] lines = text.Split(Environment.NewLine);

            foreach (string line in lines)
            {

                if (counter == 0)
                {
                    textBoxMainView.Text = line;
                }
                if (counter == 1)
                {
                    textBoxDataView.Text = line;

                }
                if (counter == 2)
                {
                    textBoxOptionView.Text = line;
                }
                counter++;
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
          
            File.Delete(path);
            using (StreamWriter writer = File.CreateText(path))
            {
                writer.WriteLine(textBoxMainView.Text);
                writer.WriteLine(textBoxDataView.Text);
                writer.WriteLine(textBoxOptionView.Text);
            }
        }
         


    }
}
