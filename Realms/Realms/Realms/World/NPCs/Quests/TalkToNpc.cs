using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Realms
{
    public class TalkToNpc : Quest
    {
        public TalkToNpc()
            : base("Talk to an NPC", 500, 100, null)
        {
        }

        public override void checkQuest(Form currentForm, Battle battle, BaseCharacter player)
        {
            if (currentForm != null)
            {
                if (currentForm.NPCGiven != null)
                {
                    Completed = true;
                }
            }
            base.checkQuest(currentForm, battle, player);
        }
    }
}
