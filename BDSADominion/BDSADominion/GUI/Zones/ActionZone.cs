namespace BDSADominion.GUI.Zones
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using BDSADominion.GUI.Sprites;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// The class for the Actionzone.
    /// </summary>
    /// <author>
    /// Frederik Lysgaard (frly@itu.dk)
    /// </author>
    internal class ActionZone
    {
        /// <summary>
        /// The list of cards in the actionzone.
        /// </summary>
        private readonly List<CardSprite> actionCards = new List<CardSprite>();

        /// <summary>
        /// next card x-coor.
        /// </summary>
        private readonly Vector2 offset = new Vector2(135, 0); // This would be an interesting candidate for GUIConstants 

        /// <summary>
        /// The starting position of the actionzone.
        /// </summary>
        private readonly Vector2 actionStartPosition = new Vector2(10, 50); // This would be an interesting candidate for GUIConstants

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionZone"/> class.
        /// </summary>
        internal ActionZone()
        {
            TouchRect = new Rectangle(
                (int)actionStartPosition.X, (int)actionStartPosition.Y, (int)(offset.X * 10), 218);
        }

        /// <summary>
        /// Gets TouchRect.
        /// </summary>
        internal Rectangle TouchRect { get; private set; }

        /// <summary>
        /// Removes a card from actionzone.
        /// </summary>
        /// <param name="card">
        /// The card.
        /// </param>
        /// <returns>
        /// returns true if card indicated is removed from actionzone.
        /// </returns>
        internal bool RemoveCard(CardSprite card)
        {
            if (this.actionCards.Any(card.Equals))
            {
                return this.actionCards.Remove(card);
            }

            return false;
        }

        /// <summary>
        /// Clear actions.
        /// </summary>
        internal void ClearAction()
        {
            Contract.Ensures(this.actionCards.Count == 0);

            CardSprite[] list = this.actionCards.ToArray();
            foreach (CardSprite card in list)
            {
                RemoveCard(card);
            }
        }

        /// <summary>
        /// Add one card to the actionzone.
        /// </summary>
        /// <param name="newCardSprite">
        /// The new Card.
        /// </param>
        internal void AddCard(CardSprite newCardSprite)
        {
            this.actionCards.Add(newCardSprite);
        }

        /// <summary>
        /// Adds a list of cards to the actionzone.
        /// </summary>
        /// <param name="cards">
        /// The cards.
        /// </param>
        internal void NewCards(List<CardSprite> cards)
        {
            ClearAction();
            foreach (CardSprite card in cards)
            {
                AddCard(card);
            }
        }

        /// <summary>
        /// Draw the actionzone spritbatch.
        /// </summary>
        /// <param name="spriteBatch">
        /// The sprite Batch.
        /// </param>
        internal void Draw(SpriteBatch spriteBatch)
        {
            if (this.actionCards.Count > 0)
            {
                Vector2 currentPosition = actionStartPosition;

                foreach (CardSprite card in this.actionCards)
                {
                    if (card != null)
                    {
                        card.Draw(spriteBatch, currentPosition);
                        currentPosition += offset;
                    }
                }
            }
        }
    }
}
