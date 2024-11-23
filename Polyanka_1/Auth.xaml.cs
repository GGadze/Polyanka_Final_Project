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
using System.Windows.Shapes;

namespace Polyanka_1
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        PolyankaEntities db;
        private int failedLoginAttempts = 0;
        private string captchaText;
        public Auth()
        {
            InitializeComponent();
            db = new PolyankaEntities();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string log = LogAuth.Text;
            string pas = PasAuth.Password;
            try
            {
                var user = db.Users.Where(d => (d.login == log)).FirstOrDefault();
                if (user != null)
                {
                    // Сброс попыток при успешном входе
                    string salt = user.salt;
                    string hash = user.password;
                    bool actual = PasswordHelper.VerifyPassword(pas, hash, salt);
                    if (actual)
                    {
                        failedLoginAttempts = 0;
                        MessageBox.Show("И снова здраствуй!");
                        MainWindow main = new MainWindow();
                        main.Show();
                        Close();
                    }
                    
                    else
                    {
                        failedLoginAttempts++;
                        MessageBox.Show("Неверный логин или пароль.");
                        if (failedLoginAttempts >= 3)
                        {
                            Auth auth = new Auth();
                            using (var captchaImageStream = CaptchaGen.GenerateCaptchaImage(out captchaText))
                            {
                                ShowCaptchaWindow(captchaImageStream);
                            }
                        }                   
                    }
                }
                else MessageBox.Show("Неверные данные, ты фрик!");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void ShowCaptchaWindow(MemoryStream captchaImageStream)
        {
            CaptchaWindow captchaWindow = new CaptchaWindow(captchaImageStream, captchaText);
            bool? result = captchaWindow.ShowDialog();

            if (result == true)
            {
                // Пользователь прошел капчу, можно продолжить обработку входа
                failedLoginAttempts = 0; // Сбрасываем счетчик попыток
            }
        }

        private void GoToReg(object sender, RoutedEventArgs e)
        {
            Reg reg = new Reg();
            reg.Show();
            Close();
        }
    }
}
