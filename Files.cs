using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace _4PD
{
    class Files
    {
        string mainFilePath = @"data\users.txt";
        Crypto crypto = new Crypto();
        public User logIn(string username, string password)
        {
            try
            {
                foreach (User u in getAllUsers())
                {
                    if(u.Username == username)
                    {
                        if(u.Password == crypto.CreateHash(password))
                        {
                            crypto.initialize(u.Password);
                            crypto.FileDecrypt(@"data\" + u.Username + ".txt");
                            return u;
                        }
                    }
                }
                return null;
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
                return null;
            }
        }



        public void logOut(User user)
        {
            crypto.initialize(user.Password);
            crypto.FileEncrypt(@"data\" + user.Username + ".txt");
        }

        public void register(string username, string password)
        {
            try
            {
                File.AppendAllText(mainFilePath, username + "," + crypto.CreateHash(password) + Environment.NewLine);
                var file = File.Create(@"data\" + username + ".txt");
                file.Close();
                crypto.initialize(crypto.CreateHash(password));
                crypto.FileEncrypt(@"data\" + username + ".txt");
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        //----------------------------------------------------------

        private List<User> getAllUsers()
        {
            return File.ReadAllLines(mainFilePath)
                .Select(v => userFromCsv(v)).ToList();
        }

        public List<Password> getAllPasswords(string username)
        {
            return File.ReadAllLines(@"data\" + username + ".txt")
                .Select(v => passwordFromCsv(v)).ToList();
        }

        //----------------------------------------------------------

        private Password passwordFromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Password password = new Password(values[0], values[1], values[2], values[3]);
            return password;
        }

        private User userFromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            User user = new User(values[0], values[1]);
            return user;
        }

        //----------------------------------------------------------

        public bool userExists(string username)
        {
            foreach(User u in getAllUsers())
            {
                if (u.Username == username)
                    return true;
            }
            return false;
        }
        
        public bool passwordNameExists(string name, string username)
        {
            foreach (Password p in getAllPasswords(username))
            {
                if (p.passName == name)
                    return true;
            }
            return false;
        }

        //----------------------------------------------------------

        public void addPassword(User user, string name, string password, string URL, string comment)
        {
            try
            {
                File.AppendAllText(@"data\" + user.Username + ".txt", name + "," + crypto.EncryptPassword(password, user.Password) + "," + URL + "," + comment + Environment.NewLine);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public void updatePassword(User user, string name, string newPassword)
        {
            try
            {
                List<Password> passwords = getAllPasswords(user.Username);
                var file = File.Create(@"data\" + user.Username + ".txt");
                file.Close();
                foreach (Password p in passwords)
                {
                    if (p.passName == name)
                        p.pass = crypto.EncryptPassword(newPassword, user.Password);
                    File.AppendAllText(@"data\" + user.Username + ".txt", p.passName + "," + p.pass + "," + p.passURL + "," + p.comment + Environment.NewLine);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("update password error: " + exc.Message);
            }
        }
    }
}
