using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Realms
{
    public enum EnemyType
    {
        LIGHT, HEAVY, CASTER
    }

    public enum BattleState
    {
        SELECTING_OPTION, SELECTING_SPRITE, WAITING, MOVING
    }
}
