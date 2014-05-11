using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace fenix
{
    /// <summary>
    /// Class that implements PowerUp object behaviour
    /// </summary>
    public class PowerUp:Colidable
    {
        /// <summary>
        /// Powerup X axis speed velocity
        /// </summary>
        public float Vx=0;
        /// <summary>
        /// Powerup Y axis speed velocity
        /// </summary>
        public float Vy=3;
        /// <summary>
        /// Constructor that initializes powerup giving it texture, random position and random horisontal speed
        /// </summary>
        /// <param name="game">GameLevel object of current game</param>
        public PowerUp(GameLevel game)
        {
            this.Vx =(float) game.rand.NextDouble() * 2-1;
            this.texture = game.Content.Load<Texture2D>("peanutbutter");
            this.X = (float)(game.rand.NextDouble() * (game.GraphicsDevice.Viewport.Width - Constants.PLAYER_WIDTH));
            this.Y = -Constants.PLAYER_WIDTH;
            this.Width = Constants.PLAYER_WIDTH;
            this.Height = Constants.PLAYER_WIDTH;
            this.Power = 1;
        }
        /// <summary>
        /// Update PowerUP position based on its X and Y speed velocities
        /// </summary>
        /// <param name="dx">Default x (0 unused)</param>
        /// <param name="dy">Default y (0 unused)</param>
        /// <param name="game">GameLevel object of current game</param>
        public override void updateState(float dx, float dy,GameLevel game)
        {
            this.X += Vx;
            this.Y += Vy;
            
        }
    }
}
