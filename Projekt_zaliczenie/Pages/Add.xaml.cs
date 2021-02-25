using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Projekt_zaliczenie.Enum;
using System.Linq;
using System.Text.RegularExpressions;
using WpfProject;
using Projekt_zaliczenie.Classes;

namespace Projekt_zaliczenie.Pages
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : UserControl
    {
        

        
        public Add()
        {
            InitializeComponent();

            foreach (var item in Country.GetValues(typeof(Country)))
            {
                Country_box.Items.Add(item);
            }

        }
        /// <summary>
        /// Dodanie danych do bazy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_btn(object sender, RoutedEventArgs e)
        {
            using (OwnerEntities db = new OwnerEntities())
            {
                try
                {
                    var eMailValidator = new System.Net.Mail.MailAddress(Mail.Text); //Sprawdzenie Czy adres E-mail jest prawidłowy
                
                    int countryID = db.Countries.Where(x => x.Country.Contains(Country_box.Text)).First().ID;
                    string a = Full_name.Text.Split(' ')[1].ToString();
                    string b = Full_name.Text.Split(' ')[0].ToString();

                    var q = db.People.Where(x => x.fName == b).Where(x => x.lName == a).Where(x => x.PhoneBookID == db.PhoneBooks.Where(o => o.OwnerID == ActualOwner.ID).FirstOrDefault().ID);
                    if (q.Count() == 0)
                    {
                        People person = new People()
                        {
                            fName = this.Full_name.Text.Split(' ')[0],
                            lName = this.Full_name.Text.Split(' ')[1],
                            CountryID = countryID,
                            PhoneBookID = db.PhoneBooks.Where(x => x.OwnerID == db.Owners.Where(o => o.fName == ActualOwner.fName).Where(o => o.lName == ActualOwner.lName).FirstOrDefault().ID).First().ID
                        };
                        db.People.Add(person);
                        db.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Ta osoba istnieje. Dodano dodatkowy adres email i numer telefonu.");
                    }
                    
                    EmailAddresses email = new EmailAddresses()
                    {
                        Email = this.Mail.Text.ToString(),
                        PersonID = db.People.Where(x => x.fName == b).Where(x => x.lName == a).Where(x => x.PhoneBookID == db.PhoneBooks.Where(o => o.OwnerID == ActualOwner.ID).FirstOrDefault().ID).FirstOrDefault().ID                        
                    };

                    PhoneNumbers number = new PhoneNumbers()
                    {
                        Number = this.Phone.Text.ToString(),
                        PersonID = db.People.Where(x => x.fName == b).Where(x => x.lName == a).Where(x => x.PhoneBookID == db.PhoneBooks.Where(o => o.OwnerID == ActualOwner.ID).FirstOrDefault().ID).FirstOrDefault().ID
                    };

                    
                    
                    //db.PhoneBooks.Add(book);
                    db.EmailAddresses.Add(email);
                    db.PhoneNumbers.Add(number);
                    db.SaveChanges();

                    this.Full_name.Text = "";
                    this.Phone.Text = "";
                    this.Mail.Text = "";


                }
                catch (Exception)
                {
                    MessageBox.Show("Podany Email jest nieprawidłowy. Spróbuj ponownie");
                    Mail.Text = "";
                    Phone.Text = "";
                }

                
            }

            

            

        }
        /// <summary>
        /// Kod ograniczający ilość cyfr i format numeru w zaleźności od wybranego kraju
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            #region Formatowanie numeru ze względu na kraj
            switch (Country_box.Text)
            {
                case "Poland":
                    Phone.MaxLength = 15;
                    Phone.Text = Validate.Number(Phone.Text, Country_box.Text);
                    break;
                case "Germany":
                    Phone.MaxLength = 17;
                    Phone.Text = Validate.Number(Phone.Text, Country_box.Text);
                    break;
                case "Norway":
                    Phone.MaxLength = 14;
                    Phone.Text = Validate.Number(Phone.Text, Country_box.Text);
                    break;
                case "Czech":
                    Phone.MaxLength = 16;
                    Phone.Text = Validate.Number(Phone.Text, Country_box.Text);
                    break;
                case "":
                    Phone.MaxLength = 0;
                    Phone.Text = "";
                    break;
                default:
                    break;
            }
            #endregion
        }
        /// <summary>
        /// Czyszczenie pola na numer telefonu, po wybraniu innego kraju.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Phone.Text = "";
        }
        /// <summary>
        /// Przyjmowanie tylko liczb w TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Mail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        /// <summary>
        /// Tworzenie tylko jednej spacji pomiędzy wyrazami, proste i do zepsucia.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Imie_nazw_TextChanged(object sender, TextChangedEventArgs e)
        {
            Full_name.Text = Validate.Name(Full_name.Text);
        }
    }
}
