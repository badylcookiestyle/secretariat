using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System.IO;
 
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
      //      DataContext = new MainViewModel();


        }

        //  SQLiteConnection sql;
        public string[] binds;
       
        public string bindMain = "F1";
        public string bindUpload = "F2";
        public string bindOptions = "F3";

        private void displayCloud(string btnDesc,System.Windows.UIElement curView) 
        {
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            int counter = 0;
            string path = Environment.CurrentDirectory + "/assets/binds.txt";
            string text = File.ReadAllText(path);
            string[] lines = text.Split(Environment.NewLine);

            foreach (string line in lines)
            {
              
                if (counter == 0)
                {
                    bindMain = line;
                }
                if (counter == 1)
                {
                    bindUpload = line;

                }
                if(counter == 2)
                {
                    bindOptions = line;
                }
                counter++;
            }
          //  var isShiftKey = Keyboard.Modifiers == ModifierKeys.Shift ? true : false;
          // var isAltKey = Keyboard.Modifiers = ModifierKeys.Alt ? true : false;
          // var isCtrlKey = Keyboard.Modifiers = ModifierKeys.Alt ? true : false;
            if (e.Key.ToString() == bindMain)
            {
                DataContext = new MainViewModel();
            }
            if (e.Key.ToString() == bindUpload)
            {
                DataContext = new DataViewModel();

            }
            if (e.Key.ToString() == bindOptions)
            {
                DataContext = new OptionsViewModel();
            }
        }
    }
}
