using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tileProto
{
    class Player : Sprite
    {
        float oldRotation = 0;
        float fireCoolDown = 0.25f;
        Vector2 bulletSpawn;
        List<Bullet> bulletList = new List<Bullet>();

        public Player()
        {
            _Tag = SpriteType.kPlayerType;
        }

        public override void LoadContent(string path, Game1 tehGame)
        {
            theGame = tehGame;
            for (int i = 0; i < 50; i++)
            {
                Bullet bullet = new Bullet();
                bullet.LoadContent("bullet", tehGame);
                bullet._Draw = false;
                bullet._CurrentState = Sprite.SpriteState.kStateInActive;
                bulletList.Add(bullet);
            }

            base.LoadContent(path,theGame);
            //base.SetupAnimation(5, 30, 1, true);
        }

        public override void Update(GameTime gameTime, List<Sprite> gameObjectList)
        {
            handleInput(gameTime);
            HandleCollistion(gameObjectList);
            Animate(0);
            foreach(Bullet bull in bulletList)
            {
                bull.Update(gameTime, gameObjectList);
            }
            base.Update(gameTime, gameObjectList);
        }

        private void handleInput(GameTime gameTime)
        {
            float maxSpeed = 5f;
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            {
                _Position.X -= maxSpeed;
            }
            else if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                _Position.X += maxSpeed;
            }
            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                _Position.Y -= maxSpeed;
            }
            else if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            {
                _Position.Y += maxSpeed;
            }

            GamePadCapabilities cap = GamePad.GetCapabilities(PlayerIndex.One);
            
            if(cap.IsConnected && cap.HasLeftXThumbStick && cap.HasLeftYThumbStick && cap.HasRightXThumbStick && cap.HasRightYThumbStick)
            {
                GamePadState gpState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);
                _Position.X += (maxSpeed * gpState.ThumbSticks.Left.X);
                _Position.Y += (maxSpeed * -gpState.ThumbSticks.Left.Y);
                if(gpState.ThumbSticks.Right.X == 0 && gpState.ThumbSticks.Right.Y == 0)
                {
                }
                else
                {
                    _Rotation = (float)Math.Atan2(gpState.ThumbSticks.Right.X, gpState.ThumbSticks.Right.Y);
                    oldRotation = _Rotation;

                }

                if(gpState.IsButtonDown(Buttons.RightTrigger) && fireCoolDown <= 0.0f)
                {
                    bulletSpawn = handyFunctions.pivotPointbyRadian(_Position, _Rotation, (_Texture.Width / 2), (_Texture.Height / 2));
                    createBullet(bulletSpawn, _Rotation);
                    fireCoolDown = 0.25f;
                }
                fireCoolDown -= delta;
            }
            //LockInBounds();
        }

        private void HandleCollistion(List<Sprite> gameObjectList)
        {
            return;
            foreach (Sprite obj in gameObjectList)
            {
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach(Bullet bull in bulletList)
            {
                bull.Draw(spriteBatch);
            }
        }

        private void createBullet(Vector2 pos, double radian)
        {
            foreach (Bullet sprite in bulletList)
            {
                if (sprite._Tag == Sprite.SpriteType.kBulletType && sprite._CurrentState == Sprite.SpriteState.kStateInActive)
                {
                    sprite.Activate(radian);
                    sprite._Position = pos;
                    return;
                }
            }
            return;
        }
    }
}
