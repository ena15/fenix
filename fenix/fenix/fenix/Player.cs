using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace fenix
{
    /// <summary>
    /// Player class that implements player behaviour
    /// </summary>
    public class Player :Colidable
    {
        /// <summary>
        /// Total player points in game
        /// </summary>
        public float Points { get; set; }
        /// <summary>
        /// Player object constructor that reads player texture and initializes position and size of player
        /// </summary>
        /// <param name="game">GameLevel object of current game</param>
        public Player(GameLevel game)
        {

            Points = 0;
            texture = game.Content.Load<Texture2D>("mug");
            this.X = Constants.START_X;
            this.Y = Constants.START_Y;
            this.Width = Constants.PLAYER_WIDTH;
            this.Height =Constants.PLAYER_HEIGHT;
            this.Health = Constants.PLAYER_HEALTH;
            this.Power = Constants.PLAYER_POWER;
        }

        /// <summary>
        /// Update position of the player and check for bounds of game Viewport not allowing player to leave screen
        /// </summary>
        /// <param name="dx">X player speed</param>
        /// <param name="dy">Y player speed</param>
        /// <param name="game">GameLevel object of current game</param>
        public override void updateState(float dx, float dy,GameLevel game)
        {
            
            base.updateState(dx, dy,game);
            if(X+dx+this.Width<game.GraphicsDevice.Viewport.Width &&
                X+dx>0)
            X += dx;
            if (Y + dy +this.Height < game.GraphicsDevice.Viewport.Height &&
                Y + dy > 0)
            Y += dy;

        }
        
    }

}
