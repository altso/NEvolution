namespace Evolution
{
    using System.Collections.ObjectModel;
    using System.Text;
    using JetBrains.Annotations;
    using Moves;

    public class Player : MetaObject
    {
        public Player(IDecisionMaker decisionMaker = null)
        {
            this.Hand = new Deck();
            this.Animals = new ObservableCollection<Animal>();
            this.Discard = new Deck();
            this.Brain = decisionMaker ?? new RandomDecisionMaker();
        }

        public string Name { get; set; }

        public Deck Hand { get; private set; }

        public Deck Discard { get; private set; }

        public ObservableCollection<Animal> Animals { get; private set; }

        public bool IsPass { get; set; }

        public IDecisionMaker Brain { get; private set; }

        [NotNull]
        public PlayerMove MakeMove(GameContext context)
        {
            switch (context.Game.State)
            {
                case GameState.DevelopmentPhase:
                    return this.Develop(context);
                case GameState.FeedingPhase:
                    return this.Feed(context);
                default:
                    throw new LogicException();
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("Hand: {0}; Animals: {1}", this.Hand.Count, string.Join(", ", this.Animals));
            return builder.ToString();
        }

        [NotNull]
        protected virtual DevelopmentMove Develop(GameContext context)
        {
            return new PassMove(this);
        }

        [NotNull]
        protected virtual FeedingMove Feed(GameContext context)
        {
            return new SkipMove(this);
        }
    }
}