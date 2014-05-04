using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class BattleEnemy : BattleSprite
    {
        public BattleEnemy(Texture2D texture, float scaleFactor, BaseEnemy enemy)
            : base(texture, scaleFactor, enemy.Stats)
        {
            color = Color.Red;
        }
    }
}
