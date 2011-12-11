namespace BDSADominion
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// This class holds the information for the representation of cards.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// the position of the card
        /// </summary>
        private readonly Vector2 position;

        /// <summary>
        /// The contentmanager
        /// </summary>
        private ContentManager contentManager;

        /// <summary>
        /// The texture for the front of the card
        /// </summary>
        private Texture2D cardFront;

        /// <summary>
        /// the texture for the back of the card
        /// </summary>
        private Texture2D cardBack;

        /// <summary>
        /// the rectangle the card is placed in
        /// </summary>
        private Rectangle rectangle;

        /// <summary>
        /// the acctual rectangle your clicking on
        /// </summary>
        private Rectangle clickRectangle;

        /// <summary>
        /// bool to check if card has picture side faceup
        /// </summary>
        private bool faceUp;

        /// <summary>
        /// bool to check if card rectangle is clicked
        /// </summary>
        private bool clicked;

        /// <summary>
        /// bool to check if the card rectangle is selected
        /// </summary>
        private bool selected;

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        public Card()
        {
            faceUp = false;
            clicked = false;
            selected = false;
            position = Vector2.Zero;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 1, 1);
        }

        /// <summary>
        /// Gets CardFront.
        /// </summary>
        public Texture2D CardFront
        {
            get { return this.cardFront; }
        }

        /// <summary>
        /// the draw method of card
        /// </summary>
        public void Draw()
        {
        }
    }
}
