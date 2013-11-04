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
        private PlayerManager playerManager;
        public List<Sprite> powerups = new List<Sprite>();
        public float poweruptimer = 0;
        public bool gotpowerup = false;

        public PowerupManager(Texture2D laserpowerup, PlayerManager playerManager)
        {
            this.laserpowerup = laserpowerup;
            this.playerManager = playerManager;

            SpawnPowerup();
        }

        public void SpawnPowerup()
        {
            Sprite laser = new Sprite(new Vector2(300, 0), laserpowerup, new Rectangle(0, 0, 60, 53), new Vector2(0, 50));

            powerups.Add(laser);

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
            if (gameTime.TotalGameTime.TotalMilliseconds = 0 || gameTime.TotalGameTime.TotalMilliseconds % 60000 == 0)
            {
                SpawnPowerup();
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
