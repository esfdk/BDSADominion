﻿namespace BDSADominion
 {
     using System;
     using System.Collections.Generic;

     using Microsoft.Xna.Framework;
     using Microsoft.Xna.Framework.Graphics;

     /// <summary>
     /// The class for the Handzone.
     /// </summary>
     public class SupplyZone
     {
         /// <summary>
         /// The list of cards in the hand
         /// </summary>
         private List<ButtonSprite> supply = new List<ButtonSprite>();

         /// <summary>
         /// next card x-coor
         /// </summary>
         private Vector2 offset = new Vector2(0, 35);

         /// <summary>
         /// touchrectangle.
         /// </summary>
         ////private Rectangle touchRect;

         /// <summary>
         /// The starting position of the hand
         /// </summary>
         private Vector2 startPosition = new Vector2(1150, 50);

         /// <summary>
         /// Initializes a new instance of the <see cref="HandZone"/> class.
         /// </summary>
         /// <param name="topmostleftlocation">
         /// The topmostleftlocation.
         /// </param>
         public SupplyZone()
         {
             TouchRect = new Rectangle(
                 (int)this.startPosition.X, (int)this.startPosition.Y, 100, (int)(this.offset.Y * 16));
         }

         /// <summary>
         /// Gets or sets TouchRect.
         /// </summary>
         public Rectangle TouchRect { get; private set; }

         /// <summary>
         /// Gets or sets a value indicating whether Clicked.
         /// </summary>
         ////public bool Clicked { get; set; }

         public ButtonSprite RemoveCard(Buttonmember buttonmember, int id)
         {
             foreach (ButtonSprite button in supply)
             {
                 if (button.Id == id & button.ButtonMember == buttonmember)
                 {
                     bool success = supply.Remove(button);
                     if (success)
                     {
                         return button;
                     }
                 }
             }
             return null;
         }

         /// <summary>
         /// Add one card to the hand.
         /// </summary>
         /// <param name="newButtonSprite">
         /// The new Card.
         /// </param>
         public void AddCard(ButtonSprite newButtonSprite)
         {
             supply.Add(newButtonSprite);
         }

         /// <summary>
         /// Adds a list of cards to hand.
         /// </summary>
         /// <param name="buttons">
         /// The cards.
         /// </param>
         public void AddCards(List<ButtonSprite> buttons)
         {
             foreach (ButtonSprite button in buttons)
             {
                 supply.Add(button);
             }
         }

         /// <summary>
         /// Draw the handzone spritbatch
         /// </summary>
         /// <param name="spriteBatch">
         /// The sprite Batch.
         /// </param>
         public void Draw(SpriteBatch spriteBatch)
         {
             if (supply.Count > 0)
             {
                 Vector2 currentPosition = startPosition;

                 foreach (ButtonSprite button in supply)
                 {
                     if (button != null)
                     {
                         button.Draw(spriteBatch, currentPosition);
                         currentPosition += offset;
                     }
                 }
             }
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
         public ButtonSprite FindCardByMouseClick(int mouseX, int mouseY)
         {
             //Rectangle mouseRect = new Rectangle(mouseX, mouseY, 1, 1);
             if (TouchRect.Contains(mouseX, mouseY))
             {

                 int mouseCardX = mouseX - (int)this.startPosition.X;
                 float clickedValue = mouseCardX / this.offset.X;
                 float clickedInto = (mouseCardX % this.offset.X) / offset.X;

                 int clickedIndex = (int)Math.Round(clickedValue - clickedInto);
                 int count = 0;
                 foreach (ButtonSprite button in supply)
                 {
                     if (clickedIndex == count)
                     {
                         return button;
                     }
                     count++;
                 }
             }

             return null; //TODO
         }
     }
 }