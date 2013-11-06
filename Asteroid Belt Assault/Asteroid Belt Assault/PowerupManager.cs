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
        public List<Sprite> powerdowns = new List<Sprite>();
        public float poweruptimer = 0;
        public bool gotpowerup = false;
        public float spawnpoweruptimer = 0;
        public float spawnblackholetimer = 0;
        public bool gotblackhole = false;
        public float blackholetimer = 0;


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
           powerdowns.Add(blackhole);
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

            if (gotblackhole)
            {
                blackholetimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (blackholetimer > 5000)
                {
                    gotblackhole = false;
                    blackholetimer = 0;
                    playerManager.Destroyed = true;
                }
            }
            
                spawnpoweruptimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (spawnpoweruptimer > 60000)
                {
                    SpawnPowerup();
                    spawnpoweruptimer = 0;                    
                }

                spawnblackholetimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (spawnblackholetimer > 30000)
                {
                    SpawnBlackhole();
                    spawnblackholetimer = 0;
                }


            for (int i = powerups.Count - 1; i >= 0; i--)
                powerups[i].Update(gameTime);

            for (int i = powerdowns.Count - 1; i >= 0; i--)
                powerdowns[i].Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = powerups.Count - 1; i >= 0; i--)
                powerups[i].Draw(spriteBatch);

            for (int i = powerdowns.Count - 1; i >= 0; i--)
                powerdowns[i].Draw(spriteBatch);
            
        }
    }
}
