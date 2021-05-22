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

namespace _4PD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User loggedInUser = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            collapseGrids();
            gridRegister.Visibility = Visibility.Visible;
            clearTextBoxes();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            collapseGrids();
            gridLogin.Visibility = Visibility.Visible;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            loggedInUser = null;
            collapseGrids();
            btnLogout.Visibility = Visibility.Collapsed;
            btnLogin.Visibility = Visibility.Visible;
            btnRegister.Visibility = Visibility.Visible;
            clearTextBoxes();
        }

        private void btnLoginLogin_Click(object sender, RoutedEventArgs e)
        {
            collapseGrids();
            gridLoggedIn.Visibility = Visibility.Visible;
            btnLogin.Visibility = Visibility.Collapsed;
            btnRegister.Visibility = Visibility.Collapsed;
            btnLogout.Visibility = Visibility.Visible;
        }

        private void btnRegisterRegister_Click(object sender, RoutedEventArgs e)
        {
            collapseGrids();
        }

        private void btnAddPassword_Click(object sender, RoutedEventArgs e)
        {
            collapseGrids();
            gridAddPassword.Visibility = Visibility.Visible;
        }

        private void btnGeneratePassword_Click(object sender, RoutedEventArgs e)
        {
            tbxGenerated.Visibility = Visibility.Visible;
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            updatePasswordList(tbxSearch.Text);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            collapseGrids();
            gridLoggedIn.Visibility = Visibility.Visible;
            clearTextBoxes();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            collapseGrids();
            gridLoggedIn.Visibility = Visibility.Visible;
            clearTextBoxes();
        }

        //-------------------------------------------------------------------------------------

        private void collapseGrids()
        {
            gridLogin.Visibility = Visibility.Collapsed;
            gridRegister.Visibility = Visibility.Collapsed;
            gridAddPassword.Visibility = Visibility.Collapsed;
            gridLoggedIn.Visibility = Visibility.Collapsed;
        }

        private void clearTextBoxes()
        {
            tbxRegisterPassword.Text = "";
            tbxRegisterUsername.Text = "";
            tbxLoginPassword.Text = "";
            tbxLoginUsername.Text = "";
            tbxGenerated.Text = "";
            tbxName.Text = "";
            tbxPassword.Text = "";
            tbxURL.Text = "";
            tbxComment.Text = "";
            tbxSearch.Text = "";
            tbxGenerated.Visibility = Visibility.Collapsed;
        }

        //-------------------------------------------------------------------------------------

        private void updatePasswordList(string name)
        {

        }
    }
}
