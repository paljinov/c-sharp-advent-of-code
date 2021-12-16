using System;
using System.Linq;

namespace App.Tasks.Year2021.Day16
{
    public class PacketDecoder
    {
        private const int HEXADECIMAL_TOTAL_BITS = 4;

        private const int PACKET_TYPE_BITS = 3;

        private const int PACKET_VERSION_BITS = 3;

        private const int LITERAL_VALUE_PACKETS = 4;

        private const char LITERAL_VALUE_LAST_GROUP_PREFIX = '0';

        private const int LITERAL_VALUE_GROUP_SIZE = 5;

        private const int SUB_PACKETS_TOTAL_LENGTH_BITS = 15;

        private const int CONTAINED_SUB_PACKETS_NUMBER_BITS = 11;

        public int CalculateSumOfAllPacketsVersionNumbers(string hexadecimalTransmission)
        {
            string binaryTransmission = ConvertHexadecimalToBinary(hexadecimalTransmission);
            int sumOfAllPacketsVersionNumbers = 0;
            DoCalculateSumOfAllPacketsVersionNumbers(binaryTransmission, ref sumOfAllPacketsVersionNumbers);

            return sumOfAllPacketsVersionNumbers;
        }

        private string DoCalculateSumOfAllPacketsVersionNumbers(string binaryNumber, ref int sumOfAllPacketsVersionNumbers)
        {
            int packetVersion = ConvertBinaryToInteger(binaryNumber[..PACKET_VERSION_BITS]);
            sumOfAllPacketsVersionNumbers += packetVersion;

            binaryNumber = binaryNumber[PACKET_VERSION_BITS..];

            int packetType = ConvertBinaryToInteger(binaryNumber[..PACKET_TYPE_BITS]);
            binaryNumber = binaryNumber[PACKET_TYPE_BITS..];

            // If packet represents a literal value
            if (packetType == LITERAL_VALUE_PACKETS)
            {
                binaryNumber = GetLiteralValue(binaryNumber);
            }
            // If packet represents operator
            else
            {
                string lengthTypeId = binaryNumber[..1];
                binaryNumber = binaryNumber[1..];

                // Total length in bits of the sub-packets contained by this packet
                if (lengthTypeId == "0")
                {
                    binaryNumber = GetLengthTypeZeroSubPackets(binaryNumber, ref sumOfAllPacketsVersionNumbers);
                }
                // Number of sub-packets immediately contained by this packet
                else
                {
                    binaryNumber = GetLengthTypeOneSubPackets(binaryNumber, ref sumOfAllPacketsVersionNumbers);
                }
            }

            return binaryNumber;
        }

        private string GetLiteralValue(string binaryNumber)
        {
            char startWith = '\0';
            // Each group is prefixed by a 1 bit except the last group, which is prefixed by a 0 bit
            while (startWith != LITERAL_VALUE_LAST_GROUP_PREFIX)
            {
                startWith = binaryNumber[0];
                binaryNumber = binaryNumber[LITERAL_VALUE_GROUP_SIZE..];
            }

            return binaryNumber;
        }

        private string GetLengthTypeZeroSubPackets(string binaryNumber, ref int sumOfAllPacketsVersionNumbers)
        {
            int subPacketsLength = ConvertBinaryToInteger(binaryNumber[..SUB_PACKETS_TOTAL_LENGTH_BITS]);
            binaryNumber = binaryNumber[SUB_PACKETS_TOTAL_LENGTH_BITS..];
            string rest = binaryNumber[subPacketsLength..];
            binaryNumber = binaryNumber[..subPacketsLength];

            while (binaryNumber.Length > 0)
            {
                binaryNumber =
                    DoCalculateSumOfAllPacketsVersionNumbers(binaryNumber, ref sumOfAllPacketsVersionNumbers);
            }

            return rest;
        }

        private string GetLengthTypeOneSubPackets(string binaryNumber, ref int sumOfAllPacketsVersionNumbers)
        {
            int subPacketsNumber = ConvertBinaryToInteger(binaryNumber[..CONTAINED_SUB_PACKETS_NUMBER_BITS]);
            binaryNumber = binaryNumber[CONTAINED_SUB_PACKETS_NUMBER_BITS..];

            while (subPacketsNumber > 0)
            {
                binaryNumber =
                    DoCalculateSumOfAllPacketsVersionNumbers(binaryNumber, ref sumOfAllPacketsVersionNumbers);
                subPacketsNumber--;
            }

            return binaryNumber;
        }

        private string ConvertHexadecimalToBinary(string hex)
        {
            string binary = string.Join(
                string.Empty,
                hex.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2)
                    .PadLeft(HEXADECIMAL_TOTAL_BITS, '0')
                )
            );

            return binary;
        }

        private int ConvertBinaryToInteger(string binary)
        {
            int integer = Convert.ToInt32(binary, 2);
            return integer;
        }
    }
}
