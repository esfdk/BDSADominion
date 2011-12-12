namespace BDSADominion
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework.Graphics;

    public static class GUIConstants
    {
        public static CardSprite Empty;

        public static CardSprite Back;

        public static Dictionary<Cardmember, Texture2D> cardImages = new Dictionary<Cardmember, Texture2D>();

        public static Dictionary<Cardmember, Texture2D> buttonImages = new Dictionary<Cardmember, Texture2D>();
    }
}

