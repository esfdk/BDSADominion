namespace BDSADominion
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    /// <summary>
    /// The deck class contains information of the representation of the deck
    /// </summary>
    public class Deck : Sprite
    {
        /// <summary>
        /// The asset name
        /// </summary>
        private const string DECKASSETNAME = "Dominion-Backside";

        /// <summary>
        /// The X start position of the deckpile
        /// </summary>
        private const int StartpositionX = 10;

        /// <summary>
        /// The Y start position of the deckpile
        /// </summary>
        private const int StartpositionY = 250;

        /// <summary>
        /// Loads the content for the Deck class
        /// </summary>
        /// <param name="theContentManager">
        /// The the content manager.
        /// </param>
        public void LoadContent(ContentManager theContentManager)
        {
            Position = new Vector2(StartpositionX, StartpositionY);
            scale = 0.5f;
            this.LoadContent(theContentManager, DECKASSETNAME);
        }
    }
}
