using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;


namespace Robot_Test_Tool
{
  
    public partial class MainPage: Page
    {
        public const string APP_NAME = "移动机器人测试工具";

        List<Scenario> scenarios = new List<Scenario>
        {
            new Scenario(){Title="Robot Control",ClassType=typeof(Scenario1) },
            new Scenario(){Title="Serial Port Tool",ClassType=typeof(Scenario2) },
            new Scenario(){Title="关于",ClassType=typeof(Scenario3)}

         };
    }

    public class Scenario
    {
        public string Title { get; set; }
        public Type ClassType { get; set; }
    }


}
