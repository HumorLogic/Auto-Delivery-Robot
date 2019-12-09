using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.IO.Ports;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Robot_Test_Tool
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        static SerialPort port;
        public MainPage()
        {
            this.InitializeComponent();
            if (port == null)
            {
                port = new SerialPort("COM3", 115200);
                port.Open();
                serial_port_btn.Content = "串口开启";
            }
        }

        private void PortWrite(string msg)
        {
            port.Write(msg);
        }

        //刹车按钮事件
        private void brake_btn_Click(object sender, RoutedEventArgs e)
        {
            PortWrite("0");
        }


        //前进按钮事件
        private void forward_btn_Click(object sender, RoutedEventArgs e)
        {
            PortWrite("1");
        }


        //后退按钮事件
        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            PortWrite("2");
        }
        
       

        private void serial_port_btn_Click(object sender, RoutedEventArgs e)
        {

        }

      
    }
}
