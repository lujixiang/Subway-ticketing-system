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
    /// QueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class QueryPage : Page
    {
        public QueryPage()
        {
            InitializeComponent();
        }

        private void query_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            using (AppDbEntities context = new AppDbEntities())
            {
                var q = from t in context.UserInfoes
                        where t.UserName == userName.Text
                        select new { 
                        姓名 = t.Name,
                        账号 = t.UserName,
                        余额 = t.MoneySum,
                        性别 = t.Sex,
                        手机号码 = t.PhoneNumber,
                        家庭住址 = t.Address,
                        身份证号 = t.IdCard,
                        生日 = t.Birthday,
                        开户时间 = t.OpenTime,
                        };
                imgIsSuccess.Source = new BitmapImage(new Uri("/images/失败.png", UriKind.Relative));
                imgIsSuccess.ToolTip = "用户名错误";
                imgIsSuccess.Visibility = System.Windows.Visibility.Visible;
                if (q.Count() == 1) {
                    dgUserInfo.Visibility = System.Windows.Visibility.Visible;
                    imgIsSuccess.Source = new BitmapImage(new Uri("/images/成功.png", UriKind.Relative));
                    imgIsSuccess.ToolTip = "查询成功";
                    dgUserInfo.ItemsSource = q.ToList();
                }
                var x = from y in context.UserDetails
                        where y.UserName == userName.Text
                        select new { 
                        姓名 = y.Name,
                        账号 = y.UserName,
                        路线 = y.LineNumber,
                        起始站 = y.StartStation,
                        终点站 = y.EndStation,
                        票数 = y.TicketsNum,
                        时间 = y.Time,
                        状态 = y.TicketsState,
                        消费金额 = y.Money
                        };
                if (x.Count() >0) {
                    dgUserDetail.Visibility = System.Windows.Visibility.Visible;
                    dgUserDetail.ItemsSource = x.ToList();
                }

            }
        }

        private void userName_TextChanged(object sender, TextChangedEventArgs e)
        {
            imgIsSuccess.Visibility = System.Windows.Visibility.Hidden;
            dgUserInfo.Visibility = System.Windows.Visibility.Hidden;
            dgUserDetail.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
