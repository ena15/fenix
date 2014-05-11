using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace fenix
{
    /// <summary>
    /// Class that implements enemy behaviour
    /// </summary>
    public class Enemy:Colidable
    {
        /// <summary>
        /// Constructor of the Enemy object, loads enemy texture, initializes random speed 
        /// and gives power and size of the enemy based on the total game time making enemies bigger and stronger
        /// as time goes by
        /// </summary>
        /// <param name="game">GameLevel object of current game</param>
        /// <param name="time">Current GameTime</param>
        public Enemy(GameLevel game, GameTime time)
        {
            this.Vy = (float)game.rand.NextDouble() * 7+1;
            this.Vx = (float)game.rand.NextDouble() * 2 - 1;
            this.texture = game.Content.Load<Texture2D>("mug");
            this.X = (float)(game.rand.NextDouble() * (game.GraphicsDevice.Viewport.Width - Constants.PLAYER_WIDTH));
            this.Y = -Constants.PLAYER_WIDTH;
            this.Width = (float)(Constants.PLAYER_WIDTH + time.TotalGameTime.TotalSeconds / 5.0f);
            this.Height = (float)(Constants.PLAYER_WIDTH + time.TotalGameTime.TotalSeconds / 5.0f);
            this.Power = (float)(Constants.PLAYER_POWER + time.TotalGameTime.TotalSeconds / 20);
            this.Health = (float)(1 + time.TotalGameTime.TotalSeconds / 20);
        }
        /// <summary>
        /// Velocity of enemy on X axis
        /// </summary>
        public float Vx { get; set; }
        /// <summary>
        /// Velocity of enemy on Y axis
        /// </summary>
        public float Vy { get; set; }
        /// <summary>
        /// Update the enemy position based on the speed velocities
        /// </summary>
        /// <param name="dx">Default X velocity (0 unused)</param>
        /// <param name="dy">Default Y velocity (0 unused)</param>
        /// <param name="game">GameLevel object of current game</param>
        public override void updateState(float dx, float dy,GameLevel game)
        {
            base.updateState(dx, dy,game);
            this.X += Vx;
            this.Y += Vy;
        }



    }
}
