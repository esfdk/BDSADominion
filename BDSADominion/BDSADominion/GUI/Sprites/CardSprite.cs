namespace BDSADominion.GUI
{
    using Gamestate;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// This class holds the information for the representation of cards.
    /// </summary>
    public class CardSprite
    {
        ////public CardName Card { get; private set; }

        internal CardName CardRef { get; private set; }

        internal int Index { get; private set; }

        /// <summary>
        /// Gets or sets isclicked
        /// </summary>
        internal bool Clicked { get; set; }

        /// <summary>
        /// The texture for the front of the card
        /// </summary>
        private Texture2D cardFront;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardSprite"/> class.
        /// </summary>
        internal CardSprite(CardName card, int index)
        {
            Index = index;
            CardRef = card;
            Clicked = false;
            ////position = Vector2.Zero;
            ////rectangle = new Rectangle((int)position.X, (int)position.Y, 1, 1);
            ////CardName = cardenum;
            ////Id = id;
            cardFront = GameClass.cardImages[CardRef];
        }

        /// <summary>
        /// Gets ImageHeight.
        /// </summary>
        /*public int ImageHeight
        {
            get
            {
                return this.cardFront.Height;
            }
        }

        /// <summary>
        /// Gets ImageWidth.
        /// </summary>
        public int ImageWidth
        {
            get
            {
                return this.cardFront.Width;
            }
        }*/

        /// <summary>
        /// Gets or sets rectangle size which holds the image of the card.
        /// </summary>
        ////public Rectangle Rectangle { get; set; }

        /// <summary>
        /// the draw method of card
        /// </summary>
        /// <param name="spriteBatch">
        /// The sprite Batch.
        /// </param>
        /// <param name="position">
        /// The position.
        /// </param>
        internal void Draw(SpriteBatch spriteBatch, Vector2 position)
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
            spriteBatch.Draw(cardFront, position, Color.White);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(CardSprite)) return false;
            return Equals((CardSprite)obj);
        }

        internal bool Equals(CardSprite other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.CardRef, CardRef) && other.Index == Index;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (CardRef.GetHashCode() * 397) ^ Index;
            }
        }
    }
}