using System.Collections.Generic;

namespace App.Tasks.Year2016.Day7
{
    public class SecurityLayer
    {
        public int CountIpAddressesWhichSupportTls(List<IpAddress> ipAddresses)
        {
            int ipAddressesWhichSupportTls = 0;

            foreach (IpAddress ipAddress in ipAddresses)
            {
                bool hypernetSequenceHasAbba = false;
                foreach (string hypernetSequence in ipAddress.HypernetSequences)
                {
                    if (HasAutonomousBridgeBypassAnnotation(hypernetSequence))
                    {
                        hypernetSequenceHasAbba = true;
                        break;
                    }
                }

                if (!hypernetSequenceHasAbba)
                {
                    foreach (string sequence in ipAddress.Sequences)
                    {
                        if (HasAutonomousBridgeBypassAnnotation(sequence))
                        {
                            ipAddressesWhichSupportTls++;
                            break;
                        }
                    }
                }
            }

            return ipAddressesWhichSupportTls;
        }

        private bool HasAutonomousBridgeBypassAnnotation(string sequence)
        {
            for (int i = 0; i < sequence.Length; i++)
            {
                if (i - 1 >= 0 && i + 2 < sequence.Length && sequence[i] == sequence[i + 1]
                    && sequence[i - 1] == sequence[i + 2] && sequence[i] != sequence[i + 2])
                {
                    return true;
                }
            }

            return false;
        }
    }
}
