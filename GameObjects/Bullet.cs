using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace tileProto
{
    class Bullet : Sprite
    {
        Vector2 _Direction;
        public Bullet()
        {
            _Tag = SpriteType.kBulletType;
        }
        public override void Update(GameTime gameTime, List<Sprite> gameObjectList)
        {
            if(this._CurrentState == SpriteState.kStateInActive)
            {
                return;
            }
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _Position.X += _Direction.X * 200 * delta;
            _Position.Y += _Direction.Y * 200 * delta;
            lifeTime -= delta;
            if(lifeTime <= 0.0f)
            {
                ReceiveDamage(1);
            }
            HandleCollision(gameObjectList);
            base.Update(gameTime, gameObjectList);
        }

        public void Activate(double direction)
        {
            _CurrentState = SpriteState.kStateActive;
            _Draw = true;
            _HP = 1;
            lifeTime = 2.0f;
            _Direction = handyFunctions.radianToVector2(direction);
        }

        private void HandleCollision(List<Sprite> gameObjectList)
        {
            foreach(Sprite sprite in gameObjectList)
            {
                if(sprite.enemy == true)
                {
                    if (sprite._CurrentState == SpriteState.kStateActive)
                    {
                        if (_BoundingBox.Intersects(sprite._BoundingBox))
                        {
                            if (sprite._Tag == SpriteType.kSilverfishType)
                            {
                                this.ReceiveDamage(1);
                                sprite.ReceiveDamage(1);
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
