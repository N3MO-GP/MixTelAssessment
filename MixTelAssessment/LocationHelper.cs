using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixTelAssessment
{
    class Position
    {
        public int ID { get; set; }
        public string Regsitration { get;set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public Position(Vehicle vehicle)
        {
            ID = vehicle.VehicleID;
            Regsitration = vehicle.VehicleReg; 
            Latitude = vehicle.Latitude;
            Longitude = vehicle.Longitude;
        }
    }

    public class LocationHelper
    {
        static Coordinates[] inputCoordinates = InputCoordinates.GetCoordinates();
        public List<Vehicle> data { get; set; }
        public LocationHelper(List<Vehicle> inputData)
        {
            data = inputData;
        }

        /// <summary>
        /// Function to calculate the distance between two points
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="long1"></param>
        /// <param name="lat2"></param>
        /// <param name="long2"></param>
        /// <returns></returns>
        private double Distance(double lat1, double long1, double lat2, double long2)
        {
            // Convert degrees to radians
            lat1 = Util.ToRadian(lat1);
            long1 = Util.ToRadian(long1);
            lat2 = Util.ToRadian(lat2);
            long2 = Util.ToRadian(long2);           

            // Apply the haversine formula
            var dlon = long2 - long1;
            var dlat = lat2 - lat1;
            var a = Math.Sin(dlat / 2) * Math.Sin(dlat / 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Sin(dlon / 2) * Math.Sin(dlon / 2);
            var c = 2 * Math.Asin(Math.Sqrt(a));
            const double r = 6371; // Radius of the earth in km

            return c * r; // Distance in km
        }


        /// <summary>
        /// Funcion to perform binary search on sorted list.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        private int BinarySearch(double target, int low, int high)
        {
            // Initialize the smallest difference and the corresponding index
            double minDiff = double.MaxValue;
            int closest = -1;

            //Loop until the search interval is empty
            while (low <= high)
            {
                //Find the middle index
                int mid = (low + high) / 2;

                //Create a Position object from the data at the middle index
                Position position = new Position(data[mid]);

                //Compare the target with the latitude of the position
                if (target == position.Latitude)
                {
                    //If they are equal, return the middle index
                    return mid;
                }
                else if (target < position.Latitude)
                {
                    //If the target is smaller, narrow the search interval to the left half
                    high = mid - 1;
                }
                else
                {
                    //If the target is larger, narrow the search interval to the right half
                    low = mid + 1;
                }

                //Calculate the absolute difference between the target and the latitude
                double diff = Math.Abs(target - position.Latitude);

                //If the difference is smaller than the current minimum, update the closest index and the minimum difference
                if (diff < minDiff)
                {
                    closest = mid;
                    minDiff = diff;
                }
            }
            // Return the closest index by difference
            return closest;
        }

        /// <summary>
        /// Function to find the closest vehicle.
        /// </summary>
        /// <returns></returns>
        public List<Vehicle> FindClosest()
        {

            var result = new List<Vehicle>();

            foreach (var coord in inputCoordinates)
            {
                // Perform binary search on the data using the latitude of the co-ordinate
                int index = BinarySearch(coord.Latitude, 0, data.Count - 1);

                //Start from the index and expand the window to the left and right until we find a position that has a larger distance than the current closest position
                var closestPosition = new Position(data[index]);
                var closestDist = Distance(coord.Latitude, coord.Longitude, closestPosition.Latitude, closestPosition.Longitude);
                var left = index - 1;
                var right = index + 1;
                bool complete = false;

                while (!complete)
                {
                    // Check if the left index is valid
                    if (left >= 0)
                    {
                        // Create a Position object from the data at the left index
                        var leftPosition = new Position(data[left]);

                        // Calculate the distance between the co-ordinate and the left position
                        var leftDist = Distance(coord.Latitude, coord.Longitude, leftPosition.Latitude, leftPosition.Longitude);

                        // Compare the left distance with the closest distance
                        if (leftDist < closestDist)
                        {
                            // If the left distance is smaller, update the closest position and distance
                            closestPosition = leftPosition;
                            closestDist = leftDist;
                        }
                        else
                        {
                            // If the left distance is larger, stop expanding the window to the left
                            complete = true;
                        }
                    }

                    // Check if the right index is valid
                    if (right < data.Count)
                    {
                        // Create a Position object from the data at the right index
                        var rightPosition = new Position(data[right]);

                        // Calculate the distance between the co-ordinate and the right position
                        var rightDist = Distance(coord.Latitude, coord.Longitude, rightPosition.Latitude, rightPosition.Longitude);

                        // Compare the right distance with the closest distance
                        if (rightDist < closestDist)
                        {
                            // If the right distance is smaller, update the closest position and distance
                            closestPosition = rightPosition;
                            closestDist = rightDist;
                        }
                        else
                        {
                            // If the right distance is larger, stop expanding the window to the right
                            complete = true;
                        }
                    }

                    // If both the left and right indices are invalid, stop the loop
                    if (left < 0 && right >= data.Count)
                    {
                        complete = true;
                    }

                    // Move the left and right indices by one step
                    left--;
                    right++;
                }

                // Store the closest position ID in the array
                result.Add(new Vehicle(closestPosition.Regsitration, closestPosition.Latitude, closestPosition.Longitude));
            }

            return result;

        }

    }
}
