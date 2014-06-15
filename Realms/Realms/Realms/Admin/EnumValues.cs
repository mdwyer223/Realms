using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Realms
{
    public enum MenuState
    {
        LOGIN, CREATE_ACCOUNT, OPTIONS, SELECT
    }

    public enum EnemyType
    {
        LIGHT, HEAVY, CASTER
    }

    public enum BattleState
    {
        SELECTING_OPTION, SELECTING_SPRITE, WAITING, MOVING
    }

    public enum Zone
    {

    }

    public enum Planet
    {

    }

    public enum PlayerState
    {
        IDLE, BUSY, ACTIVE
    }

    public enum SpellType
    {
        DARK, FIRE, ICE, HOLY, EARTH, ELECTRIC, NONE
    }

    public enum BattlePhase
    {
        BATTLING, INTRO, ENDSCREEN
    }

    public enum GameState
    {
        PAUSE, MAINMENU, PLAYING, DEATH, BATTLE
    }

    public enum EquipScreen
    {
        MATERIA, EQUIPMENT
    }
}
