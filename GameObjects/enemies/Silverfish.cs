using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace tileProto
{
    class Silverfish : Sprite
    {
        Vector2 _Direction;
        Player thePlayer;
        public Silverfish(Player tehPlayer)
        {
            thePlayer = tehPlayer;
            _Tag = SpriteType.kSilverfishType;
            _HP = 2;
            enemy = true;
        }
        public override void Update(GameTime gameTime, List<Sprite> gameObjectList)
        {
            _Direction = thePlayer._Position - _Position;
            _Rotation = (float)Math.Atan2(_Direction.Y, _Direction.X) + (float)(Math.PI * 0.5f);
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime, gameObjectList);
        }

        public void Activate(double direction)
        {
            _CurrentState = SpriteState.kStateActive;
            _Draw = true;
            _HP = 2;
            lifeTime = 2.0f;
            _Direction = handyFunctions.radianToVector2(direction);
        }
    }
}
