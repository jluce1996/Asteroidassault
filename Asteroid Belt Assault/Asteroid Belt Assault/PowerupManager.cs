using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Belt_Assault
{
    class PowerupManager
    {
        Texture2D laserpowerup;
        Texture2D Blackhole;
        private PlayerManager playerManager;
        public List<Sprite> powerups = new List<Sprite>();
        public float poweruptimer = 0;
        public bool gotpowerup = false;
        public float spawntimer = 0;

        public PowerupManager(Texture2D laserpowerup, Texture2D blackhole, PlayerManager playerManager)
        {
            this.laserpowerup = laserpowerup;
            this.Blackhole = blackhole;
            this.playerManager = playerManager;

            SpawnPowerup();
            SpawnBlackhole();
        }

        public void SpawnPowerup()
        {
            Sprite laser = new Sprite(new Vector2(100, -20), laserpowerup, new Rectangle(0, 0, 60, 53), new Vector2(0, 50));
            
            powerups.Add(laser);
        }
        
        public void SpawnBlackhole()
        {
           Sprite blackhole = new Sprite(new Vector2(400, -30), Blackhole, new Rectangle(0, 0, 80, 60), new Vector2(0, 50));
           powerups.Add(blackhole);
        }
        
        public void Update(GameTime gameTime)
        {
            if (gotpowerup)
            {
                poweruptimer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                if (poweruptimer > 10000)
                {
                    gotpowerup = false;
                    poweruptimer = 0;
                    playerManager.minShotTimer = .1f;
                }
            }
            
                spawntimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (spawntimer > 60000)
                {
                    SpawnPowerup();
                    spawntimer = 0;                    
                }

            for (int i = powerups.Count - 1; i >= 0; i--)
                powerups[i].Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = powerups.Count - 1; i >= 0; i--)
                powerups[i].Draw(spriteBatch);
        }
    }
}
