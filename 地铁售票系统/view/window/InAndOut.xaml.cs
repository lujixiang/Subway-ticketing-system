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
using 地铁售票系统.view.page.inout;

namespace 地铁售票系统.view.window
{
    /// <summary>
    /// In.xaml 的交互逻辑
    /// </summary>
    public partial class InAndOut : Window
    {
        public  Page[] pages = new Page[3];
        public string[] line1 = {"",
                                      "河南大学","郑州大学","科学大道","梧桐街","化工路","铁炉",
                                      "须水","西流湖","柿园","秦岭路","工人文化宫","碧沙岗",
                                      "绿城广场","大学路","铁郑州站","二七广场","人民南","紫荆山",
                                      "东明路","燕庄","会展中心","黄河东路","农业东路","七里河",
                                      "铁路东站","博学路","姚桥","龙子湖","穆庄"
                                   };
        public string[] line2 ={"",
"堤湾","大河路","毛庄","中州大学","金洼","王岗",
"西弓装","广播台","新龙","沙门","北环路","东风渠",
"郑州动物园","黄河路","紫荆山","东大街","陇海东路","帆布厂街",
"五里堡","陈家门","耿庄","宇通路","向阳路",
};

        public string[] line3 ={"",

"白松路","水牛张","祥营","科学大道","雪松路","垂柳路",
"南阳寨","兴隆铺","张寨","小孟寨","海棠寺","大石桥",
"康泰路","二七广场","东大街","城东路","未来路","凤凰台",
"中州大道","白庄","中周","东二十里铺","小店","莲湖",
"莆田","西营岗","单庄","洪云路","托月路","金光南路"
};

        public string[] line4 ={"",
"老鸦陈","河南省体育中心","二十铺","陈砦","沙门","桑园",
"郑州国家森林公园","南宋庵","龙湖","沙庄","天泽街","如意湖",
"郑州国际会展中心","建业","中州大道","货栈街","七里河","逸心公园",
"文德路","郎庄","曹古寺","托月路",

};


        public string[] line5 ={"",
"桐柏路","朱屯","小孟寨","郑州水上乐园","中原农业博物馆","郑州动物园",
"关虎屯","姚砦","重意路","天泽街","东外环","祭城",
"王府李","铁路郑州东站","小店","经开北","经开中","经开西",
"鲍湖","七里河","槐树林新村","陈家门","五里堡","冯庄",
"郑州客运总站","郑州航空工业管理学院","嵩山南路","帝湖","淮河路","罗达庙",
};


        public string[] line6 ={"",
"郑寨","长江路","郑州航空工业管理学院","古玩城","郑州铁路局","乔家门",
"陇海东路","杨庄","未来路","英协路","建业","百富街",
"黄河东路","东外环","东风路","彩云街","文苑西路","龙子湖西",
"龙子湖","龙子湖东",

};
        public InAndOut()
        {
            InitializeComponent();
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            pages[0] = new MainInOut(this);
            pages[1] = new In(this);
            pages[2] = new Out(this);
            inout_frame.Content = pages[0];
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
        #endregion


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
        #endregion
    }
}