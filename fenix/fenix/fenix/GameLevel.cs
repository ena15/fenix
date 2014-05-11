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
    /// <summary>
    /// Game states enumeration
    /// </summary>
    public enum State { Play, Pause, GameOver };
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameLevel : Microsoft.Xna.Framework.Game
    {
        /// <summary>
        /// Allow windows system messagebox to appear in XNA Game
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);
 
        /// <summary>
        /// Current Game State, Pause, Play or Game Over
        /// </summary>
        State GameState;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        /// <summary>
        /// Background object of current game
        /// </summary>
        Background background;
        /// <summary>
        /// List of all bullets that appear on screen
        /// </summary>
        List<Bullet> bullets;
        /// <summary>
        /// List of all enemies that appear on screen
        /// </summary>
        List<Enemy> enemies;
        /// <summary>
        /// List of all powerups that appear on screen
        /// </summary>
        List<PowerUp> powerups;
        /// <summary>
        /// Player object in current game
        /// </summary>
        Player player;
        /// <summary>
        /// Last bullet time creation used to adjust frequency of bullets
        /// </summary>
        GameTime lastBullet;
        /// <summary>
        /// Last enemy time creation used to adjust enemy frequency
        /// </summary>
        GameTime lastEnemy;
        /// <summary>
        /// Last powerup time creation used to adjust powerup frequency
        /// </summary>
        GameTime lastPowerup;
        /// <summary>
        /// Current Game random generator used for random initialization of all objects
        /// </summary>
       public Random rand;
       /// <summary>
       /// Font used to write points and health stats and pause
       /// </summary>
        SpriteFont font;
        /// <summary>
        /// Sound effects
        /// </summary>
        SoundEffect shoot;
        SoundEffect destroy;
        SoundEffect powerup;
        SoundEffect gameover;

       


        /// <summary>
        /// GameLevel constructor, initializes size, graphics, content directory and random generator
        /// </summary>
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
            font = this.Content.Load<SpriteFont>("myfont");
            shoot = this.Content.Load<SoundEffect>("shoot");
            powerup = this.Content.Load<SoundEffect>("hit");
            destroy = this.Content.Load<SoundEffect>("destroy");
            gameover = this.Content.Load<SoundEffect>("gameover");
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
            // Use P key to pause or unpause game
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.P))
            {
                
                if (this.GameState == State.Play)
                    this.GameState = State.Pause;
                else
                    this.GameState = State.Play;
            }
            // Read movement keys if state is Play
            if (GameState == State.Play)
            {
                //Update background state
                background.update(this);
                // Move character based on Pressed Keyboard Key
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
                    //Fire with Space
                else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space))//Fire
                {
                    //lastBullet is used to remember the time the last bullet was created. Don't allow creation of bullet before 
                    //this time elapses
                    if (lastBullet == null)
                    {
                        lastBullet = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);

                    }
                    if (gameTime.TotalGameTime.TotalMilliseconds - lastBullet.TotalGameTime.TotalMilliseconds > Constants.BULLET_SPEED)
                    {
                        //Create new bullet and add it to bullet list
                        bullets.Add(new Bullet(this, player));
                        // Update last bullet time
                        lastBullet = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
                        shoot.Play();
                    }
                }
              
                int i = 0;
                // Update all bullets state and destroy bullets that go out of screen
                while (i < bullets.Count)
                {
                    bullets[i].updateState(0, 1, this);
                    if (bullets[i].X + bullets[i].Width < 0 || bullets[i].X > this.GraphicsDevice.Viewport.Width
                        || bullets[i].Y + bullets[i].Height < 0 || bullets[i].Y > this.GraphicsDevice.Viewport.Height)
                        bullets.RemoveAt(i);
                    else
                        i++;
                }
             
                //Update Enemy states
                UpdateEnemies(gameTime);
                //Update Powerup states
                UpdatePowerups(gameTime);
                //Process Colisions
                ProcessColisions(gameTime);
            }
            //Exit game on escape
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            
            base.Update(gameTime);
        }
