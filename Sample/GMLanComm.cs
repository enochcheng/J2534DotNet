using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using J2534DotNet.Logger;
using J2534DotNet;

namespace Sample
{
    public class GMLanComm
    {
        private J2534Extended m_j2534Interface;
        private int m_deviceId;
        private int m_swChannelId;
        private int m_dwChannelId;
        private bool m_isConnected;
        private J2534Err m_status;

        public GMLanComm(J2534Extended j2534Interface)
        {
            m_j2534Interface = j2534Interface;
            m_isConnected = false;
            m_status = J2534Err.STATUS_NOERROR;
        }

        public bool Connect()
        {
            // Find all of the installed J2534 passthru devices
            List<J2534Device> availableJ2534Devices = J2534Detect.ListDevices();

            J2534Device j2534Device;
            var sd = new SelectDevice();
            if (sd.ShowDialog() == DialogResult.OK)
            {
                j2534Device = sd.Device;
            }
            else
            {
                return false;
            }

            // We will always choose the first J2534 device in the list, if there are multiple devices
            //   installed, you should do something more intelligent.
            m_j2534Interface.LoadLibrary(j2534Device);

            // Attempt to open a communication link with the pass thru device
            int deviceId = 0;
            m_status = m_j2534Interface.PassThruOpen(IntPtr.Zero, ref deviceId);

            if (m_status != J2534Err.STATUS_NOERROR)
                return false;

            if (ConnectSWCan(deviceId, ref m_swChannelId) && ConnectDWCan(deviceId, ref m_dwChannelId))
            {
                m_isConnected = true;
                SetNullFilters(m_swChannelId, m_dwChannelId);
                return true;
            }

            return false;
        }

        /*
         * Connect to GMLan SW CAN Protocol (PIN 1)
         */
        private bool ConnectSWCan(int deviceId, ref int channelId)
        {
            m_status = m_j2534Interface.PassThruConnect(deviceId, ProtocolID.SW_CAN, ConnectFlag.CAN_ID_BOTH, BaudRate.CAN_33300, ref channelId);
            if (J2534Err.STATUS_NOERROR != m_status)
            {
                m_j2534Interface.PassThruDisconnect(channelId);
                return false;
            }

            SConfig sConfig = new SConfig();
            sConfig.Parameter = ConfigParameter.J1962_PINS;
            sConfig.Value = 0x0100;
            List<SConfig> sConfigs = new List<SConfig>();
            sConfigs.Add(sConfig);

            SConfigList sConfigList = new SConfigList();
            sConfigList.Count = 1;
            sConfigList.ListPtr = sConfigs.ToIntPtr();

            m_status = m_j2534Interface.PassThruIoctl(channelId, (int)Ioctl.SET_CONFIG, sConfigList.ToIntPtr(), IntPtr.Zero);
            if (J2534Err.STATUS_NOERROR != m_status)
            {
                m_j2534Interface.PassThruDisconnect(channelId);
                return false;
            }

            return true;
        }

        private bool ConnectDWCan(int deviceId, ref int channelId)
        {
            m_j2534Interface.PassThruConnect(deviceId, ProtocolID.DW_CAN, ConnectFlag.CAN_ID_BOTH, BaudRate.CAN_500000, ref channelId);
            SConfig sConfig = new SConfig();
            sConfig.Parameter = ConfigParameter.J1962_PINS;
            sConfig.Value = 0x060E;
            List<SConfig> sConfigs = new List<SConfig>();
            sConfigs.Add(sConfig);

            SConfigList sConfigList = new SConfigList();
            sConfigList.Count = 1;
            sConfigList.ListPtr = sConfigs.ToIntPtr();

            m_status = m_j2534Interface.PassThruIoctl(channelId, (int)Ioctl.SET_CONFIG, sConfigList.ToIntPtr(), IntPtr.Zero);
            if (J2534Err.STATUS_NOERROR != m_status)
            {
                m_j2534Interface.PassThruDisconnect(channelId);
                return false;
            }

            return true;
        }

