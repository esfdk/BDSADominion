// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="Hewlett-Packard">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BDSADominion
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework.Graphics;

    public static class GUIConstants
    {
        public static CardSprite Empty;

        public static CardSprite Back;

        public static Dictionary<Cardmember, Texture2D> cardimage = new Dictionary<Cardmember, Texture2D>();

        public static Dictionary<Buttonmember, Texture2D> buttonimage = new Dictionary<Buttonmember, Texture2D>();
    }
}
