namespace BDSADominion
{
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Helper for reading thouch input.
    /// </summary>
    public class InputState
    {
        /// <summary>
        /// Current mousestate.
        /// </summary>
        private MouseState currentMouseState;

        /// <summary>
        /// Last mousestate.
        /// </summary>
        private MouseState lastMouseState;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputState"/> class. 
        /// Constructs a new input state.
        /// </summary>
        public InputState()
        {
            currentMouseState = new MouseState();
            lastMouseState = new MouseState();
        }

        /// <summary>
        /// Gets CurrentMouseState.
        /// </summary>
        public MouseState CurrentMouseState
        {
            get { return currentMouseState; }
        }

        /// <summary>
        /// Gets LastMouseState.
        /// </summary>
        public MouseState LastMouseState
        {
            get { return lastMouseState; }
        }


        /// <summary>
        /// Records mouse state
        /// </summary>
        public void Update()
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

        /// <summary>  
        /// Checks if the left mouse button was click, and returns true if a left click was performed.  
        /// </summary>  
        /// <returns>True: If left mouse button was clicked.</returns>  
        public bool IsNewLeftMouseClick()
        {
            return currentMouseState.LeftButton == ButtonState.Released
                    && lastMouseState.LeftButton == ButtonState.Pressed;
        }

    }
}
