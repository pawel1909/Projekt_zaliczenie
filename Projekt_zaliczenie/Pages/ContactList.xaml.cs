using Projekt_zaliczenie.Classes;
using Projekt_zaliczenie.Enum;
using Projekt_zaliczenie.PagesModels;
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
using WpfProject;

namespace Projekt_zaliczenie.Pages
{
    /// <summary>
    /// Interaction logic for ContactList.xaml
    /// </summary>
    public partial class ContactList : UserControl
    {
        public ContactList()
        {
            InitializeComponent();
            foreach (var item in Search_list.GetValues(typeof(Search_list)))
            {
                SearchCombo.Items.Add(item);
            }

            using (OwnerEntities db = new OwnerEntities())
            {
                var people = from p in db.People
                             where p.PhoneBookID == db.PhoneBooks.Where(x => x.OwnerID == db.Owners.Where(o => o.fName == ActualOwner.fName).Where(o => o.lName == ActualOwner.lName).FirstOrDefault().ID).FirstOrDefault().ID
                             select new
                             {
                                 p.fName,
                                 p.lName,
                                 Country = p.Countries.Country,
                                 Email = p.EmailAddresses.Select(x => x.Email).ToList(),
                                 Number = p.PhoneNumbers.Select(x => x.Number).ToList()
                             };

                this.ContactListGrid.ItemsSource = people.ToList();

                
            }

        }

        private void searching(object sender, TextCompositionEventArgs e)
        {
            using (OwnerEntities db = new OwnerEntities())
            {
                if (searchtxt.Text != "")
                {
                    if (SearchCombo.Text == "Imię")
                    {
                        var fNameSearch = from p in db.People
                                          where p.PhoneBookID == db.PhoneBooks.Where(x => x.OwnerID == db.Owners.Where(o => o.fName == ActualOwner.fName).Where(o => o.lName == ActualOwner.lName).FirstOrDefault().ID).FirstOrDefault().ID
                                          where p.fName.Contains(searchtxt.Text)
                                          select new
                                          {
                                              p.fName,
                                              p.lName,
                                              Country = p.Countries.Country,
                                              Email = p.EmailAddresses.Select(x => x.Email).ToList(),
                                              Number = p.PhoneNumbers.Select(x => x.Number).ToList()
                                          };
                        this.seatchGrid.ItemsSource = fNameSearch.ToList();
                    }
                    else if (SearchCombo.Text == "Nazwisko")
                    {
                        var lNameSearch = from p in db.People
                                          where p.PhoneBookID == db.PhoneBooks.Where(x => x.OwnerID == db.Owners.Where(o => o.fName == ActualOwner.fName).Where(o => o.lName == ActualOwner.lName).FirstOrDefault().ID).FirstOrDefault().ID
                                          where p.lName.Contains(searchtxt.Text)
                                          select new
                                          {
                                              p.fName,
                                              p.lName,
                                              Country = p.Countries.Country,
                                              Email = p.EmailAddresses.Select(x => x.Email).ToList(),
                                              Number = p.PhoneNumbers.Select(x => x.Number).ToList()
                                          };
                        this.seatchGrid.ItemsSource = lNameSearch.ToList();
                    }
                    else if (SearchCombo.Text == "Kraj")
                    {
                        var CountrySearch = from p in db.People
                                          where p.PhoneBookID == db.PhoneBooks.Where(x => x.OwnerID == db.Owners.Where(o => o.fName == ActualOwner.fName).Where(o => o.lName == ActualOwner.lName).FirstOrDefault().ID).FirstOrDefault().ID
                                          where p.CountryID == db.Countries.Where(x => x.Country.Contains(searchtxt.Text)).FirstOrDefault().ID
                                          select new
                                          {
                                              p.fName,
                                              p.lName,
                                              Country = p.Countries.Country,
                                              Email = p.EmailAddresses.Select(x => x.Email).ToList(),
                                              Number = p.PhoneNumbers.Select(x => x.Number).ToList()
                                          };
                        this.seatchGrid.ItemsSource = CountrySearch.ToList();
                    }
                }
            }
        }


        private void addmorebtn(object sender, RoutedEventArgs e)
        {
            string data = this.ContactListGrid.SelectedItem.ToString().Trim('}', '{').Trim();
            var q = data.Split(',')
                .Select(x => x.Split('='))
                .Select(x => x[1].Trim());
            string u = "";
            foreach (var item in q)
            {
                u += (" " + item);
            }
            string[] q2 = u.Trim().Split(' ');
            string Im = q2[0];
            string Na = q2[1];

            AddMore add = new AddMore();
            add.Show();

        }
    }
}
