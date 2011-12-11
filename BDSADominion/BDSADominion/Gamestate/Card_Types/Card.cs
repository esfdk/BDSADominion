namespace BDSADominion.Gamestate.Card_Types
{
    using System;

    /// <summary>
    /// A card in the game of Dominion.
    /// </summary>
    /// <author>
    /// Jakob Melnyk (jmel@itu.dk)
    /// </author>
    public abstract class Card : IEquatable<Card>
    {
        /// <summary>
        /// Gets name of the card.
        /// </summary>
        public CardName Name { get; private set; }

        /// <summary>
        /// Gets number of the card.
        /// </summary>
        public uint Number { get; private set; }

        /// <summary>
        /// Check if two cards are equal.
        /// </summary>
        /// <param name="left">
        /// The left card.
        /// </param>
        /// <param name="right">
        /// The right card.
        /// </param>
        /// <returns>
        /// True if the cards are equal, false if they are not.
        /// </returns>
        public static bool operator ==(Card left, Card right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Check if two cards are not equal.
        /// </summary>
        /// <param name="left">
        /// The left card.
        /// </param>
        /// <param name="right">
        /// The right card.
        /// </param>
        /// <returns>
        /// True if the cards are not equal, false if they are.
        /// </returns>
        public static bool operator !=(Card left, Card right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Sets up the card with the correct values.
        /// </summary>
        /// <param name="name">
        /// The name of the card.
        /// </param>
        /// <param name="number">
        /// The number of the card.
        /// </param>
        public virtual void Initialize(CardName name, uint number)
        {
            // This is not done in constructor, because we wanted to inherit the setting of properties without having to do code duplication in all of our cards.
            this.Name = name;
            this.Number = number;
        }

        /// <summary>
        /// Compares two objects for equality. Two card objects are equal if Number and Name are equal in both cards.
        /// </summary>
        /// <param name="obj">
        /// The object to be compared.
        /// </param>
        /// <returns>
        /// True if the objects are equal, false if not.
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

            return obj.GetType() == typeof(Card) && this.Equals((Card)obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// True if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Card other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.Name, this.Name) && other.Number == this.Number;
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Name.GetHashCode() * 397) ^ this.Number.GetHashCode();
            }
        }
    }
}
