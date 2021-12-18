using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using secretary.dbHelper;
using System.Data;
using Microsoft.Win32;
using System.IO;

using System.Text.Json;
namespace secretary.views
{
    /// <summary>
    /// Logika interakcji dla klasy MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        string currentForm = "Student";
        string currentTable = "students";
        string selectedFilePath;
        string raportDataString = "";

        bool isBeingEdited = false;
        bool isEditing = false;
        
        
        int selectedRow;

        DataTable raportData;
       
        List<Lesson> lessons = new List<Lesson>();
        List<Lesson> eLessons = new List<Lesson>();
   
        public MainView()
        {
         

            InitializeComponent();

            initializeGroupCombobox();
            initializeClassesCombobox();
            reloadData();

            DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
            studentRadio.IsChecked = true;
        }

        private void initializeGroupCombobox()
        {
            var rows = DbHelper.basicSelect("groups").DefaultView;

            for (int i = 0; i < rows.Count; i++)
            {
                var row = rows[i];
                eComboBoxCurrentGroup.Items.Add(row["name"]);
                comboBoxCurrentGroup.Items.Add(row["name"]);
            }
        }

        private void initializeClassesCombobox()
        {
            var rows = DbHelper.basicSelect("classes").DefaultView;

            for (int i = 0; i < rows.Count; i++)
            {
                var row = rows[i];
                eComboBoxCurrentClass.Items.Add(row["name"]);
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

            currentForm = "Teacher";
        }

        private void StudentFormBtn_Click(object sender, RoutedEventArgs e)
        {
            TeacherFormBtn.Foreground = new SolidColorBrush(Colors.White);
            StudentFormBtn.Foreground = new SolidColorBrush(Colors.Purple);
            EmployeeFormBtn.Foreground = new SolidColorBrush(Colors.White);

            formTeacher.Visibility = Visibility.Hidden;
            formStudent.Visibility = Visibility.Visible;
            formEmployee.Visibility = Visibility.Hidden;

            currentForm = "Student";
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
            string taughtSubjectsJson = JsonSerializer.Serialize(lessons);

            var newPath = Environment.CurrentDirectory + "/images/" + textBoxPesel.Text + ".png";
            File.Copy(selectedFilePath, newPath);

            Teacher newTeacher = new Teacher();

            newTeacher.firstName = textBoxFname.Text;
            newTeacher.secondName = textBoxFname.Text;
            newTeacher.lastname = textBoxLname.Text;
            newTeacher.maidenName = textBoxMaiName.Text;
            newTeacher.fathersName = textBoxFthName.Text;
            newTeacher.mothersName = textBoxMthName.Text;
            newTeacher.birthDate = datePickerBirthDate.SelectedDate.Value.Date;
            newTeacher.pesel = textBoxPesel.Text;
            newTeacher.imagePath = newPath;
            newTeacher.classTutor = textBoxTutor.Text;
            newTeacher.taughtSubjects = taughtSubjectsJson;
            newTeacher.gender = gender;
            newTeacher.dateOfEmployment = TdatePickerEmployment.SelectedDate.Value.Date;

            DbHelper.insertTeacher(newTeacher);
            DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;

        }
        private void addStudent()
        {
            char gender = comboBoxGender.SelectedItem.ToString() == "Male" ? 'M' : 'F';
            Student newStudent = new Student();

            var newPath = Environment.CurrentDirectory + "/images/" +  textBoxPesel.Text + ".png";
            File.Copy(selectedFilePath, newPath);

            newStudent.firstName = textBoxFname.Text;
            newStudent.secondName = textBoxFname.Text;
            newStudent.lastname = textBoxLname.Text;
            newStudent.maidenName = textBoxMaiName.Text;
            newStudent.fathersName = textBoxFthName.Text;
            newStudent.mothersName = textBoxMthName.Text;
            newStudent.birthDate = datePickerBirthDate.SelectedDate.Value.Date;
            newStudent.pesel = textBoxPesel.Text;
            newStudent.groups = "group";
            newStudent.imagePath = newPath;
            newStudent.gender = gender;
            newStudent.currentClass = "class";
            formStudent.Visibility = Visibility.Hidden;

            DbHelper.insertStudent(newStudent);
            DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
        }
        private void addEmployee()
        {
            char gender = comboBoxGender.SelectedItem.ToString() == "Male" ? 'M' : 'F';
            var newPath = Environment.CurrentDirectory + "/images/" + textBoxPesel.Text + ".png";
            File.Copy(selectedFilePath, newPath);

            Employee newEmployee = new Employee();

            newEmployee.firstName = textBoxFname.Text;
            newEmployee.secondName = textBoxFname.Text;
            newEmployee.lastname = textBoxLname.Text;
            newEmployee.maidenName = textBoxMaiName.Text;
            newEmployee.fathersName = textBoxFthName.Text;
            newEmployee.mothersName = textBoxMthName.Text;
            newEmployee.birthDate = datePickerBirthDate.SelectedDate.Value.Date;
            newEmployee.pesel = textBoxPesel.Text;
            newEmployee.imagePath = newPath;
            newEmployee.gender = gender;
            newEmployee.jobPosition = textBoxJobPosition.Text;
            newEmployee.jobDescription = textBoxJobPosition.Text;
            newEmployee.dateOfEmployment = EdatePickerEmployment.SelectedDate.Value.Date;

            DbHelper.insertEmployee(newEmployee);
            DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
        }
        private void SubmitPersonBtn_Click(object sender, RoutedEventArgs e)
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
        private void reloadData()
        {
            DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;

            initializeTableFieldsCombobox();

            raportData = DbHelper.basicSelect(currentTable);
            DataGrid1.ItemsSource = raportData.DefaultView;
        }
        private void TeacherRadioBtn_Click(object sender, RoutedEventArgs e)
        {
            currentTable = "teachers";
            reloadData();
        }

        private void StudentRadioBtn_Click(object sender, RoutedEventArgs e)
        {
            currentTable = "students";
            reloadData();
        }

        private void EmployeeRadioBtn_Click(object sender, RoutedEventArgs e)
        {
            currentTable = "employees";
            reloadData();
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {

            if (textBoxSearcher.Text != "")
            {
                try
                {
                    if (comboBoxSelectField.SelectedItem.ToString() != "id")
                    {
                        raportData = DbHelper.likeSelect(currentTable, textBoxSearcher.Text, comboBoxSelectField.SelectedItem.ToString());
                        DataGrid1.ItemsSource = raportData.DefaultView;
                    }
                    else
                    {
                        raportData = DbHelper.idSelect(currentTable, textBoxSearcher.Text);
                        DataGrid1.ItemsSource = raportData.DefaultView;
                    }
                }
                catch (Exception er)
                {

                }
            }
        }

        private void AddLessonBtn_Click(object sender, RoutedEventArgs e)
        {
            isEditing = editFormExpander.IsExpanded == true ? true : false;
            if (isEditing == true)
            {
                eLessonsListView.Items.Add(new Lesson
                {
                    name = eTextboxLesson.Text,
                    lessonTime = eDatepickerLesson.SelectedDate.Value.Date
                });
                eLessons.Add(new Lesson
                {
                    name = "fdsa",
                    lessonTime = DateTime.Now
                });
            }
            else
            {
                lessonsListView.Items.Add(new Lesson
                {
                    name = textboxLesson.Text,
                    lessonTime = datepickerLesson.SelectedDate.Value.Date
                });
                lessons.Add(new Lesson
                {
                    name = "fdsa",
                    lessonTime = DateTime.Now
                });
            }
        }

        private void DeleteLessonBtn_Click(object sender, RoutedEventArgs e)
        {
            isEditing = editFormExpander.IsExpanded == true ? true : false;
            if (isEditing == true)
            {
                eLessonsListView.Items.RemoveAt(lessonsListView.Items.Count - 1);
            }
            else
            {
                lessonsListView.Items.RemoveAt(lessonsListView.Items.Count - 1);
            }
        }

        private void LoadImgBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {

                selectedFilePath = op.FileName;
                cFileName.Content = selectedFilePath;
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
            }

        }
        
 
       
        private void DeleteEverythingBtn_Click(object sender, RoutedEventArgs e)
        {
            DbHelper.basicDelete("teachers");
            DbHelper.basicDelete("students");
            DbHelper.basicDelete("employees");

            reloadData();
        }

        private void GenerateRaportBtn_Click(object sender, RoutedEventArgs e)
        {

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
                File.WriteAllText(saveDialog.FileName, raportDataString);
            MessageBox.Show("Your raport has already been saved" + raportDataString);
        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid datagrid = sender as DataGrid;
            DataRowView row = datagrid.SelectedItem as DataRowView;

            if (row != null)
            {
                editFormExpander.IsExpanded = true;
                selectedRow = Int32.Parse(row[0].ToString());
                if (currentTable == "students")
                {
                    eStudentForm.Visibility = Visibility.Visible;
                    eTeacherForm.Visibility = Visibility.Hidden;
                    eEmployeeForm.Visibility = Visibility.Hidden;

                    eTextBoxFname.Text = row[1].ToString();
                    eTextBoxSname.Text = row[2].ToString();
                    eTextBoxLname.Text = row[3].ToString();
                    eTextBoxMaiName.Text = row[4].ToString();
                    eTextBoxFthName.Text = row[5].ToString();
                    eTextBoxMthName.Text = row[6].ToString();
                    eDatePickerBirthDate.SelectedDate = DateTime.Parse(row[7].ToString());
                    eTextBoxPesel.Text = row[8].ToString();

                    eComboBoxCurrentGroup.SelectedItem = row[11].ToString();

                }
                if (currentTable == "teachers")
                {
                    eStudentForm.Visibility = Visibility.Hidden;
                    eTeacherForm.Visibility = Visibility.Visible;
                    eEmployeeForm.Visibility = Visibility.Hidden;

                    eTextBoxFname.Text = row[1].ToString();
                    eTextBoxSname.Text = row[2].ToString();
                    eTextBoxLname.Text = row[3].ToString();
                    eTextBoxMaiName.Text = row[4].ToString();
                    eTextBoxFthName.Text = row[5].ToString();
                    eTextBoxMthName.Text = row[6].ToString();
                    eDatePickerBirthDate.SelectedDate = DateTime.Parse(row[7].ToString());
                    eTextBoxPesel.Text = row[8].ToString();

                }
                if (currentTable == "employees")
                {
                    eStudentForm.Visibility = Visibility.Hidden;
                    eTeacherForm.Visibility = Visibility.Hidden;
                    eEmployeeForm.Visibility = Visibility.Visible;

                    eTextBoxFname.Text = row[1].ToString();
                    eTextBoxSname.Text = row[2].ToString();
                    eTextBoxLname.Text = row[3].ToString();
                    eTextBoxMaiName.Text = row[4].ToString();
                    eTextBoxFthName.Text = row[5].ToString();
                    eTextBoxMthName.Text = row[6].ToString();
                    eDatePickerBirthDate.SelectedDate = DateTime.Parse(row[7].ToString());
                    eTextBoxPesel.Text = row[8].ToString();
                }

            }
        }

        private void DataGrid1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void EditRowBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentTable == "students")
            {
                char gender = eComboBoxGender.SelectedItem.ToString() == "Male" ? 'M' : 'F';
                var newPath = Environment.CurrentDirectory + "/images/" + eTextBoxPesel.Text + ".png";

                Student curStudent = new Student();

                curStudent.firstName = eTextBoxFname.Text;
                curStudent.secondName = eTextBoxFname.Text;
                curStudent.lastname = eTextBoxLname.Text;
                curStudent.maidenName = eTextBoxMaiName.Text;
                curStudent.fathersName = eTextBoxFthName.Text;
                curStudent.mothersName = eTextBoxMthName.Text;
                curStudent.birthDate = eDatePickerBirthDate.SelectedDate.Value.Date;
                curStudent.pesel = eTextBoxPesel.Text;
                curStudent.groups = eComboBoxCurrentGroup.SelectedItem.ToString();
                curStudent.imagePath = newPath;
                curStudent.gender = gender;
                curStudent.currentClass = eComboBoxCurrentClass.SelectedItem.ToString();

                DbHelper.updateStudent(curStudent, selectedRow);
                DbHelper.basicSelect(currentTable);
                DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
            }
            if (currentTable == "teachers")
            {
                string taughtSubjectsJson = JsonSerializer.Serialize(eLessons);

                char gender = eComboBoxGender.SelectedItem.ToString() == "Male" ? 'M' : 'F';
                var newPath = Environment.CurrentDirectory + "/images/" + eTextBoxPesel.Text + ".png";

                Teacher curTeacher = new Teacher();
                curTeacher.firstName = eTextBoxFname.Text;
                curTeacher.secondName = eTextBoxFname.Text;
                curTeacher.lastname = eTextBoxLname.Text;
                curTeacher.maidenName = eTextBoxMaiName.Text;
                curTeacher.fathersName = eTextBoxFthName.Text;
                curTeacher.mothersName = eTextBoxMthName.Text;
                curTeacher.birthDate = eDatePickerBirthDate.SelectedDate.Value.Date;
                curTeacher.pesel = eTextBoxPesel.Text;
                curTeacher.gender = gender;
                curTeacher.imagePath = newPath;
                curTeacher.classTutor = eTextBoxTutor.Text;
                curTeacher.taughtSubjects = taughtSubjectsJson;
                curTeacher.dateOfEmployment = eTdatePickerEmployment.SelectedDate.Value.Date;

                DbHelper.updateTeacher(curTeacher, selectedRow);
                DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
            }
            if (currentTable == "employees")
            {
                char gender = eComboBoxGender.SelectedItem.ToString() == "Male" ? 'M' : 'F';
                var newPath = Environment.CurrentDirectory + "/images/" + eTextBoxPesel.Text + ".png";

                Employee curEmployee = new Employee();
                curEmployee.firstName = eTextBoxFname.Text;
                curEmployee.secondName = eTextBoxFname.Text;
                curEmployee.lastname = eTextBoxLname.Text;
                curEmployee.maidenName = eTextBoxMaiName.Text;
                curEmployee.fathersName = eTextBoxFthName.Text;
                curEmployee.mothersName = eTextBoxMthName.Text;
                curEmployee.birthDate = eDatePickerBirthDate.SelectedDate.Value.Date;
                curEmployee.pesel = eTextBoxPesel.Text;
                curEmployee.gender = gender;
                curEmployee.jobPosition = eTextBoxJobPosition.Text;
                curEmployee.jobDescription = eTextBoxJobPosition.Text;
                curEmployee.dateOfEmployment = eEdatePickerEmployment.SelectedDate.Value.Date;
                curEmployee.imagePath = newPath;

                DbHelper.updateEmployee(curEmployee, selectedRow);
                DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
            }
            editFormExpander.IsExpanded = false;
        }

        private void EditFormExpander_Expanded(object sender, RoutedEventArgs e)
        {
            formExpander.IsExpanded = false;
        }

        private void FormExpander_Expanded(object sender, RoutedEventArgs e)
        {
            editFormExpander.IsExpanded = false;
        }

        private void DeleteChoosenRowBtn_Click(object sender, RoutedEventArgs e)
        {
            DbHelper.deleteById(currentTable, selectedRow);
            DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
            editFormExpander.IsExpanded = false;
        }
    }
}