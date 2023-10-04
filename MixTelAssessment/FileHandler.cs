using MixTelAssessment;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MixTelAssessment
{
    public class FileHandler
    {
        private readonly string fileName = "VehiclePositions.dat";
        private readonly string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString();
        private string path;


        public FileHandler()
        {
            path = Path.Combine(directory, fileName);
        }

        /// <summary>
        /// Reads file from bin folder
        /// </summary>
        /// <returns>List of vehicles from the input file</returns>
        public List<Vehicle> ReadFile()
        {

            List<Vehicle> values = new List<Vehicle>();
            FileStream stream = new FileStream(path, FileMode.Open);
            int recordCount = (int)(stream.Length / 276);

            try
            {
                if (File.Exists(path))
                {
                    using BinaryReader reader = new BinaryReader(stream);
                    for (int i = 0; i < recordCount; i++)
                    {
                        values.Add(new Vehicle(reader.ReadInt32(), ReadNullTerminatedString(reader), reader.ReadSingle(), reader.ReadSingle(), reader.ReadInt64()));
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
            finally
            {
                stream.Dispose();
            }

            return values;
        }

        /// <summary>
        /// Used to format null terminated string value
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>string value derived from formatted input</returns>
        private string ReadNullTerminatedString(BinaryReader reader)
        {
            List<char> chars = new List<char>();
            char currentChar;

            while ((currentChar = reader.ReadChar()) != '\0')
            {
                chars.Add(currentChar);
            }

            return new string(chars.ToArray());
        }
    }
}
