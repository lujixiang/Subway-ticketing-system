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
    /// PaySucceed.xaml 的交互逻辑
    /// </summary>
    public partial class PaySucceed : Page
    {
        private Border border;
        private DispatcherTimer timer;
        private int restTime;
        private Brush brush;//暂时保存Border原来的颜色，当获得焦点时恢复颜色
        private Ticket ticket;
        public PaySucceed(Ticket ticket)
        {
            InitializeComponent();
            this.ticket = ticket;
        }
        /// <summary>
        /// 启动定时器,延时5秒出票,并传送Border以设置不可获得焦点
        /// </summary>
        /// <param name="border"></param>
        public void startTimer(Border border,Brush brush)
        {
            this.brush = brush;
            this.border = border;
            //出票时返回按钮不能获得焦点
            brush = this.border.Background;
            this.border.IsEnabled = false;
            restTime = 5;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            restTime--;
            if (restTime < 0)
            {
                haveProduceTicket.Visibility = System.Windows.Visibility.Visible;
                onProduceTicket.Visibility = System.Windows.Visibility.Collapsed;
                timer.Stop();
                //出票时返回按钮重新获得焦点
                this.border.IsEnabled = true;
                this.border.Background = this.brush;
            }
            else
            {
                onProduceTicket.Text += '.';
            }
        }
    }
}
