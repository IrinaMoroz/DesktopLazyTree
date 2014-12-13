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

using System.IO;

namespace DesktopLazyTree
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void TreeView_Loaded(object sender, RoutedEventArgs e)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            List<TreeViewItem> roots = new List<TreeViewItem>();

            foreach (DriveInfo d in allDrives)
            {
                if (d.DriveType != DriveType.Unknown && d.IsReady )
                    roots.Add(CreateChild(d.Name, true));
            }

            var tree = sender as TreeView;
            foreach (var it in roots)
            {                
                tree.Items.Add(it);
            }
                
        }

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem root = e.Source as TreeViewItem;
            
            if (root != null)
            {                
                root.Items.Remove("");
                if (((Element)root.Header).IsFolder)
                    SetSubs(root);                 
            }

        }
        private void TreeViewItem_Collapsed(object sender, RoutedEventArgs e) {
            TreeViewItem root = e.Source as TreeViewItem;

            if (root != null)
            {
                if (!root.Items.IsEmpty)
                {
                    root.Items.Clear();
                    root.Items.Add("");
                }                    
            }
        }

        private void SetSubs(TreeViewItem root)
        {                            
            try
            {
                SetSubDirs(root);
                SetSubFolders(root);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(String.Format("You have no access to {0}", ((Element)root.Header).Path));
            }
        }


        private void SetSubDirs(TreeViewItem root)
        {
          
            string[] subDirs = System.IO.Directory.GetDirectories(((Element)root.Header).Path);
          
            foreach (var dir in subDirs)
            {
                root.Items.Remove("");
                root.Items.Add(CreateChild(dir, true));
            } 
            
        }

        private void SetSubFolders(TreeViewItem root)
        {
          string[] files = System.IO.Directory.GetFiles(((Element)root.Header).Path);

          foreach (var file in files)
            root.Items.Add(CreateChild(file, false));            
        }

        private TreeViewItem CreateChild(string path, bool isFoleder)
        {
            Element el = new Element(path, isFoleder);
            TreeViewItem child = new TreeViewItem();
            child.Header = el;
            if(isFoleder)
                child.Items.Add("");
            return child;
        }
    }
}