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
using System.Globalization;
using System.Windows.Data;

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
        List<String> imagesPaths = new List<String>();

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
        private string serializeLessons(List<Lesson> lessons)
        {
            string text="";
            lessons.ForEach(delegate(Lesson lesson){
                text +="name: "+lesson.name + " time: " + lesson.lessonTime+"\n";
            });
            return text;
        }
        private void initializeGroupCombobox()
        {
            var rows = DbHelper.basicSelect("groups").DefaultView;
            eComboBoxCurrentGroup.Items.Clear();
            comboBoxCurrentGroup.Items.Clear();
            for (int i = 0; i < rows.Count; i++)
            {
                var row = rows[i];
                eComboBoxCurrentGroup.Items.Add(row["name"]);
                comboBoxCurrentGroup.Items.Add(row["name"]);
            }
        }
       
        private void initializeClassesCombobox()
        {
            
                comboBoxCurrentClass.Items.Clear();
                eComboBoxCurrentClass.Items.Clear();
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

            string gender = comboBoxGender.SelectedItem.ToString() == "Male" ? "Male" : "Female";
            string taughtSubjectsJson = serializeLessons(lessons);

             var newPath = Environment.CurrentDirectory + "/images/" + textBoxPesel.Text +DateTime.Now.Ticks+ ".png";

            if (textBoxPesel.Text != null)
            {
                File.Copy(selectedFilePath, newPath);
            }
            else
            {
                newPath = "https://cdn.pixabay.com/photo/2013/07/13/12/07/avatar-159236_1280.png";
            }

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
            currentTable = "teachers";
            teacherRadio.IsChecked = true;
            reloadData();

        }
        private void addStudent()
        {
            string gender = comboBoxGender.SelectedItem.ToString() == "Male" ? "Male" : "Female";
            Student newStudent = new Student();

            var newPath = Environment.CurrentDirectory + "/images/" + textBoxPesel.Text + DateTime.Now.Ticks + ".png";

            if (textBoxPesel.Text != null)
            {
                File.Copy(selectedFilePath, newPath);
            }
            else
            {
                newPath = "https://cdn.pixabay.com/photo/2013/07/13/12/07/avatar-159236_1280.png";
            }

            newStudent.firstName = textBoxFname.Text;
            newStudent.secondName = textBoxFname.Text;
            newStudent.lastname = textBoxLname.Text;
            newStudent.maidenName = textBoxMaiName.Text;
            newStudent.fathersName = textBoxFthName.Text;
            newStudent.mothersName = textBoxMthName.Text;
            newStudent.birthDate = datePickerBirthDate.SelectedDate.Value.Date;
            newStudent.pesel = textBoxPesel.Text;
            newStudent.groups = comboBoxCurrentGroup.SelectedItem.ToString();
            newStudent.imagePath = newPath;
            newStudent.gender = gender;
            newStudent.currentClass = comboBoxCurrentClass.SelectedItem.ToString();


            DbHelper.insertStudent(newStudent);
            currentTable = "students";
            studentRadio.IsChecked = true;
            reloadData();
        }
        private void addEmployee()
        {
            string gender = comboBoxGender.SelectedItem.ToString() == "Male" ? "Male" : "Female";
            var newPath = Environment.CurrentDirectory + "/images/" + textBoxPesel.Text + DateTime.Now.Ticks + ".png";

            if (textBoxPesel.Text != null)
            {
                File.Copy(selectedFilePath, newPath);
            }
            else
            {
                newPath = "https://cdn.pixabay.com/photo/2013/07/13/12/07/avatar-159236_1280.png";
            }
    
            Employee newEmployee = new Employee();

            newEmployee.firstName = textBoxFname.Text;
            newEmployee.secondName = textBoxFname.Text;
            newEmployee.lastname = textBoxLname.Text;
            newEmployee.maidenName = textBoxMaiName.Text;
            newEmployee.fathersName = textBoxFthName.Text;
            newEmployee.mothersName = textBoxMthName.Text;
            newEmployee.tenure = textBoxTenure.Text;
            newEmployee.birthDate = datePickerBirthDate.SelectedDate.Value.Date;
            newEmployee.pesel = textBoxPesel.Text;
            newEmployee.imagePath = newPath;
            newEmployee.gender = gender;
            newEmployee.jobPosition = textBoxJobPosition.Text;
            newEmployee.jobDescription = textBoxJobPosition.Text;
            newEmployee.dateOfEmployment = EdatePickerEmployment.SelectedDate.Value.Date;

            DbHelper.insertEmployee(newEmployee);
            currentTable = "employees";
            employeeRadio.IsChecked = true;
            reloadData();
        }
        void clearForm(DependencyObject obj)

        {

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)

            {

                if (obj is TextBox)

                    ((TextBox)obj).Text = null;

                clearForm(VisualTreeHelper.GetChild(obj, i));

            }

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
                clearForm(MainViewGrid);


            }

        }
        private void reloadImagePaths()
        {
            foreach (DataRowView row in DataGrid1.ItemsSource)
            {
                try
                {
                    imagesPaths.Add(row["image_path"].ToString());
                }
                catch (Exception er) { }
            }
        }
        private void removeDataPathColumn(DataTable gridData)  {
            reloadImagePaths();
            if (gridData.Columns.Contains("image_path"))
                gridData.Columns.Remove("image_path");
        
        }
        private void reloadData()
        {
            DataGrid1.ItemsSource = DbHelper.basicSelect(currentTable).DefaultView;
            initializeTableFieldsCombobox();

            var gridData = raportData = DbHelper.basicSelect(currentTable);
            removeDataPathColumn(gridData);
            DataGrid1.ItemsSource = gridData.DefaultView;
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
                            removeDataPathColumn(raportData);
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }
                        
                        if (datePickerYoungerThan.SelectedDate != null && datePickerOlderThan.SelectedDate == null)
                        {
                            youngerThan = datePickerYoungerThan.SelectedDate.Value.Date;
                            raportData = DbHelper.advancedSelectYoungerThan(currentTable, youngerThan.ToString(), textBoxSearcher.Text, comboBoxSelectField.SelectedItem.ToString());
                            removeDataPathColumn(raportData);
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }
                        
                        if (datePickerYoungerThan.SelectedDate == null && datePickerOlderThan.SelectedDate != null)
                        {
                            olderThan = datePickerOlderThan.SelectedDate.Value.Date;
                            raportData = DbHelper.advancedSelectOlderThan(currentTable, olderThan.ToString(), textBoxSearcher.Text, comboBoxSelectField.SelectedItem.ToString());
                            removeDataPathColumn(raportData);
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }
                        
                        if (datePickerYoungerThan.SelectedDate == null && datePickerOlderThan.SelectedDate != null)
                        {
                            olderThan = datePickerOlderThan.SelectedDate.Value.Date;
                            youngerThan = datePickerYoungerThan.SelectedDate.Value.Date;
                            raportData = DbHelper.advancedSelectOlderAndYoungerThan(currentTable, olderThan.ToString(), youngerThan.ToString(), textBoxSearcher.Text, comboBoxSelectField.SelectedItem.ToString());
                            removeDataPathColumn(raportData);
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }

                    }
                    else
                    {
                        if (datePickerYoungerThan.SelectedDate == null && datePickerOlderThan.SelectedDate == null)
                        {
                            raportData = DbHelper.idSelect(currentTable, textBoxSearcher.Text);
                            removeDataPathColumn(raportData);
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }
                        
                        if (datePickerYoungerThan.SelectedDate != null && datePickerOlderThan.SelectedDate == null)
                        {
                            youngerThan = datePickerYoungerThan.SelectedDate.Value.Date;
                            raportData = DbHelper.advancedSelectYoungerThanId(currentTable, youngerThan.ToString(), textBoxSearcher.Text);
                            removeDataPathColumn(raportData);
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }
                        
                        if (datePickerYoungerThan.SelectedDate == null && datePickerOlderThan.SelectedDate != null)
                        {
                            olderThan = datePickerOlderThan.SelectedDate.Value.Date;
                            raportData = DbHelper.advancedSelectOlderThanId(currentTable, olderThan.ToString(), textBoxSearcher.Text);
                            removeDataPathColumn(raportData);
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }
                       
                        if (datePickerYoungerThan.SelectedDate == null && datePickerOlderThan.SelectedDate != null)
                        {
                            olderThan = datePickerOlderThan.SelectedDate.Value.Date;
                            youngerThan = datePickerYoungerThan.SelectedDate.Value.Date;
                            raportData = DbHelper.advancedSelectOlderAndYoungerThanId(currentTable, olderThan.ToString(), youngerThan.ToString(), textBoxSearcher.Text);
                            removeDataPathColumn(raportData);
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
                   
                }

                if (datePickerYoungerThan.SelectedDate == null && datePickerOlderThan.SelectedDate != null)
                {
                    olderThan = datePickerOlderThan.SelectedDate.Value.Date;
                    raportData = DbHelper.olderThanSelect(currentTable, olderThan.ToString());
                    DataGrid1.ItemsSource = raportData.DefaultView;
                   
                }

                if (datePickerYoungerThan.SelectedDate != null && datePickerOlderThan.SelectedDate != null)
                {
                    olderThan = datePickerOlderThan.SelectedDate.Value.Date;
                    youngerThan = datePickerYoungerThan.SelectedDate.Value.Date;
                    raportData = DbHelper.olderAndYoungerThanSelect(currentTable, olderThan.ToString(), youngerThan.ToString());
                    DataGrid1.ItemsSource = raportData.DefaultView;
               
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
                if (datepickerLesson.SelectedDate.Value.Date != null)
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
                File.WriteAllText(saveDialog.FileName+".txt", raportDataString);

            MessageBox.Show("Your raport has already been saved" + raportDataString);
        
        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid datagrid = sender as DataGrid;
            DataRowView row = datagrid.SelectedItem as DataRowView;
            var rowIndex = DataGrid1.SelectedIndex;
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
                    eImage.Source = new BitmapImage(new Uri(imagesPaths[rowIndex].ToString()));
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
                string gender = comboBoxGender.SelectedItem.ToString() == "Male" ? "Male" : "Female";
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
               
                reloadData();
            }
            if (currentTable == "teachers")
            {
                string taughtSubjectsJson = serializeLessons(eLessons);

                string gender = comboBoxGender.SelectedItem.ToString() == "Male" ? "Male" : "Female";
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
                reloadData();
            }
            if (currentTable == "employees")
            {
                string gender = comboBoxGender.SelectedItem.ToString() == "Male" ? "Male" : "Female";
                var newPath = Environment.CurrentDirectory + "/images/" + eTextBoxPesel.Text + ".png";

                Employee curEmployee = new Employee();

                curEmployee.firstName = eTextBoxFname.Text;
                curEmployee.secondName = eTextBoxFname.Text;
                curEmployee.lastname = eTextBoxLname.Text;
                curEmployee.maidenName = eTextBoxMaiName.Text;
                curEmployee.fathersName = eTextBoxFthName.Text;
                curEmployee.mothersName = eTextBoxMthName.Text;
                curEmployee.tenure = eTextBoxTenure.Text;
                curEmployee.birthDate = eDatePickerBirthDate.SelectedDate.Value.Date;
                curEmployee.pesel = eTextBoxPesel.Text;
                curEmployee.gender = gender;
                curEmployee.jobPosition = eTextBoxJobPosition.Text;
                curEmployee.jobDescription = eTextBoxJobPosition.Text;
                curEmployee.dateOfEmployment = eEdatePickerEmployment.SelectedDate.Value.Date;
                curEmployee.imagePath = newPath;

                DbHelper.updateEmployee(curEmployee, selectedRow);
                reloadData();
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

        private void closeEditModalBtn(object sender, RoutedEventArgs e)
        {
            editFormExpander.IsExpanded = false;
        }
    }
}