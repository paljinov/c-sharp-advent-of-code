using System;

namespace App.Tasks.Year2020.Day25
{
    public class PublicKeyRepository
    {
        public int GetCardPublicKey(string input)
        {
            string[] publicKeys = GetPublicKeys(input);
            int cardPublicKey = int.Parse(publicKeys[0]);

            return cardPublicKey;
        }

        public int GetDoorPublicKey(string input)
        {
            string[] publicKeys = GetPublicKeys(input);
            int doorPublicKey = int.Parse(publicKeys[1]);

            return doorPublicKey;
        }

        private string[] GetPublicKeys(string input)
        {
            string[] publicKeys = input.Split(Environment.NewLine);
            return publicKeys;
        }
    }
}
