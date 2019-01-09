using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using 地铁售票系统.view.window;

namespace 地铁售票系统.view.page.inout
{
    /// <summary>
    /// In.xaml 的交互逻辑
    /// </summary>
    public partial class In : Page, INotifyPropertyChanged
    {
        private InAndOut mainW;
        private bool isUser = false;
        private bool isIn = false;
        public In(InAndOut main)
        {
            InitializeComponent();
            this.mainW = main;
        }
        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            isIn = false;
            imgIsSuccess.Visibility = System.Windows.Visibility.Hidden;
            dgUser.Visibility = System.Windows.Visibility.Hidden;
            isUser = false;
            if (CheckUser.checkUserIsLegal(tbUserName.Text))
            {
                isUser = true;
                if (tictetsState(tbUserName.Text))
                {
                }
            }
        }

        private void in_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (isUser)
            {
                bool isOk = false;
                using (AppDbEntities context = new AppDbEntities())
                {
                    var q = from t in context.UserDetails
                            where t.UserName == tbUserName.Text && t.TicketsState == "未使用"
                            select t;
                    foreach (var v in q)
                    {
                        v.TicketsState = "进站";
                        isOk = true;
                    }
                   
                    if (isOk)
                    {
                        isIn = true;
                        //数据显示
                        tictetsState(tbUserName.Text);
                        context.SaveChanges();
                        //成功图标显示
                        imgIsSuccess.Source = new BitmapImage(new Uri("/images/成功.png", UriKind.Relative));
                        imgIsSuccess.ToolTip = "进站成功";
                    }
                    else {
                       
                        imgIsSuccess.Source = new BitmapImage(new Uri("/images/失败.png", UriKind.Relative));
                        imgIsSuccess.ToolTip = "未购票或已进站";
                    }
                }
            }
            else {
                imgIsSuccess.Source = new BitmapImage(new Uri("/images/失败.png", UriKind.Relative));
                imgIsSuccess.ToolTip = "不存在此用户";
            }
            //输入框清空
            tbUserName.Text = "";
            imgIsSuccess.Visibility = System.Windows.Visibility.Visible;
            
        }

        private bool tictetsState(string userName) {
            try
            {
                using (AppDbEntities context = new AppDbEntities())
                {
                    var q = from t in context.UserDetails
                            where t.UserName == userName && t.TicketsState == "未使用"
                            select new
                            {
                                姓名 = t.Name,
                                路线 = t.LineNumber,
                                起始站 = t.StartStation,
                                终点站 = t.EndStation,
                                票数 = t.TicketsNum,
                                时间 = t.Time,
                                状态 = isIn? "进站":t.TicketsState
                            };
                    if (q.Count() > 0)
                    {
                        dgUser.Visibility = System.Windows.Visibility.Visible;
                        dgUser.ItemsSource = q.ToList();
                        return true;
                    }
                }
            }catch(Exception ee){
                return false;
            }
            return false;
        }

        private void back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainW.inout_frame.Content = mainW.pages[0];
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
