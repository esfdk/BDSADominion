namespace BDSADominion
{

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// This class holds the information for the representation of cards.
    /// </summary>
    public class ButtonSprite
    {
        public Cardmember CardMember { get; private set; }

        ////public int Id { get; private set; }

        /// <summary>
        /// The texture for the front of the card
        /// </summary>
        private Texture2D buttonFront;

        /// <summary>
        /// the texture for the back of the card
        /// </summary>
        ////private Texture2D cardBack;

        /// <summary>
        /// bool to check if card has picture side faceup
        /// </summary>
        ////private bool faceUp;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardSprite"/> class.
        /// </summary>
        /// <param name="buttonenum">
        /// The cardenum.
        /// </param>
        public ButtonSprite(Cardmember cardenum)
        {
            Clicked = false;
            ////position = Vector2.Zero;
            ////rectangle = new Rectangle((int)position.X, (int)position.Y, 1, 1);
            CardMember = cardenum;
            //Id = id;
            this.buttonFront = GUIConstants.buttonImages[cardenum];
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
        /// Gets or sets position of the card on the screen.
        /// </summary>
        ////public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets rectangle size which holds the image of the card.
        /// </summary>
        ////public Rectangle Rectangle { get; set; }

        /// <summary>
        /// Gets or sets isclicked
        /// </summary>
        public bool Clicked { get; set; }

        /// <summary>
        /// Gets or sets card is Faceuped
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
            /*
            spriteBatch.Draw(
                cardFront,
                position,
                Rectangle.Empty, <---- THE CODE BREAKER!!!!
                Color.White,
                0.0f,
                Vector2.Zero,
                GUIConstants.SCALE,
                SpriteEffects.None,
                0);*/
            spriteBatch.Draw(buttonFront, position, Color.White);
        }
    }
}
