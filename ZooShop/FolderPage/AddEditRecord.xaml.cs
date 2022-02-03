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

namespace ZooShop.FolderPage
{
    /// <summary>
    /// Interaction logic for AddEditRecord.xaml
    /// </summary>
    public partial class AddEditRecord : Page
    {
        FolderEntity.Animal currentAnimal = new FolderEntity.Animal();

        public AddEditRecord(FolderEntity.Animal editingAnimal)
        {
            InitializeComponent();

            btDeleteRecord.Visibility = Visibility.Hidden;

            if (editingAnimal != null)
            {
                currentAnimal = editingAnimal;
                btDeleteRecord.Visibility = Visibility.Visible;
            }

            DataContext = currentAnimal;
        }

        private void btSaveRecord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder errors = new StringBuilder();

                if (string.IsNullOrWhiteSpace(tbNameAnimal.Text) || currentAnimal.nameAnimal == null)
                    errors.AppendLine("Введите корректное наименование животного");

                if (cmbxNewAnimal.SelectedIndex == 0 || currentAnimal.typeOfAnimalID == 0)
                    errors.AppendLine("Выберите тип животного");

                if (cmbxNewPlace.SelectedIndex == 0 || currentAnimal.placeAnimalID == 0)
                    errors.AppendLine("Выберите регион проживания животного");

                if (errors.Length > 0)
                    MessageBox.Show(errors.ToString(), "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    currentAnimal.description = tbDescriptionAnimal.Text;

                    if (currentAnimal.idAnimal == 0)
                        FolderEntity.ZooShopEntities.GetContext().Animal.Add(currentAnimal);

                    FolderEntity.ZooShopEntities.GetContext().SaveChanges();
                    MessageBox.Show("Запись о животном успешно сохранена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                    FolderClass.mainFrameObj.mainFrame.GoBack();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btChooseImage_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog();

            ofd.Filter = "(*.jpg)|*.jpg|(*.jpeg)|*.jpeg|JPEG|(*.png)|*.png|(*.gif)|*.gif|GIF";

            if ((bool)ofd.ShowDialog())
            {
                currentAnimal.photoPreview = File.ReadAllBytes(ofd.FileName);
                imgAnimal.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }

        private void btDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить запись о выбранном животном?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    FolderEntity.ZooShopEntities.GetContext().Animal.Remove(currentAnimal);
                    FolderEntity.ZooShopEntities.GetContext().SaveChanges();

                    MessageBox.Show("Удаление выполнено успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    FolderClass.mainFrameObj.mainFrame.GoBack();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }
    }
}
