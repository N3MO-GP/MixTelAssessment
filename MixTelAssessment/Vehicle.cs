using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixTelAssessment
{
    public class Vehicle
    {
        public int VehicleID { get; set; }

        public string VehicleReg { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public Int64 RecordedTimeUTC { get; set; }

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
        /// Constuctor with overloads for input records from data file
        /// </summary>
        /// <param name="vehicleID"></param>
        /// <param name="vehicleReg"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="recordedTimeUTC"></param>
        public Vehicle(int vehicleID, string vehicleReg, float latitude, float longitude, Int64 recordedTimeUTC)
        {
            VehicleID = vehicleID;
            VehicleReg = vehicleReg;
            Latitude = latitude;
            Longitude = longitude;

            RecordedTimeUTC = recordedTimeUTC;
        }

        /// <summary>
        /// Overriden ToString to return vehicle details
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Registration: {0}, Coordinates: {1} {2}  ", VehicleReg, Latitude, Longitude);
        }
    }
}
