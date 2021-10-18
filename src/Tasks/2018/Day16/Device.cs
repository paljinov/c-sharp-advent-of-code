namespace App.Tasks.Year2018.Day16
{
    public class Device
    {
        public int CountSamplesThatBehaveLikeThreeOrMoreOpcodes(Sample[] samples)
        {
            int samplesThatBehaveLikeThreeOrMoreOpcodes = 0;

            foreach (Sample sample in samples)
            {
                int behavesLikeOpcode = 0;

                if (AddRegister(sample))
                {
                    behavesLikeOpcode++;
                }

                if (AddImmediate(sample))
                {
                    behavesLikeOpcode++;
                }

                if (MultiplyRegister(sample))
                {
                    behavesLikeOpcode++;
                }

                if (MultiplyImmediate(sample))
                {
                    behavesLikeOpcode++;
                }

                if (BitwiseAndRegister(sample))
                {
                    behavesLikeOpcode++;
                }

                if (BitwiseAndImmediate(sample))
                {
                    behavesLikeOpcode++;
                }

                if (BitwiseOrRegister(sample))
                {
                    behavesLikeOpcode++;
                }

                if (BitwiseOrImmediate(sample))
                {
                    behavesLikeOpcode++;
                }

                if (SetRegister(sample))
                {
                    behavesLikeOpcode++;
                }

                if (SetImmediate(sample))
                {
                    behavesLikeOpcode++;
                }

                if (GreaterThanImmediateRegister(sample))
                {
                    behavesLikeOpcode++;
                }

                if (GreaterThanRegisterImmediate(sample))
                {
                    behavesLikeOpcode++;
                }

                if (GreaterThanRegisterRegister(sample))
                {
                    behavesLikeOpcode++;
                }

                if (EqualImmediateRegister(sample))
                {
                    behavesLikeOpcode++;
                }

                if (EqualRegisterImmediate(sample))
                {
                    behavesLikeOpcode++;
                }

                if (EqualRegisterRegister(sample))
                {
                    behavesLikeOpcode++;
                }

                if (behavesLikeOpcode >= 3)
                {
                    samplesThatBehaveLikeThreeOrMoreOpcodes++;
                }
            }

            return samplesThatBehaveLikeThreeOrMoreOpcodes;
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
