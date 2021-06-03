using System.Collections.Generic;

namespace App.Tasks.Year2015.Day16
{
    public class AuntSue
    {
        private static readonly Compounds auntSueCompounds = new Compounds
        {
            Children = 3,
            Cats = 7,
            Samoyeds = 2,
            Pomeranians = 3,
            Akitas = 0,
            Vizslas = 0,
            Goldfish = 5,
            Trees = 3,
            Cars = 2,
            Perfumes = 1
        };

        public int FindNumberOfTheSueThatSentTheGift(
            Dictionary<int, Compounds> suesCompounds,
            bool someOutputsIndicateRanges = false
        )
        {
            int numberOfTheSueThatSentTheGift = 0;

            foreach (KeyValuePair<int, Compounds> compounds in suesCompounds)
            {
                bool isAuntSue = !someOutputsIndicateRanges ?
                    IsAuntSue(compounds.Value) : IsAuntSueWhenSomeOutputsIndicateRanges(compounds.Value);

                if (isAuntSue)
                {
                    numberOfTheSueThatSentTheGift = compounds.Key;
                    break;
                }
            }

            return numberOfTheSueThatSentTheGift;
        }

        private bool IsAuntSue(Compounds compounds)
        {
            if (compounds.Children != null && compounds.Children != auntSueCompounds.Children)
            {
                return false;
            }
            if (compounds.Cats != null && compounds.Cats != auntSueCompounds.Cats)
            {
                return false;
            }
            if (compounds.Samoyeds != null && compounds.Samoyeds != auntSueCompounds.Samoyeds)
            {
                return false;
            }
            if (compounds.Pomeranians != null && compounds.Pomeranians != auntSueCompounds.Pomeranians)
            {
                return false;
            }
            if (compounds.Akitas != null && compounds.Akitas != auntSueCompounds.Akitas)
            {
                return false;
            }
            if (compounds.Vizslas != null && compounds.Vizslas != auntSueCompounds.Vizslas)
            {
                return false;
            }
            if (compounds.Goldfish != null && compounds.Goldfish != auntSueCompounds.Goldfish)
            {
                return false;
            }
            if (compounds.Trees != null && compounds.Trees != auntSueCompounds.Trees)
            {
                return false;
            }
            if (compounds.Cars != null && compounds.Cars != auntSueCompounds.Cars)
            {
                return false;
            }
            if (compounds.Perfumes != null && compounds.Perfumes != auntSueCompounds.Perfumes)
            {
                return false;
            }

            return true;
        }

        private bool IsAuntSueWhenSomeOutputsIndicateRanges(Compounds compounds)
        {
            if (compounds.Children != null && compounds.Children != auntSueCompounds.Children)
            {
                return false;
            }
            if (compounds.Cats != null && compounds.Cats <= auntSueCompounds.Cats)
            {
                return false;
            }
            if (compounds.Samoyeds != null && compounds.Samoyeds != auntSueCompounds.Samoyeds)
            {
                return false;
            }
            if (compounds.Pomeranians != null && compounds.Pomeranians >= auntSueCompounds.Pomeranians)
            {
                return false;
            }
            if (compounds.Akitas != null && compounds.Akitas != auntSueCompounds.Akitas)
            {
                return false;
            }
            if (compounds.Vizslas != null && compounds.Vizslas != auntSueCompounds.Vizslas)
            {
                return false;
            }
            if (compounds.Goldfish != null && compounds.Goldfish >= auntSueCompounds.Goldfish)
            {
                return false;
            }
            if (compounds.Trees != null && compounds.Trees <= auntSueCompounds.Trees)
            {
                return false;
            }
            if (compounds.Cars != null && compounds.Cars != auntSueCompounds.Cars)
            {
                return false;
            }
            if (compounds.Perfumes != null && compounds.Perfumes != auntSueCompounds.Perfumes)
            {
                return false;
            }

            return true;
        }
    }
}
