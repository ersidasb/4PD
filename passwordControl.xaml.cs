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
    /// Interaction logic for passwordControl.xaml
    /// </summary>
    public partial class passwordControl : UserControl
    {
        public Password password { get; set; }
        public User user { get; set; }
        private string decryptedPassword;
        int state = 0;
        Crypto crypto = new Crypto();
        Files files = new Files();
        public static Delegate update { get; set; }
        public passwordControl(Password password, User user)
        {
            this.password = password;
            this.user = user;
            InitializeComponent();
            decryptedPassword = crypto.DecryptPassword(password.pass, user.Password);
            tblName.Text = password.passName;
            tblURL.Text = password.passURL;
            tblComment.Text = password.comment;
            tblPassword.Text = password.pass;
            tbxPassword.Text = decryptedPassword;
        }

        private void btnShowHide_Click(object sender, RoutedEventArgs e)
        {
            if(state == 0)
            {
                state = 1;
                tblPassword.Visibility = Visibility.Collapsed;
                tbxPassword.Visibility = Visibility.Visible;
                btnEdit.IsEnabled = true;
            }
            else
            {
                state = 0;
                tblPassword.Visibility = Visibility.Visible;
                tbxPassword.Visibility = Visibility.Collapsed;
                btnEdit.IsEnabled = false;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            btnShowHide.Visibility = Visibility.Collapsed;
            btnEdit.Visibility = Visibility.Collapsed;
            btnCopy.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Visible;
            tbxPassword.IsReadOnly = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbxPassword.Text.Trim(' ') == "")
                    throw new Exception("Please enter a password");
                files.updatePassword(user, password.passName, tbxPassword.Text);
                btnShowHide.Visibility = Visibility.Visible;
                btnEdit.Visibility = Visibility.Visible;
                btnCopy.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Collapsed;
                tbxPassword.IsReadOnly = true;
                update.DynamicInvoke();
                MessageBox.Show("Password was successfully updated");
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(tbxPassword.Text);
        }

        private void tblURL_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(tblURL.Text);
            }
            catch(Exception exc)
            {
                MessageBox.Show("Not a valid URL");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            files.deletePassword(user.Username, password.passName);
            update.DynamicInvoke();
        }
    }
}
