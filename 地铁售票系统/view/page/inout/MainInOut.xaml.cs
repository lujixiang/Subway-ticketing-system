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
using 地铁售票系统.view.window;

namespace 地铁售票系统.view.page.inout
{
    /// <summary>
    /// MainInOut.xaml 的交互逻辑
    /// </summary>
    public partial class MainInOut : Page
    {
        private InAndOut mainW;
        public MainInOut(InAndOut main)
        {
            InitializeComponent();
            this.mainW = main;
        }


        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel stackpanel = sender as StackPanel;
            if (stackpanel != null)
            {
                switch (stackpanel.Name)
                {
                    case "in":
                        mainW.inout_frame.Content = new In(mainW);
                        break;
                    case "out":
                        mainW.inout_frame.Content = new Out(mainW);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
