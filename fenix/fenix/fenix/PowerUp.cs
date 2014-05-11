using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace fenix
{
    public class PowerUp:Colidable
    {
        public float Vx=0;
        public float Vy=3;
        public PowerUp(GameLevel game)
        {
            this.Vx =(float) game.rand.NextDouble() * 4-2;
            this.texture = game.Content.Load<Texture2D>("mug1");
            this.X = (float)(game.rand.NextDouble() * (game.GraphicsDevice.Viewport.Width - Constants.PLAYER_WIDTH));
            this.Y = -Constants.PLAYER_WIDTH;
            this.Width = Constants.PLAYER_WIDTH;
            this.Height = Constants.PLAYER_WIDTH;
            this.Power = 1;
        }
        public virtual void updateState(float dx, float dy,GameLevel game)
        {
            this.X += Vx;
            this.Y += Vy;
            
        }
    }
}
