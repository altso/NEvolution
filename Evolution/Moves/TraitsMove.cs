namespace Evolution.Moves
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Evolution.TraitActions;
    using Evolution.Traits;

    public class TraitsMove : FeedingMove
    {
        public TraitsMove(Player player, TraitAction trait)
            : base(player)
        {
            this.Actions = new List<TraitAction> { trait };
        }

        public IList<TraitAction> Actions { get; private set; }

        public override void Execute(GameContext context)
        {
            var actions = new LinkedList<TraitAction>(this.Actions);
            while (actions.Count > 0)
            {
                var action = actions.First.Value;

                var traitContext = new TraitContext(context.Game, action);
                ITransformer transformer;

                while ((transformer = this.SelectTransformer(context.Game.Players, traitContext)) != null)
                {
                    action = transformer.Transform(traitContext);
                    traitContext = new TraitContext(context.Game, action);
                }

                action.Execute(context);

                IAttachable attachable;
                while ((attachable = this.SelectAttachable(context.Game.Players, traitContext)) != null)
                {
                    attachable.Attach(traitContext);
                }

                actions.RemoveFirst();
            }
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.Actions.Select(a => a.ToString()));
        }

        private ITransformer SelectTransformer(IEnumerable<Player> players, TraitContext context)
        {
            return players.Select(p => this.SelectTransformer(p, context)).Where(t => t != null).FirstOrDefault();
        }

        private ITransformer SelectTransformer(Player player, TraitContext context)
        {
            var traits = player.Animals.SelectMany(a => a.Traits.OfType<ITransformer>().Where(t => t.CanTransform(context)));
            return (ITransformer)player.Brain.SelectTrait(traits.Cast<Trait>(), true);
        }

        private IAttachable SelectAttachable(IEnumerable<Player> players, TraitContext context)
        {
            return players.Select(p => this.SelectAttachable(p, context)).Where(t => t != null).FirstOrDefault();
        }

        private IAttachable SelectAttachable(Player player, TraitContext context)
        {
            var traits = player.Animals.SelectMany(a => a.Traits.OfType<IAttachable>().Where(t => t.CanAttach(context))).ToList();
            if (traits.Count > 1)
            {
                return (IAttachable)player.Brain.SelectTrait(traits.Cast<Trait>(), false);
            }

            if (traits.Count > 0)
            {
                return traits.Single();
            }

            return null;
        }
    }
}