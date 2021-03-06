using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day22
{
    public class BossStatsRepository
    {
        public FighterStats GetBossStats(string input)
        {
            Regex hitPointsRegex = new Regex(@"Hit Points: (\d+)");
            Match hitPointsMatch = hitPointsRegex.Match(input);
            int hitPoints = int.Parse(hitPointsMatch.Groups[1].Value);

            Regex damageRegex = new Regex(@"Damage: (\d+)");
            Match damageMatch = damageRegex.Match(input);
            int damage = int.Parse(damageMatch.Groups[1].Value);

            FighterStats bossStats = new FighterStats
            {
                HitPoints = hitPoints,
                Damage = damage,
                ManaPoints = 0
            };

            return bossStats;
        }
    }
}
