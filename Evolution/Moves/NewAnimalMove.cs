namespace Evolution.Moves
{
    using System.Collections.Generic;

    public class NewAnimalMove : DevelopmentMove
    {
        #region [ Pet Names ]

        private static readonly string[] PetNameList = new[]
        {
            "Alfalfa",
            "Anise",
            "Artichoke",
            "Beans",
            "Bok Choy",
            "Broccoli",
            "Cabbage",
            "Carrot",
            "Cayenne",
            "Celery",
            "Chickpea",
            "Chile",
            "Chives",
            "Cilantro",
            "Corn",
            "Endive",
            "Fennel",
            "Frisee",
            "Garbanzo",
            "Garlic",
            "Habanero",
            "Jalapeno",
            "Jicama",
            "Kale",
            "Lentil",
            "Lima",
            "Maize",
            "Mung",
            "Okra",
            "Onion",
            "Paprika",
            "Parsley",
            "Peas",
            "Pepper",
            "Potato",
            "Pumpkin",
            "Radish",
            "Rhubarb",
            "Shallot",
            "Spinach",
            "Tabasco",
            "Taro",
            "Turnip",
            "Wasabi",
            "Yam"
        };

        #endregion
        
        private static readonly object PetNamesKey = new object();

        public NewAnimalMove(Player player, Card animalBase)
            : base(player)
        {
            this.AnimalBase = animalBase;
        }

        public Card AnimalBase { get; private set; }

        public override void Execute(GameContext context)
        {
            var animal = new Animal(Player, this.AnimalBase) { Name = this.GetPetName(context) };
            Player.Animals.Add(animal);
        }

        public override string ToString()
        {
            return string.Format("{0} added new animal.", Player.Name);
        }

        private IList<string> GetPetNames(GameContext context)
        {
            return (IList<string>)(context.Game[PetNamesKey] ?? (context.Game[PetNamesKey] = new List<string>(PetNameList)));
        }

        private string GetPetName(GameContext context)
        {
            var list = this.GetPetNames(context);
            if (list.Count > 0)
            {
                int index = context.Game.Random.Next(list.Count);
                string name = list[index];
                list.RemoveAt(index);
                return name;
            }

            context.Game[PetNamesKey] = null;
            return this.GetPetName(context);
        }
    }
}