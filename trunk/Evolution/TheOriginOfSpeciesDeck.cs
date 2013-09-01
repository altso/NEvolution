namespace Evolution
{
    using Evolution.Traits;

    public class TheOriginOfSpeciesDeck : NewDeck
    {
        public TheOriginOfSpeciesDeck()
        {
            this.Add(4, () => new Camouflage(), () => new FatTissue());
            this.Add(4, () => new Grazing(), () => new FatTissue());
            this.Add(4, () => new Burrowing(), () => new FatTissue());
            this.Add(4, () => new HighBodyWeight(), () => new FatTissue());
            this.Add(4, () => new HighBodyWeight(), () => new Carnivorous());
            this.Add(4, () => new SharpVision(), () => new FatTissue());
            this.Add(4, () => new Parasite(), () => new Carnivorous());
            this.Add(4, () => new Parasite(), () => new FatTissue());
            this.Add(4, () => new Piracy());
            this.Add(8, () => new Swimming());
            this.Add(4, () => new HibernationAbility(), () => new Carnivorous());
            this.Add(4, () => new Running());
            this.Add(4, () => new TailLoss());
            this.Add(4, () => new Mimicry());
            this.Add(4, () => new Scavanger());
            /*
            this.Add(4, () => new Poisonous(), () => new Carnivorous());
            this.Add(4, () => new Symbiosis(game));
            this.Add(5, () => new Communication(game), () => new Carnivorous(game));
            this.Add(4, () => new Cooperation(game), () => new FatTissue(game));
            this.Add(3, () => new Cooperation(game), () => new Carnivorous(game));
            */
        }
    }
}