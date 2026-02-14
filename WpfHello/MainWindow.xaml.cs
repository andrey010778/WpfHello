using System.CodeDom;
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

namespace WpfHello
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string nameFile = ("C://Users//Андрей//source//repos//WpfHello//WpfHello//username.txt");
        bool isDataDirty = false;

       


        public MyWindow myWin { get; set; } 

        public MainWindow()
        {

            CommandBinding abinding = new CommandBinding();
            abinding.Command = CustomCommands.Launch;
            abinding.Executed += new ExecutedRoutedEventHandler(Launch_Handler);

            abinding.CanExecute += new CanExecuteRoutedEventHandler(LaunchEnabled_Handler);
            this.CommandBindings.Add(abinding);

            InitializeComponent();
            lbl.Content = "Добрый день!";
            setBut.IsEnabled = false;
            retBut.IsEnabled = false;
            Top = 25;
            Left = 25;
            

        }

        private void SetBut()
        {
            System.IO.StreamWriter se = new System.IO.StreamWriter(nameFile);
            se.WriteLine(setText.Text);
            se.Close();
            retBut.IsEnabled = true;
            isDataDirty = false;
        }

        private void RetBut()
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(nameFile);
            setText.Text = sr.ReadToEnd();
            sr.Close();
            setBut.IsEnabled = true;
            isDataDirty = false;
        
        }

       

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement feSource = e.Source as FrameworkElement;   
            try
            {
                switch (feSource.Name)
                {
                    case "setBut":
                        SetBut();
                        break;
                    case "retBut":
                        RetBut();
                        break;
                }
                e.Handled = true;

            }
            catch (Exception ex)  
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void setText_TextChanged(object sender, TextChangedEventArgs e)
        {
            setBut.IsEnabled = true;
            isDataDirty = true;
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.isDataDirty)
            {
                string msg = "Data was changed but didn't save! \n Close window without saving?";
                MessageBoxResult result = MessageBox.Show(msg, "Checking data", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }
    
        private void LaunchEnabled_Handler(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (bool)check.IsChecked;
        }
        
        private void Launch_Handler(object sender, RoutedEventArgs e)
        {
            if (myWin == null)
                myWin = new MyWindow();
            myWin.Owner = this;
            var location = New_win.PointToScreen(new Point(0, 0));
            myWin.Left = location.X + New_win.Width;
            myWin.Top = location.Y;
            myWin.Show();
        }
    
    }
    }