namespace BDSADominion.GUI
{
    using Gamestate;
    
    public class GUIConstants
    {
        //public static CardSprite Empty = new CardSprite(CardName.Empty, -1);

        //public static CardSprite Back = new CardSprite(CardName.Backside, -1);

        //public static Dictionary<CardName, Texture2D> cardImages = new Dictionary<CardName, Texture2D>();

        //public static Dictionary<CardName, Texture2D> buttonImages = new Dictionary<CardName, Texture2D>();
    }

    public delegate void ReturnSupplyHandler(CardName pressedCard);

    internal delegate void PressedSupplyHandler(CardName pressedCard);

    public delegate void ReturnHandHandler(int index);

    internal delegate void PressedHandHandler(CardSprite card);

    public delegate void ReturnEndPhaseHandler();

    internal delegate void PressedEndPhaseHandler();
}

