using MixTelAssessment;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MixTelAssessment
{
    internal class Program
    {
        static FileHandler _handler;

        static void Main(string[] args)
        {
            _handler = new FileHandler();
            List<Vehicle> inputData = _handler.ReadFile();
            Coordinates[] inputCoordinates = InputCoordinates.GetCoordinates();

            foreach (Vehicle vehicle in FindNeartest(inputData, inputCoordinates))
            {
                Console.WriteLine(vehicle.ToString());
            }
        }

        /// <summary>
        /// Collection search algorythm logic  
        /// </summary>
        /// <param name="latitudeArr"></param>
        /// <param name="Coord"></param>
        /// <returns></returns>
        private static int Search(float[] latitudeArr,
            float Coord)
        {
            int nearestIndex = -1;
            int min = 0;
            int max = latitudeArr.Length - 1;

            while (min <= max)
            {
                //Determining middle of array
                int middle = min + (max - min) / 2;

                //Eact Coordinate found
                if (latitudeArr[middle] == Coord)
                {
                    return middle;
                }

                //Assigning nearest index 
                if (nearestIndex == -1 || Math.Abs(latitudeArr[middle] - Coord) < Math.Abs(latitudeArr[nearestIndex] - Coord))
                {
                    nearestIndex = middle;
                }

                //Determine if coordinate is closer to the min or max
                if (latitudeArr[middle] < Coord)
                {
                    min = middle + 1;
                }
                else
                {
                    max = middle - 1;
                }
            }

            return nearestIndex;
        }

        /// <summary>
        /// Function to find the nearest vehicle
        /// </summary>
        /// <param name="inputData"></param>
        /// <param name="inputCoordinates"></param>
        /// <returns></returns>
        static List<Vehicle> FindNeartest(List<Vehicle> inputData, Coordinates[] inputCoordinates)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            string[] vehicleRegArr = inputData.Select(x => x.VehicleReg).OrderBy(x => x).ToArray();
            float[] latitudeArr = inputData.Select(x => x.Latitude).OrderBy(x => x).ToArray();
            float[] longitudeArr = inputData.Select(x => x.Longitude).OrderBy(x => x).ToArray();

            foreach (Coordinates location in inputCoordinates)
            {
                int latInd = Search(latitudeArr, location.Latitude);
                int longInd = Search(latitudeArr, location.Longitude);
                vehicles.Add(new Vehicle(vehicleRegArr[latInd], latitudeArr[latInd], longitudeArr[longInd]));
            }

            return vehicles;

        }

    }
}