        public void SetNullFilters(int swChannelId, int dwChannelId)
        {
            // Set up a message filter to watch for response messages
            int filterId = 0;


            PassThruMsg swMask1 = new PassThruMsg(
                ProtocolID.SW_CAN,
                TxFlag.CAN_29BIT_ID,
                new byte[] { 0x00, 0x00, 0x00, 0x00 });

            PassThruMsg swMask2 = new PassThruMsg(
                ProtocolID.SW_CAN,
                TxFlag.CAN_29BIT_ID,
                new byte[] { 0x10, 0x00, 0x00, 0x00 });

            PassThruMsg swMask3 = new PassThruMsg(
                ProtocolID.SW_CAN,
                TxFlag.CAN_29BIT_ID,
                new byte[] { 0x0f, 0x00, 0x00, 0x00 });

            PassThruMsg dwMask1 = new PassThruMsg(
                ProtocolID.DW_CAN,
                TxFlag.CAN_29BIT_ID,
                new byte[] { 0x00, 0x00, 0x00, 0x00 });

            PassThruMsg dwMask2 = new PassThruMsg(
                ProtocolID.DW_CAN,
                TxFlag.CAN_29BIT_ID,
                new byte[] { 0x10, 0x00, 0x00, 0x00 });

            PassThruMsg dwMask3 = new PassThruMsg(
                ProtocolID.DW_CAN,
                TxFlag.CAN_29BIT_ID,
                new byte[] { 0x0f, 0x00, 0x00, 0x00 });

            m_j2534Interface.PassThruStartMsgFilter(swChannelId, FilterType.PASS_FILTER, swMask1.ToIntPtr(), swMask1.ToIntPtr(), IntPtr.Zero, ref filterId);
            m_j2534Interface.PassThruStartMsgFilter(swChannelId, FilterType.PASS_FILTER, swMask2.ToIntPtr(), swMask2.ToIntPtr(), IntPtr.Zero, ref filterId);
            m_j2534Interface.PassThruStartMsgFilter(swChannelId, FilterType.PASS_FILTER, swMask3.ToIntPtr(), swMask3.ToIntPtr(), IntPtr.Zero, ref filterId);

            m_j2534Interface.PassThruStartMsgFilter(dwChannelId, FilterType.PASS_FILTER, dwMask1.ToIntPtr(), dwMask1.ToIntPtr(), IntPtr.Zero, ref filterId);
            m_j2534Interface.PassThruStartMsgFilter(dwChannelId, FilterType.PASS_FILTER, dwMask2.ToIntPtr(), dwMask2.ToIntPtr(), IntPtr.Zero, ref filterId);
            m_j2534Interface.PassThruStartMsgFilter(dwChannelId, FilterType.PASS_FILTER, dwMask3.ToIntPtr(), dwMask3.ToIntPtr(), IntPtr.Zero, ref filterId);
        }

