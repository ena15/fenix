using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace fenix
{
    public class Bullet:Colidable
    {
        public float Vx { get; set; }
        public float Vy { get; set; }
        public Bullet(GameLevel game,Player p)
        {
            this.texture = game.Content.Load<Texture2D>("Bullet");
            this.Width = 5+p.Power;
            this.Height = 5+p.Power;
            this.X = p.X + p.Width / 2-this.Width/2;
            this.Y = p.Y + p.Height / 2-this.Height/2;
            this.Power = p.Power;
            this.Health = 1;
            this.Vx = 0;
            this.Vy = -10;
        }
        public override void updateState(float dx, float dy,GameLevel game) {
            this.X += Vx;
            this.Y += Vy;
        }
    }
}
