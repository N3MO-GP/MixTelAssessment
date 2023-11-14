using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixTelAssessment
{
    internal class FilerHandler
    {
        /// <summary>
        /// Function to return list of vehicles
        /// </summary>
        /// <returns></returns>
        internal static List<Vehicle> ReadFile()
        {
            byte[] data = ReadData();
            List<Vehicle> vehiclePositions = new List<Vehicle>();
            int offset = 0;
            while (offset < data.Length)
                vehiclePositions.Add(ReadPositions(data, ref offset));
            return vehiclePositions;
        }

        /// <summary>
        /// Reads datafile
        /// </summary>
        /// <returns></returns>
        private static byte[] ReadData()
        {
            var path = Util.GetLocalFilePath("VehiclePositions.dat");

            try
            {
                if (File.Exists(path))
                    return File.ReadAllBytes(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file.");
                Console.WriteLine($"Exception: {1}",ex.Message);
               
            }
            return (byte[])null;
        }

        /// <summary>
        /// Returns a Vehicle object from byte array
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private static Vehicle ReadPositions(byte[] data, ref int offset) => Vehicle.FromBytes(data, ref offset);
    }
}

