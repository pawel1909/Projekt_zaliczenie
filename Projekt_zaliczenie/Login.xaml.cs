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
using System.Windows.Shapes;
using WpfProject;

namespace Projekt_zaliczenie
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            using (UsersEntities db = new UsersEntities())
            {
                
                var q = db.User.Where(x => x.Username == usernametxt.Text).Where(x => x.Password == passwordtxt.Password).Count();
                if (q == 1)
                {
                    MainWindow window = new MainWindow(db.User.Where(x => x.Username == usernametxt.Text).Select(x => x.fName).FirstOrDefault(), db.User.Where(x => x.Username == usernametxt.Text).Select(x => x.lName).FirstOrDefault());
                    
                    
                    window.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Nieprawidłowy login lub hasło, spróbuj ponownie");
                    usernametxt.Text = "";
                    passwordtxt.Password = "";
                }
            }
        }

        private void registerbtn_Click(object sender, RoutedEventArgs e)
        {
            Register reg = new Register();
            reg.Show();
            this.Close();
        }
    }
}
