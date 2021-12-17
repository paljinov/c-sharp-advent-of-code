using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day16
{
    public class PacketDecoder
    {
        private const int HEXADECIMAL_TOTAL_BITS = 4;

        private const int LENGTH_TYPE_ID_BITS = 1;

        private const char LITERAL_VALUE_LAST_GROUP_PREFIX = '0';

        private const int LITERAL_VALUE_BINARY_NUMBER_BITS = 4;

        private const int LITERAL_VALUE_PREFIX_BITS = 1;

        private const int PACKET_TYPE_BITS = 3;

        private const int PACKET_VERSION_BITS = 3;

        private const int SUB_PACKETS_NUMBER_BITS = 11;

        private const int SUB_PACKETS_TOTAL_LENGTH_BITS = 15;

        public int CalculateSumOfAllPacketsVersionNumbers(string hexadecimalTransmission)
        {
            string binaryTransmission = ConvertHexadecimalToBinary(hexadecimalTransmission);
            (_, int sumOfAllPacketsVersionNumbers, _) =
                DoCalculateSumOfAllPacketsVersionNumbers(binaryTransmission, new List<long>());

            return sumOfAllPacketsVersionNumbers;
        }

        public long CalculateEvaluatedExpressionResult(string hexadecimalTransmission)
        {
            string binaryTransmission = ConvertHexadecimalToBinary(hexadecimalTransmission);
            (_, _, long evaluatedExpressionResult) =
                DoCalculateSumOfAllPacketsVersionNumbers(binaryTransmission, new List<long>());

            return evaluatedExpressionResult;
        }

        private (string, int, long) DoCalculateSumOfAllPacketsVersionNumbers(string binaryNumber, List<long> subPackets)
        {
            int sumOfAllPacketsVersionNumbers = 0;
            long evaluatedExpressionResult = 0;

            int packetVersion = ConvertBinaryToInteger(binaryNumber[..PACKET_VERSION_BITS]);
            sumOfAllPacketsVersionNumbers += packetVersion;
            binaryNumber = binaryNumber[PACKET_VERSION_BITS..];

            PacketType packetType = GetPacketType(binaryNumber[..PACKET_TYPE_BITS]);
            binaryNumber = binaryNumber[PACKET_TYPE_BITS..];

            // If packet represents a literal value
            if (packetType == PacketType.LiteralValue)
            {
                (binaryNumber, long literalValue) = GetLiteralValue(binaryNumber);
                subPackets.Add(literalValue);
            }
            // If packet represents operator
            else
            {
                LengthType lengthType = GetLengthType(binaryNumber[..LENGTH_TYPE_ID_BITS]);
                binaryNumber = binaryNumber[LENGTH_TYPE_ID_BITS..];

                List<long> newSubPackets = new List<long>();
                int sumOfSubPacketsVersionNumbers;

                // Total length in bits of the sub-packets contained by this packet
                if (lengthType == LengthType.TotalLengthInBits)
                {
                    (binaryNumber, sumOfSubPacketsVersionNumbers) =
                        GetLengthTypeZeroSubPackets(binaryNumber, newSubPackets);
                }
                // Number of sub-packets immediately contained by this packet
                else
                {
                    (binaryNumber, sumOfSubPacketsVersionNumbers) =
                        GetLengthTypeOneSubPackets(binaryNumber, newSubPackets);
                }

                sumOfAllPacketsVersionNumbers += sumOfSubPacketsVersionNumbers;

                long expressionResult = CalculateSubPacketsExpressionResult(packetType, newSubPackets);
                evaluatedExpressionResult += expressionResult;
                subPackets.Add(expressionResult);
            }

            return (binaryNumber, sumOfAllPacketsVersionNumbers, evaluatedExpressionResult);
        }

        private PacketType GetPacketType(string binaryPacketType)
        {
            PacketType packetType = (PacketType)ConvertBinaryToInteger(binaryPacketType);
            return packetType;
        }

        private LengthType GetLengthType(string binaryLengthType)
        {
            LengthType lengthType = (LengthType)ConvertBinaryToInteger(binaryLengthType);
            return lengthType;
        }

        private (string, long) GetLiteralValue(string binaryNumber)
        {
            string literalValueString = string.Empty;
            int packetSize = LITERAL_VALUE_PREFIX_BITS + LITERAL_VALUE_BINARY_NUMBER_BITS;

            char startWith = '\0';
            // Each group is ixed by a 1 bit except the last group, which is ixed by a 0 bit
            while (startWith != LITERAL_VALUE_LAST_GROUP_PREFIX)
            {
                literalValueString += binaryNumber[LITERAL_VALUE_PREFIX_BITS..packetSize];

                startWith = binaryNumber[0];
                binaryNumber = binaryNumber[packetSize..];
            }

            long literalValue = ConvertBinaryToLongInteger(literalValueString);

            return (binaryNumber, literalValue);
        }

        private (string, int) GetLengthTypeZeroSubPackets(string binaryNumber, List<long> subPackets)
        {
            int subPacketsLength = ConvertBinaryToInteger(binaryNumber[..SUB_PACKETS_TOTAL_LENGTH_BITS]);
            binaryNumber = binaryNumber[SUB_PACKETS_TOTAL_LENGTH_BITS..];
            string rest = binaryNumber[subPacketsLength..];
            binaryNumber = binaryNumber[..subPacketsLength];

            int sumOfSubPacketsVersionNumbers = 0;
            while (binaryNumber.Length > 0)
            {
                (binaryNumber, int sumOfSubPacketVersionNumbers, _) =
                    DoCalculateSumOfAllPacketsVersionNumbers(binaryNumber, subPackets);
                sumOfSubPacketsVersionNumbers += sumOfSubPacketVersionNumbers;
            }

            return (rest, sumOfSubPacketsVersionNumbers);
        }

        private (string, int) GetLengthTypeOneSubPackets(string binaryNumber, List<long> subPackets)
        {
            int subPacketsNumber = ConvertBinaryToInteger(binaryNumber[..SUB_PACKETS_NUMBER_BITS]);
            binaryNumber = binaryNumber[SUB_PACKETS_NUMBER_BITS..];

            int sumOfSubPacketsVersionNumbers = 0;
            while (subPacketsNumber > 0)
            {
                (binaryNumber, int sumOfSubPacketVersionNumbers, _) =
                    DoCalculateSumOfAllPacketsVersionNumbers(binaryNumber, subPackets);
                sumOfSubPacketsVersionNumbers += sumOfSubPacketVersionNumbers;

                subPacketsNumber--;
            }

            return (binaryNumber, sumOfSubPacketsVersionNumbers);
        }

        private long CalculateSubPacketsExpressionResult(PacketType packetType, List<long> subPackets)
        {
            long expressionResult = 0;

            switch (packetType)
            {
                case PacketType.Sum:
                    expressionResult = subPackets.Sum();
                    break;
                case PacketType.Product:
                    expressionResult = subPackets.Aggregate((x, y) => x * y);
                    break;
                case PacketType.Minimum:
                    expressionResult = subPackets.Min();
                    break;
                case PacketType.Maximum:
                    expressionResult = subPackets.Max();
                    break;
                case PacketType.GreaterThan:
                    expressionResult = subPackets[0] > subPackets[1] ? 1 : 0;
                    break;
                case PacketType.LessThan:
                    expressionResult = subPackets[0] < subPackets[1] ? 1 : 0;
                    break;
                case PacketType.EqualTo:
                    expressionResult = subPackets[0] == subPackets[1] ? 1 : 0;
                    break;
            }

            return expressionResult;
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
