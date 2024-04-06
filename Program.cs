using System.Diagnostics;
using System.Reflection.Metadata;
using WMX3ApiCLR;

namespace MovensysIO
{
    class Program
    {
        private static readonly int CONST_MAX_BUFFER_SIZE = 250;

        public static CWmx wmx = new CWmx();

        private static byte[] ReadOutData = new byte[CONST_MAX_BUFFER_SIZE];
        static void Main(string[] args)
        {
            InitWmx();
            ReadOutIOByte();    //Read Now Output_IO State



            byte[] WriteOutData = new byte[CONST_MAX_BUFFER_SIZE];
         
            WriteOutData[0] = 117;
            /* Example
               117 = 1110101
                 Output IO
                 IO No :  6 /  5 /  4 /  3  /  2 /  1 /  0
                       : ON / ON / ON / OFF / ON / OFF/ ON
            */
            WriteOutIOByte(WriteOutData); //Write Data to Output_IO


            /*
             * Q1. How To Turn On All IO ?
             * Q2. How To Turn off IO No. 6, 5, 4 and Turn On 3, 2, 1, 0?
             * Q3. Make Function to Turn On / Off to each 1 IO
             *   
             *   -- Example
             *      Output : "Write number of IO you turn on"
             *      input : 1
             *      -> Turn On IO Number 1
             *      Make Finish SetIO
             */
        }

        void SetIO(int index, int value)
        {
  




            
        }





        static void InitWmx()
        {
            wmx.Wmx3Lib = new WMX3Api();
            wmx.CmStatus = new CoreMotionStatus();
            wmx.Wmx3Lib_cm = new CoreMotion(wmx.Wmx3Lib);
            wmx.Wmx3Lib_Io = new Io(wmx.Wmx3Lib);
            wmx.StartEngine("DeviceName");
            wmx.StartCommunication();
        }

        static void ReadOutIOByte()
        {
            wmx.Wmx3Lib_Io.GetOutBytes(0, CONST_MAX_BUFFER_SIZE, ref ReadOutData);
        }
        static void WriteOutIOByte(byte[] _outdata)
        {
            wmx.Wmx3Lib_Io.SetOutBytes(0, Constants.MaxIoOutSize, _outdata);
        }
        
    }
    public class CWmx
    {
        public WMX3Api Wmx3Lib;
        public CoreMotionStatus CmStatus;
        public CoreMotion Wmx3Lib_cm;
        public Io Wmx3Lib_Io;
        public void StartEngine(string deviceName)
        {
            DeviceInfo devInfo1 = new DeviceInfo();

            int retCreateDevices = Wmx3Lib.CreateDevice("C:\\Program Files\\SoftServo\\WMX3\\",
                DeviceType.DeviceTypeNormal,
                0xFFFFFFFF);

            Debug.WriteLine(CoreMotion.ErrorToString(retCreateDevices));

            int retSetDeviceName = Wmx3Lib.SetDeviceName(deviceName);
            Debug.WriteLine(CoreMotion.ErrorToString(retSetDeviceName));
        }
        public void StartCommunication()
        {
            int retStartCommunication = Wmx3Lib.StartCommunication(0xFFFFFFFF);
            Debug.WriteLine(CoreMotion.ErrorToString(retStartCommunication));
        }

        public void Terminate()
        {
            Wmx3Lib.StopCommunication(0xFFFFFFFF);

            //Quit device.
            Wmx3Lib.CloseDevice();
            Wmx3Lib_cm.Dispose();
            Wmx3Lib_Io.Dispose();
            Wmx3Lib.Dispose();

            Debug.WriteLine("Program End.");
        }
    }
}
