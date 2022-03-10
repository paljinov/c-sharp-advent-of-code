namespace App.Tasks.Year2018.Day24
{
    public class ImmuneSystemSimulator
    {

        public int CountWinningArmyUnits( Group[] immuneSystemArmy,  Group[] infectionArmy)
        {
            return immuneSystemArmy.Length + infectionArmy.Length;
        }
    }
}
