using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2020.Day14
{
    public class Memory
    {
        public long MemoryValuesSumForDecoderChipVersion1(List<ProgramSequence> initializationProgram)
        {
            Dictionary<int, long> memory = new Dictionary<int, long>();

            foreach (ProgramSequence programSequence in initializationProgram)
            {
                foreach (MemoryAddress memoryAddress in programSequence.MemoryAddresses)
                {
                    long valueAfterBitmask = GetValueAfterBitmask(programSequence.Bitmask, memoryAddress.Value);
                    memory[memoryAddress.Address] = valueAfterBitmask;
                }
            }

            long memoryValuesSum = memory.Sum(x => x.Value);

            return memoryValuesSum;
        }

        public long MemoryValuesSumForDecoderChipVersion2(List<ProgramSequence> initializationProgram)
        {
            Dictionary<long, long> memory = new Dictionary<long, long>();

            foreach (ProgramSequence programSequence in initializationProgram)
            {
                foreach (MemoryAddress memoryAddress in programSequence.MemoryAddresses)
                {
                    long[] memoryAddressesPermutations = GetMemoryAddressesPermutationsAfterBitmask(
                        programSequence.Bitmask,
                        memoryAddress.Address
                    );

                    foreach (long memoryAddressPermutation in memoryAddressesPermutations)
                    {
                        memory[memoryAddressPermutation] = memoryAddress.Value;
                    }
                }
            }

            long memoryValuesSum = memory.Sum(x => x.Value);

            return memoryValuesSum;
        }

        private long GetValueAfterBitmask(string bitmask, int integer)
        {
            StringBuilder binary = new StringBuilder(Convert.ToString(integer, 2));

            int i = bitmask.Length - 1;
            for (int j = binary.Length - 1; j >= 0; j--)
            {
                if (bitmask[i] == '0')
                {
                    binary[j] = '0';
                }
                else if (bitmask[i] == '1')
                {
                    binary[j] = '1';
                }

                i--;
            }

            int leadingOne = bitmask.IndexOf('1');
            if (leadingOne != -1)
            {
                int prefixLength = bitmask.Length - leadingOne - binary.Length;
                if (prefixLength > 0)
                {
                    string prefix = bitmask.Substring(leadingOne, prefixLength);
                    binary.Insert(0, prefix);
                    binary.Replace('X', '0');
                }
            }

            long value = Convert.ToInt64(binary.ToString(), 2);

            return value;
        }

        private long[] GetMemoryAddressesPermutationsAfterBitmask(string bitmask, int integer)
        {
            StringBuilder binary = new StringBuilder(Convert.ToString(integer, 2));

            int i = bitmask.Length - 1;
            for (int j = binary.Length - 1; j >= 0; j--)
            {
                if (bitmask[i] == '1')
                {
                    binary[j] = '1';
                }
                else if (bitmask[i] == 'X')
                {
                    binary[j] = 'X';
                }

                i--;
            }

            string prefix = bitmask.Substring(0, bitmask.Length - binary.Length);
            binary.Insert(0, prefix);

            List<string> memoryAddressesPermutations = new List<string>();
            GetMemoryAddressesPermutations(binary.ToString().ToCharArray(), memoryAddressesPermutations);

            long[] memoryAddresses = new long[memoryAddressesPermutations.Count];
            for (i = 0; i < memoryAddressesPermutations.Count; i++)
            {
                memoryAddresses[i] = Convert.ToInt64(memoryAddressesPermutations[i].ToString(), 2);
            }

            return memoryAddresses;
        }

        private void GetMemoryAddressesPermutations(char[] binary, List<string> memoryAddressesPermutations)
        {
            int floatingBitmaskIndex = Array.IndexOf(binary, 'X');
            if (floatingBitmaskIndex != -1)
            {
                for (char c = '0'; c <= '1'; c++)
                {
                    binary[floatingBitmaskIndex] = c;
                    GetMemoryAddressesPermutations(binary.ToArray(), memoryAddressesPermutations);
                }
            }
            else
            {
                memoryAddressesPermutations.Add(new string(binary));
            }
        }
    }
}
