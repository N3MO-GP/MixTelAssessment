using System.Text;

namespace MixTelAssessment
{
    public class Vehicle
    {
        public int VehicleID { get; set; }

        public string VehicleReg { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public DateTime RecordedTime { get; set; }

        public Vehicle()
        {
            
        }
        /// <summary>
        /// Constructor with overloads for the result list
        /// </summary>
        /// <param name="vehicleReg"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public Vehicle(string vehicleReg, float latitude, float longitude)
        {
            VehicleReg = vehicleReg;
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Vehicle object creation from the byte's recieved when reading data file
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns>
        /// Vehicle object
        /// </returns>
        internal static Vehicle FromBytes(byte[] buffer, ref int offset)
        {
            var vehicle = new Vehicle();

            vehicle.VehicleID = BitConverter.ToInt32(buffer, offset);
            offset += 4;

            StringBuilder stringBuilder = new StringBuilder();

            while (buffer[offset] != (byte)0)
            {
                stringBuilder.Append((char)buffer[offset]);
                ++offset;
            }

            vehicle.VehicleReg = stringBuilder.ToString();
            ++offset;

            vehicle.Latitude = BitConverter.ToSingle(buffer, offset);
            offset += 4;

            vehicle.Longitude = BitConverter.ToSingle(buffer, offset);
            offset += 4;

            ulong uint64 = BitConverter.ToUInt64(buffer, offset);
            vehicle.RecordedTime = Util.FromCTime(uint64);
            offset += 8; 

            return vehicle;
        }

        /// <summary>
        /// Overriden ToString to return vehicle details
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Registration: {0}, Coordinates: {1} {2}", VehicleReg, Latitude, Longitude);
        }
    }
}
