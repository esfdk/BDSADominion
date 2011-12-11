namespace BDSADominion
{
    using System.Linq;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// the view class
    /// </summary>
    public class View
    {
        /// <summary>
        /// the viewwidth
        /// </summary>
        private static int viewWidth = 0;

        /// <summary>
        /// the viewhight
        /// </summary>
        private static int viewHeight = 0;

        /// <summary>
        /// the viewport
        /// </summary>
        private static Viewport viewPort;

        /// <summary>
        /// the rectangle the viewport covers
        /// </summary>
        private static Rectangle viewPortRect;

        /// <summary>
        /// the graphicsmanager
        /// </summary>
        private static GraphicsDeviceManager graphics;

        /// <summary>
        /// the initialize method of view
        /// </summary>
        /// <param name="game">
        /// The game.
        /// </param>
        /// <param name="manager">
        /// The gd manager.
        /// </param>
        public static void Initialize(Game game, GraphicsDeviceManager manager)
        {
            graphics = manager;
            viewPort = manager.GraphicsDevice.Viewport;
            viewPort.Height = 600;
            viewPort.Width = 800;
        }

        /// <summary>
        /// Sets a default camera area for the viewport
        /// </summary>
        public static void SetDefaultCameraArea()
        {
            // Camera's default rectangle is the whole screen
            viewPortRect = new Rectangle(0, 0, viewPort.Width, viewPort.Height);
        }

        /// <summary>
        /// Initializes the screensize. Method found at: "www.planet-source-code.com/vb/scripts/ShowZip.asp?lngWId=10&lngCodeId=8451&strZipAccessCode=tp%2FX84518998"
        /// in the folder mmXNAlib.World-Engine.World.cs
        /// </summary>
        /// <param name="height">
        /// The i height.
        /// </param>
        /// <param name="width">
        /// The i width.
        /// </param>
        /// <param name="fullScreen">
        /// The b full screen.
        /// </param>
        /// <returns>
        /// returns true if screensize is
        /// </returns>
        public static bool InitScreenSize(int height, int width, bool fullScreen)
        {
            // If we aren't using a full screen mode, the height and width of the window can
            // be set to anything equal to or smaller than the actual screen size.
            if (fullScreen == false)
            {
                if ((width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                    && (height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
                {
                    graphics.PreferredBackBufferWidth = width;
                    graphics.PreferredBackBufferHeight = height;
                    graphics.IsFullScreen = fullScreen;
                    graphics.ApplyChanges();

                    viewPort = graphics.GraphicsDevice.Viewport;
                    viewHeight = viewPort.Height;
                    viewWidth = viewPort.Width;
                    SetDefaultCameraArea();

                    return true;
                }
            }
            else
            {
                // If we are using full screen mode, we should check to make sure that the display
                // adapter can handle the video mode we are trying to set.  To do this, we will
                // iterate thorugh the display modes supported by the adapter and check them against
                // the mode we want to set.
                if (GraphicsAdapter.DefaultAdapter.SupportedDisplayModes.Any(dm => (dm.Width == width) && (dm.Height == height)))
                {
                    graphics.PreferredBackBufferWidth = width;
                    graphics.PreferredBackBufferHeight = height;
                    graphics.IsFullScreen = fullScreen;
                    graphics.ApplyChanges();

                    viewPort = graphics.GraphicsDevice.Viewport;
                    viewHeight = viewPort.Height;
                    viewWidth = viewPort.Width;
                    SetDefaultCameraArea();

                    return true;
                }
            }

            return false;
        }
    }
}
