using BDSADominion.Gamestate;

namespace BDSADominion
{
    using System.Collections.Generic;
    using GUI;
    using Microsoft.Xna.Framework.Graphics;

    public static class GUIConstants
    {
        public static CardSprite Empty;

        public static CardSprite Back;

        public static Dictionary<CardName, Texture2D> cardImages = new Dictionary<CardName, Texture2D>();

        public static Dictionary<CardName, Texture2D> buttonImages = new Dictionary<CardName, Texture2D>();
    }
}

