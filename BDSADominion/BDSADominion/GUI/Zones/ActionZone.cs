namespace BDSADominion.GUI.Zones
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        internal List<CardSprite> ActionCards = new List<CardSprite>();

        /// <summary>
        /// next card x-coor.
        /// </summary>
        private Vector2 offset = new Vector2(135, 0); //TODO Move to GUIConstants

        /// <summary>
        /// The starting position of the actionzone.
        /// </summary>
        private Vector2 actionStartPosition = new Vector2(10, 50); //TODO Move to GUIConstants

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
        /// </returns>
        internal bool RemoveCard(CardSprite card)
        {
            if (ActionCards.Any(card.Equals))
            {
                return ActionCards.Remove(card);
            }
            return false;
        }

        //TODO Contract assert empty
        internal void ClearAction()
        {
            CardSprite[] list = ActionCards.ToArray();
            foreach (CardSprite card in list)
            {
                ActionCards.Remove(card);
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
            ActionCards.Add(newCardSprite);
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
            if (ActionCards.Count > 0)
            {
                Vector2 currentPosition = actionStartPosition;

                foreach (CardSprite card in ActionCards)
                {
                    if (card != null)
                    {
                        card.Draw(spriteBatch, currentPosition);
                        currentPosition += offset;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the actual card clicked on.
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
        internal CardSprite FindCardByMouseClick(int mouseX, int mouseY)
        {
            if (TouchRect.Contains(mouseX, mouseY))
            {
                int mouseCardX = mouseX - (int)this.actionStartPosition.X;
                float clickedValue = mouseCardX / this.offset.X;
                float clickedInto = (mouseCardX % this.offset.X) / offset.X;

                int clickedIndex = (int)Math.Round(clickedValue - clickedInto);
                int count = 0;
                foreach (CardSprite card in ActionCards)
                {
                    if (clickedIndex == count)
                    {
                        return card;
                    }
                    count++;
                }
            }

            return null; //TODO
        }
    }
}
