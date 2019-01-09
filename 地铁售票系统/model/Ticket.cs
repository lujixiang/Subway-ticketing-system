using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 地铁售票系统.model
{
    /// <summary>
    /// 票的模型
    /// </summary>
    public class Ticket
    {
        private int price;//票价
        private string source;//起始站
        private string destination;//终点站
        private string lineNumber;//路线，几号线
        private string type;//类型，单程票还是往返票
        private int totalPrice;//总价
        private int number;//票的数量
        private int destinationNum;//终点站标号
        private int sourceNum;//起始站标号
        private bool isDirect = false;
        private int flag;//标志位，快捷购票时使用
        /// <summary>
        /// 属性,票价
        /// </summary>
        public int Price
        {
            get;
            set;
        }
        /// <summary>
        /// 属性,起始站
        /// </summary>
        public string Source
        {
            get;
            set;
        }
        /// <summary>
        /// 属性,目的站
        /// </summary>
        public string Destination
        {
            get;
            set;
        }
        /// <summary>
        /// 属性,几号线
        /// </summary>
        public string LineNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 属性,类型,单程票，往返票
        /// </summary>
        public string Type
        {
            get;
            set;
        }
        /// <summary>
        /// 属性,总价(一共应付金额)
        /// </summary>
        public int TotalPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 属性,票的数量
        /// </summary>
        public int Number
        {
            get;
            set;
        }
        /// <summary>
        /// 属性，终点站标号
        /// </summary>
        public int DestinationNum
        {
            get;
            set;
        }
        /// <summary>
        /// 属性，起始站标号
        /// </summary>
        public int SourceNum
        {
            get;
            set;
        }
        /// <summary>
        /// 判断是否是通过立即购票
        /// </summary>
        public bool IsDirect
        {
            get;
            set;
        }
        /// <summary>
        /// 属性,标志位
        /// </summary>
        public int Flag
        {
            get;
            set;
        }
    }
}
