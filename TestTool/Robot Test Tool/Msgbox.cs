using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Robot_Test_Tool
{
    public static class Msgbox
    {
        /// <summary>
        /// 消息提示窗
        /// </summary>
        /// <param name="tipText"></param>
        static public async void Show(string tipText)
        {
            var dialog = new MessageDialog(tipText);
           
            //dialog.Title = "操作提示";
            await dialog.ShowAsync();
        }
    }
}





