using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System.IO;
using System.ComponentModel;
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
        public string bindMainModifier = "";
        public string bindUploadModifier = "";
        public string bindOptionsModifier = "";

        private void displayCloud(string btnDesc, System.Windows.UIElement curView)
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
        private void MainViewBtn_Click(object sender, RoutedEventArgs e)
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
        private void initializeUploadBind(KeyEventArgs e)
        {
            if (e.Key.ToString() == bindUpload && bindUploadModifier == "")
                DataContext = new DataViewModel();
            if (e.Key.ToString() == bindUpload && bindUploadModifier.ToLower() == "alt" && (Keyboard.IsKeyDown(Key.RightAlt) || Keyboard.IsKeyDown(Key.LeftAlt)))
                DataContext = new DataViewModel();
            if (e.Key.ToString() == bindUpload && bindUploadModifier.ToLower() == "ctrl" && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                DataContext = new DataViewModel();
            if (e.Key.ToString() == bindUpload && bindUploadModifier.ToLower() == "shift" && (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)))
                DataContext = new DataViewModel();
        }
        private void initializeOptionsBind(KeyEventArgs e)
        {
            if (e.Key.ToString() == bindOptions && bindOptionsModifier == "")
                DataContext = new OptionsViewModel();
            if (e.Key.ToString() == bindOptions && bindOptionsModifier.ToLower() == "alt" && (Keyboard.IsKeyDown(Key.RightAlt) || Keyboard.IsKeyDown(Key.LeftAlt)))
                DataContext = new OptionsViewModel();
            if (e.Key.ToString() == bindOptions && bindOptionsModifier.ToLower() == "ctrl" && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                DataContext = new OptionsViewModel();
            if (e.Key.ToString() == bindOptions && bindOptionsModifier.ToLower() == "shift" && (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)))
                DataContext = new OptionsViewModel();
        }
        private void initializeMainBind(KeyEventArgs e)
        {
            if (e.Key.ToString() == bindMain && bindMainModifier == "")
                DataContext = new MainViewModel();
            if (e.Key.ToString() == bindMain && bindMainModifier.ToLower() == "alt" && (Keyboard.IsKeyDown(Key.RightAlt) || Keyboard.IsKeyDown(Key.LeftAlt)))
                DataContext = new MainViewModel();
            if (e.Key.ToString() == bindMain && bindMainModifier.ToLower() == "ctrl" && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                DataContext = new MainViewModel();
            if (e.Key.ToString() == bindMain && bindMainModifier.ToLower() == "shift" && (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)))
                DataContext = new MainViewModel();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            int counter = 0;
            string path = Environment.CurrentDirectory + "/assets/binds.txt";
            string text = File.ReadAllText(path);
            string[] lines = text.Split(Environment.NewLine);

            bindMainModifier = "";
            bindUploadModifier = "";
            bindOptionsModifier = "";

            foreach (string line in lines)
            {
                if (counter == 0)
                {
                    if (!line.Contains("+"))
                    {
                        bindMain = line;
                    }
                    else
                    {
                        string[] hotkeys = line.Split('+');
                        if (hotkeys[0].Length > 2)
                            (hotkeys[0], hotkeys[1]) = (hotkeys[1], hotkeys[0]);
                        bindMain = hotkeys[0];
                        bindMainModifier = hotkeys[1];

                    }
                }
                if (counter == 1)
                {
                    if (!line.Contains("+"))
                    {
                        bindUpload = line;
                    }
                    else
                    {
                        string[] hotkeys = line.Split('+');
                        if (hotkeys[0].Length > 2)
                            (hotkeys[0], hotkeys[1]) = (hotkeys[1], hotkeys[0]);
                        bindUpload = hotkeys[0];
                        bindUploadModifier = hotkeys[1];
                    }


                }
                if (counter == 2)
                {
                    if (!line.Contains("+"))
                    {
                        bindOptions = line;
                    }
                    else
                    {
                        string[] hotkeys = line.Split('+');
                        if (hotkeys[0].Length > 2)
                            (hotkeys[0], hotkeys[1]) = (hotkeys[1], hotkeys[0]);
                        bindOptions = hotkeys[0];
                        bindOptionsModifier = hotkeys[1];
                    }

                }
                counter++;
            }

            initializeUploadBind(e);
            initializeMainBind(e);
            initializeOptionsBind(e);
        }
    }
}
