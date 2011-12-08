namespace BDSADominion
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// This class holds the information for the representation of cards.
    /// </summary>
    public class Card : HandZone
    {
        /// <summary>
        /// A collection of cards and corresponding images.
        /// </summary>
        private static IDictionary<int, Texture2D> cardimage = new Dictionary<int, Texture2D>();

        /// <summary>
        /// the position of the card
        /// </summary>
        private Vector2 position;

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
        /// <param name="graphicsDevice">
        /// The graphics Device.
        /// </param>
        public Card(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
            faceUp = false;
            clicked = false;
            selected = false;
            position = Vector2.Zero;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 1, 1);
        }

        /// <summary>
        /// Gets or sets ImageHeight.
        /// </summary>
        public static int ImageHeight { get; set; }

        /// <summary>
        /// Gets or sets ImageWidth.
        /// </summary>
        public static int ImageWidth { get; set; }

        /// <summary>
        /// Gets or sets Content.
        /// </summary>
        public static ContentManager Content { get; set; }


        /// <summary>
        /// The front of the card.
        /// </summary>
        public Texture2D CardFront
        {
            get { return this.cardimage; }
            set { cardimage = value; }
        }

        /// <summary>
        /// The back of the card.
        /// </summary>
        public Texture2D CardBack
        {
            get { return this.cardBack; }
            set { cardBack = value; }
        }

        /// <summary>
        /// Position of the card on the screen
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Rectangle size which holds the image of the card
        /// </summary>
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        /// <summary>
        /// The size of the clicked rectangle
        /// </summary>
        public Rectangle ClickRectangle
        {
            get { return clickRectangle; }
            set { clickRectangle = value; }

        }

        /// <summary>
        /// Card is clicked
        /// </summary>
        public bool Clicked
        {
            get { return clicked; }
            set { clicked = value; }
        }

        /// <summary>
        /// Card is selected
        /// </summary>
        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        /// <summary>
        /// Card is Faceuped
        /// </summary>
        public bool Faceup
        {
            get { return faceUp; }
            set { faceUp = value; }
        }

        /// <summary>
        /// the draw method of card
        /// </summary>
        /// <param name="spriteBatch">
        /// The sprite Batch.
        /// </param>
        /// <param name="enumm">
        /// The enumm.
        /// </param>
        public void Draw(SpriteBatch spriteBatch/*, int enumm*/)
        {
            // så længe i ikke er det samme som enumm så lig en til indtil i er lige enumm.
        }
    }
}
