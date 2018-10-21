using System;
using System.Collections.Generic;
using System.Text;
using Barcoder.Utils;

namespace Barcoder
{
    public static class Code39
    {
        public static IBarcodeIntCS Encode(string content, bool includeChecksum, bool fullAsciiMode)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            if (fullAsciiMode)
                content = Prepare(content);
            else if (content.Contains("*"))
                throw new InvalidOperationException("Invalid data! Try full ASCII mode");

            char checksum = GetChecksum(content);

            var data = new StringBuilder("*");
            data.Append(content);
            if (includeChecksum)
                data.Append(checksum);
            data.Append("*");

            var result = new BitList();
            var i = 0;
            foreach (char r in data.ToString())
            {
                if (i++ != 0)
                    result.AddBit(false);

                if (!Code39Constants.EncodingTable.TryGetValue(r, out (int value, bool[] data) info))
                    throw new InvalidOperationException("Invalid data! Try full ASCII mode");

                result.AddBit(info.data);
            }

            return new Base1DCodeIntCS(result, Constants.TypeCode39, content, checksum, Code39Constants.Margin);
        }

        private static string Prepare(string content)
        {
            var result = new StringBuilder();
            foreach (char r in content)
            {
                if (r > 127)
                    throw new InvalidOperationException("Only ASCII strings can be encoded");

                if (Code39Constants.ExtendedTable.TryGetValue(r, out var val))
                    result.Append(val);
                else
                    result.Append(r);
            }

            return result.ToString();
        }

        private static char GetChecksum(string content)
        {
            var sum = 0;
            foreach (char r in content)
            {
                if (!Code39Constants.EncodingTable.TryGetValue(r, out (int value, bool[] data) info) || info.value < 0)
                    return '#';
                sum += info.value;
            }

            sum = sum % 43;
            foreach (KeyValuePair<char, (int value, bool[] data)> kvp in Code39Constants.EncodingTable)
            {
                if (kvp.Value.value == sum)
                    return kvp.Key;
            }
            return '#';
        }
    }
}
