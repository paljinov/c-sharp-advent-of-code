using System.Text;

namespace App.Tasks.Year2016.Day16
{
    public class Checksum
    {
        public string CalculateChecksum(string initialState, int diskLength)
        {
            string data = MakeDataLonger(initialState, diskLength);

            StringBuilder checksum = null;
            // If the length of the checksum is even, repeat the process
            // until you end up with a checksum with an odd length
            while (checksum is null || checksum.Length % 2 == 0)
            {
                checksum = new StringBuilder();
                for (int i = 0; i < data.Length - 1; i += 2)
                {
                    // If the two characters match (00 or 11), the next checksum character is a 1
                    if (data[i] == data[i + 1])
                    {
                        checksum.Append('1');
                    }
                    // If the characters do not match (01 or 10), the next checksum character is a 0
                    else
                    {
                        checksum.Append('0');
                    }
                }

                data = checksum.ToString();
            }


            return checksum.ToString();
        }

        private string MakeDataLonger(string data, int diskLength)
        {
            StringBuilder a = new StringBuilder(data);

            while (a.Length < diskLength)
            {
                StringBuilder b = new StringBuilder();
                // Reverse the order of the characters in "b"
                for (int i = a.Length - 1; i >= 0; i--)
                {
                    // In "b", replace all instances of 0 with 1 and all 1s with 0
                    if (a[i] == '1')
                    {
                        b.Append('0');
                    }
                    else
                    {
                        b.Append('1');
                    }
                }

                // The resulting data is "a", then a single 0, then "b"
                a.Append('0').Append(b);
            }

            // Calculate the checksum only for the data that fits on the disk,
            // even if you generated more data than that in the previous step
            string resultData = a.ToString(0, diskLength);

            return resultData;
        }
    }
}
