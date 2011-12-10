namespace BDSADominion
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// The class for the Handzone.
    /// </summary>
    public class HandZone
    {
        /// <summary>
        /// The list of cards in the hand
        /// </summary>
        private List<Card> hand = new List<Card>();

        /// <summary>
        /// next card x-coor
        /// </summary>
        private int offsetX = 0;

        /// <summary>
        /// next card y-coor
        /// </summary>
        private int offsetY = 0;

        /// <summary>
        /// touchrectangle.
        /// </summary>
        private Rectangle touchRect;

        /// <summary>
        /// Card height and width.
        /// </summary>
        private int cardHeight, cardWidth;

        /// <summary>
        /// The texture object used when drawing the sprite.
        /// </summary>
        private Texture2D cardimage;

        bool showEmptyImage;

        /// <summary>
        /// The Size of the Sprite (with scale applied)
        /// </summary>
        private Rectangle rectangle;

        /// <summary>
        /// The current position of the Sprite.
        /// </summary>
        private Vector2 position;

        /// <summary>
        /// Topleft location of the card.
        /// </summary>
        private Vector2 cardTopLeft;

        /// <summary>
        /// The amount to increase/decrease the size of the original sprite. 
        /// </summary>
        private float scale = 1.0f;

        /// <summary>
        /// cardnumber.
        /// </summary>
        private int cardNumb = 0;

        /// <summary>
        /// Input of mouse (keys)
        /// </summary>
        private InputState input;

        /// <summary>
        /// The mousestate.
        /// </summary>
        private MouseState mouseState;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandZone"/> class.
        /// </summary>
        /// <param name="topmostleftlocation">
        /// The topmostleftlocation.
        /// </param>
        public HandZone(Vector2 topmostleftlocation)
        {
            cardNumb = Card.DeckNumber;
            cardNumb += 1;
            Card.DeckNumber = cardNumb;
            showEmptyImage = true;

            cardHeight = 150;
            cardWidth = 80;
        }

        /// <summary>
        /// Gets or sets TouchRect.
        /// </summary>
        public Rectangle TouchRect
        {
            get { return touchRect; }
            set { touchRect = value; }
        }

        /// <summary>
        /// Gets or sets Index.
        /// </summary>
        public int Index
        {
            get { return cardNumb; }
            set { cardNumb = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Clicked.
        /// </summary>
        public bool Clicked { get; set; }

        /// <summary>
        /// Gets or sets Card height for drawing (how tall?)
        /// </summary>
        public int CardHeight
        {
            get { return cardHeight; }
            set { cardHeight = value; }
        }

        /// <summary>
        /// Gets or sets Card width for drawing (how wide?)
        /// </summary>
        public int CardWidth
        {
            get { return cardWidth; }
            set { cardWidth = value; }
        }

        /// <summary>
        /// Gets or sets OffsetHeight.
        /// </summary>
        public int OffsetHeight
        {
            get { return offsetY; }
            set { offsetY = value; }
        }

        /// <summary>
        /// Gets or sets OffsetWidth.
        /// </summary>
        public int OffsetWidth
        {
            get { return offsetX; }
            set { offsetX = value; }
        }

        public bool ShowEmptyCard
        {
            get { return showEmptyImage; }
            set { showEmptyImage = value; }
        }

        /// <summary>
        /// Add one card to the hand.
        /// </summary>
        /// <param name="newCard">
        /// The new Card.
        /// </param>
        public void AddCard(Card newCard)
        {
            hand.Add(newCard);
        }

        /// <summary>
        /// Adds a list of cards to hand.
        /// </summary>
        /// <param name="cards">
        /// The cards.
        /// </param>
        public void AddCards(List<Card> cards)
        {
            foreach (Card c in cards)
            {
                hand.Add(c);
            }
        }

        /// <summary>
        /// Draw the handzone spritbatch
        /// </summary>
        /// <param name="spriteBatch">
        /// The sprite Batch.
        /// </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (hand.Count > 0)
            {
                CardLocation();
                foreach (Card card in hand)
                {
                    spriteBatch.Draw(card.CardFace(), card.Rectangle, Color.White);
                }
            }
        }

        /// <summary>
        /// Returns the actual card clicked on 
        /// </summary>
        /// <param name="mouseX">
        /// The mouse X.
        /// </param>
        /// <param name="mouseY">
        /// The mouse Y.
        /// </param>
        /// <returns>
        /// The find card by mouse click.
        /// </returns>
        public Card FindCardByMouseClick(int mouseX, int mouseY)
        {

            Rectangle mouseRect = new Rectangle(mouseX, mouseY, 1, 1);
            foreach (Card card in hand)
            {
                if (card.TouchRect.Contains(mouseX, mouseY))
                {
                    card.Clicked = true;
                    return card;
                }
            }
            return null;
        }

        /// <summary>
        /// method for finding card location and giving it an area to click.
        /// </summary>
        private void CardLocation()
        {
            for (int i = 0; i < hand.Count - 1; i++)
            {
                hand[i].Position = cardTopLeft;
                hand[i].Rectangle = new Rectangle((int)cardTopLeft.X, (int)cardTopLeft.Y, cardWidth, cardHeight);
                hand[i].TouchRect = new Rectangle((int)cardTopLeft.X, (int)cardTopLeft.Y, cardWidth, cardHeight);
            }
        }
    }
}
