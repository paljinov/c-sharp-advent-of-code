using System.Collections.Generic;

namespace App.Tasks.Year2018.Day16
{
    public class Device
    {
        public int CountSamplesThatBehaveLikeThreeOrMoreOpcodes(Sample[] samples)
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

        public int CalculateRegisterZeroValueAfterExecutingTestProgram(Sample[] samples, int[][] testProgram)
        {
            int registerZeroValue = 0;

            return registerZeroValue;
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
