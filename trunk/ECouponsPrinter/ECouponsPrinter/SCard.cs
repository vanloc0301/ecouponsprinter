using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ECouponsPrinter
{
    public class SCard
    {
        [DllImport("kernel32.dll")]
        static extern void Sleep(int dwMilliseconds);

        [DllImport("MasterRD.dll")]
        static extern int lib_ver(ref uint pVer);

        [DllImport("MasterRD.dll")]
        static extern int rf_init_com(int port, int baud);

        [DllImport("MasterRD.dll")]
        static extern int rf_ClosePort();

        [DllImport("MasterRD.dll")]
        static extern int rf_light(short icdev, byte color);

        [DllImport("MasterRD.dll")]
        static extern int rf_antenna_sta(short icdev, byte mode);

        [DllImport("MasterRD.dll")]
        static extern int rf_init_type(short icdev, byte type);

        [DllImport("MasterRD.dll")]
        static extern int rf_request(short icdev, byte mode, ref ushort pTagType);

        [DllImport("MasterRD.dll")]
        static extern int rf_anticoll(short icdev, byte bcnt, IntPtr pSnr, ref byte pRLength);

        [DllImport("MasterRD.dll")]
        static extern int rf_select(short icdev, IntPtr pSnr, byte srcLen, ref sbyte Size);

        [DllImport("MasterRD.dll")]
        static extern int rf_halt(short icdev);

        [DllImport("MasterRD.dll")]
        static extern int rf_M1_authentication2(short icdev, byte mode, byte secnr, IntPtr key);

        [DllImport("MasterRD.dll")]
        static extern int rf_M1_initval(short icdev, byte adr, Int32 value);

        [DllImport("MasterRD.dll")]
        static extern int rf_M1_increment(short icdev, byte adr, Int32 value);

        [DllImport("MasterRD.dll")]
        static extern int rf_M1_decrement(short icdev, byte adr, Int32 value);

        [DllImport("MasterRD.dll")]
        static extern int rf_M1_readval(short icdev, byte adr, ref Int32 pValue);

        [DllImport("MasterRD.dll")]
        static extern int rf_M1_read(short icdev, byte adr, IntPtr pData, ref byte pLen);

        [DllImport("MasterRD.dll")]
        static extern int rf_M1_write(short icdev, byte adr, IntPtr pData);

        [DllImport("MasterRD.dll")]
        static extern int rf_beep(short icdev, byte msec);

        public bool bConnectedDevice = false;

        public SCard()
        {

        }

        public void Init()
        {

            int port = 0;
            int baud = 0;
            int status;

            port = 2;
            baud = 19200;

            status = rf_init_com(port, baud);
            if (0 == status)
            {
                bConnectedDevice = true;
                InitType('A');
            }
            else
            {
                //         MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool InitType(char type)
        {
            byte btype = (byte)type;//mifare one 卡询卡方式为A
            short icdev = 0x0000;
            int status;
            bool success = false;

            for (int i = 0; i < 2; i++)
            {
                status = rf_antenna_sta(icdev, 0);//关闭天线
                if (status != 0)
                {
                    success = false;
                    continue;
                }
                else
                    success = true;

                Sleep(10);
                status = rf_init_type(icdev, btype);
                if (status != 0)
                {
                    success = false;
                    continue;
                }
                else
                    success = true;

                Sleep(10);
                status = rf_antenna_sta(icdev, 1);//启动天线
                if (status != 0)
                {
                    success = false;
                    continue;
                }
                else
                    success = true;
            }
            return success;
        }

        static char[] hexDigits = { 
            '0','1','2','3','4','5','6','7',
            '8','9','A','B','C','D','E','F'};

        public static byte GetHexBitsValue(byte ch)
        {
            byte sz = 0;
            if (ch <= '9' && ch >= '0')
                sz = (byte)(ch - 0x30);
            if (ch <= 'F' && ch >= 'A')
                sz = (byte)(ch - 0x37);
            if (ch <= 'f' && ch >= 'a')
                sz = (byte)(ch - 0x57);

            return sz;
        }

        public static string ToHexString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length * 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                int b = bytes[i];
                chars[i * 2] = hexDigits[b >> 4];
                chars[i * 2 + 1] = hexDigits[b & 0xF];
            }

            return new string(chars);
        }

        public static byte[] ToDigitsBytes(string theHex)
        {
            byte[] bytes = new byte[theHex.Length / 2 + (((theHex.Length % 2) > 0) ? 1 : 0)];
            for (int i = 0; i < bytes.Length; i++)
            {
                char lowbits = theHex[i * 2];
                char highbits;

                if ((i * 2 + 1) < theHex.Length)
                    highbits = theHex[i * 2 + 1];
                else
                    highbits = '0';

                int a = (int)GetHexBitsValue((byte)lowbits);
                int b = (int)GetHexBitsValue((byte)highbits);
                bytes[i] = (byte)((a << 4) + b);
            }

            return bytes;
        }

        public string searchCard()
        {
            short icdev = 0x0000;
            int status;
            byte mode = 0x52;
            ushort TagType = 0;
            byte bcnt = 0x04;//mifare 卡都用4
            IntPtr pSnr;
            byte len = 255;
            sbyte size = 0;
            string cardNo = null;

            if (!bConnectedDevice)
            {
                //      MessageBox.Show("Not connect to device!!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            pSnr = Marshal.AllocHGlobal(1024);

            for (int i = 0; i < 1; i++)
            {
                status = rf_request(icdev, mode, ref TagType);//搜寻所有的卡
                if (status != 0)
                    break;
                else
                {
                    rf_beep(icdev, 10);
                }

                status = rf_anticoll(icdev, bcnt, pSnr, ref len);//返回卡的序列号
                if (status != 0)
                {
                    //     MessageBox.Show("返回卡的序列号错误!");
                    break;
                }
                status = rf_select(icdev, pSnr, len, ref size);//锁定一张ISO14443-3 TYPE_A 卡
                if (status != 0)
                {
                    //     MessageBox.Show("锁定卡时发生错误!");
                    break;
                }

                byte[] szBytes = new byte[len + 1];
                string str = Marshal.PtrToStringAnsi(pSnr);

                for (int j = 0; j < len; j++)
                {
                    szBytes[j] = (byte)str[j];
                }

                cardNo = ToHexString(szBytes);
                //       MessageBox.Show(cardNo);
            }

            Marshal.FreeHGlobal(pSnr);
            if (cardNo == null)
            {
                return null;
            }
            else
            {
                string uid = GetUserId(cardNo);
                if (uid != null)
                {
                    return uid.Substring(0, 7);
                }

            }
            return null;
        }

        public string GetUserId(string cardId)
        {
            short icdev = 0x0000;
            int status;
            byte mode = 0x60;
            byte secnr = 0x00;

            if (!bConnectedDevice)
            {
                //       MessageBox.Show("Not connect to device!!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            secnr = 0x01;

            IntPtr keyBuffer = Marshal.AllocHGlobal(1024);

            byte[] bytesKey = ToDigitsBytes("FFFFFFFFFFFF");
            for (int i = 0; i < bytesKey.Length; i++)
                Marshal.WriteByte(keyBuffer, i, bytesKey[i]);
            status = rf_M1_authentication2(icdev, mode, (byte)(secnr * 4), keyBuffer);
            Marshal.FreeHGlobal(keyBuffer);
            if (status != 0)
            {
                //        MessageBox.Show("rf_M1_authentication2 failed!!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            //
            IntPtr dataBuffer = Marshal.AllocHGlobal(1024);

            int j;
            byte cLen = 0;
            status = rf_M1_read(icdev, (byte)(secnr * 4), dataBuffer, ref cLen);

            if (status != 0 || cLen != 16)
            {
                //       MessageBox.Show("rf_M1_read failed!!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Marshal.FreeHGlobal(dataBuffer);
                return null;
            }

            byte[] bytesData = new byte[16];
            for (j = 0; j < bytesData.Length; j++)
                bytesData[j] = Marshal.ReadByte(dataBuffer, j);

            Marshal.FreeHGlobal(dataBuffer);
            return ToHexString(bytesData);
        }


        public static int light(short icdev, byte color)
        {
            return rf_light(icdev, color);
        }

    }
}
