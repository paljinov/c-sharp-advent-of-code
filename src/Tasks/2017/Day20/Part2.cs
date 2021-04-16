/*
--- Part Two ---

To simplify the problem further, the GPU would like to remove any particles that
collide. Particles collide if their positions ever exactly match. Because
particles are updated simultaneously, more than two particles can collide at the
same time and place. Once particles collide, they are removed and cannot collide
with anything else after that tick.

For example:

p=<-6,0,0>, v=< 3,0,0>, a=< 0,0,0>
p=<-4,0,0>, v=< 2,0,0>, a=< 0,0,0>    -6 -5 -4 -3 -2 -1  0  1  2  3
p=<-2,0,0>, v=< 1,0,0>, a=< 0,0,0>    (0)   (1)   (2)            (3)
p=< 3,0,0>, v=<-1,0,0>, a=< 0,0,0>

p=<-3,0,0>, v=< 3,0,0>, a=< 0,0,0>
p=<-2,0,0>, v=< 2,0,0>, a=< 0,0,0>    -6 -5 -4 -3 -2 -1  0  1  2  3
p=<-1,0,0>, v=< 1,0,0>, a=< 0,0,0>             (0)(1)(2)      (3)
p=< 2,0,0>, v=<-1,0,0>, a=< 0,0,0>

p=< 0,0,0>, v=< 3,0,0>, a=< 0,0,0>
p=< 0,0,0>, v=< 2,0,0>, a=< 0,0,0>    -6 -5 -4 -3 -2 -1  0  1  2  3
p=< 0,0,0>, v=< 1,0,0>, a=< 0,0,0>                       X (3)
p=< 1,0,0>, v=<-1,0,0>, a=< 0,0,0>

------destroyed by collision------
------destroyed by collision------    -6 -5 -4 -3 -2 -1  0  1  2  3
------destroyed by collision------                      (3)
p=< 0,0,0>, v=<-1,0,0>, a=< 0,0,0>

In this example, particles 0, 1, and 2 are simultaneously destroyed at the time
and place marked X. On the next tick, particle 3 passes through unharmed.

How many particles are left after all collisions are resolved?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2017.Day20
{
    public class Part2 : ITask<int>
    {
        private readonly ParticlesRepository particlesRepository;

        private readonly Simulation simulation;

        public Part2()
        {
            particlesRepository = new ParticlesRepository();
            simulation = new Simulation();
        }

        public int Solution(string input)
        {
            Dictionary<int, Particle> particles = particlesRepository.GetParticles(input);
            int leftParticles = simulation.CountLeftParticlesAfterAllCollisionsAreResolved(particles);

            return leftParticles;
        }
    }
}
