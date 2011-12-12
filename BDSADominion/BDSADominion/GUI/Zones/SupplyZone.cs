﻿using BDSADominion.Gamestate;

namespace BDSADominion.GUI
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// The class for the Handzone.
    /// </summary>
    internal class SupplyZone
    {
        /// <summary>
        /// The list of cards in the hand
        /// </summary>
        internal List<ButtonSprite> Supply = new List<ButtonSprite>();

        /// <summary>
        /// next card x-coor
        /// </summary>
        private Vector2 offset = new Vector2(0, 35); //TODO Move to GUIConstants

        /// <summary>
        /// The starting position of the hand
        /// </summary>
        private Vector2 startPosition = new Vector2(1150, 50); //TODO Move to GUIConstants

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

        /*public CardSprite RemoveCard(Cardmember cardmember, int id)
        {
            foreach (ButtonSprite card in supply)
            {
                if (card.Id == id & card.CardMember == cardmember)
                {
                    bool success = supply.Remove(card);
                    if (success)
                    {
                        return card;
                    }
                }
            }
            return null;
        }*/

        /// <summary>
        /// Add one card to the hand.
        /// </summary>
        /// <param name="newButtonSprite">
        /// The new Card.
        /// </param>
        internal void AddCard(ButtonSprite newButtonSprite)
        {
            Supply.Add(newButtonSprite);
        }

        //TODO: Contract: on return: hand is empty
        public void ClearSupply()
        {
            foreach (ButtonSprite button in Supply)
            {
                Supply.Remove(button);
            }
        }

        /// <summary>
        /// Adds a list of cards to hand.
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
        /// Draw the handzone spritbatch
        /// </summary>
        /// <param name="spriteBatch">
        /// The sprite Batch.
        /// </param>
        internal void Draw(SpriteBatch spriteBatch)
        {
            if (Supply.Count > 0)
            {
                Vector2 currentPosition = startPosition;

                foreach (ButtonSprite card in Supply)
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
        internal CardName FindCardByMouseClick(int mouseY)
        { //TODO Contract: isClickWithinSupply(int mouseX, int mouseY) must be true
            int clickedIndex = ClickedIndex(mouseY);
            int count = 0;
            foreach (ButtonSprite card in Supply)
            {
                if (clickedIndex == count)
                {
                    return card.Card;
                }
                count++;
            }
            
            return CardName.Empty; //TODO
        }
            
        private int ClickedIndex(int mouseY)
        {
            int mouseCardY = mouseY - (int) this.startPosition.Y;
            float clickedValue = mouseCardY/this.offset.Y;
            float clickedInto = (mouseCardY%this.offset.Y)/offset.Y;

            return (int) Math.Round(clickedValue - clickedInto);
        }
    }
}