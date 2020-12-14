using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2020.Day14
{
    public class Memory
    {
        private const char BITMASK_BIT_ZERO = '0';

        private const char BITMASK_BIT_ONE = '1';

        private const char BITMASK_BIT_X = 'X';

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
                if (bitmask[i] == BITMASK_BIT_ZERO)
                {
                    binary[j] = BITMASK_BIT_ZERO;
                }
                else if (bitmask[i] == BITMASK_BIT_ONE)
                {
                    binary[j] = BITMASK_BIT_ONE;
                }

                i--;
            }

            string bitmaskPrefix = bitmask.Substring(0, bitmask.Length - binary.Length);
            binary.Insert(0, bitmaskPrefix);
            binary.Replace(BITMASK_BIT_X, BITMASK_BIT_ZERO);

            long value = Convert.ToInt64(binary.ToString(), 2);

            return value;
        }

        private long[] GetMemoryAddressesPermutationsAfterBitmask(string bitmask, int integer)
        {
            StringBuilder binary = new StringBuilder(Convert.ToString(integer, 2));

            int i = bitmask.Length - 1;
            for (int j = binary.Length - 1; j >= 0; j--)
            {
                if (bitmask[i] == BITMASK_BIT_ONE)
                {
                    binary[j] = BITMASK_BIT_ONE;
                }
                else if (bitmask[i] == BITMASK_BIT_X)
                {
                    binary[j] = BITMASK_BIT_X;
                }

                i--;
            }

            string bitmaskPrefix = bitmask.Substring(0, bitmask.Length - binary.Length);
            binary.Insert(0, bitmaskPrefix);

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
            int floatingBitmaskIndex = Array.IndexOf(binary, BITMASK_BIT_X);
            if (floatingBitmaskIndex != -1)
            {
                for (char c = BITMASK_BIT_ZERO; c <= BITMASK_BIT_ONE; c++)
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
