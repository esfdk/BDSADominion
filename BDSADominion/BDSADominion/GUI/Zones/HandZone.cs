﻿﻿namespace BDSADominion.GUI.Zones
 {
     using System;
     using System.Collections.Generic;

     using Microsoft.Xna.Framework;
     using Microsoft.Xna.Framework.Graphics;

     /// <summary>
     /// The class for the Handzone.
     /// </summary>
     /// <author>
     /// Frederik Lysgaard (frly@itu.dk)
     /// </author>
     internal class HandZone
     {
         /// <summary>
         /// The list of cards in the hand.
         /// </summary>
         private readonly List<CardSprite> hand = new List<CardSprite>();

         /// <summary>
         /// next card x-coor
         /// </summary>
         private Vector2 offset = new Vector2(135, 0); //TODO Move to GUIConstants

         /// <summary>
         /// The starting position of the hand.
         /// </summary>
         private Vector2 startPosition = new Vector2(290, 375); //TODO Move to GUIConstants

         /// <summary>
         /// Initializes a new instance of the <see cref="HandZone"/> class.
         /// </summary>
         internal HandZone()
         {
             TouchRect = new Rectangle(
                 (int)this.startPosition.X, (int)this.startPosition.Y, (int)(this.offset.X * 10), 218);
         }

         /// <summary>
         /// Gets or sets TouchRect.
         /// </summary>
         internal Rectangle TouchRect { get; private set; }


         //TODO: Contract: on return: hand is empty
         public void ClearHand()
         {
             CardSprite[] list = hand.ToArray();

             foreach (CardSprite card in list)
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
         /// Sets a list of cards as the hand.
         /// </summary>
         /// <param name="cards">
         /// The cards.
         /// </param>
         internal void NewCards(List<CardSprite> cards)
         {
             ClearHand();

             foreach (CardSprite card in cards)
             {
                 AddCard(card);
             }
         }

         /// <summary>
         /// Draw the handzone spritbatch.
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

         internal bool isClickWithin(int mouseX, int mouseY)
         {
             return TouchRect.Contains(mouseX, mouseY);
         }

         /// <summary>
         /// Returns the actual card clicked on.
         /// </summary>
         /// <param name="mouseX">
         /// The mouse X.
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
             int mouseCardX = mouseX - (int)startPosition.X;
             float clickedValue = mouseCardX / offset.X;
             float clickedInto = (mouseCardX % offset.X) / offset.X;

             int clickedIndex = (int)Math.Round(clickedValue - clickedInto);
             return clickedIndex;
         }
     }
 }