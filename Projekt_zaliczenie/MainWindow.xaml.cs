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
using Projekt_zaliczenie.Enum;
using Projekt_zaliczenie.PagesModels;

namespace WpfProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            DataContext = new Start_model();
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
                }
            }
        }

        private void dodaj_btn(object sender, RoutedEventArgs e)
        {
            DataContext = new Add_model();

            MessageBoxResult result = MessageBox.Show(DataContext.ToString());
        }

        private void call_btn(object sender, RoutedEventArgs e)
        {
            DataContext = new Call_model();
            MessageBoxResult result = MessageBox.Show(DataContext.ToString());
        }

        private void mail_btn(object sender, RoutedEventArgs e)
        {
            DataContext = new Email_model();
            MessageBoxResult result = MessageBox.Show(DataContext.ToString());
        }

        private void list_btn(object sender, RoutedEventArgs e)
        {
            DataContext = new Test_model();
            MessageBoxResult result = MessageBox.Show(DataContext.ToString());
        }
    }
}
