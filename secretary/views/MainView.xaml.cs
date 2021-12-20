using Microsoft.Win32;
using secretary.dbHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        DateTime olderThan;
        DateTime youngerThan;

        public MainView()
        {
            InitializeComponent();

            DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
            studentRadio.IsChecked = true;

            initializeGroupCombobox();
            initializeClassesCombobox();
            initializeTableFieldsCombobox();
            reloadData();

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

            var newPath = Environment.CurrentDirectory + "/images/" + textBoxPesel.Text + ".png";
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
            List<Uri> paths = new List<Uri>();
            DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;

            initializeTableFieldsCombobox();

            var gridData = raportData = DbHelper.basicSelect(currentTable);

            foreach (DataRowView row in gridData.DefaultView)
            {

                try
                {
                    Uri resourceUri = new Uri(row["image_path"].ToString());
                    paths.Add(resourceUri);

                }
                catch (System.NullReferenceException er)
                {

                }

            }
            if (gridData.Columns.Contains("image_path"))
                gridData.Columns.Remove("image_path");

            // DataGrid1.Columns.Add(image);
            DataGrid1.ItemsSource = gridData.DefaultView;
            foreach (DataRowView row in DataGrid1.ItemsSource)
            {

                try
                {
                    Uri resourceUri = new Uri(@ "data:image/png;base64,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");
                    string kek = "kek";
                    // row["image"] = kek;
                    // row["first_name"] = ;

                }
                catch (System.NullReferenceException er)
                {

                }

            }

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
                        if (datePickerYoungerThan.SelectedDate == null && datePickerOlderThan.SelectedDate == null)
                        {

                            raportData = DbHelper.likeSelect(currentTable, textBoxSearcher.Text, comboBoxSelectField.SelectedItem.ToString());
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }
                        if (datePickerYoungerThan.SelectedDate != null && datePickerOlderThan.SelectedDate == null)
                        {
                            youngerThan = datePickerYoungerThan.SelectedDate.Value.Date;
                            raportData = DbHelper.advancedSelectYoungerThan(currentTable, youngerThan.ToString(), textBoxSearcher.Text, comboBoxSelectField.SelectedItem.ToString());
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }
                        if (datePickerYoungerThan.SelectedDate == null && datePickerOlderThan.SelectedDate != null)
                        {
                            olderThan = datePickerOlderThan.SelectedDate.Value.Date;
                            raportData = DbHelper.advancedSelectOlderThan(currentTable, olderThan.ToString(), textBoxSearcher.Text, comboBoxSelectField.SelectedItem.ToString());
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }
                        if (datePickerYoungerThan.SelectedDate == null && datePickerOlderThan.SelectedDate != null)
                        {
                            olderThan = datePickerOlderThan.SelectedDate.Value.Date;
                            youngerThan = datePickerYoungerThan.SelectedDate.Value.Date;
                            raportData = DbHelper.advancedSelectOlderAndYoungerThan(currentTable, olderThan.ToString(), youngerThan.ToString(), textBoxSearcher.Text, comboBoxSelectField.SelectedItem.ToString());
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }

                    }
                    else
                    {
                        if (datePickerYoungerThan.SelectedDate == null && datePickerOlderThan.SelectedDate == null)
                        {
                            raportData = DbHelper.idSelect(currentTable, textBoxSearcher.Text);
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }
                        if (datePickerYoungerThan.SelectedDate != null && datePickerOlderThan.SelectedDate == null)
                        {
                            youngerThan = datePickerYoungerThan.SelectedDate.Value.Date;
                            raportData = DbHelper.advancedSelectYoungerThanId(currentTable, youngerThan.ToString(), textBoxSearcher.Text);
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }
                        if (datePickerYoungerThan.SelectedDate == null && datePickerOlderThan.SelectedDate != null)
                        {
                            olderThan = datePickerOlderThan.SelectedDate.Value.Date;
                            raportData = DbHelper.advancedSelectOlderThanId(currentTable, olderThan.ToString(), textBoxSearcher.Text);
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }
                        if (datePickerYoungerThan.SelectedDate == null && datePickerOlderThan.SelectedDate != null)
                        {
                            olderThan = datePickerOlderThan.SelectedDate.Value.Date;
                            youngerThan = datePickerYoungerThan.SelectedDate.Value.Date;
                            raportData = DbHelper.advancedSelectOlderAndYoungerThanId(currentTable, olderThan.ToString(), youngerThan.ToString(), textBoxSearcher.Text);
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }
                    }
                }
                catch (Exception er)
                {

                }
            }
            else
            {
                if (datePickerYoungerThan.SelectedDate != null && datePickerOlderThan.SelectedDate == null)
                {

                    youngerThan = datePickerYoungerThan.SelectedDate.Value.Date;
                    raportData = DbHelper.youngerThanSelect(currentTable, youngerThan.ToString());
                    DataGrid1.ItemsSource = raportData.DefaultView;
                    generateRaportBtn.Content = youngerThan.ToString();
                }
                if (datePickerYoungerThan.SelectedDate == null && datePickerOlderThan.SelectedDate != null)
                {
                    olderThan = datePickerOlderThan.SelectedDate.Value.Date;
                    raportData = DbHelper.olderThanSelect(currentTable, olderThan.ToString());
                    DataGrid1.ItemsSource = raportData.DefaultView;
                    generateRaportBtn.Content = olderThan.ToString();
                }
                if (datePickerYoungerThan.SelectedDate != null && datePickerOlderThan.SelectedDate != null)
                {
                    olderThan = datePickerOlderThan.SelectedDate.Value.Date;
                    youngerThan = datePickerYoungerThan.SelectedDate.Value.Date;
                    raportData = DbHelper.olderAndYoungerThanSelect(currentTable, olderThan.ToString(), youngerThan.ToString());
                    DataGrid1.ItemsSource = raportData.DefaultView;
                    generateRaportBtn.Content = youngerThan.ToString();
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

        private void datePickerYoungerThan_CalendarClosed(object sender, RoutedEventArgs e)
        {

        }
    }
}