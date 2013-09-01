namespace Evolution.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using Controls;
    using Evolution;
    using Traits;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IDecisionMaker
    {
        private readonly SelectableDialog dialog;
        private Game game;
        private WpfPlayer player;

        public MainWindow()
        {
            InitializeComponent();

            this.dialog = new SelectableDialog();
            this.dialog.Selected += this.DialogSelected;
            this.dialog.Selecting += this.DialogSelecting;
        }


        public Card SelectCard(IEnumerable<Card> cards, bool allowNull)
        {
            var elements = cards.Select(c => (UICard)c[typeof(UICard)]).Where(c => c != null);
            var card = this.dialog.Select(elements, allowNull ? this.NothingButton : null);
            return card != null ? card.Card : null;
        }

        public Animal SelectAnimal(IEnumerable<Animal> animals, bool allowNull)
        {
            var elements = animals.Select(a => (UIAnimal)a[typeof(UIAnimal)]).Where(a => a != null);
            var animal = this.dialog.Select(elements, allowNull ? this.NewAnimalButton : null);
            return animal != null ? animal.Animal : null;
        }

        public Trait SelectTrait(IEnumerable<Trait> traits, bool allowNull)
        {
            var elements = traits.Select(c => (UICard)c.Card[typeof(UICard)]).Where(c => c != null);
            var card = this.dialog.Select(elements, allowNull ? this.NothingButton : null);
            return card != null ? card.Card.Trait : null;
        }

        private void DialogSelecting(object sender, EventArgs e)
        {
            this.NextButton.IsEnabled = false;
        }

        private void DialogSelected(object sender, EventArgs e)
        {
            this.NextButton.IsEnabled = true;
        }

        private void NewGame()
        {
            this.game = new Game();

            var dealer = new TheOriginOfSpeciesDeck();
            dealer.Shuffle();

            Player zooKeeper;

            this.game.Dealer = dealer;
            this.game.Players.Add(zooKeeper = new LionKing { Name = "Bob" });
            this.game.Players.Add(this.player = new WpfPlayer(this.game, this) { Name = "Player" });

            HandItemsControl.ItemsSource = this.player.Hand;
            AnimalsItemsControl.ItemsSource = this.player.Animals;
            ZooKeeperAnimalsItemsControl.ItemsSource = zooKeeper.Animals;

            this.DataContext = this.game;

            this.game.MoveExecuted += (sender, e) =>
            {
                string item = string.Format("{0}: {1}", DateTime.Now.ToString("hh:mm:ss"), e.Move);
                this.LogListBox.Items.Add(item);
                this.LogListBox.ScrollIntoView(item);
            };

            this.LogListBox.Items.Clear();
            this.LogListBox.Items.Add("Game started.");
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.NewGame();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.game.State == GameState.End)
            {
                MessageBox.Show("Game over!");
                this.NewGame();
            }
            else
            {
                this.game.Next();
            }
        }

        private void ContextMenuContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var uiCard = (UICard)sender;
            uiCard.ContextMenu.Items.Clear();
            foreach (var cardGroup in this.game.Dealer.GroupBy(c => c.Trait.GetType()))
            {
                var card = cardGroup.First();
                var menuItem = new MenuItem
                {
                    DataContext = card,
                    Header = string.Format("{0} ({1})", card.Trait.Name, cardGroup.Count())
                };

                menuItem.Click += (_, __) =>
                {
                    uiCard.Card.MoveTo(game.Dealer);
                    card.MoveTo(player.Hand);
                };

                uiCard.ContextMenu.Items.Add(menuItem);
            }
        }
    }
}
