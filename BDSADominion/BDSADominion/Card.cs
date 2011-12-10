namespace BDSADominion
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// This class holds the information for the representation of cards.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// A collection of cards and corresponding images.
        /// </summary>
        private static IDictionary<Cardmember, Texture2D> cardimage = new Dictionary<Cardmember, Texture2D>();

        /// <summary>
        /// The contentmanager
        /// </summary>
        private static ContentManager contentManager;

        /// <summary>
        /// The card enum.
        /// </summary>
        private static List<Cardmember> cardenum = new List<Cardmember>();

        /// <summary>
        /// the position of the card
        /// </summary>
        private Vector2 position;

        private Cardmember cardmember;

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
        private Rectangle touchRectangle;

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
        /// <param name="cardenum">
        /// The cardenum.
        /// </param>
        public Card(Cardmember cardenum)
        {
            faceUp = false;
            clicked = false;
            selected = false;
            position = Vector2.Zero;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 1, 1);
            cardmember = cardenum;
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
        /// Gets or sets DeckNumber.
        /// </summary>
        public static int DeckNumber { get; set; }

        /// <summary>
        /// Gets or sets front of the card.
        /// </summary>
        public Texture2D CardFront
        {
            get { return this.cardFront; }
            set { cardFront = value; }
        }

        /// <summary>
        /// Gets or sets back of the card.
        /// </summary>
        public Texture2D CardBack
        {
            get { return this.cardBack; }
            set { cardBack = value; }
        }

        /// <summary>
        /// Gets or sets position of the card on the screen.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Gets or sets rectangle size which holds the image of the card.
        /// </summary>
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        /// <summary>
        /// Gets or sets size of the clicked rectangle.
        /// </summary>
        public Rectangle TouchRect
        {
            get { return touchRectangle; }
            set { touchRectangle = value; }

        }

        /// <summary>
        /// Gets or sets cardclicked.
        /// </summary>
        public bool Clicked
        {
            get { return clicked; }
            set { clicked = value; }
        }

        /// <summary>
        /// Gets or sets cardselected.
        /// </summary>
        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        /// <summary>
        /// Gets or sets card is Faceuped
        /// </summary>
        public bool Faceup
        {
            get { return faceUp; }
            set { faceUp = value; }
        }

        public static void LoadCardTextures()
        {
            foreach (Cardmember cardmember in cardenum)
            {
                LoadTexture(cardmember);
            }
        }

        private static void LoadTexture(Cardmember member)
        {
            string cardName = string.Format("Kingdom\\{0}", member.ToString());
            Texture2D cardTexture = contentManager.Load<Texture2D>(cardName);
            ImageHeight = cardTexture.Height;
            ImageWidth = cardTexture.Width;
            cardimage.Add(member, cardTexture);
        }

        /// <summary>
        /// Get's the front image of the card.
        /// </summary>
        /// <param name="cardenum">
        /// The cardenum.
        /// </param>
        /// <returns>
        /// returns the front image of the card.
        /// </returns>
        public Texture2D CardFace()
        {
            //string cardName = string.Format("Kingdom\\{0}", ((Cardmember)cardmember).ToString());

            return (from cp in cardimage where this.cardmember == cp.Key select cp.Value).FirstOrDefault();
        }

        /// <summary>
        /// the draw method of card
        /// </summary>
        public void Draw()
        {
        }
    }
}
