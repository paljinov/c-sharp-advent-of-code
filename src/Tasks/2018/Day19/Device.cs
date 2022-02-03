using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day19
{
    public class Device
    {
        private const int TOTAL_OPCODES = 16;

        private const int TOTAL_REGISTERS = 4;

        public int CalculateRegisterZeroValueWhenTheBackgroundProcessHalts(Sample[] samples)
        {
            int samplesThatBehaveLikeThreeOrMoreOpcodes = 0;

            foreach (Sample sample in samples)
            {
                List<Opcode> behavesLikeOpcodes = GetOpcodesSampleBehavesAs(sample);

                if (behavesLikeOpcodes.Count >= 3)
                {
                    samplesThatBehaveLikeThreeOrMoreOpcodes++;
                }
            }

            return samplesThatBehaveLikeThreeOrMoreOpcodes;
        }

        private List<Opcode> GetOpcodesSampleBehavesAs(Sample sample)
        {
            List<Opcode> behavesLikeOpcodes = new List<Opcode>();

            if (AddRegister(sample))
            {
                behavesLikeOpcodes.Add(Opcode.AddRegister);
            }

            if (AddImmediate(sample))
            {
                behavesLikeOpcodes.Add(Opcode.AddImmediate);
            }

            if (MultiplyRegister(sample))
            {
                behavesLikeOpcodes.Add(Opcode.MultiplyRegister);
            }

            if (MultiplyImmediate(sample))
            {
                behavesLikeOpcodes.Add(Opcode.MultiplyImmediate);
            }

            if (BitwiseAndRegister(sample))
            {
                behavesLikeOpcodes.Add(Opcode.BitwiseAndRegister);
            }

            if (BitwiseAndImmediate(sample))
            {
                behavesLikeOpcodes.Add(Opcode.BitwiseAndImmediate);
            }

            if (BitwiseOrRegister(sample))
            {
                behavesLikeOpcodes.Add(Opcode.BitwiseOrRegister);
            }

            if (BitwiseOrImmediate(sample))
            {
                behavesLikeOpcodes.Add(Opcode.BitwiseOrImmediate);
            }

            if (SetRegister(sample))
            {
                behavesLikeOpcodes.Add(Opcode.SetRegister);
            }

            if (SetImmediate(sample))
            {
                behavesLikeOpcodes.Add(Opcode.SetImmediate);
            }

            if (GreaterThanImmediateRegister(sample))
            {
                behavesLikeOpcodes.Add(Opcode.GreaterThanImmediateRegister);
            }

            if (GreaterThanRegisterImmediate(sample))
            {
                behavesLikeOpcodes.Add(Opcode.GreaterThanRegisterImmediate);
            }

            if (GreaterThanRegisterRegister(sample))
            {
                behavesLikeOpcodes.Add(Opcode.GreaterThanRegisterRegister);
            }

            if (EqualImmediateRegister(sample))
            {
                behavesLikeOpcodes.Add(Opcode.EqualImmediateRegister);
            }

            if (EqualRegisterImmediate(sample))
            {
                behavesLikeOpcodes.Add(Opcode.EqualRegisterImmediate);
            }

            if (EqualRegisterRegister(sample))
            {
                behavesLikeOpcodes.Add(Opcode.EqualRegisterRegister);
            }

            return behavesLikeOpcodes;
        }

        private void FindOpcodes(
            Dictionary<Sample, List<Opcode>> samplesBehaveLikeOpcodes,
            Dictionary<int, Opcode> opcodes)
        {
            Opcode? foundOpcode = null;

            foreach (KeyValuePair<Sample, List<Opcode>> behavesLikeOpcodes in samplesBehaveLikeOpcodes)
            {
                if (behavesLikeOpcodes.Value.Count == 1)
                {
                    foundOpcode = behavesLikeOpcodes.Value.First();
                    opcodes[behavesLikeOpcodes.Key.Instruction.Opcode] = foundOpcode.Value;
                    break;
                }
            }

            if (foundOpcode.HasValue)
            {
                // Remove found opcode
                foreach (KeyValuePair<Sample, List<Opcode>> behavesLikeOpcodes in samplesBehaveLikeOpcodes)
                {
                    if (behavesLikeOpcodes.Value.Contains(foundOpcode.Value))
                    {
                        behavesLikeOpcodes.Value.Remove(foundOpcode.Value);
                    }
                }

                if (opcodes.Count < TOTAL_OPCODES)
                {
                    FindOpcodes(samplesBehaveLikeOpcodes, opcodes);
                }
            }
        }

        private int[] ExecuteTestProgram(Dictionary<int, Opcode> opcodes, int[][] testProgram)
        {
            int[] registers = new int[TOTAL_REGISTERS];

            foreach (int[] instruction in testProgram)
            {
                Opcode opcode = opcodes[instruction[0]];
                int inputA = instruction[1];
                int inputB = instruction[2];
                int outputC = instruction[3];

                switch (opcode)
                {
                    case Opcode.AddRegister:
                        registers[outputC] = registers[inputA] + registers[inputB];
                        break;
                    case Opcode.AddImmediate:
                        registers[outputC] = registers[inputA] + inputB;
                        break;
                    case Opcode.MultiplyRegister:
                        registers[outputC] = registers[inputA] * registers[inputB];
                        break;
                    case Opcode.MultiplyImmediate:
                        registers[outputC] = registers[inputA] * inputB;
                        break;
                    case Opcode.BitwiseAndRegister:
                        registers[outputC] = registers[inputA] & registers[inputB];
                        break;
                    case Opcode.BitwiseAndImmediate:
                        registers[outputC] = registers[inputA] & inputB;
                        break;
                    case Opcode.BitwiseOrRegister:
                        registers[outputC] = registers[inputA] | registers[inputB];
                        break;
                    case Opcode.BitwiseOrImmediate:
                        registers[outputC] = registers[inputA] | inputB;
                        break;
                    case Opcode.SetRegister:
                        registers[outputC] = registers[inputA];
                        break;
                    case Opcode.SetImmediate:
                        registers[outputC] = inputA;
                        break;
                    case Opcode.GreaterThanImmediateRegister:
                        registers[outputC] = inputA > registers[inputB] ? 1 : 0;
                        break;
                    case Opcode.GreaterThanRegisterImmediate:
                        registers[outputC] = registers[inputA] > inputB ? 1 : 0;
                        break;
                    case Opcode.GreaterThanRegisterRegister:
                        registers[outputC] = registers[inputA] > registers[inputB] ? 1 : 0;
                        break;
                    case Opcode.EqualImmediateRegister:
                        registers[outputC] = inputA == registers[inputB] ? 1 : 0;
                        break;
                    case Opcode.EqualRegisterImmediate:
                        registers[outputC] = registers[inputA] == inputB ? 1 : 0;
                        break;
                    case Opcode.EqualRegisterRegister:
                        registers[outputC] = registers[inputA] == registers[inputB] ? 1 : 0;
                        break;
                }
            }

            return registers;
        }

        private bool AddRegister(Sample sample)
        {
            int outputC = sample.Before[sample.Instruction.InputA] + sample.Before[sample.Instruction.InputB];
            if (outputC == sample.After[sample.Instruction.OutputC])
            {
                return true;
            }

            return false;
        }

        private bool AddImmediate(Sample sample)
        {
            int outputC = sample.Before[sample.Instruction.InputA] + sample.Instruction.InputB;
            if (outputC == sample.After[sample.Instruction.OutputC])
            {
                return true;
            }

            return false;
        }

        private bool MultiplyRegister(Sample sample)
        {
            int outputC = sample.Before[sample.Instruction.InputA] * sample.Before[sample.Instruction.InputB];
            if (outputC == sample.After[sample.Instruction.OutputC])
            {
                return true;
            }

            return false;
        }

        private bool MultiplyImmediate(Sample sample)
        {
            int outputC = sample.Before[sample.Instruction.InputA] * sample.Instruction.InputB;
            if (outputC == sample.After[sample.Instruction.OutputC])
            {
                return true;
            }

            return false;
        }

        private bool BitwiseAndRegister(Sample sample)
        {
            int outputC = sample.Before[sample.Instruction.InputA] & sample.Before[sample.Instruction.InputB];
            if (outputC == sample.After[sample.Instruction.OutputC])
            {
                return true;
            }

            return false;
        }

        private bool BitwiseAndImmediate(Sample sample)
        {
            int outputC = sample.Before[sample.Instruction.InputA] & sample.Instruction.InputB;
            if (outputC == sample.After[sample.Instruction.OutputC])
            {
                return true;
            }

            return false;
        }

        private bool BitwiseOrRegister(Sample sample)
        {
            int outputC = sample.Before[sample.Instruction.InputA] | sample.Before[sample.Instruction.InputB];
            if (outputC == sample.After[sample.Instruction.OutputC])
            {
                return true;
            }

            return false;
        }

        private bool BitwiseOrImmediate(Sample sample)
        {
            int outputC = sample.Before[sample.Instruction.InputA] | sample.Instruction.InputB;
            if (outputC == sample.After[sample.Instruction.OutputC])
            {
                return true;
            }

            return false;
        }

        private bool SetRegister(Sample sample)
        {
            int outputC = sample.Before[sample.Instruction.InputA];
            if (outputC == sample.After[sample.Instruction.OutputC])
            {
                return true;
            }

            return false;
        }

        private bool SetImmediate(Sample sample)
        {
            int outputC = sample.Instruction.InputA;
            if (outputC == sample.After[sample.Instruction.OutputC])
            {
                return true;
            }

            return false;
        }

        private bool GreaterThanImmediateRegister(Sample sample)
        {
            int outputC = sample.Instruction.InputA > sample.Before[sample.Instruction.InputB] ? 1 : 0;
            if (outputC == sample.After[sample.Instruction.OutputC])
            {
                return true;
            }

            return false;
        }

        private bool GreaterThanRegisterImmediate(Sample sample)
        {
            int outputC = sample.Before[sample.Instruction.InputA] > sample.Instruction.InputB ? 1 : 0;
            if (outputC == sample.After[sample.Instruction.OutputC])
            {
                return true;
            }

            return false;
        }

        private bool GreaterThanRegisterRegister(Sample sample)
        {
            int outputC = sample.Before[sample.Instruction.InputA] > sample.Before[sample.Instruction.InputB] ? 1 : 0;
            if (outputC == sample.After[sample.Instruction.OutputC])
            {
                return true;
            }

            return false;
        }

        private bool EqualImmediateRegister(Sample sample)
        {
            int outputC = sample.Instruction.InputA == sample.Before[sample.Instruction.InputB] ? 1 : 0;
            if (outputC == sample.After[sample.Instruction.OutputC])
            {
                return true;
            }

            return false;
        }

        private bool EqualRegisterImmediate(Sample sample)
        {
            int outputC = sample.Before[sample.Instruction.InputA] == sample.Instruction.InputB ? 1 : 0;
            if (outputC == sample.After[sample.Instruction.OutputC])
            {
                return true;
            }

            return false;
        }

        private bool EqualRegisterRegister(Sample sample)
        {
            int outputC = sample.Before[sample.Instruction.InputA] == sample.Before[sample.Instruction.InputB] ? 1 : 0;
            if (outputC == sample.After[sample.Instruction.OutputC])
            {
                return true;
            }

            return false;
        }
    }
}
