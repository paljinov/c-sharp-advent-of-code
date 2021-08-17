using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2019.Day12
{
    public class MoonSystem
    {
        private readonly LcmHelper lcmHelper;

        public MoonSystem()
        {
            lcmHelper = new LcmHelper();
        }

        public int CalculateTotalEnergyInTheSystem(List<Position> moonsPositions, int totalSteps)
        {
            Dictionary<int, (Position Position, Velocity Velocity)> system = InitializeSystem(moonsPositions);

            for (int step = 0; step < totalSteps; step++)
            {
                Dictionary<int, Velocity> newVelocities = CalculateNewVelocities(system);
                UpdateSystemPositionsAndVelocities(system, newVelocities);
            }

            int totalEnergy = CalculateTotalEnergy(system);

            return totalEnergy;
        }

        public long CalculateNumberOfStepsNeededToReachFirstStateThatExactlyMatchesPreviousState(
            List<Position> moonsPositions
        )
        {
            IEnumerable<Dimension> dimensions = Enum.GetValues(typeof(Dimension)).Cast<Dimension>();
            Dictionary<Dimension, int> dimensionSteps = new Dictionary<Dimension, int>();

            Dictionary<int, (Position Position, Velocity Velocity)> systemStartState = InitializeSystem(moonsPositions);
            foreach (Dimension dimension in dimensions)
            {
                dimensionSteps[dimension] = 0;
                Dictionary<int, (Position Position, Velocity Velocity)> system = InitializeSystem(moonsPositions);

                while (dimensionSteps[dimension] == 0
                    || !IsSystemStartStateReachedForDimension(systemStartState, system, dimension))
                {
                    Dictionary<int, Velocity> newVelocities = CalculateNewVelocities(system);
                    UpdateSystemPositionsAndVelocities(system, newVelocities);
                    dimensionSteps[dimension]++;
                }
            }

            long steps = lcmHelper.CalculateLeastCommonMultiple(dimensionSteps.Values.ToArray());

            return steps;
        }

        private Dictionary<int, (Position, Velocity)> InitializeSystem(List<Position> moonsPositions)
        {
            Dictionary<int, (Position, Velocity)> system = new Dictionary<int, (Position, Velocity)>();
            for (int i = 0; i < moonsPositions.Count; i++)
            {
                Velocity velocity = new Velocity
                {
                    X = 0,
                    Y = 0,
                    Z = 0
                };

                system.Add(i, (moonsPositions[i], velocity));
            }

            return system;
        }

        private Dictionary<int, Velocity> CalculateNewVelocities(Dictionary<int, (Position, Velocity)> system)
        {
            Dictionary<int, Velocity> newVelocities = new Dictionary<int, Velocity>();

            foreach (KeyValuePair<int, (Position Position, Velocity Velocity)> firstMoon in system)
            {
                Velocity newVelocity = new Velocity
                {
                    X = firstMoon.Value.Velocity.X,
                    Y = firstMoon.Value.Velocity.Y,
                    Z = firstMoon.Value.Velocity.Z
                };

                foreach (KeyValuePair<int, (Position Position, Velocity Velocity)> secondMoon in system)
                {
                    if (firstMoon.Key != secondMoon.Key)
                    {
                        // Update velocities
                        newVelocity.X += UpdateVelocityBy(firstMoon.Value.Position.X, secondMoon.Value.Position.X);
                        newVelocity.Y += UpdateVelocityBy(firstMoon.Value.Position.Y, secondMoon.Value.Position.Y);
                        newVelocity.Z += UpdateVelocityBy(firstMoon.Value.Position.Z, secondMoon.Value.Position.Z);
                    }
                }

                newVelocities.Add(firstMoon.Key, newVelocity);
            }

            return newVelocities;
        }

        private int UpdateVelocityBy(int firstMoonPosition, int secondMoonPosition)
        {
            if (firstMoonPosition < secondMoonPosition)
            {
                return 1;
            }
            else if (firstMoonPosition > secondMoonPosition)
            {
                return -1;
            }

            return 0;
        }

        private void UpdateSystemPositionsAndVelocities(
            Dictionary<int, (Position Position, Velocity Velocity)> system,
            Dictionary<int, Velocity> newVelocities
        )
        {
            // Update system velocities and positions
            foreach (KeyValuePair<int, Velocity> newVelocity in newVelocities)
            {
                Position newPosition = new Position
                {
                    X = system[newVelocity.Key].Position.X + newVelocity.Value.X,
                    Y = system[newVelocity.Key].Position.Y + newVelocity.Value.Y,
                    Z = system[newVelocity.Key].Position.Z + newVelocity.Value.Z
                };

                system[newVelocity.Key] = (newPosition, newVelocity.Value);
            }
        }

        private int CalculateTotalEnergy(Dictionary<int, (Position Position, Velocity Velocity)> system)
        {
            int totalEnergy = 0;

            foreach (KeyValuePair<int, (Position Position, Velocity Velocity)> moon in system)
            {
                int potentialEnergy = Math.Abs(moon.Value.Position.X) + Math.Abs(moon.Value.Position.Y)
                    + Math.Abs(moon.Value.Position.Z);
                int kineticEnergy = Math.Abs(moon.Value.Velocity.X) + Math.Abs(moon.Value.Velocity.Y)
                    + Math.Abs(moon.Value.Velocity.Z);

                totalEnergy += potentialEnergy * kineticEnergy;
            }

            return totalEnergy;
        }

        private bool IsSystemStartStateReachedForDimension(
            Dictionary<int, (Position Position, Velocity Velocity)> systemStartState,
            Dictionary<int, (Position Position, Velocity Velocity)> system,
            Dimension dimension
        )
        {
            foreach (KeyValuePair<int, (Position Position, Velocity Velocity)> moon in systemStartState)
            {
                switch (dimension)
                {
                    case Dimension.X:
                        if (moon.Value.Position.X != system[moon.Key].Position.X)
                        {
                            return false;
                        }
                        if (moon.Value.Velocity.X != system[moon.Key].Velocity.X)
                        {
                            return false;
                        }
                        break;
                    case Dimension.Y:
                        if (moon.Value.Position.Y != system[moon.Key].Position.Y)
                        {
                            return false;
                        }
                        if (moon.Value.Velocity.Y != system[moon.Key].Velocity.Y)
                        {
                            return false;
                        }
                        break;
                    case Dimension.Z:
                        if (moon.Value.Position.Z != system[moon.Key].Position.Z)
                        {
                            return false;
                        }
                        if (moon.Value.Velocity.Z != system[moon.Key].Velocity.Z)
                        {
                            return false;
                        }
                        break;
                }
            }

            return true;
        }
    }
}
