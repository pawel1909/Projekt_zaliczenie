﻿using Projekt_zaliczenie.Classes;
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
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : UserControl
    {
        public Start()
        {
            InitializeComponent();
            using (OwnerEntities db = new OwnerEntities())
            {
                Nazwa.Text = $"Witaj {ActualOwner.fName}"+ "\n" + " Aktualnie masz: " + db.People.Where(x => x.PhoneBookID == db.PhoneBooks.Where(o => o.OwnerID == ActualOwner.ID).FirstOrDefault().ID).Count().ToString() + " kontaktów";
            }
            
        }
    }
}
