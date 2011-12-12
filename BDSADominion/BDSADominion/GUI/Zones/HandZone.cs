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
     internal class HandZone
     {
         /// <summary>
         /// The list of cards in the hand
         /// </summary>
         private List<CardSprite> hand = new List<CardSprite>();

         /// <summary>
         /// next card x-coor
         /// </summary>
         private Vector2 offset = new Vector2(135, 0); //TODO Move to GUIConstants

         /// <summary>
         /// touchrectangle.
         /// </summary>
         ////private Rectangle touchRect;

         /// <summary>
         /// The starting position of the hand
         /// </summary>
         private Vector2 startPosition = new Vector2(290, 375); //TODO Move to GUIConstants

         /// <summary>
         /// Initializes a new instance of the <see cref="HandZone"/> class.
         /// </summary>
         /// <param name="topmostleftlocation">
         /// The topmostleftlocation.
         /// </param>
         internal HandZone()
         {
             TouchRect = new Rectangle(
                 (int)this.startPosition.X, (int)this.startPosition.Y, (int)(this.offset.X * 10), 218);
         }

         /// <summary>
         /// Gets or sets TouchRect.
         /// </summary>
         internal Rectangle TouchRect { get; private set; }

         /// <summary>
         /// Gets or sets a value indicating whether Clicked.
         /// </summary>
         ////public bool Clicked { get; set; }

         /*private CardSprite RemoveCard(CardName card, int index)
         {
             foreach (CardSprite checkedCard in hand)
             {
                 if (card == checkedCard.CardRef & index == checkedCard.Index)
                 {
                     bool success = hand.Remove(checkedCard);
                     if (success)
                     {
                         return checkedCard;
                     }
                 }
             }
             return null;
         }*/

         //TODO: Contract: on return: hand is empty
         public void ClearHand()
         {
             foreach (CardSprite card in hand)
             {
                 hand.Remove(card);
             }
         }

         /// <summary>
         /// Add one card to the hand.
         /// </summary>
         /// <param name="newCardSprite">
         /// The new Card.
         /// </param>
         internal void AddCard(CardSprite newCardSprite)
         {
             hand.Add(newCardSprite);
         }

         /// <summary>
         /// Adds a list of cards to hand.
         /// </summary>
         /// <param name="cards">
         /// The cards.
         /// </param>
         internal void AddCards(List<CardSprite> cards)
         {
             foreach (CardSprite card in cards)
             {
                 AddCard(card);
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
             if (hand.Count > 0)
             {
                 Vector2 currentPosition = startPosition;

                 foreach (CardSprite card in hand)
                 {
                     if (card != null)
                     {
                         card.Draw(spriteBatch, currentPosition);
                         currentPosition += offset;
                     }
                 }
             }
         }

         internal bool isClickWithinHand(int mouseX, int mouseY)
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
         internal CardSprite FindCardByMouseClick(int mouseX)
         {
             int clickedIndex = ClickedIndex(mouseX);
             int count = 0;
             foreach (CardSprite card in hand)
             {
                 if (clickedIndex == count)
                 {
                     return card;
                 }
                 count++;
             }

             return null; //TODO
         }

         private int ClickedIndex(int mouseX)
         {
             int mouseCardX = mouseX - (int) startPosition.X;
             float clickedValue = mouseCardX/offset.X;
             float clickedInto = (mouseCardX%offset.X)/offset.X;

             int clickedIndex = (int) Math.Round(clickedValue - clickedInto);
             return clickedIndex;
         }
     }
 }