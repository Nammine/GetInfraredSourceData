using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace ConsoleApplication1
{
    class Program
    {
        static ushort[] irData;
        static byte[] irDataConvert;
        static void Main(string[] args)
        {
            KinectSensor sensor;
            InfraredFrameReader irReader;
            sensor = KinectSensor.GetDefault();
            irReader = sensor.InfraredFrameSource.OpenReader();
            FrameDescription fd = sensor.InfraredFrameSource.FrameDescription;
            irData = new ushort[fd.LengthInPixels];
            irDataConvert = new byte[irData.Length * 4];
            Console.ForegroundColor = ConsoleColor.Green;
            sensor.Open();
            irReader.FrameArrived += IrReader_FrameArrived;
            while (Console.ReadKey().Key != ConsoleKey.Spacebar)
            {

            }
        }
        private static void IrReader_FrameArrived(object sender, InfraredFrameArrivedEventArgs e)
        {
            // ColorFrame is IDisposable
            using (InfraredFrame irFrame = e.FrameReference.AcquireFrame())
            {
                if (irFrame != null)
                {
                    irFrame.CopyFrameDataToArray(irData);
                    foreach (var data in irData)
                        Console.Write(data);
                }
            }
        }
    }
}
