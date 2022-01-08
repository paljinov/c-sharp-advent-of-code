using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace App.Tasks.Year2021.Day18
{
    [Serializable]
    public class Pair
    {
        public Pair Parent { get; set; }
        public Pair LeftPair { get; set; }
        public Pair RightPair { get; set; }
        public int? LeftNumber { get; set; }
        public int? RightNumber { get; set; }

        public Pair Clone()
        {
            using MemoryStream nemoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(nemoryStream, this);
            nemoryStream.Position = 0;

            return (Pair)binaryFormatter.Deserialize(nemoryStream);
        }
    }
}
