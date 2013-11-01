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

        public PowerupManager(Texture2D laserpowerup, PlayerManager playerManager)
        {
            this.laserpowerup = laserpowerup;
            this.playerManager = playerManager;

            SpawnPowerup();
        }

        public void SpawnPowerup()
        {
            Sprite pup = new Sprite(new Vector2(300, 0), laserpowerup, new Rectangle(0, 0, 60, 53), new Vector2(0, 50));

            powerups.Add(pup);

        }

        public void Update(GameTime gameTime)
        {
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
