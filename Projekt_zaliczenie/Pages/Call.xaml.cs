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
    /// Interaction logic for Call.xaml
    /// </summary>
    public partial class Call : UserControl
    {
        public Call()
        {
            InitializeComponent();
            Number.Text = "+48 123 123 123";
        }
    }
}
