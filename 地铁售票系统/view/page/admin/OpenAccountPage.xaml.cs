using System;
using System.Collections.Generic;
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
using 地铁售票系统.utils;

namespace 地铁售票系统.view.page.admin
{
    /// <summary>
    /// OpenAccountPage.xaml 的交互逻辑
    /// </summary>
    public partial class OpenAccountPage : Page
    {
        SolidColorBrush scRight = new SolidColorBrush();
        SolidColorBrush scError = new SolidColorBrush();
        SolidColorBrush scNormal = new SolidColorBrush();
        SolidColorBrush scUnEnable = new SolidColorBrush();
        SolidColorBrush scEnable = new SolidColorBrush();
        public OpenAccountPage()
        {
            InitializeComponent();
            scRight.Color = System.Windows.Media.Color.FromArgb(255, 5, 211, 0);
            scNormal.Color = System.Windows.Media.Color.FromArgb(255, 171, 171, 171);
            scError.Color = System.Windows.Media.Color.FromArgb(255, 234, 32, 0);
            scEnable.Color = System.Windows.Media.Color.FromArgb(255, 105, 185, 70);
            scUnEnable.Color = System.Windows.Media.Color.FromArgb(255, 190, 204, 183);
        }

        /// <summary>
        /// 开通账户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openImmediate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            sex_nan.Focus();
            try
            {
                string user_name = userName.Text;
                string user_passwrod = MD5.generateMD5(userPassword.Password);
                string user_realName = realName.Text.Replace(" ", "");
                string user_sex = "";
                if (sex_nan.IsChecked == true)
                {
                    user_sex = "男";
                }
                else if(sex_nv.IsChecked == true)
                {
                    user_sex = "女";
                }
                // DateTime user_birthday = DateTime.Parse(birthday.Text);
                string user_birthday = birthday.Text;
                string user_idCard = idCard.Text;
                string user_address = address.Text.Replace(" ", "");
                string user_phone = phoneNumber.Text;
                decimal user_money = 0;
                decimal.TryParse(money.Text, out user_money);
                UserInfoes user = new UserInfoes()
                {
                    UserName = user_name,
                    Password = user_passwrod,
                    Name = user_realName,
                    Sex = user_sex,
                    Birthday = user_birthday,
                    OpenTime = DateTime.Now,
                    IdCard = user_idCard,
                    Address = user_address,
                    PhoneNumber = user_phone,
                    MoneySum = user_money
                };
                if (infoIsOk(user))
                {
                    using (AppDbEntities context = new AppDbEntities())
                    {
                        try
                        {
                            context.UserInfoes.Add(user);
                            if (CheckUser.rechargeToCo(user.MoneySum))
                            {
                                context.SaveChanges();
                                MessageBox.Show("开户成功");
                                setStateNormal();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("开户失败:" + ee.Message);
            }
        }
        /// <summary>
        /// 检测填写的信息是否正确
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool infoIsOk(UserInfoes user)
        {
            setStateRight();
            bool isRight = true;
            //用户名
            if (user.UserName.Length < 6 || user.UserName.Length > 9)
            {
                userName.BorderBrush = scError;
                // MessageBox.Show("用户名长度不符合要求");
                isRight = false;
            }
            if (!Regex.IsMatch(user.UserName, @"^[A-Za-z]{1}[A-Za-z0-9]+$"))
            {
                userName.BorderBrush = scError;
                // MessageBox.Show("用户名必须以字母开头");
                isRight = false;
            }
            //用户名是否存在
            if (CheckUser.checkUserIsLegal(user.UserName))
            {
                userName.BorderBrush = scError;
                MessageBox.Show("该用户已存在");
                isRight = false;
            }
            //密码长度
            if (!(userPassword.Password.Length >= 6 && userPassword.Password.Length <= 15))
            {
                userPassword.BorderBrush = scError;
                userPasswordRepeat.BorderBrush = scError;
                isRight = false;
            }
            //密码是否由数字和字母构成
            else if (!Regex.IsMatch(userPassword.Password, @"^[A-Za-z0-9]+$"))
            {
                userPassword.BorderBrush = scError;
                userPasswordRepeat.BorderBrush = scError;
                isRight = false;
            }
            else//是否与重复密码相等
                if (!userPassword.Password.Equals(userPasswordRepeat.Password.ToString()))
                {
                    userPasswordRepeat.BorderBrush = scError;
                    isRight = false;
                }
            //名字
            if (user.Name.Equals(""))
            {
                realName.BorderBrush = scError;
                isRight = false;
            }
            //性别
            if (user.Sex.Equals(""))
            {
                imgIsSuccess.Visibility = System.Windows.Visibility.Visible;
                isRight = false;
            }
            else
            {
                imgIsSuccess.Visibility = System.Windows.Visibility.Hidden;
            }
            //出生年月
            if (!Regex.IsMatch(user.Birthday, "^[1-9][0-9]{3}/([1-9]|1[0-2])/([1-9]|[1-2][1-9]|3[0-1])$"))
            {
                birthday.BorderBrush = scError;
                isRight = false;
            }
            //身份证
            if (user.IdCard.Length != 18)
            {
                // MessageBox.Show("身份证号位数不够");
                    idCard.BorderBrush = scError;
                    isRight = false;
            }
            //身份证出现非数字字符
            else if (!Regex.IsMatch(user.IdCard,"^[1-9]\\d{5}[1-9]\\d{3}((0[1-9])||(1[0-2]))((0[1-9])||(1\\d)||(2\\d)||(3[0-1]))\\d{3}([0-9]||X)$"))
            {
                idCard.BorderBrush = scError;
                //  MessageBox.Show("身份证号只能为数字");
                isRight = false;
            }
            //地址
            if (user.Address.Equals(""))
            {
                address.BorderBrush = scError;
                isRight = false;
                // MessageBox.Show("家庭住址不能为空");
            }
            //电话号码
            if (user.PhoneNumber.Length != 11)
            {
                phoneNumber.BorderBrush = scError;
                isRight = false;
            }
            else if (!Regex.IsMatch(user.PhoneNumber, @"^1(3[0-9]|4[57]|5[0-35-9]|7[0135678]|8[0-9])\d{8}$"))
            {
                phoneNumber.BorderBrush = scError;
                isRight = false;
            }
            //金额
            if (!Regex.IsMatch(money.Text.ToString(), @"^[0-9]*$"))
            {
                money.BorderBrush = scError;
                isRight = false;
            }
            else if ((decimal)user.MoneySum <= 0)
            {
                money.BorderBrush = scError;
                isRight = false;
            }
            return isRight;
        }
        //充值正常状态
        private void setStateNormal()
        {
            userName.BorderBrush = scNormal;
            userPassword.BorderBrush = scNormal;
            userPasswordRepeat.BorderBrush = scNormal;
            realName.BorderBrush = scNormal;
            birthday.BorderBrush = scNormal;
            idCard.BorderBrush = scNormal;
            address.BorderBrush = scNormal;
            phoneNumber.BorderBrush = scNormal;
            money.BorderBrush = scNormal;

            userName.Text = "";
            userPassword.Password = "";
            userPasswordRepeat.Password = "";
            realName.Text = "";
            sex_nan.IsChecked = false;
            sex_nv.IsChecked = false;
            birthday.Text = "";
            idCard.Text = "";
            address.Text = "";
            phoneNumber.Text = "";
            money.Text = "";
        }
        //正确状态
        private void setStateRight()
        {
            userName.BorderBrush = scRight;
            userPassword.BorderBrush = scRight;
            userPasswordRepeat.BorderBrush = scRight;
            realName.BorderBrush = scRight;
            birthday.BorderBrush = scRight;
            idCard.BorderBrush = scRight;
            address.BorderBrush = scRight;
            phoneNumber.BorderBrush = scRight;
            money.BorderBrush = scRight;
        }

        private void sex_nan_Checked(object sender, RoutedEventArgs e)
        {
            imgIsSuccess.Visibility = System.Windows.Visibility.Hidden;
        } 
    }
}
