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
using 地铁售票系统.db;

namespace 地铁售票系统.view.window
{
    /// <summary>
    /// AdminLogin.xaml 的交互逻辑
    /// </summary>
    public partial class AdminLogin : Window
    {
        public AdminLogin()
        {
            InitializeComponent();
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            login.IsDefault = true;
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

        private void login_Click(object sender, RoutedEventArgs e)
        {
            string adminNameTmp = adminName.Text;
            string adminPasswordTmp = adminPassword.Password;
            if (CheckUser.checkAdminIsLegal(adminNameTmp, adminPasswordTmp))
            {
                AdminWindow adminWindow = new AdminWindow();
                this.Close();
                adminWindow.Show();
            }
            else
            {
                MessageBox.Show("管理员登录失败");
            }
        }
    }
}
