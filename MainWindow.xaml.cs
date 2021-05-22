using System;
using System.Collections.Generic;
using System.IO;
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
        delegate void updateDelegate();
        updateDelegate update;

        private Files files = new Files();
        private User loggedInUser = null;
        private string mainFilePath = @"data\users.txt";

        public MainWindow()
        {
            this.Closing += Window_Closing;
            InitializeComponent();
            if (!File.Exists(mainFilePath))
            {
                File.Create(mainFilePath);
            }
            update = updatePasswordList;
            passwordControl.update = update;
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
            files.logOut(loggedInUser);
            loggedInUser = null;
            collapseGrids();
            btnLogout.Visibility = Visibility.Collapsed;
            btnLogin.Visibility = Visibility.Visible;
            btnRegister.Visibility = Visibility.Visible;
            clearTextBoxes();
        }

        private void btnLoginLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbxLoginUsername.Text.Trim(' ') == "" || tbxLoginPassword.Text.Trim(' ') == "")
                    throw new Exception("Please fill in required fields");
                loggedInUser = files.logIn(tbxLoginUsername.Text, tbxLoginPassword.Text);
                if (loggedInUser == null)
                    throw new Exception("Wrong username or password");
                collapseGrids();
                gridLoggedIn.Visibility = Visibility.Visible;
                btnLogin.Visibility = Visibility.Collapsed;
                btnRegister.Visibility = Visibility.Collapsed;
                btnLogout.Visibility = Visibility.Visible;
                updatePasswordList();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnRegisterRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbxRegisterUsername.Text.Trim(' ') == "" || tbxRegisterPassword.Text.Trim(' ') == "")
                    throw new Exception("Please fill in required fields");
                if (files.userExists(tbxRegisterUsername.Text) || tbxRegisterUsername.Text == "users")
                    throw new Exception("Username is taken");
                files.register(tbxRegisterUsername.Text, tbxRegisterPassword.Text);
                collapseGrids();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnAddPassword_Click(object sender, RoutedEventArgs e)
        {
            collapseGrids();
            gridAddPassword.Visibility = Visibility.Visible;
        }

        private void btnGeneratePassword_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            int passLength = random.Next(10, 20);
            string generated = "";
            int randomNumber;
            for (int i = 0; i <= passLength; i++)
            {
                randomNumber = 60;
                while(randomNumber > 57 && randomNumber <65 || randomNumber > 90 && randomNumber < 97)
                {
                    randomNumber = random.Next(48, 122);
                }
                generated += (char)randomNumber;
            }
            tbxGenerated.Text = generated;
            tbxGenerated.Visibility = Visibility.Visible;
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            updatePasswordList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbxName.Text.Trim(' ') == "" || tbxPassword.Text.Trim(' ') == "" || tbxURL.Text.Trim(' ') == "" || tbxComment.Text.Trim(' ') == "")
                    throw new Exception("Please fill required fields");
                if (files.passwordNameExists(tbxName.Text, loggedInUser.Username))
                    throw new Exception("Password name already exists");
                files.addPassword(loggedInUser, tbxName.Text, tbxPassword.Text, tbxURL.Text, tbxComment.Text);
                updatePasswordList();
                collapseGrids();
                gridLoggedIn.Visibility = Visibility.Visible;
                clearTextBoxes();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            collapseGrids();
            clearTextBoxes();
            gridLoggedIn.Visibility = Visibility.Visible;
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

        private void updatePasswordList()
        {
            pnlPasswords.Children.Clear();
            List<Password> passwords =  files.getAllPasswords(loggedInUser.Username);
            foreach(Password p in passwords)
            {
                if(tbxSearch.Text != "")
                {
                    if(p.passName.Contains(tbxSearch.Text))
                    {
                        passwordControl control = new passwordControl(p, loggedInUser);
                        pnlPasswords.Children.Add(control);
                    }
                }
                else
                {
                    passwordControl control = new passwordControl(p, loggedInUser);
                    pnlPasswords.Children.Add(control);
                }
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (loggedInUser != null)
                files.logOut(loggedInUser);
        }
    }
}
