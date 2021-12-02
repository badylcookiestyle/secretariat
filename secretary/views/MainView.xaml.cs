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
using System.Data;
using System.Data.SQLite;
//using dbhelper;

namespace secretary.views
{
    /// <summary>
    /// Logika interakcji dla klasy MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        string currentForm = "Student";
        string currentTable = "students";
        bool isBeingEdited = false;

        public MainView()
        {
            InitializeComponent();

            studentRadio.IsChecked=true;

 

            initializeGroupCombobox();
            initializeClassesCombobox();
          
        

            DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
        }
        private void initializeGroupCombobox()
        {
            var rows = DbHelper.basicSelect("groups").DefaultView;

            for (int i = 0; i<rows.Count; i++)  
            {
               // your code goes here
                 var row = rows[i];
        comboBoxCurrentGroup.Items.Add(row["name"]);
            }
}
        private void initializeClassesCombobox()
        {
            var rows = DbHelper.basicSelect("classes").DefaultView;

            for (int i = 0; i < rows.Count; i++)
            {
                // your code goes here
                var row = rows[i];
                comboBoxCurrentClass.Items.Add(row["name"]);
            }
        }
        private void initializeTableFieldsCombobox()
        {
            comboBoxSelectField.Items.Clear();
           var rows = DbHelper.basicSelect(currentTable).DefaultView;
            for (var i = 0; i < DataGrid1.Columns.Count; i++)
            {
                var name = DataGrid1.Columns[i].Header;
                comboBoxSelectField.Items.Add(name.ToString());
            }
        

        }
        private void TeacherFormBtn_Click(object sender, RoutedEventArgs e)
          {
              TeacherFormBtn.Foreground = new SolidColorBrush(Colors.Purple);
              StudentFormBtn.Foreground = new SolidColorBrush(Colors.White);
              EmployeeFormBtn.Foreground = new SolidColorBrush(Colors.White);

              formTeacher.Visibility = Visibility.Visible;
              formStudent.Visibility = Visibility.Hidden;
              formEmployee.Visibility = Visibility.Hidden;

              currentForm = "Student";
          }

          private void StudentFormBtn_Click(object sender, RoutedEventArgs e)
          {
              TeacherFormBtn.Foreground = new SolidColorBrush(Colors.White);
              StudentFormBtn.Foreground = new SolidColorBrush(Colors.Purple);
              EmployeeFormBtn.Foreground = new SolidColorBrush(Colors.White);

              formTeacher.Visibility = Visibility.Hidden;
              formStudent.Visibility = Visibility.Visible;
              formEmployee.Visibility = Visibility.Hidden;

              currentForm = "Teacher";
          }

          private void EmployeeFormBtn_Click(object sender, RoutedEventArgs e)
          {
              TeacherFormBtn.Foreground = new SolidColorBrush(Colors.White);
              StudentFormBtn.Foreground = new SolidColorBrush(Colors.White);
              EmployeeFormBtn.Foreground = new SolidColorBrush(Colors.Purple);

              formTeacher.Visibility = Visibility.Hidden;
              formStudent.Visibility = Visibility.Hidden;
              formEmployee.Visibility = Visibility.Visible;

              currentForm = "Employee";
          }
        
         private void addTeacher()
         {
             char gender = comboBoxGender.SelectedItem.ToString() == "Male" ? 'M' : 'F';

             Teacher newTeacher = new Teacher();

             newTeacher.firstName = textBoxFname.Text;
             newTeacher.secondName = textBoxFname.Text;
             newTeacher.lastname = textBoxLname.Text;
             newTeacher.maidenName = textBoxMaiName.Text;
             newTeacher.fathersName = textBoxFthName.Text;
             newTeacher.mothersName = textBoxMthName.Text;
             newTeacher.birthDate = datePickerBirthDate.SelectedDate.Value.Date;
             newTeacher.pesel = new []{ textBoxPesel.Text};
             newTeacher.imagePath = "pathhh";
             newTeacher.gender = gender;
             newTeacher.dateOfEmployment = TdatePickerEmployment.SelectedDate.Value.Date;

             DbHelper.insertTeacher(newTeacher);
             DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;

         }
         private void addStudent()
         {
             char gender = comboBoxGender.SelectedItem.ToString() == "Male" ? 'M' : 'F';

             Student newStudent = new Student();

             newStudent.firstName = textBoxFname.Text;
             newStudent.secondName = textBoxFname.Text;
             newStudent.lastname = textBoxLname.Text;
             newStudent.maidenName = textBoxMaiName.Text;
             newStudent.fathersName = textBoxFthName.Text;
             newStudent.mothersName = textBoxMthName.Text;
             newStudent.birthDate = datePickerBirthDate.SelectedDate.Value.Date;
             newStudent.pesel = new string[] { textBoxPesel.Text };
             newStudent.imagePath = "pathhh";
             newStudent.gender = gender;
             newStudent.currentClass = comboBoxCurrentGroup.Text;
            formStudent.Visibility = Visibility.Hidden;
             //  persons.Add(newStudent);
             DbHelper.insertStudent(newStudent);
             DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
         }
         private void addEmployee()
         {
             char gender = comboBoxGender.SelectedItem.ToString() == "Male" ? 'M' : 'F';

             Employee newEmployee = new Employee();

             newEmployee.firstName = textBoxFname.Text;
             newEmployee.secondName = textBoxFname.Text;
             newEmployee.lastname = textBoxLname.Text;
             newEmployee.maidenName = textBoxMaiName.Text;
             newEmployee.fathersName = textBoxFthName.Text;
             newEmployee.mothersName = textBoxMthName.Text;
             newEmployee.birthDate = datePickerBirthDate.SelectedDate.Value.Date;
             newEmployee.pesel = new[] { textBoxPesel.Text };
             newEmployee.imagePath = "pathhh";
             newEmployee.gender = gender;
             newEmployee.jobPosition = textBoxJobPosition.Text;
             newEmployee.jobDescription = textBoxJobPosition.Text;
             newEmployee.dateOfEmployment = EdatePickerEmployment.SelectedDate.Value.Date;

             DbHelper.insertEmployee(newEmployee);
             DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
         }
         private void submitPerson_Click(object sender, RoutedEventArgs e)
         {
             if (isBeingEdited == false)
             {
                 switch (currentForm)
                 {
                     case "Student":
                         addStudent();
                         break;
                     case "Teacher":
                         addTeacher();
                         break;
                     case "Employee":
                         addEmployee();
                         break;
                 }
             }

         }

        private void TeacherRadioBtn_Click(object sender, RoutedEventArgs e)
        {
            currentTable = "teachers";
            DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
            initializeTableFieldsCombobox();
            DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
        }

        private void StudentRadioBtn_Click(object sender, RoutedEventArgs e)
        {
            currentTable = "students";
            DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
            initializeTableFieldsCombobox();
            DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
        }

        private void EmployeeRadioBtn_Click(object sender, RoutedEventArgs e)
        {
            currentTable = "employees";
             DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
            initializeTableFieldsCombobox();
            DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
           
            if (textBoxSearcher.Text != "")
            {
                try
                {
                    if (comboBoxSelectField.SelectedItem.ToString() != "id")
                    {
                        DataGrid1.ItemsSource = DbHelper.likeSelect(currentTable, textBoxSearcher.Text, comboBoxSelectField.SelectedItem.ToString()).DefaultView;
                    }
                    else
                    {
                        DataGrid1.ItemsSource = DbHelper.idSelect(currentTable, textBoxSearcher.Text).DefaultView;
                    }
                    }
                catch (Exception er)
                {

                }
                }
        }
    }
}
