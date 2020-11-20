/*
--- Part Two ---

With all the decoy data out of the way, it's time to decrypt this list and get
moving.

The room names are encrypted by a state-of-the-art shift cipher, which is nearly
unbreakable without the right software. However, the information kiosk designers
at Easter Bunny HQ were not expecting to deal with a master cryptographer like
yourself.

To decrypt a room name, rotate each letter forward through the alphabet a number
of times equal to the room's sector ID. A becomes B, B becomes C, Z becomes A,
and so on. Dashes become spaces.

For example, the real name for qzmt-zixmtkozy-ivhz-343 is very encrypted name.

What is the sector ID of the room where North Pole objects are stored?
*/

using System.Collections.Generic;
using System.Text;

namespace App.Tasks.Year2016.Day4
{
    public class Part2 : ITask<int>
    {
        private readonly RoomRepository roomRepository;

        private readonly RealRoom realRoom;

        public Part2()
        {
            roomRepository = new RoomRepository();
            realRoom = new RealRoom();
        }

        public int Solution(string input)
        {
            List<Room> possibleRooms = roomRepository.GetPossibleRooms(input);
            List<Room> realRooms = realRoom.FilterRealRooms(possibleRooms);

            int northPoleSectorID = FindNorthPoleSectorId(realRooms);

            return northPoleSectorID;
        }

        private int FindNorthPoleSectorId(List<Room> realRooms)
        {
            int northPoleSectorID = 0;

            foreach (Room realRoom in realRooms)
            {
                string name = realRoom.Name.Replace("-", string.Empty);

                for (int i = 0; i < realRoom.SectorId; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (char c in name)
                    {
                        if (c == 'z')
                        {
                            sb.Append('a');
                        }
                        else
                        {
                            sb.Append((char)(c + 1));
                        }
                    }

                    name = sb.ToString();
                }

                if (name.StartsWith("northpole"))
                {
                    northPoleSectorID = realRoom.SectorId;
                    break;
                }
            }

            return northPoleSectorID;
        }
    }
}
