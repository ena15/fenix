using System;

using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace fenix
{
    public enum State { Play, Pause, GameOver };
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameLevel : Microsoft.Xna.Framework.Game
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);
 
        State GameState;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Background background;
        List<Bullet> bullets;
        List<Enemy> enemies;
        List<PowerUp> powerups;
        Player player;
        GameTime lastBullet;
        GameTime lastEnemy;
        GameTime lastPowerup;
       public Random rand;
       // SpriteFont font;
        private string Points;
        public GameLevel()
        {
            rand = new Random((int)DateTime.Now.Ticks);
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 640;
          //  graphics.desi
            Content.RootDirectory = "Content";
          
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            bullets = new List<Bullet>();
            powerups = new List<PowerUp>();
            enemies = new List<Enemy>();
            player = new Player(this);
            background = new Background( this);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
           
            spriteBatch = new SpriteBatch(GraphicsDevice);
        //    font = this.Content.Load<SpriteFont>("myfont");
         
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent wills be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.P))
            {
                
                if (this.GameState == State.Play)
                    this.GameState = State.Pause;
                else
                    this.GameState = State.Play;
            }
            if (GameState == State.Play)
            {
                //Update background state
                background.update(this);
                // Move character
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left))
                {
                    player.updateState(-Constants.PLAYER_SPEED, 0, this);
                }
                else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right))
                {
                    player.updateState(Constants.PLAYER_SPEED, 0, this);
                }
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up))
                {
                    player.updateState(0, -Constants.PLAYER_SPEED, this);
                }
                else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down))
                {
                    player.updateState(0, Constants.PLAYER_SPEED, this);
                }
                else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space))//Fire
                {
                    if (lastBullet == null)
                    {
                        lastBullet = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);

                    }
                    if (gameTime.TotalGameTime.TotalMilliseconds - lastBullet.TotalGameTime.TotalMilliseconds > Constants.BULLET_SPEED)
                    {
                        bullets.Add(new Bullet(this, player));
                        lastBullet = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
                    }
                }
                // TODO: Add your update logic here
                int i = 0;
                while (i < bullets.Count)
                {
                    bullets[i].updateState(0, 1, this);
                    if (bullets[i].X + bullets[i].Width < 0 || bullets[i].X > this.GraphicsDevice.Viewport.Width
                        || bullets[i].Y + bullets[i].Height < 0 || bullets[i].Y > this.GraphicsDevice.Viewport.Height)
                        bullets.RemoveAt(i);
                    else
                        i++;
                }
             

                UpdateEnemies(gameTime);
                UpdatePowerups(gameTime);
                ProcessColisions(gameTime);
            }
            
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            
            base.Update(gameTime);
        }

        private void ProcessColisions(GameTime gameTime)
        {
            //process powerups
            int k = 0;
            while (k < powerups.Count)
            {
                 if (player.inColisionWith(powerups[k]))
                {
                    player.Power += powerups[k].Power;
                    powerups.RemoveAt(k);
                }
                else {
                    k++;
                }
            }
            //process player and enemies
            k = 0;
            while(k<enemies.Count)
            {
                if (player.inColisionWith(enemies[k]))
                {
                    player.Power =(float)Math.Max(Constants.PLAYER_POWER,player.Power/ 2.0);
                    player.Health -= enemies[k].Power;
                    enemies.RemoveAt(k);
                    if (player.Health <= 0)
                    {
                        GameState = State.GameOver;
                        break;
                    }
                }
                else
                {
                    k++;
                }
            }
            //process bullets and enemies
            int i = 0;
            while (i < enemies.Count)
            {
                int j = 0;
                bool ishit = false;
                while (j < bullets.Count)
                {
                    if (enemies[i].inColisionWith(bullets[j]))
                    {
                        enemies[i].Health -= bullets[j].Power;
                        bullets.RemoveAt(j);
                        ishit = true;
                    }
                    else
                    {
                        j++;
                    }
                }
                if (ishit)
                {
                    if (enemies[i].Health <= 0)
                    {

                        player.Points += enemies[i].Power;
                        enemies.RemoveAt(i);
                    }
                    else
                        i++;
                }
                else
                {
                    i++;
                }
            }
        }

        private void UpdatePowerups(GameTime gameTime)
        {
            //throw new NotImplementedException();
            if (lastPowerup == null)
                    {
                        lastPowerup = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);

                    }
            if (gameTime.TotalGameTime.TotalMilliseconds - lastPowerup.TotalGameTime.TotalMilliseconds > rand.NextDouble()*2*Constants.POWERUP_SPEED)
            {
                lastPowerup = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
                this.powerups.Add(new PowerUp(this));
            }
            int i = 0;
            while (i < powerups.Count)
            {
                powerups[i].updateState(0, 0, this);
                if (powerups[i].X + powerups[i].Width < 0 || powerups[i].X > this.GraphicsDevice.Viewport.Width
                       || powerups[i].Y > this.GraphicsDevice.Viewport.Height)
                    powerups.RemoveAt(i);
                else
                    i++;
            }
                
        }

        private void UpdateEnemies(GameTime gameTime)
        {
            double enemySpeed = Math.Min(Math.Abs(10 - gameTime.TotalGameTime.TotalSeconds)+1,1);
            if (lastEnemy == null)
            {
                lastEnemy = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);

            }
            if (gameTime.TotalGameTime.TotalSeconds - lastEnemy.TotalGameTime.TotalSeconds > rand.NextDouble() * 2 * enemySpeed)
            {
                lastEnemy = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
                this.enemies.Add(new Enemy(this,gameTime));
            }
            int i = 0;
            while (i < enemies.Count)
            {
                enemies[i].updateState(0, 0, this);
                if (enemies[i].X + enemies[i].Width < 0 || enemies[i].X > this.GraphicsDevice.Viewport.Width
                       || enemies[i].Y > this.GraphicsDevice.Viewport.Height)
                    enemies.RemoveAt(i);
                else
                    i++;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
           
            // TODO: Add your drawing code here
            if (GameState == State.Play)
            {
                //draw background
                spriteBatch.Begin();
                spriteBatch.Draw(background.texture, background.rect1, Color.White);
                spriteBatch.Draw(background.texture, background.rect2, Color.White);
                spriteBatch.End();

                //draw foreground
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
              
                foreach (Colidable c in powerups)
                {
                    spriteBatch.Draw(c.texture, new Rectangle((int)c.X, (int)c.Y, (int)c.Width, (int)c.Height),Color.White);
                
                }
                foreach (Colidable c in enemies)
                {
                    spriteBatch.Draw(c.texture, new Rectangle((int)c.X, (int)c.Y, (int)c.Width, (int)c.Height), Color.White);
                
                }
                foreach (Colidable c in bullets)
                {
                    spriteBatch.Draw(c.texture, new Rectangle((int)c.X, (int)c.Y, (int)c.Width, (int)c.Height), Color.White);
                }
                spriteBatch.Draw(player.texture, new Rectangle((int)player.X, (int)player.Y, (int)player.Width, (int)player.Height), Color.White);
                // spriteBatch.Draw(texture1, rectangle2, Color.White);

             //   spriteBatch.DrawString(font, "PAUSE", new Vector2(200, 300), Color.Red);
                spriteBatch.End();
                base.Draw(gameTime);
            }
            else if (GameState == State.GameOver)
            {
               // spriteBatch.DrawString(font, "GAME OVER", new Vector2(200, 300), Color.Red);

                MessageBox(new IntPtr(0), string.Format("GAME OVER, You scored {0} points!",(int)player.Points), "MessageBox title", 0);
                this.Exit();
            }
        }
    }
}
