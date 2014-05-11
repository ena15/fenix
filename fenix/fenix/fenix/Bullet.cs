using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace fenix
{
    /// <summary>
    /// Class that implements bullet behaviour
    /// </summary>
    public class Bullet:Colidable
    {
        /// <summary>
        /// X Velocity of the bullet
        /// </summary>
        public float Vx { get; set; }
        /// <summary>
        /// Y Velocity of the bullet
        /// </summary>
        public float Vy { get; set; }
        /// <summary>
        /// Constructor that initializes the bullet parameters. Each bullet has predefined Velocity strait up and dimensions
        /// and power that depend on the Players power. The bigger the player power, the larger the bullet and its power.
        /// </summary>
        /// <param name="game">GameLevel object of current game</param>
        /// <param name="p">Player object</param>
        public Bullet(GameLevel game,Player p)
        {
            this.texture = game.Content.Load<Texture2D>("Bullet");
            this.Width = 5+(float)Math.Pow((p.Power-4),2);
            this.Height = 5+(float)Math.Pow((p.Power-4),2);
            this.X = p.X + p.Width / 2-this.Width/2;
            this.Y = p.Y + p.Height / 2-this.Height/2;
            this.Power = p.Power;
            this.Health = 1;
            this.Vx = 0;
            this.Vy = -10;
        }
        /// <summary>
        /// Updates the position of the bullet in each game iteration by moving the bullet in X and Y direction by 
        /// the values of its Vx and Vy
        /// </summary>
        /// <param name="dx">Default X speed ( 0 unused )</param>
        /// <param name="dy">Default Y speed ( 0 unused )</param>
        /// <param name="game">GameLevel object of the game</param>
        public override void updateState(float dx, float dy,GameLevel game) {
            this.X += Vx;
            this.Y += Vy;
        }
    }
}
