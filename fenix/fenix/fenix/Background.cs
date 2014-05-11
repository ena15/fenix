using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace fenix
{
    public class Background
    {
        public Texture2D texture;
        public Rectangle rect1;
        public Rectangle rect2;
        public Background(GameLevel game)
        {
            GraphicsDevice device = game.GraphicsDevice;
            texture = game.Content.Load<Texture2D>("background");
            rect1 = new Rectangle(0,0,device.Viewport.Bounds.Width,device.Viewport.Bounds.Height);

            rect2 = new Rectangle(0, device.Viewport.Bounds.Height, device.Viewport.Bounds.Width, device.Viewport.Bounds.Height);
        }

        public void update(GameLevel game)
        {
            GraphicsDevice device = game.GraphicsDevice;
            if (rect1.Y >= device.Viewport.Height)
                rect1.Y = rect2.Y - device.Viewport.Height;
            // Then repeat this check for rectangle2.
            if (rect2.Y >= device.Viewport.Height )
                rect2.Y = rect1.Y - device.Viewport.Height;

            // 6. Incrementally move the rectangles to the left. 
            // Optional: Swap X for Y if you want to scroll vertically.
            rect1.Y += 5;
            rect2.Y += 5;

        }
    }
}
