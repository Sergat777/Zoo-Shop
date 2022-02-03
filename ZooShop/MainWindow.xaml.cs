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

namespace ZooShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            mainFrame.Navigate(new FolderPage.AnimalViewPage());
            FolderClass.mainFrameObj.mainFrame = mainFrame;
        }

        private void btMainView_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new FolderPage.AnimalViewPage());
        }

        private void btAddRecord_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new FolderPage.AddEditRecord(null));
        }
    }
}
