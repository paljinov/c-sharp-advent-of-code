using System.Collections.Generic;
using System.Linq;

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

        public int CountIpAddressesWhichSupportSsl(List<IpAddress> ipAddresses)
        {
            int ipAddressesWhichSupportSsl = 0;

            foreach (IpAddress ipAddress in ipAddresses)
            {
                bool ipAddressSupportsSsl = false;

                List<string> areaBroadcastAccessorSequences = new List<string>();
                foreach (string sequence in ipAddress.Sequences)
                {
                    areaBroadcastAccessorSequences = areaBroadcastAccessorSequences
                        .Union(GetAreaBroadcastAccessorSequences(sequence)).ToList();
                }

                foreach (string hypernetSequence in ipAddress.HypernetSequences)
                {
                    foreach (string areaBroadcastAccessorSequence in areaBroadcastAccessorSequences)
                    {
                        string byteAllocationBlockSequence =
                            GetByteAllocationBlockSequence(areaBroadcastAccessorSequence);

                        if (hypernetSequence.Contains(byteAllocationBlockSequence))
                        {
                            ipAddressesWhichSupportSsl++;
                            ipAddressSupportsSsl = true;
                            break;
                        }
                    }

                    if (ipAddressSupportsSsl)
                    {
                        break;
                    }
                }
            }

            return ipAddressesWhichSupportSsl;
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

        private List<string> GetAreaBroadcastAccessorSequences(string sequence)
        {
            List<string> areaBroadcastAccessorSequences = new List<string>();


            for (int i = 0; i < sequence.Length; i++)
            {
                if (i + 2 < sequence.Length && sequence[i] == sequence[i + 2] && sequence[i] != sequence[i + 1])
                {
                    char[] areaBroadcastAccessorSequence = new char[3]
                    {
                        sequence[i],
                        sequence[i + 1],
                        sequence[i + 2]
                    };
                    areaBroadcastAccessorSequences.Add(new string(areaBroadcastAccessorSequence));
                }
            }

            return areaBroadcastAccessorSequences;
        }

        private string GetByteAllocationBlockSequence(string areaBroadcastAccessorSequence)
        {
            char[] byteAllocationBlockSequence = new char[3]
            {
                areaBroadcastAccessorSequence[1],
                areaBroadcastAccessorSequence[0],
                areaBroadcastAccessorSequence[1]
            };

            return new string(byteAllocationBlockSequence);
        }
    }
}
