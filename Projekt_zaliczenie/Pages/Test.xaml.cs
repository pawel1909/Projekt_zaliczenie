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

        public Test()
        {
            InitializeComponent();
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

            var q = db.Owners;
            var q1 = db.PhoneBooks;
            var q2 = db.People;
            var q3 = db.EmailAddresses;
            var q4 = db.PhoneNumbers;

            var people = from p in db.People
                         select new
                         {
                             p.fName,
                             p.lName,
                             Country = p.Countries.Country,
                             Email = p.EmailAddresses.Select(x => x.Email).ToList(),
                             Number = p.PhoneNumbers.Select(x => x.Number).ToList()
                         };

            this.test.ItemsSource = people.ToList();

            this.TestgridOwner.ItemsSource = q.ToList();
            this.TestgridPhoneBook.ItemsSource = q1.ToList();
            this.TestgridPeople.ItemsSource = people.ToList();
            this.TestgridEmailAddres.ItemsSource = q3.ToList();
            this.TestgridPhoneNumber.ItemsSource = q4.ToList();
        }

        private void zmiana_test(object sender, SelectionChangedEventArgs e)
        {
            //if (this.Testgrid.SelectedIndex == 0)
            //{
            //    if (this.Testgrid.SelectedItems.Count >= 0)
            //    {
            //        if (this.Testgrid.SelectedItems[0].GetType() == typeof(PhoneBooks))
            //        {
            //            PhoneBooks p = new PhoneBooks();

                        
            //        }
            //    }
            //}
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

            if (db.Owners.Count() != 0)
            {
                var q = db.Owners;
                foreach (var item in q)
                {
                    db.Owners.Remove(db.Owners.Include(x => x.PhoneBooks).First());
                }
            }
            if (db.People.Count() != 0)
            {
                var q = db.People;
                foreach (var item in q)
                {
                    db.People.Remove(db.People.OrderBy(x => x.ID).Include(x => x.Countries).Include(x => x.EmailAddresses).Include(x => x.PhoneNumbers).First());
                }
            }
            if (db.PhoneBooks.Count() != 0)
            {
                var q = db.PhoneBooks;
                foreach (var item in q)
                {
                    db.PhoneBooks.Remove(db.PhoneBooks.Include(x => x.Owners).Include(x => x.People).First());
                }
            }
            if (db.EmailAddresses.Count() != 0)
            {
                var q = db.EmailAddresses;
                foreach (var item in q)
                {
                    db.EmailAddresses.Remove(db.EmailAddresses.First());
                }
            }
            if (db.PhoneNumbers.Count() != 0)
            {
                var q = db.PhoneNumbers;
                foreach (var item in q)
                {
                    db.PhoneNumbers.Remove(db.PhoneNumbers.First());
                }
            }
            //if (db.Countries.Count() != 0)
            //{
            //    var q = db.Countries;
            //    foreach (var item in q)
            //    {
            //        db.Countries.Remove(db.Countries.First());
            //    }
            //}

            //db.Database.ExecuteSqlCommand("DELETE FROM [Owners]");
            db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Owner.dbo.Owners', RESEED, 0)");
            db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Owner.dbo.EmailAddresses', RESEED, 0)");
            db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Owner.dbo.People', RESEED, 0)");
            db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Owner.dbo.PhoneBooks', RESEED, 0)");
            db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Owner.dbo.PhoneNumbers', RESEED, 0)");
            //db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Owner.dbo.Countries', RESEED, 0)");
            db.SaveChanges();


        }
    }
}
