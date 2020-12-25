namespace App.Tasks.Year2020.Day25
{
    public class EncryptionKey
    {
        private const int SUBJECT_NUMBER = 7;

        private const int DIVIDE_BY = 20201227;

        public int GetHandshakeEncryptionKey(int cardPublicKey, int doorPublicKey)
        {
            int cardLoopSize = GetLoopSize(cardPublicKey);
            int encryptionKey = GetEncryptionKey(cardLoopSize, doorPublicKey);

            return encryptionKey;
        }

        private int GetLoopSize(int publicKey)
        {
            int cardLoopSize = 0;

            int value = 1;
            while (value != publicKey)
            {
                value *= SUBJECT_NUMBER;
                value %= DIVIDE_BY;

                cardLoopSize++;
            }

            return cardLoopSize;
        }

        private int GetEncryptionKey(int loopSize, int publicKey)
        {
            long encryptionKey = 1;
            for (int i = 0; i < loopSize; i++)
            {
                encryptionKey *= publicKey;
                encryptionKey %= DIVIDE_BY;
            }

            return (int)encryptionKey;
        }
    }
}
