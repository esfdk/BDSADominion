namespace BDSADominion.Gamestate.Card_Types
{
    /// <summary>
    /// A card in the game of Dominion.
    /// </summary>
    /// <author>
    /// Jakob Melnyk (jmel@itu.dk)
    /// </author>
    public abstract class Card
    {
        /// <summary>
        /// Gets name of the card.
        /// </summary>
        public CardName Name { get; private set; }

        /// <summary>
        /// Gets number of the card.
        /// </summary>
        public uint Number { get; private set; }

        /// <summary>
        /// Sets up the card with the correct values.
        /// </summary>
        /// <param name="name">
        /// The name of the card.
        /// </param>
        /// <param name="number">
        /// The number of the card.
        /// </param>
        public virtual void Initialize(CardName name, uint number)
        {
            // This is not done in constructor, because we wanted to inherit the setting of properties without having to do code duplication in all of our cards.
            this.Name = name;
            this.Number = number;
        }
    }
}
