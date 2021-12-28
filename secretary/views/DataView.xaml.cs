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
using System.Text;
using System.Data.SQLite;
using System.Data;


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
        private string generateTableString(DataTable curTable)
        {
            string curTableString = "";
            foreach (DataRow row in curTable.Rows)
            {
                
                for (int i = 0; i < curTable.Columns.Count; i++)
                {
                    curTableString +=row[i].ToString();
                    curTableString += i == curTable.Columns.Count - 1 ? "" : ",";
                }
                curTableString += "\n";
            }
            return curTableString;
        }
        private void saveDataBtn_Click(object sender, RoutedEventArgs e)
        {
            string dbDump = "";
            dbDump += generateTableString(DbHelper.basicSelect("students"));
            dbDump += generateTableString(DbHelper.basicSelect("teachers"));
            dbDump += generateTableString(DbHelper.basicSelect("employees"));

            SaveFileDialog saveDialog = new SaveFileDialog();
            if (saveDialog.ShowDialog() == true)
                File.WriteAllText(saveDialog.FileName,dbDump);
        }

        private void uploadDataBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) { 
                string[] lines = File.ReadAllLines(openFileDialog.FileName);
            foreach (string line in lines)
                {
                    string insertType = "";
                    string[] columnArray = new string[1000];
                    int counter = 0;
                    int i = 0;
                    foreach (char c in line)
                    {
                        if (c == ',')
                            counter++;
                    }
                    if (counter == 12)
                        insertType = "student";
                    if (counter == 13)
                        insertType = "teacher";
                    if (counter == 14)
                        insertType = "employee";
                    
                    lll.Content = counter.ToString();
                    string[] columns = line.Split(',');
                foreach (string column in columns)
                {
                        columnArray[i] = column;
                        i++;
                }
                    i = 0;
                    if (insertType == "student")
                    {
                        Student newStudent = new Student();
                      
                        newStudent.firstName = columnArray[1];
                        newStudent.secondName = columnArray[2];
                        newStudent.lastname = columnArray[3];
                        newStudent.maidenName = columnArray[4];
                        newStudent.fathersName = columnArray[5];
                        newStudent.mothersName = columnArray[6];
                        newStudent.birthDate = Convert.ToDateTime(columnArray[7]) ;
                        newStudent.pesel = columnArray[8]; ;
                        newStudent.imagePath = columnArray[9];
                        newStudent.gender = columnArray[10];
                        newStudent.currentClass = columnArray[11];
                        newStudent.groups = columnArray[12]; ;
                        
                        DbHelper.insertStudent(newStudent);
                    }
                    if(insertType == "teacher") 
                    {
                        Teacher newTeacher = new Teacher();

                        newTeacher.firstName = columnArray[1];
                        newTeacher.secondName = columnArray[2];
                        newTeacher.lastname = columnArray[3];
                        newTeacher.maidenName = columnArray[4];
                        newTeacher.fathersName = columnArray[5];
                        newTeacher.mothersName = columnArray[6];
                        newTeacher.birthDate = Convert.ToDateTime(columnArray[7]);
                        newTeacher.pesel = columnArray[8]; ;
                        newTeacher.imagePath = columnArray[9];
                        newTeacher.gender = columnArray[10];
                        newTeacher.dateOfEmployment = Convert.ToDateTime(columnArray[11]);
                        newTeacher.classTutor = columnArray[12];
                        newTeacher.taughtSubjects = columnArray[13];

                        DbHelper.insertTeacher(newTeacher);
                    }
                    if (insertType == "employee")
                    {
                        Employee newEmployee = new Employee();

                        newEmployee.firstName = columnArray[1];
                        newEmployee.secondName = columnArray[2];
                        newEmployee.lastname = columnArray[3];
                        newEmployee.maidenName = columnArray[4];
                        newEmployee.fathersName = columnArray[5];
                        newEmployee.mothersName = columnArray[6];
                        newEmployee.birthDate = Convert.ToDateTime(columnArray[7]);
                        newEmployee.pesel = columnArray[8]; ;
                        newEmployee.imagePath = columnArray[9];
                        newEmployee.gender = columnArray[10];
                        newEmployee.dateOfEmployment = Convert.ToDateTime(columnArray[11]);
                        newEmployee.jobPosition = columnArray[12];
                        newEmployee.jobDescription = columnArray[13];
                        newEmployee.tenure = columnArray[14];

                        DbHelper.insertEmployee(newEmployee);
                    }
                }
            }
        }
    }
}
