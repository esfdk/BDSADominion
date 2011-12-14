namespace BDSADominion.GUI.Sprites
{
    using BDSADominion.Gamestate;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// This class holds the information for the representation of buttons.
    /// </summary>
    /// <author>
    /// Frederik Lysgaard (frly@itu.dk)
    /// </author>
    internal class ButtonSprite
    {
        ////public int Id { get; private set; }

        /// <summary>
        /// The texture for the front of the button.
        /// </summary>
        private readonly Texture2D buttonFront;

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonSprite"/> class. 
        /// </summary>
        /// <param name="card">
        /// The card.
        /// </param>
        internal ButtonSprite(CardName card)
        {
            Clicked = false;
            Card = card;
            if (GameClass.ButtonImages.ContainsKey(card))
            {
                this.buttonFront = GameClass.ButtonImages[Card];
            }
        }

        /// <summary>
        /// Gets ImageHeight.
        /// </summary>
        public int ImageHeight
        {
            get
            {
                return this.buttonFront.Height;
            }
        }

        /// <summary>
        /// Gets ImageWidth.
        /// </summary>
        public int ImageWidth
        {
            get
            {
                return this.buttonFront.Width;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Clicked.
        /// </summary>
        public bool Clicked { get; set; }

        /// <summary>
        /// Gets Card.
        /// </summary>
        internal CardName Card { get; private set; }

        /// <summary>
        /// Draws the button.
        /// </summary>
        /// <param name="spriteBatch">
        /// The sprite Batch.
        /// </param>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <summary>
        /// the draw method of card
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (buttonFront != null)
            {
                spriteBatch.Draw(buttonFront, position, Color.White);
            }
        }
    }
}
