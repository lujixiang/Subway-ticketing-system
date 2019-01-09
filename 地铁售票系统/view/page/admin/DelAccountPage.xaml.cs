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
using 地铁售票系统.db;

namespace 地铁售票系统.view.page.admin
{
    /// <summary>
    /// DelAccountPage.xaml 的交互逻辑
    /// </summary>
    public partial class DelAccountPage : Page
    {
        //用户名输入是否正确
        bool isUserName;
        SolidColorBrush scRight = new SolidColorBrush();
        SolidColorBrush scError = new SolidColorBrush();
        SolidColorBrush scNormal = new SolidColorBrush();
        SolidColorBrush scUnEnable = new SolidColorBrush();
        SolidColorBrush scEnable = new SolidColorBrush();
        public DelAccountPage()
        {
            InitializeComponent();
            scRight.Color = System.Windows.Media.Color.FromArgb(255, 5, 211, 0);
            scNormal.Color = System.Windows.Media.Color.FromArgb(255, 171, 171, 171);
            scError.Color = System.Windows.Media.Color.FromArgb(255, 234, 32, 0);
            scEnable.Color = System.Windows.Media.Color.FromArgb(255, 105, 185, 70);
            scUnEnable.Color = System.Windows.Media.Color.FromArgb(255, 190, 204, 183);
        }

        private void userName_TextChanged(object sender, TextChangedEventArgs e)
        {
            imgIsSuccess.Visibility = System.Windows.Visibility.Hidden;
            imgIsSuccess.ToolTip = "";
            string userNameTmp = userName.Text.ToString();
            string userPsdTem = userPassword.Password.ToString();

            userPassword.BorderBrush = scNormal;
            if (userPsdTem != null && userNameTmp != null)
            {
                if (CheckUser.checkUserIsLegal(userNameTmp))
                {
                    //正确
                    userName.BorderBrush = scRight;
                    delete.Background = scEnable;
                    balance.Content = CheckUser.getBalance(userNameTmp);
                    isUserName = true;
                }
                else
                {
                    //错误
                    delete.Background = scUnEnable;
                    userName.BorderBrush = scError;
                    //余额初始化
                    balance.Content = "0";
                    isUserName = false;
                }
            }
        }
        private void delete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            imgIsSuccess.Visibility = System.Windows.Visibility.Visible;
            //用户名是否正确
            if (isUserName)
            {

                string userNameTmp = userName.Text.ToString();

                string userPsdTmp = userPassword.Password.ToString();
                //密码是否为空
                if (!userPsdTmp.Equals(""))
                {
                    //密码是否正确
                    if (CheckUser.checkUserIsLegal(userNameTmp, userPsdTmp))
                    {
                        if (CheckUser.getBalance(userNameTmp) == 0)
                        {

                            bool isOk = CheckUser.deleteUser(userNameTmp, userPsdTmp);
                            if (isOk)
                            {
                                imgIsSuccess.Source = new BitmapImage(new Uri("/images/成功.png", UriKind.Relative));
                                imgIsSuccess.ToolTip = "成功";
                                balance.Content = "0";
                                //用为名状态为错误
                                isUserName = false;
                                //清空密码框
                                userPassword.Password = "";
                                userName.BorderBrush = scNormal;
                                userPassword.BorderBrush = scNormal;
                            }
                            else
                            {
                                imgIsSuccess.ToolTip = "删除账户失败，请联系技术人员";
                                imgIsSuccess.Source = new BitmapImage(new Uri("/images/失败.png", UriKind.Relative));
                            }
             
                        }
                        else
                        {
                            imgIsSuccess.ToolTip = "账户余额不为0";
                            imgIsSuccess.Source = new BitmapImage(new Uri("/images/失败.png", UriKind.Relative));

                        }
                    }
                    else
                    {
                        userPassword.BorderBrush = scError;
                        imgIsSuccess.Source = new BitmapImage(new Uri("/images/失败.png", UriKind.Relative));
                        imgIsSuccess.ToolTip = "密码错误";
                    }
                }
                else
                {
                    imgIsSuccess.Source = new BitmapImage(new Uri("/images/失败.png", UriKind.Relative));
                    imgIsSuccess.ToolTip = "密码不能为空";
                }
            }
            else
            {
                imgIsSuccess.Visibility = System.Windows.Visibility.Hidden;
                imgIsSuccess.ToolTip = "不存在该用户名";
            }
        }

        private void userPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            userPassword.BorderBrush = scNormal;
            //imgIsSuccess.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
