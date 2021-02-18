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
                    var eMailValidator = new System.Net.Mail.MailAddress(Mail.Text);

                    var q = db.Owners.Where(x => x.fName.Contains("Pawel"));
                    if (q.Count() == 0)
                    {
                        Owners pawel = new Owners()
                        {
                            fName = "Pawel",
                            lName = "Papiernik"
                        };
                        db.Owners.Add(pawel);
                    }
                    db.SaveChanges();


                    if (db.PhoneBooks.Count() == 0)
                    {
                        PhoneBooks book = new PhoneBooks()
                        {
                            OwnerID = db.Owners.Where(x => x.fName == "Pawel").First().ID
                        };
                        db.PhoneBooks.Add(book);
                    }


                    
                    db.SaveChanges();

                    int countryID = db.Countries.Where(x => x.Country.Contains(Country_box.Text)).FirstOrDefault().ID;


                    People person = new People()
                    {
                        fName = this.Full_name.Text.Split(' ')[0],
                        lName = this.Full_name.Text.Split(' ')[1],
                        CountryID = countryID,
                        PhoneBookID = db.PhoneBooks.Where(x => x.OwnerID == db.Owners.Where(o => o.fName == "Pawel").FirstOrDefault().ID).First().ID
                    };
                    db.People.Add(person);
                    db.SaveChanges();
                    string a = Full_name.Text.Split(' ')[1].ToString();
                    string b = Full_name.Text.Split(' ')[0].ToString();
                    EmailAddresses email = new EmailAddresses()
                    {
                        Email = this.Mail.Text.ToString(),
                        PersonID = db.People.Where(x => x.lName.Contains(a)).Where(x => x.fName.Contains(b)).FirstOrDefault().ID                        
                    };

                    PhoneNumbers number = new PhoneNumbers()
                    {
                        Number = this.Phone.Text.ToString(),
                        PersonID = db.People.Where(x => x.lName.Contains(a)).Where(x => x.fName.Contains(b)).FirstOrDefault().ID
                    };

                    
                    
                    //db.PhoneBooks.Add(book);
                    db.EmailAddresses.Add(email);
                    db.PhoneNumbers.Add(number);
                    db.SaveChanges();


                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Podany Email jest nieprawidłowy. Spróbuj ponownie");
                    Mail.Text = "";
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
            string[] split = Phone.Text.Split(new char[] { '-', '(', ')' });
            StringBuilder sb = new StringBuilder();
            if (Country_box.Text == "")
            {
                Phone.Text = "";
            }
            if (Country_box.Text == "")
            {
                Phone.MaxLength = 0;
            }
            if (Country_box.Text == "Poland")
            {
                Phone.MaxLength = 15;
                if (Phone.Text.ToString().Length == 9)
                {
                    foreach (var item in split)
                    {
                        if (item.Trim() != "")
                        {
                            sb.Append(item);
                        }
                    }
                    this.Phone.Text = String.Format("+48 {0:000 000 000}", double.Parse(sb.ToString()));
                }
            }

            if (Country_box.Text == "Germany")
            {
                Phone.MaxLength = 17;
                if (Phone.Text.ToString().Length == 11)
                {
                    foreach (var item in split)
                    {
                        if (item.Trim() != "")
                        {
                            sb.Append(item);
                        }
                    }
                    this.Phone.Text = String.Format("+49 {0:000 000 00000}", double.Parse(sb.ToString()));
                }
            }

            if (Country_box.Text == "Norway")
            {
                Phone.MaxLength = 14;
                if (Phone.Text.ToString().Length == 9)
                {
                    foreach (var item in split)
                    {
                        if (item.Trim() != "")
                        {
                            sb.Append(item);
                        }
                    }
                    this.Phone.Text = String.Format("+47 {0:00 00 00 00}", double.Parse(sb.ToString()));
                }
            }

            if (Country_box.Text == "Czech")
            {
                Phone.MaxLength = 16;
                if (Phone.Text.ToString().Length == 9)
                {
                    foreach (var item in split)
                    {
                        if (item.Trim() != "")
                        {
                            sb.Append(item);
                        }
                    }
                    this.Phone.Text = String.Format("+420 {0:000 000 000}", double.Parse(sb.ToString()));
                }
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
            if (Full_name.Text.Length > 2)
            {
                if (Full_name.Text[Full_name.Text.Length - 1].ToString() == " " && Full_name.Text[Full_name.Text.Length - 2].ToString() == " ")
                {
                    string[] spl = Full_name.Text.Split(' ');
                    string temp = "";

                    for (int i = 0; i < spl.Length; i++)
                    {
                        temp += spl[i];
                    }

                    Full_name.Text = temp;
                }
            }
        }
    }
}
