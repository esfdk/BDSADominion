namespace BDSADominion
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// The actionzone.
    /// </summary>
    public class ActionZone
    {
        /// <summary>
        /// The list of cards in the actionzone
        /// </summary>
        private List<Card> actionzone = new List<Card>();

        bool showEmptyImage;

        /// <summary>
        /// Card height and width.
        /// </summary>
        private int cardHeight, cardWidth;

        /// <summary>
        /// The texture object used when drawing the sprite.
        /// </summary>
        private Texture2D cardimage;

        /// <summary>
        /// next card x-coor
        /// </summary>
        private int offsetX = 0;

        /// <summary>
        /// next card y-coor
        /// </summary>
        private int offsetY = 0;

        /// <summary>
        /// The Size of the Sprite (with scale applied)
        /// </summary>
        private Rectangle rectangle;

        /// <summary>
        /// The current position of the Sprite.
        /// </summary>
        private Vector2 position;

        /// <summary>
        /// cardnumber.
        /// </summary>
        private int cardNumb = 0;

        /// <summary>
        /// Topleft location of the card.
        /// </summary>
        private Vector2 cardTopLeft;

        /// <summary>
        /// The amount to increase/decrease the size of the original sprite. 
        /// </summary>
        private float scale = 1.0f;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionZone"/> class.
        /// </summary>
        /// <param name="graphicsDevice">
        /// The graphics device.
        /// </param>
        public ActionZone(Vector2 topmostleftlocation)
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
        public Rectangle TouchRect { get; set; }

        public bool ShowEmptyCard
        {
            get { return showEmptyImage; }
            set { showEmptyImage = value; }
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

        /// <summary>
        /// Add one card to the actionzone.
        /// </summary>
        /// <param name="newCard">
        /// The new Card.
        /// </param>
        public void AddCard(Card newCard)
        {
            actionzone.Add(newCard);
        }

        /// <summary>
        /// method for finding card location and giving it an area to click.
        /// </summary>
        private void CardLocation()
        {
            for (int i = 0; i < actionzone.Count - 1; i++)
            {
                actionzone[i].Position = cardTopLeft;
                actionzone[i].Rectangle = new Rectangle((int)cardTopLeft.X, (int)cardTopLeft.Y, cardWidth, cardHeight);
                actionzone[i].TouchRect = new Rectangle((int)cardTopLeft.X, (int)cardTopLeft.Y, cardWidth, cardHeight);
            }
        }

        /// <summary>
        /// Draw the actionzone spritbatch.
        /// </summary>
        /// <param name="spriteBatch">
        /// The sprite Batch.
        /// </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (actionzone.Count > 0)
            {
                CardLocation();
                foreach (Card card in actionzone)
                {
                    spriteBatch.Draw(card.CardFace(), card.Rectangle, Color.White);
                }
            }
        }
    }
}
