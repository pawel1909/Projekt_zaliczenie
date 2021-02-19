using Projekt_zaliczenie.Classes;
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
    /// Interaction logic for ContactList.xaml
    /// </summary>
    public partial class ContactList : UserControl
    {
        public ContactList()
        {
            InitializeComponent();

            using(OwnerEntities db = new OwnerEntities())
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
    }
}
