///Author: Humor Logic
///www.humorlogic.com

# region Include
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Core;

using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Foundation;

using System.Collections.ObjectModel;
using Windows.Storage.Streams;
using System.Threading;
using System.Threading.Tasks;
using Robot_Test_Tool.Model;
using System.Diagnostics;

#endregion

namespace Robot_Test_Tool
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Scenario1 : Page
    {
        #region Members

        private SerialDevice serialPort = null;
        private string aqs = null;
        private DeviceWatcher deviceWatcher;
        DataWriter dataWriteObject = null;

        private Dictionary<DeviceWatcher, String> mapDeviceWatchersToDeviceSelector;
        private Boolean watcherStarted;
        private Boolean isAllDevicesEnumerated;

        private ObservableCollection<DeviceInformation> listOfDevice;
        private List<string> AllPortName = new List<string>();
        private Dictionary<string, string> serialPortDeviceDic = new Dictionary<string, string>();
        private string message;
        public List<SerialPortSettingsModel> BaudRate { get; private set; }
        UInt32[] baudRate = { 9600, 19200, 38400, 57600, 115200 };

        private MainPage rootPage = MainPage.Current;

        #endregion

        #region Constructor
        public Scenario1()
        {
            this.InitializeComponent();

            mapDeviceWatchersToDeviceSelector = new Dictionary<DeviceWatcher, string>();
            watcherStarted = false;
            isAllDevicesEnumerated = false;
            DeviceListSource.Source = AllPortName;

            listOfDevice = new ObservableCollection<DeviceInformation>();
            InitializeDeviceWatcher();
            //ListAvailablePorts();
            InitComboxItem();
           // InitializeDeviceWatcher();
            StartDeviceWatchers();
        }

        #endregion

        #region Methods
        private async void ListAvailablePorts()
        {
            try
            {
                aqs = SerialDevice.GetDeviceSelector();
                var dis = await DeviceInformation.FindAllAsync(aqs);

                for (int i = 0; i < dis.Count; i++)
                {
                    listOfDevice.Add(dis[i]);
                    AllPortName.Add(dis[i].Name);
                    serialPortDeviceDic.Add(dis[i].Name, dis[i].Id);
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
        /// Update Available Serial Port to UI List
        /// 更新可选的串口到UI列表中
        /// </summary>
        /// <param name="deviceInfo">DeviceInformation Type</param>
        public void UpdateAvailablePorts(DeviceInformation deviceInfo)
        {
           // DeviceListSource.Source = AllPortName;
            if (!AllPortName.Contains(deviceInfo.Name))
            {
                AllPortName.Add(deviceInfo.Name);
                serialPortDeviceDic.Add(deviceInfo.Name, deviceInfo.Id);
            }
            else return;
           
            //DeviceListSource.Source = AllPortName;
        }

        /// <summary>
        /// Close the serial port device关闭串口设备
        /// </summary>
        private void CloseDevice()
        {
            if (serialPort != null)
            {
                serialPort.Dispose();
            }
            serialPort = null;
            listOfDevice.Clear();
            AllPortName.Clear();
            serialPortDeviceDic.Clear();
        }


        private async Task WriteAsync()
        {
            //throw new NotImplementedException();
            Task<UInt32> storeAsyncTask;

            dataWriteObject.WriteString(message);
            storeAsyncTask = dataWriteObject.StoreAsync().AsTask();
            UInt32 bytesWritten = await storeAsyncTask;
        }

        /// <summary>
        /// 初始化DeviceWatcher
        /// </summary>
        private void InitializeDeviceWatcher()
        {
            // var deviceSelector = SerialDevice.GetDeviceSelector();
            aqs = SerialDevice.GetDeviceSelector();
            deviceWatcher = DeviceInformation.CreateWatcher(aqs);
            AddDeviceWatcher(deviceWatcher, aqs);
        }

        private void AddDeviceWatcher(DeviceWatcher deviceWatcher, String deviceSelector)
        {
            deviceWatcher.Added += new TypedEventHandler<DeviceWatcher, DeviceInformation>(this.OnDeviceAdded);
            deviceWatcher.Removed += new TypedEventHandler<DeviceWatcher, DeviceInformationUpdate>(this.OnDeviceRemoved);
            deviceWatcher.EnumerationCompleted += new TypedEventHandler<DeviceWatcher, Object>(this.OnDeviceEnumerationComplete);

            // mapDeviceWatchersToDeviceSelector.Add(deviceWatcher, deviceSelector);
        }

        private async void OnDeviceAdded(DeviceWatcher sender, DeviceInformation args)
        {
            //TipText.Text = args.Name + "加入";
            Debug.WriteLine(args.Name + " Serial Port Added");
            
            await rootPage.Dispatcher.RunAsync(
            CoreDispatcherPriority.Normal,
            new DispatchedHandler(() =>
            {
                UpdateAvailablePorts(args);
            }));



            //try
            //{
            //    TipText.Text = args.Name + "加入";
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //}


        }

        private void OnDeviceEnumerationComplete(DeviceWatcher sender, object args)
        {
            
            Debug.WriteLine("Device Enumeration Completed");
        }

        private void OnDeviceRemoved(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            Debug.WriteLine("Device Removed");
        }


        /// <summary>
        /// 打开Device Watcher
        /// </summary>
        private void StartDeviceWatchers()
        {
            //Start all device watcher
            watcherStarted = true;
            isAllDevicesEnumerated = false;
            deviceWatcher.Start();

            //foreach (DeviceWatcher deviceWatcher in mapDeviceWatchersToDeviceSelector.Keys)
            //{
            //    if((deviceWatcher.Status!=DeviceWatcherStatus.Started)
            //        && (deviceWatcher.Status != DeviceWatcherStatus.EnumerationCompleted))
            //    {
            //        deviceWatcher.Start();
            //    }

            //}
        }


        #region UI Event Method


        /// <summary>
        /// 初始化波特率下拉的选择内容
        /// </summary>
        private void InitComboxItem()
        {

            //foreach (int i in baudRate)
            //{
            //    BaudRateComboBox.Items.Add(i);
            //}
            //BaudRateComboBox.Items.Add(SerialPortSettingsModel.Instance.getBaudRates());
            BaudRate = SerialPortSettingsModel.Instance.getBaudRates();
            for (int i = 0; i < BaudRate.Count; i++)
            {
                BaudRateComboBox.Items.Add(BaudRate[i].BaudRateValue);
            }
        }


        /// <summary>
        /// 打开串口按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SerialConnectBtn_Click(object sender, RoutedEventArgs e)
        {
           string selection = serialPortDeviceDic[PortNameComboBox.SelectedItem.ToString()];
            //var selection = serialPortDeviceDic[PortNameComboBox.SelectedItem.ToString()];
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

         //   DeviceInformation entry = (DeviceInformation)selection;
            try
            {
                // serialPort = await SerialDevice.FromIdAsync(entry.Id);

                serialPort = await SerialDevice.FromIdAsync(selection);
                if (serialPort == null) return;

                // Configure serial settings
                serialPort.WriteTimeout = TimeSpan.FromMilliseconds(1000);
                serialPort.ReadTimeout = TimeSpan.FromMilliseconds(1000);
                serialPort.BaudRate = BaudRate[BaudRateComboBox.SelectedIndex].BaudRateValue;
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
            //ListAvailablePorts();

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

        #endregion


        #endregion

    }
}

