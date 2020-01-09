///Author: Humor Logic
///www.humorlogic.com

# region Include
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Core;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls.Primitives;
using System.Numerics;

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
        private string selectPortName;

        private Boolean watcherStarted;
        private Boolean isAllDevicesEnumerated;

        private ObservableCollection<DeviceListEntry> listOfDevice;
        private List<string> AllPortName = new List<string>();
        private Dictionary<string, string> serialPortDeviceDic = new Dictionary<string, string>();
        private string message;
        public List<SerialPortSettingsModel> BaudRate { get; private set; }

        private double controllerAeraRadius = 75d;
        private double controllerRadius = 25d;
        private bool controllerPressed = false;

        private int leftMotorSpeed, rightMotorSpeed;
        private char carDir = 'f';
        private int speed = 0;
        private int tempOffset = 0;
        private int offset = 2;
        private int steer = 90;
        private int count = 0;
        private bool motorMoveState = true;
        private Vector2 controllerInitPos;

        private MainPage rootPage = MainPage.Current;

        #endregion

        #region Constructor
        public Scenario1()
        {
            this.InitializeComponent();

            watcherStarted = false;
            isAllDevicesEnumerated = false;

            listOfDevice = new ObservableCollection<DeviceListEntry>();
            DeviceListSource.Source = listOfDevice;
            InitializeDeviceWatcher();
            InitComboxItem();
            StartDeviceWatchers();

            // ConfigureUIEvents();
            Loaded += OnLoaded;

        }

        #endregion

        #region Methods

        private void OnLoaded(object sender, RoutedEventArgs args)
        {
            ConfigureUIEvents();

        }

        #region Serial Port Connect
        private async void ListAvailablePorts()
        {
            try
            {
                aqs = SerialDevice.GetDeviceSelector();
                var dis = await DeviceInformation.FindAllAsync(aqs);

                for (int i = 0; i < dis.Count; i++)
                {
                    //listOfDevice.Add(dis[i]);
                    AllPortName.Add(dis[i].Name);
                    serialPortDeviceDic.Add(dis[i].Name, dis[i].Id);
                }

                DeviceListSource.Source = AllPortName;
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
        private void AddDeviceToList(DeviceInformation deviceInformation, string deviceSelector)
        {
            var match = FindDevice(deviceInformation.Id);
            if (match == null)
            {
                match = new DeviceListEntry(deviceInformation, deviceSelector);
                listOfDevice.Add(match);
                serialPortDeviceDic[deviceInformation.Name] = deviceInformation.Id;
            }
        }

        private DeviceListEntry FindDevice(string deviceID)
        {
            if (deviceID != null)
            {
                foreach (DeviceListEntry entry in listOfDevice)
                {
                    if (entry.DeviceInformation.Id == deviceID)
                    {
                        return entry;
                    }
                }
            }
            return null;
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
            dataWriteObject.WriteString(carDir.ToString());
            dataWriteObject.WriteByte((byte)speed);
            dataWriteObject.WriteByte((byte)steer);
            // dataWriteObject.WriteString(message);

            // dataWriteObject.WriteByte((byte)leftMotorSpeed);
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
                // UpdateAvailablePorts(args);
                AddDeviceToList(args, aqs);
            }));

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

        #endregion

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
            string selection = serialPortDeviceDic[selectPortName];
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

                TipText.Text = selectPortName + "已打开";
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }

        }

        private void PortNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //BText.Text = PortNameComboBox.SelectedItem.ToString() + "串口被选择";
            var selection = PortNameComboBox.SelectedItem;
            DeviceListEntry entry = (DeviceListEntry)selection;
            TipText.Text = entry.InstancePortName + "已选择";
            selectPortName = entry.InstancePortName;
        }

        private void PortNameComboBox_DropDownOpened(object sender, object e)
        {


        }

        private async void SendMsg_Click(object sender, RoutedEventArgs e)
        {

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
            carDir = 'F';
            speed = (int)SpeedSlider.Value;
            await SetMotorSpeed(speed);
            //if (serialPort != null)
            //{
            //    // Create the DataWriter object and attach to OutputStream
            //    dataWriteObject = new DataWriter(serialPort.OutputStream);

            //    //Launch the WriteAsync task to perform the write
            //    await WriteAsync();
            //}

        }

        private async void BackwardBtn_Click(object sender, RoutedEventArgs e)
        {
            carDir = 'B';
            speed = (int)SpeedSlider.Value;
            await SetMotorSpeed(speed);
            //if (serialPort != null)
            //{
            //    // Create the DataWriter object and attach to OutputStream
            //    dataWriteObject = new DataWriter(serialPort.OutputStream);

            //    //Launch the WriteAsync task to perform the write
            //    await WriteAsync();
            //}
        }

        private async void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            carDir = 'S';
            speed = 0;
            //speed = 0;
            //if (serialPort != null) ;
            //{
            //    // Create the DataWriter object and attach to OutputStream
            //    dataWriteObject = new DataWriter(serialPort.OutputStream);

            //    //Launch the WriteAsync task to perform the write
            //    await WriteAsync();
            //}
            //controllerPressed = false;
            //JoystickTransform.X = 0;
            //JoystickTransform.Y = 0;
            //speed = 203;
            await SetMotorSpeed(speed);

        }


        #endregion

        #region Joystick

        private void ConfigureUIEvents()
        {
            Window.Current.Content.PointerMoved += OnPointerMoved;
            Window.Current.Content.PointerReleased += OnPointerReleased;
        }

        private async void OnPointerMoved(object sender, PointerRoutedEventArgs eventArgs)
        {
            //   Debug.WriteLine("pointer pressed moved");
            if (!controllerPressed)
                return;

            double x = eventArgs.GetCurrentPoint(ControllerAera).Position.X - controllerAeraRadius;
            double y = eventArgs.GetCurrentPoint(ControllerAera).Position.Y - controllerAeraRadius;
            double side = Math.Sqrt(x * x + y * y);
            double limitLength = controllerAeraRadius - controllerRadius;

            if (side < limitLength)
            {
                JoystickTransform.X = x;
                JoystickTransform.Y = y;
             //   Debug.WriteLine(side.ToString());


                if (y < 0)
                {
                    speed = (int)(-y + 1) * 4;// I set the motor PWM under 200, the biggest of y is 50
                    if (Math.Abs(side-tempOffset) > offset)
                    {
                        count++;
                        Debug.WriteLine(count);
                        tempOffset = (int)side;
                        carDir = 'F';
                        steer = (int)(90 - x);
                        await SetMotorSpeed(speed);
                        MotorInfoText.Text = "前进速度(m/s)：" + String.Format("{0:f}", speed) + "     转向(°):" + steer.ToString();
                        
                    }

                }
                else if (y > 0)
                {
                    speed = (int)(y * 4);
                    if (Math.Abs(side - tempOffset) > offset)
                    {
                        tempOffset = (int)side;
                        carDir = 'B';
                        steer = (int)(90 - x );
                        await SetMotorSpeed(speed);
                        MotorInfoText.Text = "后退速度(m/s)：" + String.Format("{0:f}", speed) + "     转向(°):" + steer.ToString();

                    }
                }
                else
                {
                    carDir = 'S';
                    speed = 0;
                    steer = 90;
                    await SetMotorSpeed(speed);
                    MotorInfoText.Text = "速度(m/s)：" + String.Format("{0:f}", speed) + "     转向(°):" + steer.ToString();
                }
            }
            else
            {
                JoystickTransform.X = limitLength * (x / side); // cos(x)
                JoystickTransform.Y = limitLength * (y / side);// sin(x)
            }

        }

        private async void OnPointerReleased(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            //   Debug.WriteLine("pointer realeased");

            controllerPressed = false;

            JoystickTransform.X = 0;
            JoystickTransform.Y = 0;

            speed = 0;
            steer = 90;
            tempOffset = 0;
            await SetMotorSpeed(speed);
            MotorInfoText.Text = "速度(m/s)：" + String.Format("{0:f}", speed) + "     转向(°):" + steer.ToString();
        }

        private void Controller_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("pointer pressed");
            controllerPressed = true;
        }


        private void Controller_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("pointer released");
            controllerPressed = false;
            JoystickTransform.X = 0;
            JoystickTransform.Y = 0;
        }

        private async void RightBtn_Click(object sender, RoutedEventArgs e)
        {
            carDir = 'R';
            speed = (int)SpeedSlider.Value;
            await SetMotorSpeed(speed);
        }

        private async void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            carDir = 'L';
            speed = (int)SpeedSlider.Value;
            await SetMotorSpeed(speed);

        }

        private void SpeedSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            //Debug.WriteLine(e.NewValue);
            SliderSpeedText.Text = "速度：" + e.NewValue.ToString();
            speed = (int)e.NewValue;
        }

        #endregion

        #region Motor Control
        private async Task SetMotorSpeed(int speed)
        {

            leftMotorSpeed = speed;
            try
            {
                if (serialPort != null)
                {
                    // Create the DataWriter object and attach to OutputStream
                    dataWriteObject = new DataWriter(serialPort.OutputStream);

                    //Launch the WriteAsync task to perform the write
                    await WriteAsync();
                }
                else return;
            }
            catch (Exception e)
            {
                Msgbox.Show(e.Message);
            }
            finally
            {
                if (dataWriteObject != null)
                {
                    dataWriteObject.DetachStream();
                    dataWriteObject = null;
                }
            }

        }
        #endregion

        #endregion




    }
}

