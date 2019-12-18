using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_Test_Tool.Model
{
    public class SerialPortSettingsModel:SingletonBase<SerialPortSettingsModel>
    {

        #region Baud Rate比特率
        public string BaudRateName { get; set; }
        public UInt32 BaudRateValue { get; set; }

        public List<SerialPortSettingsModel> getBaudRates()
        {
            List<SerialPortSettingsModel> returnBaudRates = new List<SerialPortSettingsModel>();
            returnBaudRates.Add(new SerialPortSettingsModel() { BaudRateName = "4800 baud", BaudRateValue = 4800 });
            returnBaudRates.Add(new SerialPortSettingsModel() { BaudRateName = "4800 baud", BaudRateValue = 4800 });
            returnBaudRates.Add(new SerialPortSettingsModel() { BaudRateName = "9600 baud", BaudRateValue = 9600 });
            returnBaudRates.Add(new SerialPortSettingsModel() { BaudRateName = "19200 baud", BaudRateValue = 19200 });
            returnBaudRates.Add(new SerialPortSettingsModel() { BaudRateName = "38400 baud", BaudRateValue = 38400 });
            returnBaudRates.Add(new SerialPortSettingsModel() { BaudRateName = "57600 baud", BaudRateValue = 57600 });
            returnBaudRates.Add(new SerialPortSettingsModel() { BaudRateName = "115200 baud", BaudRateValue = 115200 });
            returnBaudRates.Add(new SerialPortSettingsModel() { BaudRateName = "230400 baud", BaudRateValue = 230400 });
            return returnBaudRates;
        }

        #endregion

    }
}
