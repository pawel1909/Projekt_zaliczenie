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
using System.Windows.Shapes;

namespace Projekt_zaliczenie
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void registerbtn_Click(object sender, RoutedEventArgs e)
        {
            using (UsersEntities db = new UsersEntities())
            {
                OwnerEntities owner = new OwnerEntities();

                if (db.User.Where(x => x.Username == usernametxt.Text).Count() == 1)
                {
                    MessageBox.Show("Użytkownik o tej nazwie już istnieje. Spróbuj innej");
                    usernametxt.Text = "";
                    passwordtxt.Password = "";
                }
                else if (owner.Owners.Where(x => x.fName == fNametxt.Text).Where(x => x.lName == lnametxt.Text).Count() == 1)
                {
                    MessageBoxResult result = MessageBox.Show( "Chcesz się zalogować?","Ta osoba posiada już konto",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question,
                        MessageBoxResult.No);
                    if (result == MessageBoxResult.Yes)
                    {
                        Login log = new Login();
                        log.Show();
                        this.Close();
                    }
                    else
                    {
                        fNametxt.Text = "";
                        lnametxt.Text = "";
                    }
                }
                else if (usernametxt.Text == "" || fNametxt.Text == "" || lnametxt.Text == "")
                {
                    MessageBox.Show("Nazwa użytkownika, imię i nazwisko nie mogą być puste");
                }
                else
                {
                    User user = new User()
                    {
                        Username = usernametxt.Text,
                        Password = passwordtxt.Password,
                        fName = fNametxt.Text,
                        lName = lnametxt.Text
                    };

                    Owners ow = new Owners()
                    {
                        fName = fNametxt.Text,
                        lName = lnametxt.Text
                    };
                    db.User.Add(user);
                    owner.Owners.Add(ow);
                    db.SaveChanges();
                    owner.SaveChanges();
                    Login log = new Login();
                    log.Show();
                    this.Close();
                }
            }
        }
    }
}
