using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace fenix
{
    public class Enemy:Colidable
    {

        public Enemy(GameLevel game, GameTime time)
        {
            this.Vy = (float)game.rand.NextDouble() * 6;
            this.Vx = (float)game.rand.NextDouble() * 4 - 2;
            this.texture = game.Content.Load<Texture2D>("enemy");
            this.X = (float)(game.rand.NextDouble() * (game.GraphicsDevice.Viewport.Width - Constants.PLAYER_WIDTH));
            this.Y = -Constants.PLAYER_WIDTH;
            this.Width = (float)(Constants.PLAYER_WIDTH + time.TotalGameTime.TotalSeconds / 30.0f);
            this.Height = (float)(Constants.PLAYER_WIDTH + time.TotalGameTime.TotalSeconds / 30.0f);
            this.Power = (float)(Constants.PLAYER_POWER + time.TotalGameTime.TotalSeconds / 20);
            this.Health = (float)(1 + time.TotalGameTime.TotalSeconds / 20);
        }
        
        //public float GravityX { get; set; }
        //public float GravityY { get; set; }
        public float Vx { get; set; }
        public float Vy { get; set; }
        public override void updateState(float dx, float dy,GameLevel game)
        {
            base.updateState(dx, dy,game);
            this.X += Vx;
            this.Y += Vy;
        }



    }
}
