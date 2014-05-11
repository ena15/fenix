using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fenix
{
    /// <summary>
    /// Class containing various constants used to tune the game experience
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Player speed in each direction
        /// </summary>
        public static float PLAYER_SPEED = 5f;
        /// <summary>
        /// Time of bullet frequency (1 bullet each BULLET_SPEED milliseconds)
        /// </summary>
        public static float BULLET_SPEED = 300f;
        /// <summary>
        /// Average level time (Time in seconds in which enemies achieve full strength)
        /// </summary>
        public static float LEVEL_TIME = 180f;
        /// <summary>
        /// Starting X position of the player rectangle
        /// </summary>
        public static float START_X = 400f;
        /// <summary>
        /// Starting Y position of the player rectagnle
        /// </summary>
        public static float START_Y = 320f;
        /// <summary>
        /// Width of player
        /// </summary>
        public static float PLAYER_WIDTH = 80f;
        /// <summary>
        /// Height of player
        /// </summary>
        public static float PLAYER_HEIGHT = 60f;
        /// <summary>
        /// Starting player health
        /// </summary>
        public static float PLAYER_HEALTH = 100f;
        /// <summary>
        /// Frequency of Powerups in seconds
        /// </summary>
        public static double POWERUP_SPEED=15;
        /// <summary>
        /// Starting (and minimum) player power
        /// </summary>
        public static float PLAYER_POWER=5;
    }
}
