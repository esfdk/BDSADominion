namespace BDSADominion.Gamestate.Card_Types
{
    /// <summary>
    /// A card in the game of Dominion.
    /// </summary>
    public abstract class Card
    {
        /// <summary>
        /// Gets name of the card.
        /// </summary>
        public CardName Name
        {
            get
            {
                return Name;
            }
        }

        /// <summary>
        /// Gets number of the card.
        /// </summary>
        public CardName Number
        {
            get
            {
                return Number;
            }
        }
    }
}
