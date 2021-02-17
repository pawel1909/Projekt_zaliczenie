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
using System.Data.Entity;

namespace Projekt_zaliczenie.Pages
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : UserControl
    {
        OwnerEntities context = new OwnerEntities();
        CollectionViewSource peopleViewSources;

        public Test()
        {
            InitializeComponent();
            peopleViewSources = ((CollectionViewSource)(FindResource("peopleViewSource")));
            DataContext = this;
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

            //Owners owner = new Owners()
            //{
            //    lName = "xxx",
            //    fName = "xxxxx"
            //};

            //PhoneBooks phone = new PhoneBooks()
            //{
            //    OwnerID = 1
            //};

            //Countries country = new Countries()
            //{
            //    Country = "Poland"
            //};

            //People people = new People()
            //{
            //    lName = "xxxx",
            //    fName = "xww",
            //    CountryID = 1,
            //    PhoneBookID = 1
            //};

            //EmailAddresses email = new EmailAddresses()
            //{
            //    PersonID = 1,
            //    Email = "xxxxxx@wp.pl"
            //};

            //PhoneNumbers number = new PhoneNumbers()
            //{
            //    PersonID = 1,
            //    Number = "123123123"
            //};


            //db.Owners.Add(owner);
            //db.People.Add(people);
            //db.PhoneBooks.Add(phone);
            //db.PhoneNumbers.Add(number);
            //db.Countries.Add(country);
            //db.SaveChanges();

            var q = db.PhoneBooks;

            this.Testgrid.ItemsSource = q.ToList();
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
        /// <summary>
        /// Przycisk odpowiedzialny za usuwanie obiektów z bazy.
        /// </summary>
        /// <param name="sender">Nie mam pojęcia co to jest</param>
        /// <param name="e"></param>
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            OwnerEntities db = new OwnerEntities();


            //dodaj using (
            //var q = db.Owners.Include(x => x.PhoneBooks).First();
            //var q1 = db.People.OrderBy(x => x.ID).Include(x => x.Countries).Include(x => x.EmailAddresses).Include(x => x.PhoneNumbers).First();
            //var q2 = db.PhoneBooks.Include(x => x.Owners).Include(x => x.People).First();
            //var q4 = db.EmailAddresses.First();
            //var q5 = db.PhoneNumbers.First();

            //db.People.Remove(q1);
            //db.EmailAddresses.Remove(q4);
            //db.PhoneNumbers.Remove(q5);
            //db.Owners.Remove(q);
            //db.PhoneBooks.Remove(q2);
            db.SaveChanges();


        }
    }
}
