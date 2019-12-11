using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using System.Collections.ObjectModel;
using Windows.Storage.Streams;
using System.Threading;
using System.Threading.Tasks;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Robot_Test_Tool
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Scenario1 : Page
    {

        private SerialDevice serialPort = null;
        DataWriter dataWriteObject = null;
        private ObservableCollection<DeviceInformation> listOfDevice;
        private List<string> AllPortName = new List<string>();
        private Dictionary<string, object> serialPortDeviceDic = new Dictionary<string, object>();

        UInt32[] baudRate = { 9600, 19200, 38400, 57600, 115200 };

        private string message;
        public Scenario1()
        {
            this.InitializeComponent();
            listOfDevice = new ObservableCollection<DeviceInformation>();
            ListAvailablePorts();
            InitComboxItem();
        }


        private async void ListAvailablePorts()
        {
            try
            {
                string aqs = SerialDevice.GetDeviceSelector();
                var dis = await DeviceInformation.FindAllAsync(aqs);

                for (int i = 0; i < dis.Count; i++)
                {
                    listOfDevice.Add(dis[i]);
                    AllPortName.Add(dis[i].Name);
                    serialPortDeviceDic.Add(dis[i].Name, dis[i]);
                }

                DeviceListSource.Source = AllPortName;
                //PortNameComboBox.SelectedItem = null;
                //TipText.Text = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("List AvailablePorts Failed");
                Console.WriteLine(ex);
            }
        }


        /// <summary>
        /// 打开串口按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SerialConnectBtn_Click(object sender, RoutedEventArgs e)
        {

            var selection = serialPortDeviceDic[PortNameComboBox.SelectedItem.ToString()];
            if (selection == null)
            {
                TipText.Text = "请选择串口号并连接";
                return;
            }
            else if (BaudRateComboBox.SelectedItem == null)
            {
                Msgbox.Show("请选择波特率!");
                SerialConnectBtn.IsEnabled = false;
                return;
            }

            DeviceInformation entry = (DeviceInformation)selection;
            try
            {
                serialPort = await SerialDevice.FromIdAsync(entry.Id);
                if (serialPort == null) return;

                // Configure serial settings
                serialPort.WriteTimeout = TimeSpan.FromMilliseconds(1000);
                serialPort.ReadTimeout = TimeSpan.FromMilliseconds(1000);
                serialPort.BaudRate = baudRate[BaudRateComboBox.SelectedIndex];
                //serialPort.BaudRate = (uint)BaudRateComboBox.SelectedValue;
                serialPort.Parity = SerialParity.None;
                serialPort.StopBits = SerialStopBitCount.One;
                serialPort.DataBits = 8;
                serialPort.Handshake = SerialHandshake.None;

                TipText.Text = PortNameComboBox.SelectedItem.ToString() + "已打开";
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 初始化波特率下拉的选择内容
        /// </summary>
        private void InitComboxItem()
        {
            foreach (int i in baudRate)
            {
                BaudRateComboBox.Items.Add(i);
            }
        }


        private void PortNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //BText.Text = PortNameComboBox.SelectedItem.ToString() + "串口被选择";
            TipText.Text = PortNameComboBox.SelectedItem.ToString() + "已选择";
        }

        private void PortNameComboBox_DropDownOpened(object sender, object e)
        {
            ////serial.SerialList();
            //if (AllPortName.Count == 0)
            //{
            //    AllPortName.Clear();
            //    PortNameComboBox.Items.Clear();
            //}
            //else
            //{
            //    foreach (string name in AllPortName)
            //    {

            //        if (PortNameComboBox.Items.Contains(name))
            //        {
            //            continue;
            //        }
            //        else
            //        {
            //            PortNameComboBox.Items.Add(name);
            //        }
            //    }
            //}
        }



        private async void SendMsg_Click(object sender, RoutedEventArgs e)
        {
            //port.Write("111");
            //try
            //{

            //}catch(Exception ex)
            //{

            //}
            //Msgbox.Show("请选择串口！");
            if (serialPort != null)
            {
                // Create the DataWriter object and attach to OutputStream
                dataWriteObject = new DataWriter(serialPort.OutputStream);

                //Launch the WriteAsync task to perform the write
                await WriteAsync();
            }
        }

        private async Task WriteAsync()
        {
            //throw new NotImplementedException();
            Task<UInt32> storeAsyncTask;

            dataWriteObject.WriteString(message);
            storeAsyncTask = dataWriteObject.StoreAsync().AsTask();
            UInt32 bytesWritten = await storeAsyncTask;
        }

        private void BaudRateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //BText.Text = BaudRateComboBox.Text;
            //BText.Text = BaudRateComboBox.SelectedItem.ToString();
            SerialConnectBtn.IsEnabled = true;
            SerialConnectBtn.IsChecked = false;
            TipText.Text = BaudRateComboBox.SelectedItem.ToString() + " 波特率已选择";
        }

        private async void ForwardBtn_Click(object sender, RoutedEventArgs e)
        {
            message = "1";
            if (serialPort != null)
            {
                // Create the DataWriter object and attach to OutputStream
                dataWriteObject = new DataWriter(serialPort.OutputStream);

                //Launch the WriteAsync task to perform the write
                await WriteAsync();
            }

        }

        private async void BackwardBtn_Click(object sender, RoutedEventArgs e)
        {
            message = "2";
            if (serialPort != null)
            {
                // Create the DataWriter object and attach to OutputStream
                dataWriteObject = new DataWriter(serialPort.OutputStream);

                //Launch the WriteAsync task to perform the write
                await WriteAsync();
            }
        }

        private async void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            message = "0";
            if (serialPort != null)
            {
                // Create the DataWriter object and attach to OutputStream
                dataWriteObject = new DataWriter(serialPort.OutputStream);

                //Launch the WriteAsync task to perform the write
                await WriteAsync();
            }
        }
    }


}

