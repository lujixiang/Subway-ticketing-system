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
using 地铁售票系统.view.window;


namespace 地铁售票系统
{
    /// <summary>
    /// CheckManagerOrUser.xaml 的交互逻辑
    /// </summary>
    public partial class CheckManagerOrUser : Window
    {
        public CheckManagerOrUser()
        {
            InitializeComponent();
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            ////测试
            //AdminWindow a = new AdminWindow();
            //a.Show();
            //this.Close();
        }

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
                    case "售票系统":
                     MainWindow mainWindow = new MainWindow();
                    this.Close();
                    mainWindow.Show();
                    break;
                    case "管理系统":
                    AdminLogin adminLogin = new AdminLogin();
                    this.Close();
                     adminLogin.Show();
                        break;
                    case "进出站系统":
                        InAndOut inandout = new InAndOut();
                        this.Close();
                        inandout.Show();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
