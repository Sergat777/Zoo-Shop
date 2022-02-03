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

namespace ZooShop.FolderPage
{
    /// <summary>
    /// Interaction logic for AnimalViewPage.xaml
    /// </summary>
    public partial class AnimalViewPage : Page
    {
        public AnimalViewPage()
        {
            InitializeComponent();
            lvAnimals.ItemsSource = FolderEntity.ZooShopEntities.GetContext().Animal.ToList();
        }

        private void btEditAnimal_Click(object sender, RoutedEventArgs e)
        {
            FolderClass.mainFrameObj.mainFrame.Navigate(new FolderPage.AddEditRecord((sender as Button).DataContext as FolderEntity.Animal));
        }

        bool animal = false;    // выбран тип животного
        bool place = false;     // выбрано место
        
        private void cmbxPlaceOfAnimal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbxPlaceOfAnimal.SelectedIndex == 0)   // место не выбрано
            {
                place = false;
                if (animal)                             // животное выбрано
                    lvAnimals.ItemsSource = FolderEntity.ZooShopEntities.GetContext().Animal.
                         Where(u => u.typeOfAnimalID == cmbxTypeOfAnimal.SelectedIndex).ToList();
                else                                    // животное не выбрано
                    lvAnimals.ItemsSource = FolderEntity.ZooShopEntities.GetContext().Animal.ToList();
            }
            else                                        // место выбрано
            {
                place = true;
                if (animal)                             // животное выбрано
                    lvAnimals.ItemsSource = FolderEntity.ZooShopEntities.GetContext().Animal.
                        Where(u => u.typeOfAnimalID == cmbxTypeOfAnimal.SelectedIndex && u.placeAnimalID == cmbxPlaceOfAnimal.SelectedIndex).ToList();
                else                                    // животное не выбрано
                    lvAnimals.ItemsSource = FolderEntity.ZooShopEntities.GetContext().Animal.
                        Where(u => u.placeAnimalID == cmbxPlaceOfAnimal.SelectedIndex).ToList();
            }

            lvAnimals.SelectedIndex = 0;
        }

        private void cmbxTypeOfAnimal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbxTypeOfAnimal.SelectedIndex == 0)   // животное не выбрано
            {
                animal = false;
                if (place)                             // место выбрано
                    lvAnimals.ItemsSource = FolderEntity.ZooShopEntities.GetContext().Animal.
                        Where(u => u.placeAnimalID == cmbxPlaceOfAnimal.SelectedIndex).ToList();
                else                                   // место не выбрано
                    lvAnimals.ItemsSource = FolderEntity.ZooShopEntities.GetContext().Animal.ToList();
            }
            else                                       // животное выбрано
            {
                animal = true;
                if (place)                             // место выбрано
                    lvAnimals.ItemsSource = FolderEntity.ZooShopEntities.GetContext().Animal.
                        Where(u => u.typeOfAnimalID == cmbxTypeOfAnimal.SelectedIndex && u.placeAnimalID == cmbxPlaceOfAnimal.SelectedIndex).ToList();
                else                                   // место не выбрано
                    lvAnimals.ItemsSource = FolderEntity.ZooShopEntities.GetContext().Animal.
                        Where(u => u.typeOfAnimalID == cmbxTypeOfAnimal.SelectedIndex).ToList();
            }

            lvAnimals.SelectedIndex = 0;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                FolderEntity.ZooShopEntities.GetContext().ChangeTracker.Entries().ToList();
                cmbxTypeOfAnimal.SelectedIndex = 0;
                cmbxPlaceOfAnimal.SelectedIndex = 0;
                lvAnimals.ItemsSource = FolderEntity.ZooShopEntities.GetContext().Animal.ToList();
            }
        }
    }
}
