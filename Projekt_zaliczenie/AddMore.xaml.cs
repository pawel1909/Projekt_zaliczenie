using Projekt_zaliczenie.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekt_zaliczenie
{
    /// <summary>
    /// Interaction logic for AddMore.xaml
    /// </summary>
    public partial class AddMore : Window
    {
        public AddMore()
        {
            InitializeComponent();
            OwnerEntities db = new OwnerEntities();

            int pID = db.PhoneBooks.Where(x => x.OwnerID == ActualOwner.ID).FirstOrDefault().ID;

            var q = from p in db.People
                    where p.PhoneBookID == pID
                    select new
                    {
                        Name = p.fName + " " + p.lName
                    };

            var q2 = q.ToList();
            //usernamecombo.ItemsSource = q.ToList();
            usernamecombo.ItemsSource = q2;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            OwnerEntities db = new OwnerEntities();

            string a = usernamecombo.Text.Split(' ')[1].ToString();
            string b = usernamecombo.Text.Split(' ')[0].ToString();

            int tempID = db.People.Where(x => x.fName == b).Where(x => x.lName == a).FirstOrDefault().ID;
            try
            {
                var eMailValidator = new System.Net.Mail.MailAddress(emailtxt.Text);
                EmailAddresses email = new EmailAddresses()
                {
                    Email = this.emailtxt.Text.ToString(),
                    PersonID = tempID
                };
            }
            catch (Exception)
            {

                MessageBox.Show("Podany Email jest nieprawidłowy. Spróbuj ponownie");
                emailtxt.Text = "";
            }

            PhoneNumbers number = new PhoneNumbers()
            {
                Number = this.numbertxt.Text.ToString(),
                PersonID = tempID
            };
            emailtxt.Text = "";
            numbertxt.Text = "";

        }

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void numbertxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void numbertxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            OwnerEntities db = new OwnerEntities();

            try
            {
                string lastName = usernamecombo.Text.Split(' ')[1].ToString();
                string firstName = usernamecombo.Text.Split(' ')[0].ToString();
                string country = db.Countries.Where(x => x.ID == db.People.Where(o => o.fName == firstName).Where(o => o.lName == lastName).FirstOrDefault().CountryID).First().Country;
                switch (country)
                {
                    case "Poland":
                        numbertxt.MaxLength = 15;
                        numbertxt.Text = Validate.Number(numbertxt.Text, country);
                        break;
                    case "Germany":
                        numbertxt.MaxLength = 17;
                        numbertxt.Text = Validate.Number(numbertxt.Text, country);
                        break;
                    case "Norway":
                        numbertxt.MaxLength = 14;
                        numbertxt.Text = Validate.Number(numbertxt.Text, country);
                        break;
                    case "Czech":
                        numbertxt.MaxLength = 16;
                        numbertxt.Text = Validate.Number(numbertxt.Text, country);
                        break;
                    case "":
                        numbertxt.MaxLength = 0;
                        numbertxt.Text = "";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Wybierz osobę.");
            }
        }
    }
}
