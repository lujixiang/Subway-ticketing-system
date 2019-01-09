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
using 地铁售票系统.model;

namespace 地铁售票系统.view.page
{
    /// <summary>
    /// LineNo2.xaml 的交互逻辑
    /// </summary>
    public partial class LineNo2 : Page
    {
        private Ticket ticket;//票
        private TextBlock destinctionPoint;//目的站站
        private TextBlock sourcePoint;//起始站
        public LineNo2(Ticket ticket, TextBlock destinctionPoint, TextBlock sourcePoint)
        {
            InitializeComponent();
            this.ticket = ticket;
            this.destinctionPoint = destinctionPoint;
            this.sourcePoint = sourcePoint;
        }

        /// <summary>
        /// 点击左键选择起始站点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border border = e.Source as Border;
            TextBlock tb = e.Source as TextBlock;
            //设置路线
            ticket.LineNumber = "2号线";
            if (border == null && tb != null)
            {
                this.destinctionPoint.Text = tb.Text;
                ticket.Destination = tb.Text;
                ticket.DestinationNum = int.Parse(tb.Tag.ToString());//设置目的站点标号
            }
            else if (border != null)
            {
                TextBlock t = border.Child as TextBlock;
                this.destinctionPoint.Text = t.Text;
                ticket.Destination = t.Text;
                ticket.DestinationNum = int.Parse(t.Tag.ToString());//设置目的站点标号
            }
        }
        /// <summary>
        /// 点击右键选择目的站点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border border = e.Source as Border;
            TextBlock tb = e.Source as TextBlock;
            //设置路线
            ticket.LineNumber = "2号线";
            if (border == null && tb != null)
            {
                this.sourcePoint.Text = tb.Text;
                ticket.Source = tb.Text;
                ticket.SourceNum = int.Parse(tb.Tag.ToString());//设置起始站点标号
            }
            else if (border != null)
            {
                TextBlock t = border.Child as TextBlock;
                this.sourcePoint.Text = t.Text;
                ticket.Source = t.Text;
                ticket.SourceNum = int.Parse(t.Tag.ToString());//设置起始站点标号
            }
        }
    }
}
