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
using 地铁售票系统.db;
using 地铁售票系统.view.window;

namespace 地铁售票系统.view.page.inout
{
    /// <summary>
    /// Out.xaml 的交互逻辑
    /// </summary>
    public partial class Out : Page
    {
        private bool isYanshi = false;
        //跳过点击出战按钮又加载一边getCurrentStation;
        private bool isSkip = true;
        private int currentNum = 0;
        private int startNum;
        private int endNum;
        //超过的站数
        private int outNum;
        //需支付的钱
        private int outMoney;
        //剩余金额
        private int leftMoney;
        //账户余额是否足够支付
        private bool isMoney = false;
        private InAndOut mainW;
        private bool isUser = false;
        private Random r = new Random();
        //是否出站
        private bool isOut = false;
        public Out(InAndOut main)
        {
            InitializeComponent();
            this.mainW = main;

        }
        /// <summary>
        /// 获取当前站点
        /// </summary>
        /// <param name="currentNum"></param>
        /// <param name="strLineNum"></param>
        /// <returns></returns>
        public string getCurrentStation(string tmpUserName)
        {

            //坐过站可能性
            int tmpNum = 0;
            string tmpLineStr = "";
            using (AppDbEntities context = new AppDbEntities())
            {
                var q = from t in context.UserDetails
                        where t.UserName == tmpUserName && t.TicketsState == "进站"
                        select t;
                foreach (var v in q)
                {
                    startNum = v.StartTag;
                    endNum = v.EndTag;
                    if (startNum > endNum && endNum > 5)
                    {
                        //设置
                        tmpNum = endNum - 2;
                    }
                    else if (endNum > startNum && endNum <= 12)
                    {
                        tmpNum = 8;
                    }
                    //随机一个当前站点标识
                    if (startNum < endNum)
                    {
                        currentNum = 1 + startNum + r.Next(endNum - startNum + tmpNum);
                        //超过的站数
                        outNum = currentNum > endNum ? currentNum - endNum : 0;
                    }
                    else
                    {
                        currentNum = 1 + endNum + r.Next(startNum - endNum) - tmpNum;
                        outNum = currentNum < endNum ? endNum - currentNum : 0;
                    }
                    tmpLineStr = v.LineNumber;
                }
            }

            string currentStation = "";
            //设置当前站点
            switch (tmpLineStr)
            {
                case "1号线":
                    currentStation = mainW.line1[currentNum];
                    break;
                case "2号线":
                    currentStation = mainW.line2[currentNum]; break;
                case "3号线": currentStation = mainW.line3[currentNum];
                    break;
                case "4号线": currentStation = mainW.line4[currentNum];
                    break;
                case "5号线": currentStation = mainW.line5[currentNum];
                    break;
                case "6号线": currentStation = mainW.line6[currentNum];
                    break;
            }
            return currentStation;

        }
        /// <summary>
        /// 用户名输入框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            isSkip = false;
            //警告信息隐藏
            spWarning.Visibility = System.Windows.Visibility.Hidden;
            isOut = false;
            //隐藏图标，当前站点，票的状态
            imgIsSuccess.Visibility = System.Windows.Visibility.Hidden;
            spCurrentStation.Visibility = System.Windows.Visibility.Hidden;
            dgUser.Visibility = System.Windows.Visibility.Hidden;
            //用户名是否正确
            isUser = false;
            //检查用户名
            if (CheckUser.checkUserIsLegal(tbUserName.Text))
            {
                //用户名正确
                isUser = true;
                //显示票状态
                if (tictetsState(tbUserName.Text))
                {
                }
                else
                {
                    //未知的错误
                }
            }
        }
        /// <summary>
        /// 出站按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void out_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isSkip = true;
            outMoney = 0;

