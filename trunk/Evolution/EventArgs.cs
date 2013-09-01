namespace Evolution
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using Evolution.TraitActions;
    using Moves;
    using Traits;

    public class MoveExecutingEventArgs : CancelEventArgs
    {
        public Move Move { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public class MoveExecutedEventArgs : EventArgs
    {
        public Move Move { get; set; }
    }

    public class TraitActionExecutingEventArgs : CancelEventArgs
    {
        public TraitAction Action { get; set; }
    }

    public class TraitActionExecutedEventArgs : EventArgs
    {
        public TraitAction Action { get; set; }
    }
}