namespace Evolution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;
    using Traits;

    public interface IDecisionMaker
    {
        [CanBeNull]
        Card SelectCard(IEnumerable<Card> cards, bool allowNull);

        [CanBeNull]
        Animal SelectAnimal(IEnumerable<Animal> animals, bool allowNull);

        [CanBeNull]
        Trait SelectTrait(IEnumerable<Trait> traits, bool allowNull);
    }

    public class RandomDecisionMaker : IDecisionMaker
    {
        private static readonly Random Random = new Random();

        public Card SelectCard(IEnumerable<Card> cards, bool allowNull)
        {
            return this.Select(cards, allowNull);
        }

        public Animal SelectAnimal(IEnumerable<Animal> animals, bool allowNull)
        {
            return this.Select(animals, allowNull);
        }

        public Trait SelectTrait(IEnumerable<Trait> traits, bool allowNull)
        {
            return this.Select(traits, allowNull);
        }

        private T Select<T>(IEnumerable<T> items, bool allowNull)
        {
            var list = items.ToList();
            if (list.Count > 0)
            {
                int index = Random.Next(list.Count + (allowNull ? 1 : 0));
                return index < list.Count ? list[index] : default(T);
            }

            if (allowNull)
            {
                return default(T);
            }

            throw new LogicException();
        }
    }
}