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
        }

        private void MainViewMouse_Entr(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = MainViewBtn;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Home";
        }
        private void OptionsMouse_Entr(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = OptionsViewBtn;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Options";
        }
        private void DataViewMouse_Entr(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = DataViewBtn;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Upload data";
        }

        private void MainViewMouse_Lv(object sender, MouseEventArgs e)
        {
            
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }
        
        private void DataViewMouse_Lv(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void OptionsMouse_Lv(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void MinimalizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        
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
        private void WindowDrag_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
