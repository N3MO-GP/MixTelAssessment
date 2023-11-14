using System.Reflection;
using System.Text;

namespace MixTelAssessment
{
    internal static class Util
    {
        internal static DateTime Epoch => new DateTime(1970, 1, 1, 0, 0, 0, 0);

        internal static string GetLocalFilePath(string fileName) => Util.GetLocalFilePath(string.Empty, fileName);

        internal static string GetLocalFilePath(string subDirectory, string fileName) => Path.Combine(Util.GetLocalPath(subDirectory), fileName);

        internal static string GetLocalPath(string subDirectory)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (subDirectory != string.Empty)
                path = Path.Combine(path, subDirectory);

            return path;
        }

        internal static DateTime FromCTime(ulong cTime) => Util.Epoch.AddSeconds((double)cTime);

        internal static double ToRadian(double value)
        {
            return value * Math.PI / 180;
        }
    }
}
