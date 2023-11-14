using MixTelAssessment;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MixTelAssessment
{
    internal class Program
    {
        static LocationHelper _locationHelper;

        static void Main(string[] args)
        {           
            List<Vehicle> inputData = FilerHandler.ReadFile().OrderBy(x => x.Latitude).ToList();
            _locationHelper = new LocationHelper(inputData);

            foreach (var vehicle in _locationHelper.FindClosest())
            {
                Console.WriteLine(vehicle.ToString());
            }
        }
    }
}