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
using 地铁售票系统.model;
using 地铁售票系统.utils;

namespace 地铁售票系统.view.page
{
    /// <summary>
    /// Pay.xaml 的交互逻辑
    /// </summary>
    public partial class Pay : Page
    {
        private Ticket ticket;//车票对象
        private Random rd;
        public Pay(Ticket ticket)
        {
            InitializeComponent();
            this.ticket = ticket;
            rd = new Random();
        }
        /// <summary>
        /// 更新订单信息
        /// </summary>
        public void updatePayInfo()
        {
            calTotalPrice();//计算总票价
            ticketType.Content = ticket.Type;
            ticketDestinction.Content = ticket.Destination;
            ticketSource.Content = ticket.Source;
            ticketPrice.Content = ticket.Price;
            ticketTotal.Content = ticket.TotalPrice;
            ticketNum.Content = ticket.Number;
            setYanZhengMa();//设置验证码
        }
        /// <summary>
        /// 设置验证码
        /// </summary>
        private void setYanZhengMa()
        {
            int yan = 1000+rd.Next(9999);
            yanZhengMaG.Text = yan.ToString();
        }
        public int buyTicket()
        {
            if (yanZhengMa.Text.Equals(yanZhengMaG.Text))
            {
                int cp = buyTicket(userName.Text, userPassword.Password, ticket);
                if (cp==1)
                {
                    return 1;
                }
                else 
                {
                    return cp;
                }
            }            
            return 3;//验证码错误
        }
        /// <summary>
        /// 计算总票价
        /// </summary>
        public void calTotalPrice()
        {
            if (ticket.IsDirect)
            {
                ticket.Price = ticket.Flag;
                ticket.TotalPrice = ticket.Price * ticket.Number;
                if(ticket.Type.Equals("往返票"))
                {
                    ticket.TotalPrice *= 2;
                    ticket.Price *= 2;
                }
                return;
            }
            else
            {
                int des = ticket.DestinationNum;
                int sou = ticket.SourceNum;
                int all = des - sou + 1;
                int priceFac = 1;
                if (ticket.Type.Equals("往返票"))
                {
                    priceFac = 2;
                }
                if (all < 0)
                {
                    all = -all;
                }
                if (all <= 6)
                {
                    ticket.TotalPrice = ticket.Number * 2 * priceFac;
                    ticket.Price = 2 * priceFac;
                }
                else if (all <= 12)
                {
                    ticket.TotalPrice = ticket.Number * 3 * priceFac;
                    ticket.Price = 3 * priceFac;
                }
                else if (all <= 18)
                {
                    ticket.TotalPrice = ticket.Number * 4 * priceFac;
                    ticket.Price = 4 * priceFac;
                }
                else if (all <= 24)
                {
                    ticket.TotalPrice = ticket.Number * 5 * priceFac;
                    ticket.Price = 5 * priceFac;
                }
                else if (all <= 30)
                {
                    ticket.TotalPrice = ticket.Number * 6 * priceFac;
                    ticket.Price = 6 * priceFac;
                }
            }
        }
        /// <summary>
        /// 检测用户是否合法并买票
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public  int buyTicket(string userName, string userPassword, Ticket ticket)
        {
            try
            {
                using (AppDbEntities context = new AppDbEntities())
                {
                    bool isOk = false;
                    string userPasswordTmp = MD5.generateMD5(userPassword);
                    var users = from t in context.UserInfoes
                                where t.UserName == userName && t.Password == userPasswordTmp
                                select t;
                    foreach (var item in users)
                    {
                        if (item.MoneySum - ticket.TotalPrice < 0)
                        {
                            return -1;//金额不足返回-1
                        }
                        else
                        {
                            item.MoneySum -= ticket.TotalPrice;
                            //创建购票细节对象
                            DateTime dt = DateTime.Now;
                            UserDetails userDetail = new UserDetails()
                            {
                                Name = item.Name,
                                UserName = item.UserName,
                                LineNumber = ticket.LineNumber,
                                StartStation = ticket.Source,
                                EndStation = ticket.Destination,
                                TicketsNum = ticket.Number,
                                Time = dt,
                                Money = ticket.TotalPrice,
                                StartTag = ticket.SourceNum,
                                EndTag = ticket.DestinationNum,
                                TicketsState = "未使用"
                            };
                            context.UserDetails.Add(userDetail);
                            isOk = true;
                        }
                    }
                    if (isOk)
                    {
                        context.SaveChanges();
                        return 1;//购票成功返回1
                    }
                }
            }
            catch (Exception ee)
            {
              //  MessageBox.Show(ee.Message+"路线号:"+ticket.LineNumber+"起始站号:"+ticket.SourceNum+"终点站号:"+ticket.DestinationNum);
                return 0;//异常出错返回0
            }
            return 2;//用户信息出错返回2
        }
    }
}
