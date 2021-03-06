﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Belt_Assault
{
    enum Weapon
    {
        SINGLESHOT,
        FASTSHOT,
        MULTISHOT
    }

    class PlayerManager
    {
        public Sprite playerSprite;
        private float playerSpeed = 160.0f;
        private Rectangle playerAreaLimit;

        private long playerscore = 0;
        public int LivesRemaining = 3;
        public bool Destroyed = false;

        private Vector2 gunOffset = new Vector2(25, 10);
        private float shotTimer = 0.0f;
        public float minShotTimer = .1f;
        private int playerRadius = 15;
        public ShotManager PlayerShotManager;

        public Weapon weapon;

        public PlayerManager(
            Texture2D texture,  
            Rectangle initialFrame,
            int frameCount,
            Rectangle screenBounds)
        {
            weapon = Weapon.SINGLESHOT;

            playerSprite = new Sprite(
                new Vector2(500, 500),
                texture,
                initialFrame,
                Vector2.Zero);

            PlayerShotManager = new ShotManager(
                texture,
                new Rectangle(0, 300, 5, 5),
                4,
                2,
                250f,
                screenBounds);

            playerAreaLimit =
                new Rectangle(
                    0,
                    0,
                    screenBounds.Width,
                    screenBounds.Height);

            for (int x = 1; x < frameCount; x++)
            {
                playerSprite.AddFrame(
                    new Rectangle(
                        initialFrame.X + (initialFrame.Width * x),
                        initialFrame.Y,
                        initialFrame.Width,
                        initialFrame.Height));
            }
            playerSprite.CollisionRadius = playerRadius;
        }

        public long PlayerScore
        {
            get
            {
                return playerscore;
            }
            set
            {
                playerscore = value;
                if (playerscore % 10000 == 0 && playerscore > 0)
                {
                    LivesRemaining++;
                }
            }
        }

        private void FireShot()
        {
            Vector2 vel;

            if (shotTimer >= minShotTimer)
            {
                switch (weapon)
                {
                    case Weapon.SINGLESHOT:
                        vel = new Vector2((float)Math.Sin(playerSprite.Rotation), -(float)Math.Cos(playerSprite.Rotation));

                        PlayerShotManager.FireShot(
                            playerSprite.Center + new Vector2(-3, 0) + vel * 20,
                            vel,
                            true);
                        break;

                    case Weapon.MULTISHOT:


                        for (int x = -5; x < 5; x++)
                        {
                            vel = new Vector2((float)Math.Sin(playerSprite.Rotation + (float)x*Math.PI/180 * 4), -(float)Math.Cos(playerSprite.Rotation + (float)x*Math.PI/180 * 5));

                            PlayerShotManager.FireShot(
                                playerSprite.Center + new Vector2(-3, 0) + vel * 20,
                                vel,
                                true);
                        }
                        break;
                } 
                shotTimer = 0.0f;
            }
        }

        private void HandleKeyboardInput(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Up))
            {
                playerSprite.Velocity += new Vector2(0, -1);
            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                playerSprite.Velocity += new Vector2(0, 1);
            }

            if (keyState.IsKeyDown(Keys.Left))
            {
                playerSprite.Velocity += new Vector2(-1, 0);
            }

            if (keyState.IsKeyDown(Keys.Right))
            {
                playerSprite.Velocity += new Vector2(1, 0);
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                FireShot();
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                playerSprite.Rotation += -0.06f;
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                playerSprite.Rotation += 0.06f;
                
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                playerSprite.Rotation = 0;
            }

        }

        private void HandleGamepadInput(GamePadState gamePadState)
        {
            playerSprite.Velocity +=
                new Vector2(
                    gamePadState.ThumbSticks.Left.X,
                    -gamePadState.ThumbSticks.Left.Y);

            if (gamePadState.Buttons.A == ButtonState.Pressed)
            {
                FireShot();
            }
        }

        private void imposeMovementLimits()
        {
            Vector2 location = playerSprite.Location;

            if (location.X < playerAreaLimit.X)
                location.X = playerAreaLimit.X;

            if (location.X >
                (playerAreaLimit.Right - playerSprite.Source.Width))
                location.X =
                    (playerAreaLimit.Right - playerSprite.Source.Width);

            if (location.Y < playerAreaLimit.Y)
                location.Y = playerAreaLimit.Y;

            if (location.Y >
                (playerAreaLimit.Bottom - playerSprite.Source.Height))
                location.Y =
                    (playerAreaLimit.Bottom - playerSprite.Source.Height);

            playerSprite.Location = location;
        }

        public void Update(GameTime gameTime)
        {
            PlayerShotManager.Update(gameTime);

            if (!Destroyed)
            {
                playerSprite.Velocity = Vector2.Zero;

                shotTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                HandleKeyboardInput(Keyboard.GetState());
                HandleGamepadInput(GamePad.GetState(PlayerIndex.One));

                playerSprite.Velocity.Normalize();
                playerSprite.Velocity *= playerSpeed;

                playerSprite.Update(gameTime);
                imposeMovementLimits();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerShotManager.Draw(spriteBatch);

            if (!Destroyed)
            {
                playerSprite.Draw(spriteBatch);
            }
        }

    }
}
