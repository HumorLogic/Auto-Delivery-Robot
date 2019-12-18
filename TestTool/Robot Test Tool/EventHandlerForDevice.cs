using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace Robot_Test_Tool
{
    //class EventHandlerForDevice
    //{
    //    #region Field字段

    //    /// <summary>
    //    /// EventHandlerForDevice的单例
    //    /// </summary>
    //    private static EventHandlerForDevice eventHandlerForDevice;

    //    /// <summary>
    //    /// 用于同步线程以避免eventHandleForDevice的多个实例化
    //    /// </summary>
    //    private static Object singletonCreationLock = new object();

    //    private DeviceWatcher deviceWatcher;

    //    private SuspendingEventHandler appSuspendEventHandler;
    //    private EventHandler<Object> appResumeEventHandler;

    //    private TypedEventHandler<EventHandlerForDevice, DeviceInformation> deviceConnectedCallback;

    //    private Boolean watcherSuspended;
    //    private Boolean watcherStarted;
    //    #endregion


    //    #region Property属性

    //    /// <summary>
    //    /// 强制执行单例模式，以便只有一个对象处理与串口设备相关的应用程序事件
    //    /// </summary>
    //    public static EventHandlerForDevice Current
    //    {
    //        get {
    //            if (eventHandlerForDevice == null)
    //            {
    //                lock (singletonCreationLock)
    //                {
    //                    if (eventHandlerForDevice == null)
    //                    {
    //                        CreateNewEventHandlerForDevice();
    //                    }
    //                }
    //            }
    //            return eventHandlerForDevice; }
            
    //    }

    //    /// <summary>
    //    /// 创建EventHandlerForDevice的新实例，能够自动重新连接，并将其用作当前实例。 
    //    /// </summary>
    //    private static void CreateNewEventHandlerForDevice()
    //    {
    //        eventHandlerForDevice = new EventHandlerForDevice();
    //    }

    //    public TypedEventHandler<EventHandlerForDevice,DeviceInformation> OnDeviceConnected
    //    {
    //        get{ return deviceConnectedCallback; }
    //        set{ deviceConnectedCallback = value; }
    //    }

    //    #endregion


    //    #region Method方法

    //    /// <summary>
    //    /// 注册应用程序暂停/恢复事件。
    //    /// </summary>
    //    private void RegisterForAppEvents()
    //    {
    //        appSuspendEventHandler = new SuspendingEventHandler(EventHandlerForDevice.Current.OnAppSuspension);
    //        appResumeEventHandler = new EventHandler<object>(EventHandlerForDevice.Current.OnAppResume);

    //        //当应用程序退出和应用程序暂停时，将引发此事件。
    //        App.Current.Suspending += appSuspendEventHandler;
    //        App.Current.Resuming += appResumeEventHandler;
    //    }
    //    private void UnregisterAppEvents()
    //    {
    //        //当应用程序退出和应用程序暂停时，将引发此事件。 
    //        App.Current.Suspending -= appSuspendEventHandler;
    //        appSuspendEventHandler = null;

    //        App.Current.Resuming -= appResumeEventHandler;
    //        appResumeEventHandler = null;
    //    }

    //    /// <summary>
    //    /// 开始搜索设备，并订阅枚举设备
    //    /// </summary>
    //    private void StartDeviceWatcher()
    //    {
    //        watcherStarted = true;

    //        if((deviceWatcher.Status!=DeviceWatcherStatus.Started)
    //            && (deviceWatcher.Status != DeviceWatcherStatus.EnumerationCompleted))
    //        {
    //            deviceWatcher.Start();
    //        }
    //    }


    //    /// <summary>
    //    /// 如果已经实例化了一个串行对象(打开了设备的一个handler)，我们必须在应用程序暂停之前关闭它，因为如果我们不这样做，API就会自动关闭它。 
    //    /// </summary>
    //    /// <param name="sender"></param>
    //    /// <param name="e"></param>
    //    private void OnAppSuspension(object sender, SuspendingEventArgs e)
    //    {
    //        if (watcherStarted) {
    //            watcherSuspended = true;
    //            //StopDeviceWatcher();
    //        }
    //        else
    //        {
    //            watcherSuspended = false;
    //        }

    //        //CloseCurrentlyConnectedDevice();
    //    }

    //    /// <summary>
    //    /// 继续进入应用程序时，重新打开对串行设备的操作。
    //    /// </summary>
    //    /// <param name="sender"></param>
    //    /// <param name="arg"></param>
    //    private void OnAppResume(Object sender, Object arg)
    //    {
    //        if (watcherSuspended)
    //        {
    //            watcherSuspended = false;
    //            StartDeviceWatcher();
    //        }
    //    }
     
  

    //    #endregion


    //}
}
