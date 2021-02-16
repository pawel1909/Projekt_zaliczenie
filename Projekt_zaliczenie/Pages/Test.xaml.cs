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

namespace Projekt_zaliczenie.Pages
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : UserControl
    {
        public Test()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void test_btn_Click(object sender, RoutedEventArgs e)
        {
            OwnerEntities db = new OwnerEntities();

            this.Testgrid.ItemsSource = db.PhoneBooks.ToList();
        }

        private void zmiana_test(object sender, SelectionChangedEventArgs e)
        {
            if (this.Testgrid.SelectedIndex == 0)
            {
                if (this.Testgrid.SelectedItems.Count >= 0)
                {
                    if (this.Testgrid.SelectedItems[0].GetType() == typeof(PhoneBooks))
                    {
                        PhoneBooks p = new PhoneBooks();

                        
                    }
                }
            }
        }
    }
}
