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
using System.Windows.Threading;
using 地铁售票系统.model;

namespace 地铁售票系统.view.page
{
    /// <summary>
    /// SelectTicketNum.xaml 的交互逻辑
    /// </summary>
    public partial class SelectTicketNum : Page
    {
        private Ticket ticket;//车票对象
        private DispatcherTimer timerIsSelectLine;//用于实时检测站点
        public SelectTicketNum(Ticket ticket)
        {
            InitializeComponent();
            this.ticket = ticket;
            //设置单程票默认被选中
            singleLine.IsChecked = true;
            //设置票数默认为1张
            ticket.Number = 1;
            //初始化定时器
            timerIsSelectLine = new DispatcherTimer();
            timerIsSelectLine.Interval = TimeSpan.FromMilliseconds(50);
            timerIsSelectLine.Tick += timerIsSelectLine_Tick;
            timerIsSelectLine.Start();
        }

        void timerIsSelectLine_Tick(object sender, EventArgs e)
        {
            startT.Text = ticket.Source;
            stopT.Text = ticket.Destination;
        }


        //实时检测textBox中输入的按键,屏蔽非数字按键
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = false;
                return;
            }
            else if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                e.Handled = false;
                return;
            }
            else
            {
                e.Handled = true;
                return;
            }
        }
        /// <summary>
        /// 设置票数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            int num = 1;
            if (border != null)
            {
                string op = (string)border.Tag;
                int.TryParse(textBoxTicketNum.Text, out num);
                if (op.Equals("+"))
                {
                    num++;
                    if (num >= 99)
                    {
                        num = 99;
                    }
                }
                else if (op.Equals("-"))
                {
                    num--;
                    if (num <= 1)
                    {
                        num = 1;
                    }
                }
                else
                {
                    try
                    {
                        num = int.Parse(op);
                    }
                    catch
                    {
                        num = 1;
                    }
                }
                textBoxTicketNum.Text = num + "";
                ticket.Number = num;
            }
        }
        /// <summary>
        /// 设置票的类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rd = sender as RadioButton;
            if (rd != null)
            {
                ticket.Type = rd.Content.ToString();

            }
        }
        /// <summary>
        /// TextBox里面的内容改变时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxTicketNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            int num = 1;
            if (tb != null&&ticket!=null)
            {
                try
                {
                    num = int.Parse(tb.Text);
                }
                catch
                {
                    num = 1;
                }
                ticket.Number = num;
            }
        }      
    }
}
