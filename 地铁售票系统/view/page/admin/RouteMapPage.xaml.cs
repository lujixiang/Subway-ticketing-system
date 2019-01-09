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

namespace 地铁售票系统.view.page.admin
{
    /// <summary>
    /// AdminWelcomePage.xaml 的交互逻辑
    /// </summary>
    public partial class RouteMapPage : Page
    {
        private Brush[] brushs = new SolidColorBrush[6];
        public RouteMapPage()
        {

            InitializeComponent();
            ////为路线添加鼠标移入事件
            no1.MouseEnter += no_MouseEnter;
            no2.MouseEnter += no_MouseEnter;
            no3.MouseEnter += no_MouseEnter;
            no4.MouseEnter += no_MouseEnter;
            no5.MouseEnter += no_MouseEnter;
            no6.MouseEnter += no_MouseEnter;
            //为路线添加鼠标移出事件
            no1.MouseLeave += no_MouseLeave;
            no2.MouseLeave += no_MouseLeave;
            no3.MouseLeave += no_MouseLeave;
            no4.MouseLeave += no_MouseLeave;
            no5.MouseLeave += no_MouseLeave;
            no6.MouseLeave += no_MouseLeave;

            brushs[0] = no1.Stroke;
            brushs[1] = no2.Stroke;
            brushs[2] = no3.Stroke;
            brushs[3] = no4.Stroke;
            brushs[4] = no5.Stroke;
            brushs[5] = no6.Stroke;
        }
        /// <summary>
        /// 撤销总线的鼠标移动上去的效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void no_MouseLeave(object sender, MouseEventArgs e)
        {
            Path path = sender as Path;
            if (path != null)
            {
                switch (path.Name)
                {
                    case "no1":
                        no1.Stroke = brushs[0];
                        break;
                    case "no2":
                        no2.Stroke = brushs[1];
                        break;
                    case "no3":
                        no3.Stroke = brushs[2];
                        break;
                    case "no4":
                        no4.Stroke = brushs[3];
                        break;
                    case "no5":
                        no5.Stroke = brushs[4];
                        break;
                    case "no6":
                        no6.Stroke = brushs[5];
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 总线的鼠标移动上去的效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void no_MouseEnter(object sender, MouseEventArgs e)
        {
            Path path = sender as Path;
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromArgb(255, 0, 0, 0);
            if (path != null)
            {
                switch (path.Name)
                {
                    case "no1":
                        no1.Stroke = brush;
                        break;
                    case "no2":
                        no2.Stroke = brush;
                        break;
                    case "no3":
                        no3.Stroke = brush;
                        break;
                    case "no4":
                        no4.Stroke = brush;
                        break;
                    case "no5":
                        no5.Stroke = brush;
                        break;
                    case "no6":
                        no6.Stroke = brush;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
