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

        List<DirItem> items = new List<DirItem>();
        List<DirItem> currentItems = new List<DirItem>();

        public String ImageSource { get; set; }
        public String fileName { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ImageSource = @"/Extractor;component/icons/cropsquare.png";
            maxi.DataContext = this;
            filename.DataContext = this;
            fileName = "Downloads/AllMyMods.rar";
            lbTodoList.ItemsSource = currentItems;
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
                    items.Add(new DirItem() { Title = reader.Entry.Key });
                }                
                setupFolders();
            }
        }

        private void getChildren(object sender, RoutedEventArgs e)
        {
            currentItems.Clear();
            String folder = ((Button)sender).Content.ToString();
            foreach(DirItem i in items)
            {

                List<String> split = i.Title.Split('\\').ToList();
                int di = split.IndexOf(folder);
                Console.Write("Title " + i.Title + "\n");
                Console.Write("di " + di + "\n");
                Console.Write("Count " + split.Count + "\n");
                if(split.Count == di + 2 && di >= 0)
                {
                    currentItems.Add(new DirItem() { Title = System.IO.Path.GetFileName(i.Title) });
                }
            }
            var itemsView = CollectionViewSource.GetDefaultView(lbTodoList.ItemsSource);
            itemsView.Refresh();
        }

        private void setupFolders()
        {
            String temppath = items[0].Title;
            String rootDirectory = temppath.Split('\\')[0];
            currentItems.Add(new DirItem() { Title = rootDirectory });
            var itemsView = CollectionViewSource.GetDefaultView(lbTodoList.ItemsSource);
            itemsView.Refresh();
        }

        public class DirItem
        {
            public string Title { get; set; }
        }

        
    }
}
