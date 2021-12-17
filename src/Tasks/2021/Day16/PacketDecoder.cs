using System;
using System.Collections.Generic;
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
            long expressionValue = 0;
            List<long> subPackets = new List<long>();
            DoCalculateSumOfAllPacketsVersionNumbers(
                binaryTransmission, ref sumOfAllPacketsVersionNumbers, ref expressionValue, subPackets);

            return sumOfAllPacketsVersionNumbers;
        }

        public long CalculateExpressionValue(string hexadecimalTransmission)
        {
            string binaryTransmission = ConvertHexadecimalToBinary(hexadecimalTransmission);
            int sumOfAllPacketsVersionNumbers = 0;
            long expressionValue = 0;
            List<long> subPackets = new List<long>();
            DoCalculateSumOfAllPacketsVersionNumbers(
                binaryTransmission, ref sumOfAllPacketsVersionNumbers, ref expressionValue, subPackets);

            return expressionValue;
        }

        private string DoCalculateSumOfAllPacketsVersionNumbers(
            string binaryNumber,
            ref int sumOfAllPacketsVersionNumbers,
            ref long expressionValue,
            List<long> subPackets
        )
        {
            int packetVersion = ConvertBinaryToInteger(binaryNumber[..PACKET_VERSION_BITS]);
            sumOfAllPacketsVersionNumbers += packetVersion;

            binaryNumber = binaryNumber[PACKET_VERSION_BITS..];

            int packetType = ConvertBinaryToInteger(binaryNumber[..PACKET_TYPE_BITS]);
            binaryNumber = binaryNumber[PACKET_TYPE_BITS..];

            // If packet represents a literal value
            if (packetType == LITERAL_VALUE_PACKETS)
            {
                (binaryNumber, long literalValue) = GetLiteralValue(binaryNumber);
                subPackets.Add(literalValue);
            }
            // If packet represents operator
            else
            {
                string lengthTypeId = binaryNumber[..1];
                binaryNumber = binaryNumber[1..];

                long newExpressionValue = 0;
                List<long> newSubPackets = new List<long>();

                // Total length in bits of the sub-packets contained by this packet
                if (lengthTypeId == "0")
                {
                    binaryNumber = GetLengthTypeZeroSubPackets(
                        binaryNumber, ref sumOfAllPacketsVersionNumbers, ref newExpressionValue, newSubPackets);
                }
                // Number of sub-packets immediately contained by this packet
                else
                {
                    binaryNumber = GetLengthTypeOneSubPackets(
                        binaryNumber, ref sumOfAllPacketsVersionNumbers, ref newExpressionValue, newSubPackets);
                }

                long result = 0;
                switch (packetType)
                {
                    case 0:
                        result = newSubPackets.Sum();
                        break;
                    case 1:
                        result = newSubPackets.Aggregate((x, y) => x * y);
                        break;
                    case 2:
                        result = newSubPackets.Min();
                        break;
                    case 3:
                        result = newSubPackets.Max();
                        break;
                    case 5:
                        result = newSubPackets[0] > newSubPackets[1] ? 1 : 0;
                        break;
                    case 6:
                        result = newSubPackets[0] < newSubPackets[1] ? 1 : 0;
                        break;
                    case 7:
                        result = newSubPackets[0] == newSubPackets[1] ? 1 : 0;
                        break;
                }

                expressionValue += result;
                subPackets.Add(result);
            }

            return binaryNumber;
        }

        private (string, long) GetLiteralValue(string binaryNumber)
        {
            string literalValueString = string.Empty;

            char startWith = '\0';
            // Each group is prefixed by a 1 bit except the last group, which is prefixed by a 0 bit
            while (startWith != LITERAL_VALUE_LAST_GROUP_PREFIX)
            {
                startWith = binaryNumber[0];
                literalValueString += binaryNumber[1..LITERAL_VALUE_GROUP_SIZE];
                binaryNumber = binaryNumber[LITERAL_VALUE_GROUP_SIZE..];
            }

            long literalValue = ConvertBinaryToLongInteger(literalValueString);

            return (binaryNumber, literalValue);
        }

        private string GetLengthTypeZeroSubPackets(
            string binaryNumber,
            ref int sumOfAllPacketsVersionNumbers,
            ref long expressionValue,
            List<long> subPackets
        )
        {
            int subPacketsLength = ConvertBinaryToInteger(binaryNumber[..SUB_PACKETS_TOTAL_LENGTH_BITS]);
            binaryNumber = binaryNumber[SUB_PACKETS_TOTAL_LENGTH_BITS..];
            string rest = binaryNumber[subPacketsLength..];
            binaryNumber = binaryNumber[..subPacketsLength];

            while (binaryNumber.Length > 0)
            {
                string newBinaryNumber = DoCalculateSumOfAllPacketsVersionNumbers(
                    binaryNumber, ref sumOfAllPacketsVersionNumbers, ref expressionValue, subPackets);

                binaryNumber = newBinaryNumber;
            }

            return rest;
        }

        private string GetLengthTypeOneSubPackets(
            string binaryNumber,
            ref int sumOfAllPacketsVersionNumbers,
            ref long expressionValue,
            List<long> subPackets
        )
        {
            int subPacketsNumber = ConvertBinaryToInteger(binaryNumber[..CONTAINED_SUB_PACKETS_NUMBER_BITS]);
            binaryNumber = binaryNumber[CONTAINED_SUB_PACKETS_NUMBER_BITS..];

            while (subPacketsNumber > 0)
            {
                binaryNumber = DoCalculateSumOfAllPacketsVersionNumbers(
                    binaryNumber, ref sumOfAllPacketsVersionNumbers, ref expressionValue, subPackets);
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

        private long ConvertBinaryToLongInteger(string binary)
        {
            long longInteger = Convert.ToInt64(binary, 2);
            return longInteger;
        }
    }
}
