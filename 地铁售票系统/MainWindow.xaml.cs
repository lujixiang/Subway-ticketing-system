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
using System.Windows.Shapes;
using System.Windows.Threading;
using 地铁售票系统.model;
using 地铁售票系统.view.page;
using 地铁售票系统.db;
using 地铁售票系统.utils;

namespace 地铁售票系统.view.window
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Brush brushNext = new LinearGradientBrush();
        private SelectLinePage selectLinePage;//选路线Page
        private Brush ok, noOk;//购票进度的颜色
        private DispatcherTimer timerIsSelectLine;//用于实时检测是否选择站点
        //页面集合
        private Page[] pages = new Page[4];
        private Ticket ticket;
        public MainWindow()
        {
            InitializeComponent();
            //设置窗体大小
            double height = SystemParameters.PrimaryScreenHeight;
            double width = SystemParameters.PrimaryScreenWidth;
            this.Width = width * 6 / 7;
            this.Height = height * 8 / 9;


            //获取下一步按钮原来的颜色
            brushNext = NextBtn.Background;

            //获取购票进度的不同颜色
            ok = selectLine.Background;
            noOk = selectTicketNum.Background;
            //创建车票对象
            ticket = new Ticket();
            pages[0] = new SelectLinePage(ticket);
            pages[1] = new SelectTicketNum(ticket);
            pages[2] = new Pay(ticket);
            pages[3] = new PaySucceed(ticket);
            main_frame.Content = pages[0];
            //设置第一个页面的相关属性
            SelectLinePage selectLinePage = (SelectLinePage)pages[0];
            selectLinePage.setTicketFrameAndPage(main_frame, pages);

            //初始化计时器
            timerIsSelectLine = new DispatcherTimer();
            timerIsSelectLine.Interval = TimeSpan.FromMilliseconds(100);
            timerIsSelectLine.Tick += timerIsSelectLine_Tick;
            timerIsSelectLine.Start();
            //
        }

        //每隔0.5秒检测所有页面,设置相关属性
        #region
        /// <summary>
        /// 每隔0.1秒检测页面,设置相关属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timerIsSelectLine_Tick(object sender, EventArgs e)
        {
            if (main_frame.Content == pages[0])
            {
                //设置下一步的按钮
                ConPay.Visibility = System.Windows.Visibility.Collapsed;
                nextText.Visibility = System.Windows.Visibility.Visible;
                if (ticket.Destination.Equals("未选") || ticket.Destination.Equals(ticket.Source))
                {
                    NextBtn.IsEnabled = false;
                    NextBtn.Background = Brushes.Gray;
                }
                else
                {
                    if (!NextBtn.IsEnabled)
                    {
                        NextBtn.Background = brushNext;
                    }
                    NextBtn.IsEnabled = true;
                }
                PreBtn.Visibility = System.Windows.Visibility.Collapsed;
                selectTicketNum.Background = noOk;
                pay.Background = noOk;
                paySucceed.Background = noOk;
            }
            else if (main_frame.Content == pages[1])
            {
                PreBtn.Visibility = System.Windows.Visibility.Visible;
                selectTicketNum.Background = ok;
                pay.Background = noOk;
                paySucceed.Background = noOk;
                if (!NextBtn.IsEnabled)
                {
                    NextBtn.IsEnabled = true;
                    NextBtn.Background = brushNext;
                }
                //设置下一步的按钮
                ConPay.Visibility = System.Windows.Visibility.Collapsed;
                nextText.Visibility = System.Windows.Visibility.Visible;
            }
            else if (main_frame.Content == pages[2])
            {
                pay.Background = ok;
                paySucceed.Background = noOk;
                //设置下一步的按钮
                ConPay.Visibility = System.Windows.Visibility.Visible;
                nextText.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (main_frame.Content == pages[3])
            {
                PreBtn.Visibility = System.Windows.Visibility.Collapsed;
                paySucceed.Background = ok;
                //设置下一步的按钮
                ConPay.Visibility = System.Windows.Visibility.Collapsed;
                nextText.Visibility = System.Windows.Visibility.Visible;
            }
        }
        #endregion

        //各种效果的折叠代码，没事不要乱动
        #region
        /// <summary>
        /// 窗体随鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DockPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// 返回按钮鼠标进入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromArgb(255, 245, 205, 27);
            PreBtn.Background = brush;
        }
        /// <summary>
        /// 返回按钮鼠标离开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            PreBtn.Background = brushNext;
        }
        /// <summary>
        /// 返回按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (main_frame.Content == pages[1])
            {
                PreBtn.Visibility = System.Windows.Visibility.Hidden;
                ticket.TotalPrice = 0;
                main_frame.Content = pages[0];
                selectTicketNum.Background = noOk;
                ticket.TotalPrice = 0;
                ticket.IsDirect = false;
                ticket.Destination = "未选";
            }
            else if (main_frame.Content == pages[2])
            {
                ticket.TotalPrice = 0;
                main_frame.Content = pages[1];
                pay.Background = noOk;
                //nextText.Text = "下一步";
            }
        }
        /// <summary>
        /// 下一步按钮鼠标进入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromArgb(255, 245, 205, 27);
            NextBtn.Background = brush;
        }
        /// <summary>
        /// 下一步按钮鼠标离开时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (NextBtn.IsEnabled)
            {
                //如果可以获得焦点
                NextBtn.Background = brushNext;
            }
            else
            {
                NextBtn.Background = Brushes.Gray;
            }
        }
        /// <summary>
        /// 下一步按钮鼠标单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (main_frame.Content == pages[0])
            {//进入选票数页面
                if (ticket.Destination.Equals("未选"))
                {//判断是否选择站点
                    MessageBox.Show("请选择站点");
                }
                else
                {//已经选择站点
                    main_frame.Content = pages[1];
                    selectTicketNum.Background = ok;
                }

            }
            else if (main_frame.Content == pages[1])
            {//进入购票页面
                Pay p = (Pay)pages[2];
                p.updatePayInfo();//更新购票信息
                main_frame.Content = pages[2];
                //nextText.Text = "确认购票";
                pay.Background = ok;
            }
            else if (main_frame.Content == pages[2])
            {//进入支付成功页面
                Pay p2 = (Pay)pages[2];
                int cp = p2.buyTicket();//得到购票返回的信息:-1余额不足,0异常出错,1购票成功,3验证码错误
                if (cp == 1)
                {
                    main_frame.Content = pages[3];
                    //nextText.Text = "返回";
                    paySucceed.Background = ok;
                    //启动出票计时器
                    PaySucceed p = (PaySucceed)pages[3];
                    p.startTimer(NextBtn, brushNext);
                }
                else if (cp == -1)
                {
                    MessageBox.Show("用户余额不足!");
                }
                else if (cp == 0)
                {
                    MessageBox.Show("系统异常,请联系管理人员!");
                }
                else if (cp == 3)
                {
                    MessageBox.Show("验证码错误!");
                }
                else if (cp == 2)
                {
                    MessageBox.Show("用户信息错误!");
                }

            }
            else if (main_frame.Content == pages[3])
            {//返回选路线页面
                //nextText.Text = "下一步";
                selectTicketNum.Background = noOk;
                pay.Background = noOk;
                paySucceed.Background = noOk;
                main_frame.NavigationService.RemoveBackEntry();
                //生成一个新的界面
                this.ticket = new Ticket();//初始化新的票
                pages[0] = new SelectLinePage(ticket);
                pages[1] = new SelectTicketNum(ticket);
                pages[2] = new Pay(ticket);
                pages[3] = new PaySucceed(ticket);
                main_frame.Content = pages[0];
                //设置第一个页面的相关属性
                SelectLinePage selectLinePage = (SelectLinePage)pages[0];
                selectLinePage.setTicketFrameAndPage(main_frame, pages);
            }
        }
        /// <summary>
        /// 最小化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void window_min_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }
        /// <summary>
        /// 关闭程序事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void window_close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.Current.Shutdown();
        }
        /// <summary>
        /// 鼠标移入最小化自定义按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void window_min_MouseEnter(object sender, MouseEventArgs e)
        {
            Border bd = sender as Border;
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromArgb(255, 50, 143, 227);
            bd.Background = brush;
        }
        /// <summary>
        /// 鼠标移入关闭自定义按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void window_close_MouseEnter(object sender, MouseEventArgs e)
        {
            Border bd = sender as Border;
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromArgb(180, 200, 0, 0);
            bd.Background = brush;
        }
        /// <summary>
        /// 鼠标移出最小化自定义按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void window_min_MouseLeave(object sender, MouseEventArgs e)
        {
            Border bd = sender as Border;
            bd.Background = null;
        }
        /// <summary>
        /// 鼠标移出关闭自定义按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void window_close_MouseLeave(object sender, MouseEventArgs e)
        {
            Border bd = sender as Border;
            bd.Background = null;
        }
        #endregion
        //语言切换
        #region
        private void Chinese_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            List<ResourceDictionary> dictionaryList = new List<ResourceDictionary>();
            foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
            {
                dictionaryList.Add(dictionary);
            }
            string requestedCulture = @"myStyle\ChineseDictionary.xaml";
            ResourceDictionary resourceDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedCulture));
            Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

        }

        private void English_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            List<ResourceDictionary> dictionaryList = new List<ResourceDictionary>();
            foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
            {
                dictionaryList.Add(dictionary);
            }
            string requestedCulture = @"myStyle\EnglishDictionary.xaml";
            ResourceDictionary resourceDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedCulture));
            Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }
        #endregion
    }
}
