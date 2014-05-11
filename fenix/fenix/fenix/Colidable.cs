using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace fenix
{
    /// <summary>
    /// General class that describes a colideable object. The object has the property of being able to colide with other objects
    /// Each object is represented by its dimensions, texture, health and power. 
    /// </summary>
    public class Colidable
    {
        /// <summary>
        /// The texture that describes the visual properties of the colidable rectangle
        /// </summary>
        public Texture2D texture{get;set;}
        /// <summary>
        /// Height of rectangle
        /// </summary>
        public float Width { get; set; }
        /// <summary>
        /// Width of rectangle
        /// </summary>
        public float Height { get; set; }
        /// <summary>
        /// X position of Rectangle
        /// </summary>
        public float X{get;set;}
        /// <summary>
        /// Y position of Rectangle
        /// </summary>
        public float Y{get;set;}
        /// <summary>
        /// Object health. Usually used as a measure when the object in game needs to be destroyed
        /// </summary>
        public float Health { get; set; }
        /// <summary>
        /// Object power. Used to determine the damage this object does to other objects
        /// </summary>
        public float Power { get; set; }

      /// <summary>
      /// Method that checks if two Colidable objects colide with eachother
      /// </summary>
      /// <param name="other">The other object that is compared for collision with this object</param>
      /// <returns></returns>
        public Boolean inColisionWith(Colidable other) {

            if ((this.X - Width / 2.0 < other.X + other.Width / 2.0) &&
                (this.X + Width / 2.0 > other.X - other.Width / 2.0) &&
                (this.Y - this.Height / 2.0 < other.Y + other.Height / 2.0) &&
                (this.Y + this.Height / 2.0 > other.Y - other.Height / 2.0))
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Virtual function for state update of each Colidable
        /// </summary>
        /// <param name="dx">X movement</param>
        /// <param name="dy">Y movement</param>
        /// <param name="game">GameLevel object of game</param>
        public virtual void updateState(float dx, float dy,GameLevel game)
        {
           
        }
    }
}
