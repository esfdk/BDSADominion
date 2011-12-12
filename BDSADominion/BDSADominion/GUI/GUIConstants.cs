using BDSADominion.Gamestate;

namespace BDSADominion
{
    using System.Collections.Generic;
    using GUI;
    using Microsoft.Xna.Framework.Graphics;

    internal static class GUIConstants
    {
        internal static CardSprite Empty = new CardSprite(CardName.Empty, -1);

        internal static CardSprite Back = new CardSprite(CardName.Backside, -1);

        internal static Dictionary<CardName, Texture2D> cardImages = new Dictionary<CardName, Texture2D>();

        internal static Dictionary<CardName, Texture2D> buttonImages = new Dictionary<CardName, Texture2D>();
    }

    internal delegate void PressedSupplyCard(CardName pressedCard);

    public delegate void PressedHandIndex(int index);

    internal delegate void PressedHandCard(CardSprite card);

    internal delegate void PressedEndPhase();
}

