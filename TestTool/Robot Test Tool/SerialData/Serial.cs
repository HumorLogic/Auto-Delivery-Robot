using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;

namespace Robot_Test_Tool.SerialData
{
    class Serial
    {
        private SerialDevice Device = null;
        public List<string> AllPortName = new List<string>();

        /// <summary>
        /// 异步加载串口列表
        /// </summary>
        public async void SerialList()
        {
            string selectors = SerialDevice.GetDeviceSelector();
            DeviceInformationCollection decices = await DeviceInformation.FindAllAsync(selectors);
            if (decices.Any())
            {
                for (int i = 0; i < decices.Count(); i++)
                {
                    if (AllPortName.Contains(decices[i].Name))
                    {
                        continue;
                    }
                    else
                    {
                        AllPortName.Add(decices[i].Name);
                    }
                }
                //PortName = PortName.Distinct().ToList();
            }
            else
            {
                AllPortName.Clear();
            }
        }

        /// <summary>
        /// 串口异步设置
        /// </summary>
        /// <param name="PortName"></param>
        private async void SerialSet(string PortName)
        {
            string selectors = SerialDevice.GetDeviceSelector();
            DeviceInformationCollection decices = await DeviceInformation.FindAllAsync(selectors);

            if (decices.Any())
            {
                for (int i = 0; i < decices.Count(); i++)
                {
                    if (decices[i].Name.Equals(PortName))
                    {
                        Device = await SerialDevice.FromIdAsync(decices[i].Id);
                    }
                }
            }
        }


        /// <summary>
        /// 串口数据设置
        /// </summary>
        /// <param name="PortName"></param>
        /// <param name="BaudRate"></param>
        /// <param name="ParityBit"></param>
        /// <param name="DataBit"></param>
        /// <param name="StopBit"></param>
        public void SerialDataSet(string PortName, string BaudRate, string ParityBit, string DataBit, string StopBit)
        {
            SerialSet(PortName);

            if (Device != null)
            {
                Device.BaudRate = uint.Parse(BaudRate);

                //无校验（no parity）
                //奇校验（odd parity）：如果字符数据位中"1"的数目是偶数，校验位为"1"，如果"1"的数目是奇数，校验位应为"0"。（校验位调整个数）
                //偶校验（even parity）：如果字符数据位中"1"的数目是偶数，则校验位应为"0"，如果是奇数则为"1"。（校验位调整个数）
                //mark parity：校验位始终为1
                //space parity：校验位始终为0
                switch (ParityBit)
                {
                    case "无校验":
                        Device.Parity = SerialParity.None;
                        break;
                    case "奇校验":
                        Device.Parity = SerialParity.Odd;
                        break;
                    case "偶校验":
                        Device.Parity = SerialParity.Even;
                        break;
                    case "校验位为1":
                        Device.Parity = SerialParity.Mark;
                        break;
                    case "校验位为0":
                        Device.Parity = SerialParity.Space;
                        break;
                }

                Device.DataBits = ushort.Parse(DataBit);

                switch (StopBit)
                {
                    case "1":
                        Device.StopBits = SerialStopBitCount.One;
                        break;
                    case "1.5":
                        Device.StopBits = SerialStopBitCount.OnePointFive;
                        break;
                    case "2":
                        Device.StopBits = SerialStopBitCount.Two;
                        break;
                }
            }
        }


        /// <summary>
        /// 串口数去读取
        /// </summary>
        /// <returns></returns>
        public string SerialDataRead()
        {
            var ReceiveData = "";

            if (Device != null)
            {
                Device.IsDataTerminalReadyEnabled = true;
                Device.ReadTimeout = TimeSpan.FromMilliseconds(500);
                DataReader dataReader = new DataReader(Device.InputStream)
                {
                    UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8,
                    ByteOrder = ByteOrder.LittleEndian
                };


                while (dataReader.UnconsumedBufferLength > 0)
                {
                    uint bytesToRead = dataReader.ReadUInt32();
                    ReceiveData += dataReader.ReadString(bytesToRead) + "\n";
                }
            }
            return ReceiveData;
        }
    }
}
