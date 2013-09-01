namespace Evolution.Traits
{
    using System;

    public abstract class TapableTrait : Trait
    {
        private readonly int turnsInterval;
        private readonly int roundsInterval;

        private GameDate tapDate = new GameDate { Turn = -100 };

        protected TapableTrait(int turnsInterval, int roundsInterval)
        {
            this.turnsInterval = turnsInterval;
            this.roundsInterval = roundsInterval;
        }

        public bool IsTapped { get; private set; }

        public void Tap(GameContext context)
        {
            context.Game.StateChanged += this.GameStateChanged;
            this.IsTapped = true;
            this.tapDate = context.Game.Date;
            this.OnPropertyChanged("IsTapped");
        }

        private void GameStateChanged(object sender, EventArgs e)
        {
            var game = (Game)sender;
            long current = this.ToInt64(game.Date);
            long last = this.ToInt64(this.tapDate);
            long interval = this.ToInt64(new GameDate { Turn = this.turnsInterval, Round = this.roundsInterval });
            if (current - last >= interval)
            {
                this.IsTapped = false;
                this.OnPropertyChanged("IsTapped");
                game.StateChanged -= this.GameStateChanged;
            }
        }

        private long ToInt64(GameDate date)
        {
            return (date.Turn * 100000L) + date.Round;
        }
    }
}