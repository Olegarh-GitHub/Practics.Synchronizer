using System;
using System.Linq;
using System.Text;

namespace Practics.Synchronizer.Core.Extensions
{
    public static class OctetStringExtensions
    {
        public static string ToOctetString(this byte[] byteArray)
        {
            var sb = new StringBuilder();
            
            for (int i = 0; i < byteArray.Length; i++)
            {
                sb.AppendFormat(
                    "\\{0}",
                    byteArray[i].ToString("X2")
                );
            }

            return sb.ToString();
        }

        public static string BytesToGuid(this byte[] byteArray)
        {
            var convertedBin = BitConverter.ToString(byteArray).Replace("-", "").ToLower();

            var builder = new StringBuilder();

            builder.Append(convertedBin.Substring(24, 8) + "-");
            builder.Append(convertedBin.Substring(20, 4) + "-");
            builder.Append(convertedBin.Substring(16, 4) + "-");
            builder.Append(convertedBin.Substring(0, 4) + "-");
            builder.Append(convertedBin.Substring(4, 12));

            return builder.ToString();
        }

        public static byte[] GuidToBytes(this string guid)
        {
            var substrings = guid.Split('-');

            var builder = new StringBuilder();
            builder.Append(substrings[3]);
            builder.Append(substrings[4]);
            builder.Append(substrings[2]);
            builder.Append(substrings[1]);
            builder.Append(substrings[0]);

            var hex = builder.ToString();

            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
    }
}