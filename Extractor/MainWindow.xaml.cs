using SharpCompress.Readers.Rar;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Extractor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public String ImageSource { get; set; }
        public String fileName { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ImageSource = @"/Extractor;component/icons/cropsquare.png";
            maxi.DataContext = this;
            filename.DataContext = this;
            fileName = "Downloads/AllMyMods.rar";
        }

        private void barMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Maximize(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
                this.ImageSource = @"/Extractor;component/icons/windowrestore.png";
                BindingOperations.GetBindingExpression(maxi, Image.SourceProperty).UpdateTarget();
            }else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
                this.ImageSource = @"/Extractor;component/icons/cropsquare.png";
                BindingOperations.GetBindingExpression(maxi, Image.SourceProperty).UpdateTarget();
            }            
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void OpenRar(object sender, RoutedEventArgs e)
        {
            using (var reader = RarReader.Open(File.OpenRead("polymerredo.rar")))
            {
                while (reader.MoveToNextEntry())
                {
                    Console.Write(reader.Entry.Key);
                }
            }
        }
    }
}
