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
using Projekt_zaliczenie;
using Projekt_zaliczenie.Classes;
using Projekt_zaliczenie.Enum;
using Projekt_zaliczenie.PagesModels;

namespace WpfProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow(string x, string y)
        {
            InitializeComponent();
            this.Current_Owner.Text = x + " " + y;
            ActualOwner.fName = x;
            ActualOwner.lName = y;



            using (OwnerEntities db = new OwnerEntities())
            {
                
                
                if (db.Countries.Count() == 0)
                {
                    foreach (var item in Country.GetValues(typeof(Country)))
                    {
                        Countries c = new Countries()
                        {
                            Country = item.ToString()
                        };
                        db.Countries.Add(c);
                    }
                    db.SaveChanges();
                } // wypełnienie bazy Krajami z enum

                if (db.PhoneBooks.Where(a => a.OwnerID == db.Owners.Where(o => o.fName == ActualOwner.fName).Where(o => o.lName == ActualOwner.lName).FirstOrDefault().ID).Count() == 0)
                {
                    PhoneBooks book = new PhoneBooks()
                    {
                        OwnerID = db.Owners.Where(o => o.fName == ActualOwner.fName).Where(o => o.lName == ActualOwner.lName).First().ID
                    };
                    db.PhoneBooks.Add(book);
                    db.SaveChanges();
                }
            }
        }

        private void dodaj_btn(object sender, RoutedEventArgs e)
        {
            DataContext = new Add_model();
        }

        private void call_btn(object sender, RoutedEventArgs e)
        {
            DataContext = new Call_model();
        }

        private void mail_btn(object sender, RoutedEventArgs e)
        {
            DataContext = new Email_model();
        }

        private void list_btn(object sender, RoutedEventArgs e)
        {
            DataContext = new ContactList_model();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Close();
        }
    }
}
