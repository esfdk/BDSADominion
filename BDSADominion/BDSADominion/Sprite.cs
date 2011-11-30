namespace BDSADominion
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// The sprite class for 
    /// </summary>
    public class Sprite
    {
        /// <summary>
        /// The current position of the Sprite
        /// </summary>
        protected Vector2 Position = new Vector2(0, 0);

        /// <summary>
        /// The amount to increase/decrease the size of the original sprite. 
        /// </summary>
        protected float scale = 1.0f;

        /// <summary>E
        /// The asset name for the Sprite's Texture
        /// </summary>
        private string assetName;

        /// <summary>
        /// The Size of the Sprite (with scale applied)
        /// </summary>
        private Rectangle size;

        /// <summary>
        /// The texture object used when drawing the sprite.
        /// </summary>
        private Texture2D spriteTexture;

        /// <summary>
        /// Gets or sets Scale.
        /// </summary>
        public float CalculateScale
        {
            get
            {
                return this.scale;
            }

            set
            {
                this.scale = value;

                // Recalculate the Size of the Sprite with the new scale
                this.size = new Rectangle(
                    0, 0, (int)(this.spriteTexture.Width * this.scale), (int)(this.spriteTexture.Height * this.scale));
            }
        }

        /// <summary>
        /// Load the texture for the sprite using the Content Pipeline
        /// </summary>
        /// <param name="theContentManager">
        /// The the content manager.
        /// </param>
        /// <param name="theAssetName">
        /// The the asset name.
        /// </param>
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            this.spriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            this.assetName = theAssetName;
            this.size = new Rectangle(
                0, 0, (int)(this.spriteTexture.Width * this.scale), (int)(this.spriteTexture.Height * this.scale));
        }

        /// <summary>
        /// The method in charge of drawing the sprite object.
        /// </summary>
        /// <param name="theSpriteBatch">
        /// The sprite batch.
        /// </param>
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(
                this.spriteTexture,
                this.Position,
                new Rectangle(0, 0, this.spriteTexture.Width, this.spriteTexture.Height),
                Color.White,
                0.0f,
                Vector2.Zero,
                this.scale,
                SpriteEffects.None,
                0);
        }
    }
}