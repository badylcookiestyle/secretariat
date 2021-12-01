using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

using secretary.viewModels;
using secretary;
namespace Secretary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
      //      DataContext = new MainViewModel();


        }

        //  SQLiteConnection sql;
      
        private void displayCloud(string btnDesc,System.Windows.UIElement curView) {
            hoverCloud.PlacementTarget = curView;
            hoverCloud.Placement = PlacementMode.Right;
            hoverCloud.IsOpen = true;
            Header.PopupText.Text = btnDesc;
            
        }


        private void MainViewMouse_Entr(object sender, MouseEventArgs e)
        {
            displayCloud("Home", MainViewBtn);
        }
        private void OptionsMouse_Entr(object sender, MouseEventArgs e)
        {
            displayCloud("Options", OptionsViewBtn);
        }
        private void DataViewMouse_Entr(object sender, MouseEventArgs e)
        {
            displayCloud("Upload data", DataViewBtn);
        }

        private void MainViewMouse_Lv(object sender, MouseEventArgs e)
        {
            
            hoverCloud.Visibility = Visibility.Collapsed;
            hoverCloud.IsOpen = false;
        }
        
        private void DataViewMouse_Lv(object sender, MouseEventArgs e)
        {
            hoverCloud.Visibility = Visibility.Collapsed;
            hoverCloud.IsOpen = false;
        }

        private void OptionsMouse_Lv(object sender, MouseEventArgs e)
        {
            hoverCloud.Visibility = Visibility.Collapsed;
            hoverCloud.IsOpen = false;
        }
        //Navbar btns listeners
        private void MainViewBtn_Click(object sender,RoutedEventArgs e)
        {
           DataContext = new MainViewModel();
         

        }
        private void DataViewBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new DataViewModel();

        }
        private void OptionsViewBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new OptionsViewModel();
        }
        //Basic window stuff
        void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void MinimalizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void WindowDrag_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
