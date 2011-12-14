namespace BDSADominion.GUI
{
    using BDSADominion.GUI.Sprites;

    using Gamestate;

    /// <summary>
    /// Listener on cardname.
    /// </summary>
    /// <param name="pressedCard">
    /// The pressed card.
    /// </param>
    public delegate void CardNameHandler(CardName pressedCard);

    /// <summary>
    /// event is raised when index in hand is pressed.
    /// </summary>
    /// <param name="index">
    /// The index.
    /// </param>
    public delegate void IndexHandler(int index);

    /// <summary>
    /// event is raised when card is clicked on in handzone.
    /// </summary>
    /// <param name="card">
    /// The card.
    /// </param>
    public delegate void CardSpriteHandler(CardSprite card);

    /// <summary>
    /// event is raised when card is clicked.
    /// </summary>
    public delegate void ClickHandler();
}