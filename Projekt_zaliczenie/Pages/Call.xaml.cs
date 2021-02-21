using System;
using Projekt_zaliczenie.Enum;
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
using Projekt_zaliczenie.Classes;

namespace Projekt_zaliczenie.Pages
{
    /// <summary>
    /// Interaction logic for Call.xaml
    /// </summary>
    public partial class Call : UserControl
    {
        public Call()
        {
            InitializeComponent();
            foreach (var item in DeleteSearch.GetValues(typeof(DeleteSearch)))
            {
                deleteSearchCombo.Items.Add(item);
            }
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

        private void deletesearchtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (OwnerEntities db = new OwnerEntities())
            {
                if (deletesearchtxt.Text != "")
                {
                    if (deleteSearchCombo.Text == "Imię")
                    {
                        var fNameSearch = from p in db.People
                                          where p.PhoneBookID == db.PhoneBooks.Where(x => x.OwnerID == db.Owners.Where(o => o.fName == ActualOwner.fName).Where(o => o.lName == ActualOwner.lName).FirstOrDefault().ID).FirstOrDefault().ID
                                          where p.fName.Contains(deletesearchtxt.Text)
                                          select new
                                          {
                                              p.fName,
                                              p.lName,
                                              Country = p.Countries.Country,
                                              Email = p.EmailAddresses.Select(x => x.Email).ToList(),
                                              Number = p.PhoneNumbers.Select(x => x.Number).ToList()
                                          };
                        this.DeleteListGrid.ItemsSource = fNameSearch.ToList();
                    }
                    else if (deleteSearchCombo.Text == "Nazwisko")
                    {
                        var lNameSearch = from p in db.People
                                          where p.PhoneBookID == db.PhoneBooks.Where(x => x.OwnerID == db.Owners.Where(o => o.fName == ActualOwner.fName).Where(o => o.lName == ActualOwner.lName).FirstOrDefault().ID).FirstOrDefault().ID
                                          where p.lName.Contains(deletesearchtxt.Text)
                                          select new
                                          {
                                              p.fName,
                                              p.lName,
                                              Country = p.Countries.Country,
                                              Email = p.EmailAddresses.Select(x => x.Email).ToList(),
                                              Number = p.PhoneNumbers.Select(x => x.Number).ToList()
                                          };
                        this.DeleteListGrid.ItemsSource = lNameSearch.ToList();
                    }
                    else if (deleteSearchCombo.Text == "Email")
                    {
                        var CountrySearch = from p in db.People
                                            where p.PhoneBookID == db.PhoneBooks.Where(x => x.OwnerID == db.Owners.Where(o => o.fName == ActualOwner.fName).Where(o => o.lName == ActualOwner.lName).FirstOrDefault().ID).FirstOrDefault().ID
                                            where p.ID == db.EmailAddresses.Where(x => x.Email.Contains(deletesearchtxt.Text)).FirstOrDefault().PersonID
                                            select new
                                            {
                                                p.fName,
                                                p.lName,
                                                Country = p.Countries.Country,
                                                Email = p.EmailAddresses.Select(x => x.Email).ToList(),
                                                Number = p.PhoneNumbers.Select(x => x.Number).ToList()
                                            };
                        this.DeleteListGrid.ItemsSource = CountrySearch.ToList();
                    }
                }
            }
        }

        private void delete_btn(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
