using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace fenix
{
    public class Colidable
    {
        public Texture2D texture{get;set;}
        public float Width { get; set; }
        public float Height { get; set; }
        public float X{get;set;}
        public float Y{get;set;}
        public float Health { get; set; }
        public float Power { get; set; }

      
        public Boolean inColisionWith(Colidable other) {
            if ((this.X - Width / 2.0 < other.X + other.Width / 2.0) &&
                (this.X + Width / 2.0 > other.X - other.Width / 2.0) &&
                (this.Y - this.Height / 2.0 < other.Y + other.Height / 2.0) &&
                (this.Y + this.Height / 2.0 > other.Y - other.Height / 2.0))
            {
                //this.Health -= other.Power;
                //other.Health -= this.Power;
                return true;
            }
            else
                return false;
        }

        public virtual void updateState(float dx, float dy,GameLevel game)
        {
           
        }
    }
}
