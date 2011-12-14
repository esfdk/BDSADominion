﻿﻿using System.Diagnostics.Contracts;

namespace BDSADominion.GUI.Zones
{
    using System;
    using System.Collections.Generic;
    using BDSADominion.Gamestate;
    using BDSADominion.GUI.Sprites;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// The class for the Supplyzone.
    /// </summary>
    /// <author>
    /// Frederik Lysgaard (frly@itu.dk)
    /// </author>
    internal class SupplyZone
    {
        /// <summary>
        /// The list of cards in the supplyzone.
        /// </summary>
        private readonly List<ButtonSprite> supply = new List<ButtonSprite>();

        /// <summary>
        /// next card x-coor
        /// </summary>
        private Vector2 offset = new Vector2(0, 35); //This would be an interesting candidate for GUIConstants

        /// <summary>
        /// The starting position of the supplyzone.
        /// </summary>
        private Vector2 startPosition = new Vector2(1150, 10); //This would be an interesting candidate for GUIConstants

        /// <summary>
        /// Initializes a new instance of the <see cref="HandZone"/> class.
        /// </summary>
        internal SupplyZone()
        {
            TouchRect = new Rectangle(
                (int)this.startPosition.X, (int)this.startPosition.Y, 100, (int)(this.offset.Y * 16));
        }

        /// <summary>
        /// Gets or sets TouchRect.
        /// </summary>
        internal Rectangle TouchRect { get; private set; }

        /// <summary>
        /// Add one card to the supplyzone.
        /// </summary>
        /// <param name="newButtonSprite">
        /// The new Card.
        /// </param>
        internal void AddCard(ButtonSprite newButtonSprite)
        {
            supply.Add(newButtonSprite);
        }

        public void ClearSupply()
        {
            Contract.Ensures(supply.Count == 0);

            ButtonSprite[] list = supply.ToArray();

            foreach (ButtonSprite button in list)
            {
                supply.Remove(button);
            }
        }

        /// <summary>
        /// Adds a list of cards to the supplyzone.
        /// </summary>
        /// <param name="buttons">
        /// The cards.
        /// </param>
        internal void NewCards(List<CardName> buttons)
        {
            ClearSupply();
            foreach (CardName button in buttons)
            {
                AddCard(new ButtonSprite(button));
            }
        }

        /// <summary>
        /// Draw the supplyzone spritbatch.
        /// </summary>
        /// <param name="spriteBatch">
        /// The sprite Batch.
        /// </param>
        internal void Draw(SpriteBatch spriteBatch)
        {
            if (supply.Count > 0)
            {
                Vector2 currentPosition = startPosition;

                foreach (ButtonSprite card in supply)
                {
                    if (card != null)
                    {
                        card.Draw(spriteBatch, currentPosition);
                        currentPosition += offset;
                    }
                }
            }
        }

        internal bool IsClickWithin(int mouseX, int mouseY)
        {
            return TouchRect.Contains(mouseX, mouseY);
        }

        /// <summary>
        /// finds which button has been clicked on in supplyzone.
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
        internal CardName FindCardByMouseClick(int mouseX, int mouseY)
        {
            Contract.Requires(IsClickWithin(mouseX, mouseY));

            int clickedIndex = ClickedIndex(mouseY);
            int count = 0;
            foreach (ButtonSprite card in supply)
            {
                if (clickedIndex == count)
                {
                    return card.Card;
                }
                count++;
            }

            return CardName.Empty;
        }

        private int ClickedIndex(int mouseY)
        {
            int mouseCardY = mouseY - (int)startPosition.Y;
            float clickedValue = mouseCardY / offset.Y;
            float clickedInto = (mouseCardY % offset.Y) / offset.Y;

            return (int)Math.Round(clickedValue - clickedInto);
        }
    }
}