            if (isUser)
            {
                if (!isOut)
                {
                    if (isOut) { }
                    bool isOk = false;

                    //坐过站
                    if (outNum > 0)
                    {
                        //超过金额计算
                        outMoney = 1 + (outNum - 1) / 6;
                        using (AppDbEntities context = new AppDbEntities())
                        {
                            var q1 = from f in context.UserDetails
                                    where f.UserName == tbUserName.Text && f.TicketsState == "进站"
                                    select f;
                            foreach (var v1 in q1) {
                                outMoney *= v1.TicketsNum;
                            }


                            var q2 = from t in context.UserInfoes
                                    where t.UserName == tbUserName.Text
                                    select t;
                            foreach (var v2 in q2)
                            {
                                if (v2.MoneySum >= outMoney)
                                {
                                    v2.MoneySum -= outMoney;
                                    leftMoney = (int)v2.MoneySum;
                                    isMoney = true;
                                    isOk = true;
                                    isYanshi = false;
                                }
                                else
                                {
                                    isYanshi = true;
                                    isMoney = true;
                                    isOk = true;
                                    //数据显示
                                    tictetsState(tbUserName.Text);
                                    imgIsSuccess.Source = new BitmapImage(new Uri("/images/警告.png", UriKind.Relative));
                                    imgIsSuccess.ToolTip = "账户余额不足";
                                }
                            }
                            //账户余额足够支付
                            if (isMoney&&!isYanshi)
                            {
                                payState();
                                if (isOk)
                                {
                                    isOut = true;
                                    //数据显示
                                    tictetsState(tbUserName.Text);
                                    //存储至数据库
                                    context.SaveChanges();
                                    //成功图标显示
                                    imgIsSuccess.Source = new BitmapImage(new Uri("/images/成功.png", UriKind.Relative));
                                    imgIsSuccess.ToolTip = "出站成功";
                                }
                                else
                                {
                                    imgIsSuccess.Source = new BitmapImage(new Uri("/images/失败.png", UriKind.Relative));
                                    imgIsSuccess.ToolTip = "未进站或已出站";
                                }
                            }
                            //账户余额不足够支付
                            else
                            {
                                unPayState();
                            }
                        }
                    }

                    //未超坐站或已经支付过超坐的钱
                    if (outNum == 0 || isMoney)
                    {
                        using (AppDbEntities context = new AppDbEntities())
                        {
                            var q = from t in context.UserDetails
                                    where t.UserName == tbUserName.Text && t.TicketsState == "进站"
                                    select t;
                            foreach (var v in q)
                            {
                              
                                v.TicketsState = "出站";
                                isOk = true;
                            }

                            if (isOk)
                            {
                                isOut = true;
                                //数据显示
                                tictetsState(tbUserName.Text);
                                context.SaveChanges();
                                //成功图标显示
                                imgIsSuccess.Source = new BitmapImage(new Uri("/images/成功.png", UriKind.Relative));
                                imgIsSuccess.ToolTip = "出站成功";
                            }
                            else
                            {
                                imgIsSuccess.Source = new BitmapImage(new Uri("/images/失败.png", UriKind.Relative));
                                imgIsSuccess.ToolTip = "未购票或已出站";
                            }
                        }
                    }

                }
                else
                {
                    imgIsSuccess.Source = new BitmapImage(new Uri("/images/失败.png", UriKind.Relative));
                    imgIsSuccess.ToolTip = "未购票或已出站";
                }
            }
            else
            {
                imgIsSuccess.Source = new BitmapImage(new Uri("/images/失败.png", UriKind.Relative));
                imgIsSuccess.ToolTip = "不存在此用户";
            }

            if (isOut && !isMoney)
            {
                //输入框清空
                tbUserName.Text = "";
            }
            imgIsSuccess.Visibility = System.Windows.Visibility.Visible;
        }
        public void payState()
        {
            spWarning.Visibility = System.Windows.Visibility.Visible;

            lbOutNum.Visibility = System.Windows.Visibility.Visible;
            lbTip1.Visibility = System.Windows.Visibility.Visible;
            lbOutMoney.Visibility = System.Windows.Visibility.Visible;
            lbTip2.Visibility = System.Windows.Visibility.Visible;
            lbLeftMpney.Visibility = System.Windows.Visibility.Visible;
            lbTip.Visibility = System.Windows.Visibility.Collapsed;
            lbOutNum.Content = outNum + "";
            lbOutMoney.Content = outMoney + "元";
            lbLeftMpney.Content = leftMoney + "元";
        }
        public void unPayState()
        {
            spWarning.Visibility = System.Windows.Visibility.Visible;
            lbOutNum.Visibility = System.Windows.Visibility.Visible;

            lbTip1.Visibility = System.Windows.Visibility.Collapsed;
            lbOutMoney.Visibility = System.Windows.Visibility.Collapsed;
            lbTip2.Visibility = System.Windows.Visibility.Collapsed;
            lbLeftMpney.Visibility = System.Windows.Visibility.Collapsed;
            lbOutNum.Content = outNum + "";

        }

        /// <summary>
        /// 票的状态
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private bool tictetsState(string userName)
        {
            try
            {
                using (AppDbEntities context = new AppDbEntities())
                {
                    var q = from t in context.UserDetails
                            where t.UserName == userName && t.TicketsState == "进站"
                            select new
                            {
                                姓名 = t.Name,
                                路线 = t.LineNumber,
                                起始站 = t.StartStation,
                                终点站 = t.EndStation,
                                票数 = t.TicketsNum,
                                时间 = t.Time,
                                状态 = isOut ? "出站" : t.TicketsState
                            };
                    foreach (var v in q)
                    {
                        //显示当前站点
                        spCurrentStation.Visibility = System.Windows.Visibility.Visible;
                        if (!isSkip)
                        {
                            lbCurrentStation.Content = getCurrentStation(userName);
                        }//显示票的信息
                        dgUser.Visibility = System.Windows.Visibility.Visible;
                        dgUser.ItemsSource = q.ToList();

                        return true;
                    }
                }
            }
            catch (Exception ee)
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainW.inout_frame.Content = mainW.pages[0];
        }
    }
}