/// <summary>
/// Method used for processing all ingame colisions with Player, Enemies, Bullets and PowerUps
/// </summary>
/// <param name="gameTime">Provides snapshot of timing values</param>
        private void ProcessColisions(GameTime gameTime)
        {
            //process powerups colision with player, add power to player and remove powerup from screen on collision
            int k = 0;
            while (k < powerups.Count)
            {
                 if (player.inColisionWith(powerups[k]))
                {
                    player.Power += powerups[k].Power;
                    powerups.RemoveAt(k);
                    powerup.Play();
                }
                else {
                    k++;
                }
            }
            //process player and enemies remove enemy and reduce player power and health based on enemy stats
            k = 0;
            while(k<enemies.Count)
            {
                if (player.inColisionWith(enemies[k]))
                {
                    player.Power =(float)Math.Max(Constants.PLAYER_POWER,player.Power/ 2.0);
                    player.Health -= enemies[k].Power;
                    destroy.Play();
                    enemies.RemoveAt(k);
                    //End gmae if player health is 0 or less
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
            //process bullets and enemies, reduce enemy health based on bullet power on colision and destroy enemies
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
                        destroy.Play();
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
                        destroy.Play();
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
        /// <summary>
        /// Updates states of each powerup, generating powerups on random intervals (not less than Constants.POWERUP_SPEED seconds
        /// Destroys powerups that go out of visible screen
        /// </summary>
        /// <param name="gameTime">Snapshot of game timing values</param>
        private void UpdatePowerups(GameTime gameTime)
        {
            //throw new NotImplementedException();
            if (lastPowerup == null)
                    {
                        lastPowerup = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);

                    }
            if (gameTime.TotalGameTime.TotalSeconds - lastPowerup.TotalGameTime.TotalSeconds > rand.NextDouble()*2*Constants.POWERUP_SPEED + Constants.POWERUP_SPEED)
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
        /// <summary>
        /// Method used to update enemies, generate new enemies based on frequency dependant on current game time
        /// destroy enemies that are out of visible screen
        /// </summary>
        /// <param name="gameTime">Snapshot of current game timing values</param>
        private void UpdateEnemies(GameTime gameTime)
        {
            double enemySpeed=0;
            // generate frequency of enemies based on game Time
            if(gameTime.TotalGameTime.TotalSeconds>Constants.LEVEL_TIME)
            {
                enemySpeed=80;
            }
            else
            {
                enemySpeed=1000-gameTime.TotalGameTime.TotalMilliseconds/180.0;
            }

            if (lastEnemy == null)
            {
                lastEnemy = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);

            }
            if (gameTime.TotalGameTime.TotalMilliseconds - lastEnemy.TotalGameTime.TotalMilliseconds > rand.NextDouble() * 2 * enemySpeed+enemySpeed)
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
            if(GameState==State.Pause){
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "BATHROOM BREAK", new Vector2(250, 200), Color.Red);
                
                spriteBatch.End();
            }
            else if (GameState == State.Play)
            {
                //draw background
                spriteBatch.Begin();
                spriteBatch.Draw(background.texture, background.rect1, Color.White);
                spriteBatch.Draw(background.texture, background.rect2, Color.White);
                spriteBatch.End();

                //draw foreground
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
              //draw powerups
                foreach (Colidable c in powerups)
                {
                    spriteBatch.Draw(c.texture, new Rectangle((int)c.X, (int)c.Y, (int)c.Width, (int)c.Height),Color.White);
                
                }
                //draw enemies
                foreach (Colidable c in enemies)
                {
                    spriteBatch.Draw(c.texture, new Rectangle((int)c.X, (int)c.Y, (int)c.Width, (int)c.Height), Color.White);
                
                }
                //draw bullets
                foreach (Colidable c in bullets)
                {
                    spriteBatch.Draw(c.texture, new Rectangle((int)c.X, (int)c.Y, (int)c.Width, (int)c.Height), Color.White);
                }
                //draw player
                spriteBatch.Draw(player.texture, new Rectangle((int)player.X, (int)player.Y, (int)player.Width, (int)player.Height), Color.White);


                spriteBatch.DrawString(font, String.Format("Beer: {0}", (int)player.Points), new Vector2(10, 10), Color.White);
                spriteBatch.DrawString(font, String.Format("Power: {0}", (int)player.Power), new Vector2(250, 10), Color.White);
                Color healthColor;
                if(player.Health>10)
                    healthColor=Color.White;
                else
                    healthColor=Color.Red;
                spriteBatch.DrawString(font, String.Format("Capacity: {0}", (int)player.Health), new Vector2(500, 10), healthColor);
                spriteBatch.DrawString(font, String.Format("Time: {0}:{1}", (int)gameTime.TotalGameTime.TotalMinutes, (int)gameTime.TotalGameTime.Seconds), new Vector2(10, 460), Color.Blue);
                spriteBatch.End();
                base.Draw(gameTime);
            }
            else if (GameState == State.GameOver)
            {
              //Show Gameover Message Box and exit game
                gameover.Play();
                MessageBox(new IntPtr(0), string.Format("GAME OVER, You've drank {0} deciliters!",(int)player.Points), "MessageBox title", 0);
                this.Exit();
            }
        }
    }
}
