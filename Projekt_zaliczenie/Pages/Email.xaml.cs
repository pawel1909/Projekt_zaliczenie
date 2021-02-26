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
    /// Interaction logic for Email.xaml
    /// </summary>
    public partial class Email : UserControl
    {
        public Email()
        {
            InitializeComponent();
            this.Sender.Text = ActualOwner.fName + " " + ActualOwner.lName;

            OwnerEntities db = new OwnerEntities();

            int pID = db.PhoneBooks.Where(x => x.OwnerID == ActualOwner.ID).FirstOrDefault().ID;

            var q = from p in db.People
                    where p.PhoneBookID == pID
                    select new
                    {
                        Name = p.fName + " " + p.lName
                    };
            Receiver.ItemsSource = q.ToList();
            


        }

        private void Sendbtn_Click(object sender, RoutedEventArgs e)
        {
            string combinedRec = $"{Receiver.Text}, {recMail.Text}";
            Saving.Mail_Save(Sender.Text, combinedRec, Theme.Text, Content.Text);
            this.Theme.Text = "";
            this.Content.Text = "";
        }


        private void changeEmail(object sender, EventArgs e)
        {
            OwnerEntities db = new OwnerEntities();

            if (Receiver.Text.Split().Length == 2)
            {
                string f = Receiver.Text.ToString().Split(' ')[0];
                string l = Receiver.Text.ToString().Split(' ')[1];

                var q1 = from em in db.EmailAddresses
                         where em.PersonID == db.People.Where(x => x.PhoneBookID == db.PhoneBooks.Where(o => o.OwnerID == ActualOwner.ID).FirstOrDefault().ID).Where(x => x.fName == f).Where(x => x.lName == l).FirstOrDefault().ID
                         select new
                         {
                             mail = em.Email
                         };


                recMail.ItemsSource = q1.ToList();
            }
        }
    }
}
