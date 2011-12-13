namespace BDSADominion.GUI
{
    using Gamestate;
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
        internal CardName Card { get; private set; }

        ////public int Id { get; private set; }

        /// <summary>
        /// The texture for the front of the button.
        /// </summary>
        private Texture2D buttonFront;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardSprite"/> class.
        /// </summary>
        /// <param name="buttonenum">
        /// The cardenum.
        /// </param>
        internal ButtonSprite(CardName card)
        {
            Clicked = false;
            Card = card;
            //Id = id;
            this.buttonFront = GameClass.buttonImages[Card];
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
        /// Gets or sets isclicked
        /// </summary>
        public bool Clicked { get; set; }

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
            spriteBatch.Draw(buttonFront, position, Color.White);
        }
    }
}
