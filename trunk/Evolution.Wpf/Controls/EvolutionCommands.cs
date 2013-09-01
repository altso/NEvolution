namespace Evolution.Wpf.Controls
{
    using System.Windows.Input;

    public static class EvolutionCommands
    {
         public static readonly RoutedUICommand UseTrait = new RoutedUICommand("Use This Trait", "UseTrait", typeof(EvolutionCommands));
    }
}