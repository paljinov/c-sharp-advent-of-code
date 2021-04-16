using System;
using System.Collections.Generic;

namespace App.Tasks.Year2017.Day20
{
    public class Simulation
    {
        private const int SIMULATION_ITERATIONS = 1000;

        public int FindParticleWhichStaysClosestToOriginPositionInTheLongTerm(Dictionary<int, Particle> particles)
        {
            int closestParticleId = 0;
            int minDistance = int.MaxValue;

            for (int i = 0; i < SIMULATION_ITERATIONS; i++)
            {
                DoSimulation(particles);
            }

            foreach (KeyValuePair<int, Particle> particleKeyValuePair in particles)
            {
                int particleId = particleKeyValuePair.Key;
                Particle particle = particleKeyValuePair.Value;

                int distance = CalculateManhattanDistanceToOrigin(particle);
                // If new closest distance
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestParticleId = particleId;
                }
            }

            return closestParticleId;
        }

        public int CountLeftParticlesAfterAllCollisionsAreResolved(Dictionary<int, Particle> particles)
        {
            for (int i = 0; i < SIMULATION_ITERATIONS; i++)
            {
                Dictionary<string, int> positions = new Dictionary<string, int>();

                // Count particles on the same position
                foreach (KeyValuePair<int, Particle> particleKeyValuePair in particles)
                {
                    Particle particle = particleKeyValuePair.Value;
                    string positionKey = $"{particle.Position.X}{particle.Position.Y}{particle.Position.Z}";

                    if (positions.ContainsKey(positionKey))
                    {
                        positions[positionKey]++;
                    }
                    else
                    {
                        positions[positionKey] = 1;
                    }
                }

                // Remove particles which collide
                foreach (KeyValuePair<int, Particle> particleKeyValuePair in particles)
                {
                    int particleId = particleKeyValuePair.Key;
                    Particle particle = particleKeyValuePair.Value;
                    string positionKey = $"{particle.Position.X}{particle.Position.Y}{particle.Position.Z}";

                    if (positions[positionKey] > 1)
                    {
                        particles.Remove(particleId);
                    }
                }

                DoSimulation(particles);
            }

            return particles.Count;
        }

        private void DoSimulation(Dictionary<int, Particle> particles)
        {
            foreach (Particle particle in particles.Values)
            {
                // Increase velocity
                particle.Velocity.X += particle.Acceleration.X;
                particle.Velocity.Y += particle.Acceleration.Y;
                particle.Velocity.Z += particle.Acceleration.Z;

                // Increase position
                particle.Position.X += particle.Velocity.X;
                particle.Position.Y += particle.Velocity.Y;
                particle.Position.Z += particle.Velocity.Z;
            }
        }

        private int CalculateManhattanDistanceToOrigin(Particle particle)
        {
            return Math.Abs(particle.Position.X) + Math.Abs(particle.Position.Y) + Math.Abs(particle.Position.Z);
        }
    }
}
