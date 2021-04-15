using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2017.Day20
{
    public class ParticlesRepository
    {
        public Dictionary<int, Particle> GetParticles(string input)
        {
            Dictionary<int, Particle> particles = new Dictionary<int, Particle>();

            string[] particlesString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Regex particleRegex =
                new Regex(@"^p=<(-?\d+),(-?\d+),(-?\d+)>,\sv=<(-?\d+),(-?\d+),(-?\d+)>,\sa=<(-?\d+),(-?\d+),(-?\d+)>$");

            for (int i = 0; i < particlesString.Length; i++)
            {
                Match particleMatch = particleRegex.Match(particlesString[i]);
                GroupCollection particleGroups = particleMatch.Groups;

                Coordinates position = new Coordinates
                {
                    X = int.Parse(particleGroups[1].Value),
                    Y = int.Parse(particleGroups[2].Value),
                    Z = int.Parse(particleGroups[3].Value)
                };

                Coordinates velocity = new Coordinates
                {
                    X = int.Parse(particleGroups[4].Value),
                    Y = int.Parse(particleGroups[5].Value),
                    Z = int.Parse(particleGroups[6].Value)
                };

                Coordinates acceleration = new Coordinates
                {
                    X = int.Parse(particleGroups[7].Value),
                    Y = int.Parse(particleGroups[8].Value),
                    Z = int.Parse(particleGroups[9].Value)
                };

                Particle particle = new Particle
                {
                    Position = position,
                    Velocity = velocity,
                    Acceleration = acceleration
                };

                particles.Add(i, particle);
            }

            return particles;
        }
    }
}
