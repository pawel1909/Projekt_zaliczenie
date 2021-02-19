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
        public Owners owner = new Owners();
        public MainWindow()
        {

            InitializeComponent();
            DataContext = new Start_model();

            using (OwnerEntities db = new OwnerEntities())
            {
                this.Current_Owner.Text = $"{db.Owners.Where(o => o.ID == 1).FirstOrDefault().fName} {db.Owners.Where(o => o.ID == 1).FirstOrDefault().lName}";
                //var ow = db.Owners.Where(x => x.fName == "Pawel").FirstOrDefault();
                //ActualOwner owner = new ActualOwner(ow.ID, ow.fName, ow.lName);
                //MessageBox.Show(owner.ID.ToString());
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
            DataContext = new Test_model();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
