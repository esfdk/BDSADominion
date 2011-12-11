namespace BDSADominion
{

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// The deck class contains information of the representation of the deck
    /// </summary>
    public class DeckZone : SpriteBatch
    {

        /// <summary>
        /// The discardpile.
        /// </summary>
        private Texture2D deck;

        /// <summary>
        /// The asset name
        /// </summary>
        private string assetname = "Backside";

        /// <summary>
        /// The Size of the Sprite (with scale applied)
        /// </summary>
        private Rectangle size;

        /// <summary>
        /// The current position of the Sprite.
        /// </summary>
        private Vector2 position = new Vector2(200, 330);

        /// <summary>
        /// The amount to increase/decrease the size of the original sprite. 
        /// </summary>
        private float scale = 0.67f;

        public DeckZone(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
        }

        /// <summary>
        /// Loads the content for the Deck class.
        /// </summary>
        /// <param name="theContentManager">
        /// The the content manager.
        /// </param>
        /// <param name="theAssetName">
        /// The the Asset Name.
        /// </param>
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            this.deck = theContentManager.Load<Texture2D>("Backside");
            assetname = theAssetName;
            this.size = new Rectangle(
                0, 0, (int)(this.deck.Width * this.scale), (int)(this.deck.Height * this.scale));
        }

        /// <summary>
        /// Draw the deck spritbatch 
        /// </summary>
        /// <param name="spriteBatch">
        /// The sprite Batch.
        /// </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
            this.deck,
            this.position,
            new Rectangle(0, 0, this.deck.Width, this.deck.Height),
            Color.White,
            0.0f,
            Vector2.Zero,
            this.scale,
            SpriteEffects.None,
            0);
        }
    }
}
