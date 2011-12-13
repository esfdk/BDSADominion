namespace BDSADominion.GUI
{
    using Gamestate;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// This class holds the information for the representation of cards.
    /// </summary>
    /// <author>
    /// Frederik Lysgaard (frly@itu.dk)
    /// </author>
    public class CardSprite
    {
        /// <summary>
        /// The texture for the front of the card
        /// </summary>
        private readonly Texture2D cardFront;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardSprite"/> class.
        /// </summary>
        /// <param name="card">
        /// The card.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
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
        /// Gets CardRef.
        /// </summary>
        internal CardName CardRef { get; private set; }

        /// <summary>
        /// Gets Index.
        /// </summary>
        internal int Index { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether Clicked.
        /// </summary>
        internal bool Clicked { get; set; }

        /// <summary>
        /// Checks if a object is equal to the passed object.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// true if object equals passed object.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof(CardSprite))
            {
                return false;
            }
            return Equals((CardSprite)obj);
        }

        /// <summary>
        /// hashcode.
        /// </summary>
        /// <returns>
        /// returns the given hashcode.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (CardRef.GetHashCode() * 397) ^ Index;
            }
        }

        /// <summary>
        /// Checks if a Cardsprite is equal to the passed cardsprite.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// true if cardsprite equals passed cardsprite.
        /// </returns>
        internal bool Equals(CardSprite other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.CardRef, CardRef) && other.Index == Index;
        }

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
            spriteBatch.Draw(cardFront, position, Color.White);
        }
    }
}