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
        
        public static MainPage Current;
        public MainPage()
        {
            this.InitializeComponent();
            Current = this;
            TitleName.Text = APP_NAME;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Populate the scenario list from the SampleConfiguration.cs file
            var itemCollection = new List<Scenario>();
            int i = 1;
            foreach (Scenario s in scenarios)
            {
                // itemCollection.Add(new Scenario { Title = $"{i++}) {s.Title}", ClassType = s.ClassType });
                itemCollection.Add(new Scenario { Title = $" {s.Title}", ClassType = s.ClassType });
            }
            ScenarioControl.ItemsSource = itemCollection;

            if (Window.Current.Bounds.Width < 640)
            {
                ScenarioControl.SelectedIndex = -1;
            }
            else
            {
                ScenarioControl.SelectedIndex = 0;
            }
        }

        public List<Scenario> Scenarios
        {
            get { return this.scenarios; }
        }
        private void PortWrite(string msg)
        {
            //port.Write(msg);
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

        /// <summary>
        /// Called whenever the user changes selection in the scenarios list.  This method will navigate to the respective
        /// sample scenario page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScenarioControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           //Clear the status block when navigating scenarios.
           //NotifyUser(String.Empty, NotifyType.StatusMessage);

            ListBox scenarioListBox = sender as ListBox;
            Scenario s = scenarioListBox.SelectedItem as Scenario;
            if (s != null)
            {
                ScenarioFrame.Navigate(s.ClassType);
                if (Window.Current.Bounds.Width < 640)
                {
                    Splitter.IsPaneOpen = false;
                }
            }
        }


        

        
        public enum NotifyType
        {
            StatusMessage,
            ErrorMessage
        };


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Splitter.IsPaneOpen = !Splitter.IsPaneOpen;
            Console.WriteLine("导航按钮点击");
        }
    }
}
