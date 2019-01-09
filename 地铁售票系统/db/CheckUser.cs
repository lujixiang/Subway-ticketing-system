using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using 地铁售票系统.model;
using 地铁售票系统.utils;

namespace 地铁售票系统.db
{
    /// <summary>
    /// 用户验证相关类
    /// </summary>
    public static class CheckUser
    {
        /// <summary>
        /// 检测管理员是否合法
        /// </summary>
        /// <param name="adminName"></param>
        /// <param name="adminPassword"></param>
        /// <returns></returns>
        public static bool checkAdminIsLegal(string adminName, string adminPassword)
        {
            try
            {
                using (AppDbEntities appDb = new AppDbEntities())
                {
                    var admins = from t in appDb.ManagerInfoes
                                 where t.Name == adminName && t.Password == adminPassword
                                 select t;
                    foreach (var item in admins)
                    {
                        return true;
                    }
                }
            }
            catch(Exception ee)
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// 用户账号是否正确
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool checkUserIsLegal(string userName)
        {
            try
            {
                using (AppDbEntities context = new AppDbEntities())
                {
                    var users = from t in context.UserInfoes
                                where t.UserName == userName
                                select t;
                    foreach (var v in users)
                    {
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
        /// 检查用户是否合法
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public static bool checkUserIsLegal(string userName, string userPassword)
        {
            try
            {
                string userPsdTmp = MD5.generateMD5(userPassword);
                using (AppDbEntities context = new AppDbEntities())
                {
                    var users = from t in context.UserInfoes
                                where t.UserName == userName && t.Password == userPsdTmp
                                select t;
                    foreach (var v in users)
                    {
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
        /// 获取用户余额
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static decimal getBalance(string userName)
        {
            try
            {
                using (AppDbEntities context = new AppDbEntities())
                {
                    var users = from t in context.UserInfoes
                                where t.UserName == userName
                                select t;
                    foreach (var v in users)
                    {
                        return v.MoneySum;
                    }
                }
            }
            catch (Exception ee)
            {
                return 0;
            }
            return 0;
        }
        /// <summary>
        /// 为公司充钱
        /// </summary>
        /// <param name="rechargeMoney"></param>
        /// <returns></returns>
        public static bool rechargeToCo(decimal rechargeMoney)
        {
            try
            {
                using (AppDbEntities context = new AppDbEntities())
                {
                    var company = from w in context.CompanyInfoes
                                  where w.Id == 1
                                  select w;
                    foreach (var v in company)
                    {
                        v.MoneySum += rechargeMoney;
                    }
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ee)
            {
                return false;
            }
        }
        /// <summary>
        /// 用户充值
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="moneySum"></param>
        /// <returns></returns>
        public static bool rechargeMoney(string userName, decimal moneySum)
        {
            try
            {
                using (AppDbEntities context = new AppDbEntities())
                {
                    var users = from t in context.UserInfoes
                                where t.UserName == userName
                                select t;
                    foreach (var v in users)
                    {

                        v.MoneySum += moneySum;
                    }
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ee)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除账户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        public static bool deleteUser(string userName, string userPassword)
        {
            try
            {
                string userPsdTmp = MD5.generateMD5(userPassword);
                using (AppDbEntities context = new AppDbEntities())
                {
                    var users = from t in context.UserInfoes
                                where t.UserName == userName && t.Password == userPsdTmp
                                select t;
                    foreach (var v in users)
                    {
                        context.UserInfoes.Remove(v);
                    }
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ee)
            {
                return false;
            }
           
        }
        /// <summary>
        /// 检测用户是否合法并买票
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public static int buyTicket(string userName, string userPassword, Ticket ticket)
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
                                StartStation = ticket.Source,
                                EndStation = ticket.Destination,
                                TicketsNum = ticket.Number,
                                Time = dt,
                                Money = ticket.TotalPrice,
                             
                              
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
                return 0;//异常出错返回0
            }
            return 2;//用户信息出错返回2
        }
    }
}
