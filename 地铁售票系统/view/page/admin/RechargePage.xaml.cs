using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// RechargePage.xaml 的交互逻辑
    /// </summary>
    public partial class RechargePage : Page
    {
        SolidColorBrush scRight = new SolidColorBrush();
        SolidColorBrush scError = new SolidColorBrush();
        SolidColorBrush scNormal = new SolidColorBrush();
        SolidColorBrush scUnEnable = new SolidColorBrush();
        SolidColorBrush scEnable = new SolidColorBrush();

        bool isUserName = false;
        public RechargePage()
        {
            InitializeComponent();
            scRight.Color = System.Windows.Media.Color.FromArgb(255, 5, 211, 0);
            scNormal.Color = System.Windows.Media.Color.FromArgb(255, 171, 171, 171);
            scError.Color = System.Windows.Media.Color.FromArgb(255, 234, 32, 0);
            scEnable.Color = System.Windows.Media.Color.FromArgb(255, 105, 185, 70);
            scUnEnable.Color = System.Windows.Media.Color.FromArgb(255, 190, 204, 183);
        }
        /// <summary>
        /// 充值数目  TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void tb_GotFocus(object sender, RoutedEventArgs e)
        {
            string userNameTmp = userName.Text.ToString();
            string userPsdTem = userPassword.Password.ToString();
            //用户名是否正确
            if (isUserName)
            {
                //密码是否正确
                if (CheckUser.checkUserIsLegal(userNameTmp, userPsdTem))
                {
                    userPassword.BorderBrush = scRight;
                }
                else {
                    userPassword.BorderBrush = scError;
                }
            }
            else
            {
                userName.BorderBrush = scError;
            }
        }



        /// <summary>
        /// 充值按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void recharge_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            imgIsSuccess.Focus();
            //用户名是否正确
            if (isUserName)
            {

                //充值是否成功图片可见
                imgIsSuccess.Visibility = System.Windows.Visibility.Visible;
                string userNameTmp = userName.Text.ToString();
                string userPsdTem = userPassword.Password.ToString();
                decimal moneySum;
                decimal.TryParse(rechargeSum.Text.ToString(), out moneySum);
                //密码是否为空
                if (isUserName && userNameTmp != null)
                {
                    //密码是否正确
                    if (CheckUser.checkUserIsLegal(userNameTmp, userPsdTem))
                    {

                        if (!rechargeSum.Text.ToString().Equals("") && Regex.IsMatch(rechargeSum.Text.ToString(), @"^[0-9]*$"))
                        {
                            if (CheckUser.rechargeToCo(moneySum))
                            {
                                CheckUser.rechargeMoney(userNameTmp, moneySum);
                                balance.Content = CheckUser.getBalance(userNameTmp);
                                userPassword.BorderBrush = scRight;
                                rechargeSum.BorderBrush = scRight;
                                //显示充值成功图片
                                imgIsSuccess.Source = new BitmapImage(new Uri("/images/成功.png", UriKind.Relative));
                                imgIsSuccess.ToolTip = "充值成功";
                            }
                        }
                        else {
                            rechargeSum.BorderBrush = scError;
                            imgIsSuccess.Source = new BitmapImage(new Uri("/images/失败.png", UriKind.Relative));
                            imgIsSuccess.ToolTip = "金额输入错误";
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
                    userPassword.BorderBrush = scError;
                    imgIsSuccess.Source = new BitmapImage(new Uri("/images/失败.png",UriKind.Relative));
                    imgIsSuccess.ToolTip = "密码错误";
                }
            }
        }
        /// <summary>
        /// 账号输入  TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userName_TextChanged(object sender, TextChangedEventArgs e)
        {

            string userNameTmp = userName.Text.ToString();
            string userPsdTem = userPassword.Password.ToString();
            decimal moneySum;
            decimal.TryParse(rechargeSum.Text.ToString(), out moneySum);
            imgIsSuccess.Visibility = System.Windows.Visibility.Hidden;
            userPassword.BorderBrush = scNormal;
            if (userPsdTem != null && userNameTmp != null)
            {
                if (CheckUser.checkUserIsLegal(userNameTmp))
                {
                    //正确
                    userName.BorderBrush = scRight;
                    isUserName = true;
                    recharge.Background = scEnable;
                    balance.Content = CheckUser.getBalance(userNameTmp);
                }
                else
                {
                    //错误
                    recharge.Background = scUnEnable;
                    userName.BorderBrush = scError;
                    isUserName = false;
                    //余额初始化
                    balance.Content = "0";
                }
            }
        }
        /// <summary>
        /// Money  TextBox 监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rechargeSum_TextChanged(object sender, TextChangedEventArgs e) {
            if (rechargeSum.Text.ToString().Equals(""))
            {
                rechargeSum.BorderBrush = scError;
            }
            else {
                rechargeSum.BorderBrush = scRight;
            }
        }
    }
}
