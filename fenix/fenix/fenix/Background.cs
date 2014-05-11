using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace fenix
{
    /// <summary>
    /// Class for drawing and moving background texture
    /// </summary>
    public class Background
    {
        /// <summary>
        /// Texture of background
        /// </summary>
        public Texture2D texture;
        /// <summary>
        /// Rectangles for swapping one after another
        /// </summary>
        public Rectangle rect1;
        public Rectangle rect2;
        /// <summary>
        /// Constructor that initalizes the background textures and the initial rectangle positions
        /// </summary>
        /// <param name="game">GameLevel object from the game</param>
        public Background(GameLevel game)
        {
            GraphicsDevice device = game.GraphicsDevice;
            texture = game.Content.Load<Texture2D>("background");
            rect1 = new Rectangle(0,0,device.Viewport.Bounds.Width,device.Viewport.Bounds.Height);

            rect2 = new Rectangle(0, device.Viewport.Bounds.Height, device.Viewport.Bounds.Width, device.Viewport.Bounds.Height);
        }
        /// <summary>
        /// Update positions of the game
        /// </summary>
        /// <param name="game">GameLevel object of the game</param>
        public void update(GameLevel game)
        {
            GraphicsDevice device = game.GraphicsDevice;
           //Check for rect1 and rect2 positions and update 
            if (rect1.Y >= device.Viewport.Height)
                rect1.Y = rect2.Y - device.Viewport.Height;
           
            if (rect2.Y >= device.Viewport.Height )
                rect2.Y = rect1.Y - device.Viewport.Height;

            //move rectangles one after another
            rect1.Y += 5;
            rect2.Y += 5;

        }
    }
}
