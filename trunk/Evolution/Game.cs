namespace Evolution
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Evolution.Moves;
    using Stateless;

    public class Game : MetaObject
    {
        private readonly StateMachine<GameState, bool> game;
        private readonly Random random = new Random();

        public Game()
        {
            this.BankFood = new BankStorage();
            this.BankFood.PropertyChanged += (_, __) => this.OnPropertyChanged(new PropertyChangedEventArgs("BankFood"));
            this.ExtraFood = new ExtraFoodSource();

            this.Players = new PlayerCollection();
            this.Date = new GameDate();

            this.game = new StateMachine<GameState, bool>(GameState.None);

            this.game.Configure(GameState.None)
                .Permit(true, GameState.DealCards)
                .OnEntry(this.ResetPlayers);

            this.game.Configure(GameState.DealCards)
                .Permit(true, GameState.DevelopmentPhase)
                .OnEntry(() =>
                {
                    this.Date = new GameDate { Turn = this.Date.Turn + 1, Round = 1 };
                    this.ResetPlayers();
                });

            this.game.Configure(GameState.DevelopmentPhase)
                .PermitDynamic(true, () => this.Players.All(p => p.IsPass) ? GameState.FoodBankDeterminationPhase : GameState.DevelopmentPhase);

            this.game.Configure(GameState.FoodBankDeterminationPhase)
                .Permit(true, GameState.FeedingPhase)
                .OnExit(this.ResetPlayers);

            this.game.Configure(GameState.FeedingPhase)
                .PermitDynamic(true, () => this.Players.All(p => p.IsPass) ? GameState.ExtinctionPhase : GameState.FeedingPhase);

            this.game.Configure(GameState.ExtinctionPhase)
                .PermitDynamic(true, () => this.Dealer.Any() ? GameState.DealCards : GameState.End)
                .OnExit(this.ResetPlayers)
                .OnExit(this.ResetAnimals);
        }

        public event EventHandler<MoveExecutingEventArgs> MoveExecuting;

        public event EventHandler<MoveExecutedEventArgs> MoveExecuted;

        public event EventHandler<EventArgs> StateChanged;

        public PlayerCollection Players { get; private set; }

        public Deck Dealer { get; set; }

        public GameState State
        {
            get { return this.game.State; }
        }

        public Random Random
        {
            get { return this.random; }
        }

        public BankStorage BankFood { get; private set; }

        public IFoodSource ExtraFood { get; private set; }

        public IEnumerable<Deck> Decks
        {
            get
            { 
                yield return this.Dealer;
                
                foreach (var player in this.Players)
                {
                    yield return player.Hand;
                    yield return player.Discard;

                    foreach (var animal in player.Animals)
                    {
                        yield return animal;
                    }
                }
            }
        }

        public IEnumerable<Animal> Animals
        {
            get { return this.Players.SelectMany(p => p.Animals); }
        }

        public GameDate Date { get; private set; }

        public void Next()
        {
            this.game.Fire(true);
            switch (this.State)
            {
                case GameState.None:
                    break;
                case GameState.DealCards:
                    this.ExecuteMove(new DealCardsMove());
                    break;
                case GameState.DevelopmentPhase:
                    this.ExecutePlayerMove<DevelopmentMove>();
                    break;
                case GameState.FoodBankDeterminationPhase:
                    this.ExecuteMove(new ThrowDicesMove());
                    break;
                case GameState.FeedingPhase:
                    this.ExecutePlayerMove<FeedingMove>();
                    break;
                case GameState.ExtinctionPhase:
                    this.ExecuteMove(new ExtinctMove());
                    break;
                case GameState.End:
                    break;
            }

            this.OnStateChanged(EventArgs.Empty);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("Turn: {2}, Round {1}, State: {0}", this.State, this.Date.Round, this.Date.Turn);
            builder.AppendLine();
            builder.AppendLine("Players:");
            foreach (var player in this.Players)
            {
                builder.AppendLine(player.ToString());
            }

            return builder.ToString();
        }

        protected virtual void OnMoveExecuting(MoveExecutingEventArgs e)
        {
            var handler = this.MoveExecuting;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnMoveExecuted(MoveExecutedEventArgs e)
        {
            var handler = this.MoveExecuted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void OnStateChanged(EventArgs e)
        {
            var handler = this.StateChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void ResetPlayers()
        {
            this.Players.SetCurrentPlayerIndex(this.Date.Turn - 1);
            foreach (var player in this.Players)
            {
                player.IsPass = false;
            }
        }

        private void ResetAnimals()
        {
            foreach (var animal in this.Players.SelectMany(p => p.Animals))
            {
                animal.Reset();
            }
        }

        private void ExecutePlayerMove<T>()
            where T : PlayerMove
        {
            if (this.Players.All(p => p.IsPass))
            {
                throw new LogicException();
            }

            while (this.Players.Current.IsPass)
            {
                this.Players.Next();
            }

            var move = this.Players.Current.MakeMove(new GameContext(this));
            if (move is T)
            {
                this.ExecuteMove(move);
            }
            else
            {
                throw new LogicException();
            }

            this.Players.Next();
        }

        private void ExecuteMove(Move move)
        {
            var movingEventArgs = new MoveExecutingEventArgs
            {
                Move = move
            };
            this.OnMoveExecuting(movingEventArgs);
            if (!movingEventArgs.Cancel)
            {
                move.Execute(new GameContext(this));
                this.OnMoveExecuted(new MoveExecutedEventArgs { Move = move });
            }
        }
    }
}