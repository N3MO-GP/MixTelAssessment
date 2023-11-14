using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixTelAssessment
{
    /// <summary>
    /// struct for input coordinates from word document
    /// </summary>
    internal struct Coordinates
    {
        public float Latitude;
        public float Longitude;
    }
    internal class InputCoordinates
    {
        internal static Coordinates[] GetCoordinates() => InputCoordinates.GetPositions();

        private static Coordinates[] GetPositions()
        {
            Coordinates[] inputPositions = new Coordinates[10];
            inputPositions[0].Latitude = 18.544909f;
            inputPositions[0].Longitude = -102.100843f;
            inputPositions[1].Latitude = 32.345544f;
            inputPositions[1].Longitude = -99.123124f;
            inputPositions[2].Latitude = 33.234235f;
            inputPositions[2].Longitude = -100.214124f;
            inputPositions[3].Latitude = 35.195739f;
            inputPositions[3].Longitude = -95.348899f;
            inputPositions[4].Latitude = 31.895839f;
            inputPositions[4].Longitude = -92.789573f;
            inputPositions[5].Latitude = 32.895839f;
            inputPositions[5].Longitude = -101.789573f;
            inputPositions[6].Latitude = 34.115839f;
            inputPositions[6].Longitude = -100.225732f;
            inputPositions[7].Latitude = 32.335839f;
            inputPositions[7].Longitude = -99.992232f;
            inputPositions[8].Latitude = 33.535339f;
            inputPositions[8].Longitude = -94.792232f;
            inputPositions[9].Latitude = 32.234235f;
            inputPositions[9].Longitude = -100.222222f;

            return inputPositions;
        }
    }
}
