namespace BDSADominion
{

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// The deck class contains information of the representation of the deck
    /// </summary>
    public class Discard : SpriteBatch
    {
        /// <summary>
        /// The discardpile.
        /// </summary>
        private Texture2D discardpile;

        /// <summary>
        /// The asset name for the Sprite's Texture
        /// </summary>
        private string assetName = "emptyspace";

        /// <summary>
        /// The Size of the Sprite (with scale applied)
        /// </summary>
        private Rectangle size;

        /// <summary>
        /// The current position of the Sprite.
        /// </summary>
        private Vector2 position = new Vector2(10, 330);

        /// <summary>
        /// The amount to increase/decrease the size of the original sprite. 
        /// </summary>
        private float scale = 0.67f;

        /// <summary>
        /// Initializes a new instance of the <see cref="Discard"/> class.
        /// </summary>
        /// <param name="graphicsDevice">
        /// The graphics device.
        /// </param>
        public Discard(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
        }

        /// <summary>
        /// Load content of the Discard spritbatch.
        /// </summary>
        /// <param name="theContentManager">
        /// The the Content Manager.
        /// </param>
        /// <param name="theAssetName">
        /// The the Asset Name.
        /// </param>
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            this.discardpile = theContentManager.Load<Texture2D>("emptyspace");
            this.assetName = theAssetName;
            this.size = new Rectangle(
                0, 0, (int)(this.discardpile.Width * this.scale), (int)(this.discardpile.Height * this.scale));
        }

        /// <summary>
        /// Draw the discard spritBatch.
        /// </summary>
        /// <param name="spriteBatch">
        /// The sprite Batch.
        /// </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
            this.discardpile,
            this.position,
            new Rectangle(0, 0, this.discardpile.Width, this.discardpile.Height),
            Color.White,
            0.0f,
            Vector2.Zero,
            this.scale,
            SpriteEffects.None,
            0);
        }
    }
}