        public List<PassThruMsg> ReadSwMessage()
        {
            int numSwMsgs = 1;
            IntPtr swMsg = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PassThruMsg)) * numSwMsgs);
            J2534Err status = J2534Err.STATUS_NOERROR;
            m_status = m_j2534Interface.PassThruReadMsgs(m_swChannelId, swMsg, ref numSwMsgs, 200);

            if (numSwMsgs > 0)
            {
                List<PassThruMsg> swMessages = swMsg.AsList<PassThruMsg>(numSwMsgs);
                Marshal.FreeHGlobal(swMsg);
                return swMessages;
            }
            Marshal.FreeHGlobal(swMsg);

            return new List<PassThruMsg>();
        }

        public List<PassThruMsg> ReadDwMessage()
        {
            int numDwMsgs = 1;
            IntPtr dwMsg = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PassThruMsg)) * numDwMsgs);
            J2534Err status = J2534Err.STATUS_NOERROR;
            m_status = m_j2534Interface.PassThruReadMsgs(m_dwChannelId, dwMsg, ref numDwMsgs, 200);

            if (numDwMsgs > 0)
            {
                List<PassThruMsg> dwMessages = dwMsg.AsList<PassThruMsg>(numDwMsgs);
                Marshal.FreeHGlobal(dwMsg);
                return dwMessages;
            }
            Marshal.FreeHGlobal(dwMsg);

            return new List<PassThruMsg>();
        }

        public bool SendSwMessage(String id, bool extended, bool highVoltage, String data)
        {
            PassThruMsg txMsg = new PassThruMsg();
            int timeout = 200;
            String rawMessage;

            if (extended)
            {
                rawMessage = CreateExtendedMessage(id, data);
            }
            else
            {
                rawMessage = CreateStandardMessage(id, data);
            }

            txMsg.ProtocolID = ProtocolID.SW_CAN;
            txMsg.SetBytes(StringToHex(rawMessage));
            if (extended)
            {
                txMsg.TxFlags |= TxFlag.CAN_29BIT_ID;
            }
            if (highVoltage)
            {
                txMsg.TxFlags |= TxFlag.SW_CAN_HV_TX;
            }

            int numMsgs = 1;
            m_status = m_j2534Interface.PassThruWriteMsgs(m_swChannelId, txMsg.ToIntPtr(), ref numMsgs, timeout);
            if (J2534Err.STATUS_NOERROR != m_status)
            {
                return false;
            }

            return true;
        }

        public bool SendDwMessage(String id, bool extended, bool highVoltage, String data)
        {
            PassThruMsg txMsg = new PassThruMsg();
            int timeout = 200;
            String rawMessage;

            if (extended)
            {
                rawMessage = CreateExtendedMessage(id, data);
            }
            else
            {
                rawMessage = CreateStandardMessage(id, data);
            }

            txMsg.ProtocolID = ProtocolID.DW_CAN;
            txMsg.SetBytes(StringToHex(rawMessage));
            if (extended)
            {
                txMsg.TxFlags |= TxFlag.CAN_29BIT_ID;
            }
            if (highVoltage)
            {
                txMsg.TxFlags |= TxFlag.SW_CAN_HV_TX;
            }

            int numMsgs = 1;
            m_status = m_j2534Interface.PassThruWriteMsgs(m_dwChannelId, txMsg.ToIntPtr(), ref numMsgs, timeout);
            if (J2534Err.STATUS_NOERROR != m_status)
            {
                return false;
            }

            return true;
        }

        public bool Disconnect()
        {
            m_status = m_j2534Interface.PassThruDisconnect(m_swChannelId);
            m_status = m_j2534Interface.PassThruDisconnect(m_dwChannelId);
            m_status = m_j2534Interface.PassThruClose(m_deviceId);
            if (m_status != J2534Err.STATUS_NOERROR)
            {
                return false;
            }
            return true;
        }

        public J2534Err GetLastError()
        {
            return m_status;
        }

        internal static string CreateExtendedMessage(string frameID, string data)
        {
            string str = frameID;
            while (str.Length < 8)
            {
                str = "0" + str;
            }
            return (str + data);
        }

        internal static string CreateStandardMessage(string frameID, string data)
        {
            string str = frameID;
            while (str.Length < 8)
            {
                str = "0" + str;
            }
            return (str + data);
        }

        public static string GetIDFromBytes(byte[] stream, uint len)
        {
            if (len < 4)
            {
                return "";
            }
            uint num = (uint)((((stream[0] << 0x18) | (stream[1] << 0x10)) | (stream[2] << 8)) | stream[3]);
            return Convert.ToString((long)num, 0x10).ToUpper();
        }

        public static string GetDataFromBytes(byte[] stream, uint len)
        {
            string str = "";
            if (len <= 4)
            {
                return "";
            }
            for (int i = 4; i < len; i++)
            {
                byte num = (byte)((stream[i] & 240) >> 4);
                char ch = (num >= 10) ? ((char)((num - 10) + 0x41)) : ((char)(num + 0x30));
                str = str + ch.ToString();
                num = (byte)(stream[i] & 15);
                str = str + ((num >= 10) ? ((char)((num - 10) + 0x41)) : ((char)(num + 0x30))).ToString();
            }
            return str;
        }

        public bool IsConnected()
        {
            return m_isConnected;
        }

        internal static byte[] StringToHex(string msg)
        {
            byte[] buffer = new byte[msg.Length >> 1];
            if ((msg.Length % 2) != 0)
            {
                Debug.WriteLine("Invalid Message Length: " + msg);
                throw new Exception("Invalid Message Length: " + msg);
            }
            int index = 0;
            for (int i = 0; i < msg.Length; i += 2)
            {
                buffer[index] = (byte)((GetHex(msg[i]) << 4) | GetHex(msg[i + 1]));
                index++;
            }
            return buffer;
        }

        internal static byte GetHex(char chr)
        {
            chr = char.ToUpper(chr);
            if (chr >= 'A')
            {
                return (byte)((chr - 'A') + 10);
            }
            return (byte)(chr - '0');
        }
    }
}
