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
    /// SelectLinePage.xaml 的交互逻辑
    /// 选择路线
    /// </summary>
    public partial class SelectLinePage : Page
    {
        private DispatcherTimer timer;//时间定时器
        private Page[] pages = new Page[7];
        private Border[] borders = new Border[7];
        private Brush[] brushs = new SolidColorBrush[7];//保存自定义按钮原来的颜色
        private Ticket ticket;//车票对象
        private Frame main_frame;//主框架
        private Page[] main_pages;//页面集合
        private static string[] station = {
                                      "河南大学","郑州大学","科学大道","梧桐街","化工路","铁炉",
                                      "须水","西流湖","柿园","秦岭路","工人文化宫","碧沙岗",
                                      "绿城广场","大学路","铁郑州站","二七广场","人民南","紫荆山",
                                      "东明路","燕庄","会展中心","黄河东路","农业东路","七里河",
                                      "铁路东站","博学路","姚桥","龙子湖","穆庄"
                                   };
        public SelectLinePage(Ticket ticket)
        {
            InitializeComponent();
            //定时器
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Start();

            //保存自定义按钮原来的颜色
            brushs[0] = allLineBtn.Background;
            brushs[1] = line1Btn.Background;
            brushs[2] = line2Btn.Background;
            brushs[3] = line3Btn.Background;
            brushs[4] = line4Btn.Background;
            brushs[5] = line5Btn.Background;
            brushs[6] = line6Btn.Background;
            //为自定义的按钮添加鼠标点击事件
            allLineBtn.MouseLeftButtonDown += LineBtn_MouseLeftButtonDown;
            line1Btn.MouseLeftButtonDown += LineBtn_MouseLeftButtonDown;
            line2Btn.MouseLeftButtonDown += LineBtn_MouseLeftButtonDown;
            line3Btn.MouseLeftButtonDown += LineBtn_MouseLeftButtonDown;
            line4Btn.MouseLeftButtonDown += LineBtn_MouseLeftButtonDown;
            line5Btn.MouseLeftButtonDown += LineBtn_MouseLeftButtonDown;
            line6Btn.MouseLeftButtonDown += LineBtn_MouseLeftButtonDown;
            //将各Border添加到数组中
            borders[0] = allLineBtn;
            borders[1] = line1Btn;
            borders[2] = line2Btn;
            borders[3] = line3Btn;
            borders[4] = line4Btn;
            borders[5] = line5Btn;
            borders[6] = line6Btn;
            //车票对象
            this.ticket = ticket;
            this.ticket.Source = "河南大学";
            this.ticket.Destination = "未选";
            pages[0] = new AllLinePage();
            pages[1] = new LineNo1(ticket, destinctionPoint, sourcePoint);
            pages[2] = new LineNo2(ticket, destinctionPoint, sourcePoint);
            pages[3] = new LineNo3(ticket, destinctionPoint, sourcePoint);
            pages[4] = new LineNo4(ticket, destinctionPoint, sourcePoint);
            pages[5] = new LineNo5(ticket, destinctionPoint, sourcePoint);
            pages[6] = new LineNo6(ticket, destinctionPoint, sourcePoint);

            line_frame.Content = pages[0];
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

        public void setTicketFrameAndPage(Frame main_frame, Page[] main_pages)
        {

            this.main_frame = main_frame;
            this.main_pages = main_pages;
        }
        /// <summary>
        /// 设置选站状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LineBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            //默认起始站标识
            ticket.SourceNum = 1;
            switch (border.Name)
            {
                case "allLineBtn":
                    setBackGround(allLineBtn);
                    line_frame.Content = pages[0];
                    break;
                case "line1Btn":
                    sourcePoint.Text = "河南大学";
                    destinctionPoint.Text = "未选";
                    ticket.Source = "河南大学";
                    ticket.Destination = "未选";
                    setBackGround(line1Btn);
                    line_frame.Content = pages[1];
                    break;
                case "line2Btn":
                    sourcePoint.Text = "堤湾";
                    destinctionPoint.Text = "未选";
                    ticket.Source = "堤湾";
                    ticket.Destination = "未选";
                    setBackGround(line2Btn);
                    line_frame.Content = pages[2];
                    break;
                case "line3Btn":
                    sourcePoint.Text = "白松路";
                    destinctionPoint.Text = "未选";
                    ticket.Source = "白松路";
                    ticket.Destination = "未选";
                    setBackGround(line3Btn);
                    line_frame.Content = pages[3];
                    break;
                case "line4Btn":
                    sourcePoint.Text = "老鸦陈";
                    destinctionPoint.Text = "未选";
                    ticket.Source = "老鸦陈";
                    ticket.Destination = "未选";
                    setBackGround(line4Btn);
                    line_frame.Content = pages[4];
                    break;
                case "line5Btn":
                    sourcePoint.Text = "桐柏路";
                    destinctionPoint.Text = "未选";
                    ticket.Source = "桐柏路";
                    ticket.Destination = "未选";
                    setBackGround(line5Btn);
                    line_frame.Content = pages[5];
                    break;
                case "line6Btn":
                    sourcePoint.Text = "郑寨";
                    destinctionPoint.Text = "未选";
                    ticket.Source = "郑寨";
                    ticket.Destination = "未选";
                    setBackGround(line6Btn);
                    line_frame.Content = pages[6];
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 切换路线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LineBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            switch (border.Name)
            {
                case "allLineBtn":
                    allLineBtn.Background = brushs[0];
                    break;
                case "line1Btn":
                    line1Btn.Background = brushs[0];
                    break;
                case "line2Btn":
                    line2Btn.Background = brushs[0];
                    break;
                case "line3Btn":
                    line3Btn.Background = brushs[0];
                    break;
                case "line4Btn":
                    line4Btn.Background = brushs[0];
                    break;
                case "line5Btn":
                    line5Btn.Background = brushs[0];
                    break;
                case "line6Btn":
                    line6Btn.Background = brushs[0];
                    break;
                default:
                    break;
            }
        }

        private void setBackGround(Border border)
        {
            for (int i = 0; i < borders.Length; i++)
            {
                if (borders[i] != border)
                {
                    borders[i].Background = brushs[1];
                }
                else
                {
                    borders[i].Background = brushs[0];
                }
            }
        }

        /// <summary>
        /// 快捷购票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            ticket.SourceNum = 1;//设置起始站标号
            ticket.LineNumber = "1号线";
            if (tb != null)
            {
                switch (tb.Name)
                {
                    case "two":
                        ticket.Source = "河南大学";
                        ticket.Destination = station[5];//终点站为铁炉
                        ticket.Flag = 2;
                        ticket.IsDirect = true;
                        ticket.DestinationNum = 6;//设置终点站标号
                        main_frame.Content = main_pages[1];
                        break;
                    case "three":
                        ticket.Source = "河南大学";
                        ticket.Destination = station[11];//终点站为碧沙岗
                        ticket.Flag = 3;
                        ticket.IsDirect = true;
                        ticket.DestinationNum = 12;//设置终点站标号
                        main_frame.Content = main_pages[1];
                        break;
                    case "four":
                        ticket.Source = "河南大学";
                        ticket.Destination = station[17];//终点站为紫荆山
                        ticket.IsDirect = true;
                        ticket.Flag = 4;
                        ticket.DestinationNum = 18;//设置终点站标号
                        main_frame.Content = main_pages[1];
                        break;
                    case "five":
                        ticket.Source = "河南大学";
                        ticket.Destination = station[23];//终点站为七里河
                        ticket.Flag = 5;
                        ticket.IsDirect = true;
                        ticket.DestinationNum = 24;//设置终点站标号
                        main_frame.Content = main_pages[1];
                        break;
                    case "six":
                        ticket.Source = "河南大学";
                        ticket.Destination = station[28];//终点站为穆庄
                        ticket.Flag = 6;
                        ticket.IsDirect = true;
                        ticket.DestinationNum = 29;//设置终点站标号
                        main_frame.Content = main_pages[1];
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
