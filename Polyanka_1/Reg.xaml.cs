using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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

namespace Polyanka_1
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Window
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            Auth auto = new Auth();
            auto.Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PolyankaEntities db = new PolyankaEntities();
            string log = RegLog.Text;
            string salt = PasswordHelper.GenerateSalt();
            string pas = PasswordHelper.HashPassword(RegPas.Password, salt);

            Users users = new Users
            {
                login = RegLog.Text,
                password = pas,
                salt = salt
            };

            try
            {
                db.Users.Add(users);
                db.SaveChanges();
                MessageBox.Show("Вы зарегестрированы");
                Auth auth = new Auth();
                auth.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("фрик ты");
            }

        }

       
        /// <summary> 
        /// Метод RetSalt который возвращает массив байтов, представляющий "соль" 
        /// </summary> 
        /// <returns></returns> 
        private byte[] RetSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            return salt;
        }
        /// <summary> 
        /// Метод GenerateHash принимает два аргумента: пароль (pas) и соль (salt). Он создает хэш для пароля с учетом соли,  
        /// что делает хэш уникальным даже при одинаковом пароле, но разной соли.  
        /// </summary> 
        /// <param name="pas"></param> 
        /// <param name="salt"></param> 
        /// <returns></returns> 
        private byte[] GenerateHash(string pas, byte[] salt)
        {
            var pfc = new Rfc2898DeriveBytes(pas, salt, 10000);
            byte[] hash = pfc.GetBytes(20);
            return hash;
        }

        private void GoToAuth(object sender, RoutedEventArgs e)
        {
            Auth auth = new Auth();
            auth.Show();
            Close();
        }
    }
}
