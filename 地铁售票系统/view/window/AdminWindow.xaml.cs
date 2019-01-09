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
using 地铁售票系统.view.page;
using 地铁售票系统.view.page.admin;

namespace 地铁售票系统.view.window
{
    /// <summary>
    /// AdminWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AdminWindow : Window
    {
        private Page[] pages = new Page[5];//管理程序的页面的集合
        private DispatcherTimer timer;//时间定时器
        public AdminWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //创建所有页面的对象
            pages[0] = new RouteMapPage();
            pages[1] = new QueryPage();
            pages[2] = new OpenAccountPage();
            pages[3] = new RechargePage();
            pages[4] = new DelAccountPage();
            admin_frame.Content = pages[0];

            //定时器
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Start();
        }

        /// <summary>
        /// 动态设置时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            ymd.Content = string.Format("{0:yyyy-M-d}", date);
            hms.Content = string.Format("{0:HH:mm:ss}", date);
        }

        //窗体相关
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

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            if (border != null)
            {
                switch (border.Name)
                {
                    case "route":
                        pages[0] = new RouteMapPage();
                        admin_frame.Content = pages[0];
                        break;
                    case "query":
                        pages[1] = new QueryPage();
                        admin_frame.Content = pages[1];
                        break;
                    case "openAccount":
                        pages[2] = new OpenAccountPage();
                        admin_frame.Content = pages[2];
                        break;
                    case "recharge":
                        pages[3] = new RechargePage();
                        admin_frame.Content = pages[3];
                        break;
                    case "delAccount":
                        pages[4] = new DelAccountPage();
                        admin_frame.Content = pages[4];
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
        //中英文切换
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

        private void English_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
    }
        #endregion
}