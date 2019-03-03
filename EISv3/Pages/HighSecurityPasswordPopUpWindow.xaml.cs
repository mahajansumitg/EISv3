using EISv3.Utils;
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

namespace EISv3.Pages
{
    
    public partial class HighSecurityPasswordPopUpWindow : Window
    {
        public HighSecurityPasswordPopUpWindow()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(HighSecPswd.Password))
            {
                MessageBox.Show("Password should not empty.");
            }
            else
            {
                if (HighSecPswd.Password == "Sumit123") Mediator.PerformAction("CloseDialog");
                else MessageBox.Show("Incorrect high security password.");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Mediator.PerformAction("CloseDialog");
        }
    }
}
