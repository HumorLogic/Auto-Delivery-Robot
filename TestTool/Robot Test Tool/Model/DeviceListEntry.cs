using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace Robot_Test_Tool
{
    /// <summary>
    /// The class will only expose properties from DeviceInformation that are going to be used in this program.
    /// This class is used by the UI to display device specific information so that
    /// the user can identify which device to use.
    /// </summary>
    public class DeviceListEntry
    {
        private DeviceInformation device;
        private string deviceSelector;

        public string InstanceID
        {
            get => device.Properties[DeviceProperties.DeviceInstanceID] as string;
        }

        public string InstancePortName
        {
            get => device.Name;
        }

        public DeviceInformation DeviceInformation
        {
            get => device;
        }

        public DeviceListEntry(DeviceInformation deviceInformation,string deviceSelector)
        {
            device = deviceInformation;
            this.deviceSelector = deviceSelector;
        }
    }
}
