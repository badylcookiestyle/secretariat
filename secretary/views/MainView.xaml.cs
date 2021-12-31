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

        List<String> imagesPaths = new List<String>();
        List<String> groups = new List<String>();
        List<String> eGroups = new List<String>();

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
          

            studentRadio.IsChecked = true;

     
         
            initializeGroupCombobox();

            reloadData();
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

      

        private void initializeTableFieldsCombobox()
        {
            comboBoxSelectField.Items.Clear();
            for (var i = 0; i < DataGrid1.Columns.Count; i++)
            {
                var name = DataGrid1.Columns[i].Header;
                comboBoxSelectField.Items.Add(name.ToString());
            }
        }

        private void TeacherFormBtn_Click(object sender, RoutedEventArgs e)
        {
            TeacherFormBtn.Foreground = new SolidColorBrush(Colors.Pink);
            StudentFormBtn.Foreground = new SolidColorBrush(Colors.White);
            EmployeeFormBtn.Foreground = new SolidColorBrush(Colors.White);

            hideForms();
            showTeacherForm();
            currentForm = "Teacher";
        }

        private void StudentFormBtn_Click(object sender, RoutedEventArgs e)
        {
            TeacherFormBtn.Foreground = new SolidColorBrush(Colors.White);
            StudentFormBtn.Foreground = new SolidColorBrush(Colors.Pink);
            EmployeeFormBtn.Foreground = new SolidColorBrush(Colors.White);

            hideForms();
            showStudentForm();
            currentForm = "Student";
        }

        private void EmployeeFormBtn_Click(object sender, RoutedEventArgs e)
        {
            TeacherFormBtn.Foreground = new SolidColorBrush(Colors.White);
            StudentFormBtn.Foreground = new SolidColorBrush(Colors.White);
            EmployeeFormBtn.Foreground = new SolidColorBrush(Colors.Pink);

            hideForms();
            showEmployeeForm();
            currentForm = "Employee";
        }

        private void addTeacher()
        {
            
            string taughtSubjectsJson = Serializers.serializeLessons(lessons);

            var newPath = Environment.CurrentDirectory + "/images/" + textBoxPesel.Text + DateTime.Now.Ticks + Path.GetExtension(selectedFilePath);

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
            newTeacher.gender = comboBoxGender.SelectionBoxItem.ToString();
            newTeacher.dateOfEmployment = TdatePickerEmployment.SelectedDate.Value.Date;

            DbHelper.insertTeacher(newTeacher);
            currentTable = "teachers";
            teacherRadio.IsChecked = true;
            reloadData();

        }
        private void addStudent()
        {
           
            Student newStudent = new Student();

            var newPath = Environment.CurrentDirectory + "/images/" + textBoxPesel.Text + DateTime.Now.Ticks + Path.GetExtension(selectedFilePath);

            File.Copy(selectedFilePath, newPath);

            newStudent.firstName = textBoxFname.Text;
            newStudent.secondName = textBoxFname.Text;
            newStudent.lastname = textBoxLname.Text;
            newStudent.maidenName = textBoxMaiName.Text;
            newStudent.fathersName = textBoxFthName.Text;
            newStudent.mothersName = textBoxMthName.Text;
            newStudent.birthDate = datePickerBirthDate.SelectedDate.Value.Date;
            newStudent.pesel = textBoxPesel.Text;
            newStudent.groups = Serializers.serializeGroups(groups);
            newStudent.imagePath = newPath;
            newStudent.gender = comboBoxGender.SelectionBoxItem.ToString();
            newStudent.currentClass = comboBoxCurrentClass.Text;

            DbHelper.insertStudent(newStudent);
            currentTable = "students";
            studentRadio.IsChecked = true;
            reloadData();
        }
        private void addEmployee()
        {
        
            var newPath = Environment.CurrentDirectory + "/images/" + textBoxPesel.Text + DateTime.Now.Ticks + Path.GetExtension(selectedFilePath);

            File.Copy(selectedFilePath, newPath);

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
            newEmployee.gender = comboBoxGender.SelectionBoxItem.ToString();
            newEmployee.jobPosition = textBoxJobPosition.Text;
            newEmployee.jobDescription = textBoxJobPosition.Text;
            newEmployee.dateOfEmployment = eDatePickerEmployment.SelectedDate.Value.Date;

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
                if (obj is ListView)
                    ((ListView)obj).Items.Clear();
                clearForm(VisualTreeHelper.GetChild(obj, i));
            }
        }

        private void SubmitPersonBtn_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception)
            {
                MessageBox.Show("The form is completed incorrectly ");
            }
        }

        private void reloadImagePaths()
        {
            foreach (DataRowView row in DataGrid1.ItemsSource)
            {
                try
                {
                    imagesPaths.Add(row[9].ToString());
                }
                catch (Exception er) { }
            }
        }
        private void removeDataPathColumn(DataTable gridData)
        {
            reloadImagePaths();
        
            try
            {
                    DataGrid1.Columns.Remove(DataGrid1.Columns[9]);
            }
            catch(Exception er) { }
        }
        private void reloadData()
        {
            var gridData = raportData = DbHelper.basicSelect(currentTable);
            DataGrid1.ItemsSource = gridData.DefaultView;
            initializeTableFieldsCombobox();
            removeDataPathColumn(gridData);
       
           
        }


        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxSelectField.SelectionBoxItem.ToString() == "")
                MessageBox.Show("I think, that you should select any column");
            if (textBoxSearcher.Text != "")
            {
                try
                {
                    if (comboBoxSelectField.SelectionBoxItem.ToString() != "id")
                    {
                        if (datePickerYoungerThan.SelectedDate == null && datePickerOlderThan.SelectedDate == null)
                        {
                            raportData = DbHelper.likeSelect(currentTable, textBoxSearcher.Text, comboBoxSelectField.SelectionBoxItem.ToString());
                   
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }

                        if (datePickerYoungerThan.SelectedDate != null && datePickerOlderThan.SelectedDate == null)
                        {
                            youngerThan = datePickerYoungerThan.SelectedDate.Value.Date;
                            raportData = DbHelper.advancedSelectYoungerThan(currentTable, youngerThan.ToString(), textBoxSearcher.Text, comboBoxSelectField.SelectionBoxItem.ToString());
                      
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }

                        if (datePickerYoungerThan.SelectedDate == null && datePickerOlderThan.SelectedDate != null)
                        {
                            olderThan = datePickerOlderThan.SelectedDate.Value.Date;
                            raportData = DbHelper.advancedSelectOlderThan(currentTable, olderThan.ToString(), textBoxSearcher.Text, comboBoxSelectField.SelectionBoxItem.ToString());
                     
                            DataGrid1.ItemsSource = raportData.DefaultView;
                        }

                        if (datePickerYoungerThan.SelectedDate == null && datePickerOlderThan.SelectedDate != null)
                        {
                            olderThan = datePickerOlderThan.SelectedDate.Value.Date;
                            youngerThan = datePickerYoungerThan.SelectedDate.Value.Date;
                            raportData = DbHelper.advancedSelectOlderAndYoungerThan(currentTable, olderThan.ToString(), youngerThan.ToString(), textBoxSearcher.Text, comboBoxSelectField.SelectionBoxItem.ToString());
                        
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
            removeDataPathColumn(raportData);
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
                    name = textboxLesson.Text,
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
                        name = eTextboxLesson.Text,
                        lessonTime = DateTime.Now
                    });
                }
            }
        }

        private void DeleteLessonBtn_Click(object sender, RoutedEventArgs e)
        {
            isEditing = editFormExpander.IsExpanded == true ? true : false;
            try
            {

                if (isEditing == true)
                    eLessonsListView.Items.RemoveAt(lessonsListView.Items.Count - 1);
                else
                    lessonsListView.Items.RemoveAt(lessonsListView.Items.Count - 1);
            }
            catch (Exception er) { };
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
            Raport.GenerateRaport(raportData);
        }

        private void setEditedImage(int index)
        {
            try
            {
                eImage.Source = new BitmapImage(new Uri(imagesPaths[index].ToString()));
            }
            catch(Exception er)
            {
                 eImage.Source = new BitmapImage(new Uri("https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_960_720.png"));
            }
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
                    setEditedImage(rowIndex);
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
                    setEditedImage(rowIndex);
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
                    setEditedImage(rowIndex);
                    eTextBoxPesel.Text = row[8].ToString();
                }
            }
        }


        private void editStudent()
        {
          
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
            curStudent.groups = Serializers.serializeGroups(eGroups);
            curStudent.imagePath = newPath;
            curStudent.gender = eComboBoxGender.SelectionBoxItem.ToString();
            curStudent.currentClass = eComboBoxCurrentClass.Text;

            DbHelper.updateStudent(curStudent, selectedRow);
        }
        private void editTeacher()
        {
            string taughtSubjectsJson = Serializers.serializeLessons(eLessons);

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
            curTeacher.gender = eComboBoxGender.SelectionBoxItem.ToString();
            curTeacher.imagePath = newPath;
            curTeacher.classTutor = eTextBoxTutor.Text;
            curTeacher.taughtSubjects = taughtSubjectsJson;
            curTeacher.dateOfEmployment = eTdatePickerEmployment.SelectedDate.Value.Date;

            DbHelper.updateTeacher(curTeacher, selectedRow);
        }
        private void editEmployee()
        {
      
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
            curEmployee.gender = eComboBoxGender.SelectionBoxItem.ToString();
            curEmployee.jobPosition = eTextBoxJobPosition.Text;
            curEmployee.jobDescription = eTextBoxJobPosition.Text;
            curEmployee.dateOfEmployment = eEdatePickerEmployment.SelectedDate.Value.Date;
            curEmployee.imagePath = newPath;

            DbHelper.updateEmployee(curEmployee, selectedRow);
        }
        private void EditRowBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentTable == "students")
                {
                    editStudent();
                    reloadData();
                }
                if (currentTable == "teachers")
                {
                    editTeacher();
                    reloadData();
                }
                if (currentTable == "employees")
                {
                    editEmployee();
                    reloadData();
                }
                editFormExpander.IsExpanded = false;
            }
            catch (Exception)
            {
                MessageBox.Show("The form is completed incorrectly ");
            }
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

        private void closeEditModalBtn(object sender, RoutedEventArgs e)
        {
            editFormExpander.IsExpanded = false;
        }

        private void addGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            isEditing = editFormExpander.IsExpanded == true ? true : false;
            if (isEditing != true)
            {
                groupsListView.Items.Add(comboBoxCurrentGroup.SelectionBoxItem.ToString());
                groups.Add(comboBoxCurrentGroup.SelectionBoxItem.ToString());
            }
            else
            {
                eGroupsListView.Items.Add(eComboBoxCurrentGroup.SelectionBoxItem.ToString());
                eGroups.Add(comboBoxCurrentGroup.SelectionBoxItem.ToString());
            }
        }

        private void deleteGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            isEditing = editFormExpander.IsExpanded == true ? true : false;

            if (isEditing == true)
            {
                eGroupsListView.Items.RemoveAt(eGroupsListView.Items.Count - 1);
                eGroups.RemoveAt(eGroupsListView.Items.Count - 1);
            }
            else
            {
                if (groupsListView.Items.Count>0)
                {
                    groupsListView.Items.RemoveAt(groupsListView.Items.Count - 1);
                    groups.RemoveAt(groupsListView.Items.Count - 1);
                }
            }
        }

        private void hideForms()
        {
            textBoxJobDescription.Visibility = Visibility.Hidden;
            textBoxJobPosition.Visibility = Visibility.Hidden;
            textboxLesson.Visibility = Visibility.Hidden;
            comboBoxCurrentClass.Visibility = Visibility.Hidden;
            comboBoxCurrentGroup.Visibility = Visibility.Hidden;
            classTutorLabel.Visibility = Visibility.Hidden;
            tDateOfEmploymentLabel.Visibility = Visibility.Hidden;
            jobPositionLabel.Visibility = Visibility.Hidden;
            jobDescriptionLabel.Visibility = Visibility.Hidden;
            tenureLabel.Visibility = Visibility.Hidden;
            eDateOfEmploymentLabel.Visibility = Visibility.Hidden;
            eDatepickerLesson.Visibility = Visibility.Hidden;
            classTutorLabel.Visibility = Visibility.Hidden;
            textBoxTutor.Visibility = Visibility.Hidden;
            groupsLabel.Visibility = Visibility.Hidden;
            studentClassesLabel.Visibility = Visibility.Hidden;
            studentDockpanel.Visibility = Visibility.Hidden;
            TdatePickerEmployment.Visibility = Visibility.Hidden;
            eDatePickerEmployment.Visibility = Visibility.Hidden;
            textBoxJobPosition.Visibility = Visibility.Hidden;
            teacherForm.Visibility = Visibility.Hidden;
            textBoxTenure.Visibility = Visibility.Hidden;
            jobDescriptionLabel.Visibility = Visibility.Hidden;
            jobPositionLabel.Visibility = Visibility.Hidden;
        }
        private void showEmployeeForm()
        {
            tenureLabel.Visibility = Visibility.Visible;
            textBoxJobPosition.Visibility = Visibility.Visible;
            textBoxJobDescription.Visibility = Visibility.Visible;
            eDateOfEmploymentLabel.Visibility = Visibility.Visible;
            textBoxTenure.Visibility = Visibility.Visible;
            jobDescriptionLabel.Visibility = Visibility.Visible;
            jobPositionLabel.Visibility = Visibility.Visible;
            eDatePickerEmployment.Visibility = Visibility.Visible;
        }
        private void showTeacherForm()
        {
            textboxLesson.Visibility = Visibility.Visible;
            textBoxTutor.Visibility = Visibility.Visible;
            classTutorLabel.Visibility = Visibility.Visible;
            TdatePickerEmployment.Visibility = Visibility.Visible;
            tDateOfEmploymentLabel.Visibility = Visibility.Visible;
            teacherForm.Visibility = Visibility.Visible;
        }
        private void showStudentForm()
        {
            comboBoxCurrentClass.Visibility = Visibility.Visible;
            comboBoxCurrentGroup.Visibility = Visibility.Visible;
            studentDockpanel.Visibility = Visibility.Visible;
            groupsLabel.Visibility = Visibility.Visible;
            studentClassesLabel.Visibility = Visibility.Visible;
        }

        private void comboBoxSelectField_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            initializeTableFieldsCombobox();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            DataGrid1.ItemsSource = DbHelper.basicSelect("students").DefaultView;
            showStudentForm();  
        }

        private void DataGrid1_Loaded(object sender, RoutedEventArgs e)
        {
             DataGrid1.Columns.Remove(DataGrid1.Columns[9]);
        }
    }

}