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
                {
                    TreeViewItem tw = new TreeViewItem();
                    tw.Header = d.Name;
                    tw.Items.Add("");
                    roots.Add(tw);
                }

            }

            var tree = sender as TreeView;
            foreach (var it in roots)
            {
                
                tree.Items.Add(it);
            }
                
        }

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem root = TreeView1.SelectedItem as TreeViewItem;
            
            if (root != null)
            {
                if(root.IsExpanded)
                    root.Items.Remove("");
                SetSubs(root);                 
            }

        }
        private void TreeViewItem_Collapsed(object sender, RoutedEventArgs e) { 
            TreeViewItem root = TreeView1.SelectedItem as TreeViewItem;

            if (root != null)
            {
                root.Items.Clear();
                root.Items.Add("");
            }
        }

        private void SetSubs(TreeViewItem root)
        {
            
            string[] files, subDirs;
           
            if (root != null)
            {
                root.Items.Remove(1);
                try
                {
                    subDirs = System.IO.Directory.GetDirectories((string)root.Header);
                    files = System.IO.Directory.GetFiles((string)root.Header);
                    foreach (var dir in subDirs)
                    {
                        TreeViewItem child = new TreeViewItem();
                        child.Header = dir;
                        child.Items.Add("");
                        root.Items.Add(child);
                    }
                    foreach (var fl in files)
                        root.Items.Add(GetNameFromPath(fl));
                    
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show(String.Format("You have no Access to {0}", (string)root.Header));
                }
            }
        }

        private string GetNameFromPath(string path)
        {
            StringBuilder sb = new StringBuilder();
            int idx = path.LastIndexOf("\\");
            string name = path.Substring(idx + 1);
            return name;
        }
    }

}