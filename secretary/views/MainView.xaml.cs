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

namespace secretary.views
{
    /// <summary>
    /// Logika interakcji dla klasy MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        string currentForm = "Student";
        public MainView()
        {
            InitializeComponent();
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
    }
}
