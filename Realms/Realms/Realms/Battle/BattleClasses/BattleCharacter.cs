using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Realms
{
    public class BattleCharacter : BattleSprite
    {
        Equips equipment;
        BattleState state;
        Rectangle waitRec, backgroundWait, selectorRec;
        Texture2D selectorImage;
        int waitWidth, waitHeight, selectorIndex, optionsIndex = 0, skillsIndex = 0;
        List<Label> battleOptions, skills, summons;
        List<Materia> actualMateria;
        List<Item> inventoryList;

        bool doubleStrike, cover, engineer, hasSummon;
        float doubleStrikeChance;

        bool options = true, selectingSkills = false, selectingItems = false;
        int lastIndexOption, lastIndexSkill;

        public bool SelectingSkills
        {
            get { return selectingSkills; }
        }

        public List<Label> BattleOpts
        {
            get
            {
                return battleOptions;
            }
        }

        public List<Label> Skills
        {
            get { return skills; }
        }

        public float WaitPercent
        {
            get { return (waitTimer / wait); }
        }

        public BattleCharacter(Texture2D texture, float scaleFactor, BaseCharacter character)
            : base(texture, scaleFactor, character.calcStats())
        {
            color = Color.Green;
            battleOptions = new List<Label>();
            skills = new List<Label>();
            summons = new List<Label>();
            actualMateria = new List<Materia>();
            this.stats = character.calcStats();
            this.equipment = character.Equipment;//check materia for any options to add on

            wait = (float)(MAX_WAIT) - (MAX_WAIT * (stats.Speed / 100f));
            state = BattleState.WAITING;

            waitRec.X = backgroundWait.X = (int)this.Position.X;
            waitRec.Y = backgroundWait.Y = 300;
            backgroundWait.Width = waitWidth = 50;
            backgroundWait.Height = waitRec.Height = waitHeight = 25;

            selectorIndex = 0;
            selectorRec = new Rectangle((int)this.Position.X, (int)this.Position.Y, 15, 15);
            selectorImage = Image.Particle;

            List<Materia> matItems = character.Equipment.wep.MateriaList;
            for (int i = 0; i < matItems.Count; i++)
            {
                if(matItems[i] != null)
                {
                    if (!matItems[i].Passive)
                    {
                        this.actualMateria.Add(matItems[i]);
                    }
                    else
                    {
                        //create a checkType method to get which boolean should be turned on
                        if(matItems[i].GetType() == typeof(DoubleStrike))
                        {
                            doubleStrike = true;
                            DoubleStrike db = (DoubleStrike)matItems[i];
                            doubleStrikeChance = db.Chance;
                        }
                    }
                }
            }

            matItems = character.Equipment.armor.MateriaList;
            for (int i = 0; i < matItems.Count; i++)
            {
                if (matItems[i] != null)
                {
                    if (!matItems[i].Passive)
                    {
                        this.actualMateria.Add(matItems[i]);
                    }
                    else
                    {
                    }
                }
            }

            //cycle through materia
            battleOptions.Add(new Label("Attack", Vector2.Zero, Fonts.BattleMenu));
            battleOptions.Add(new Label("Items",Vector2.Zero, Fonts.BattleMenu));
            for (int i = 0; i < matItems.Count; i++)
            {
                if (matItems[i] != null)
                {
                    skills.Add(new Label(matItems[i].Skill,Vector2.Zero, Fonts.BattleMenu));
                }
            }
            if (matItems.Count > 0)
            {
                battleOptions.Add(new Label("Skills", Vector2.Zero, Fonts.BattleMenu));
            }

            selectorRec.X = (int)Position.X;
            selectorRec.Y = (int)Position.Y;
        }

        public override void update(GameTime gameTime, List<BattleSprite> battleField)
        {
            
            waitRec.Width = (int)(waitWidth * (waitTimer / wait));
            if (state == BattleState.WAITING)
            {
                if (waitTimer < wait)
                {
                    waitTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    state = BattleState.WAITING;
                }
                else
                {
                    waitTimer = wait;
                    state = BattleState.SELECTING_OPTION;
                }
            }
            else if (state == BattleState.SELECTING_OPTION)
            {
                //display box, show battle options
                //using attack for now
                if (options)
                {
                    if (Input.downPressed())
                    {
                        lastIndexOption = optionsIndex;
                        optionsIndex++;
                        if (optionsIndex >= battleOptions.Count)
                            optionsIndex = 0;
                        
                    }
                    else if (Input.upPressed())
                    {
                        lastIndexOption = optionsIndex;
                        optionsIndex--;
                        if (optionsIndex < 0)
                            optionsIndex = battleOptions.Count - 1;
                    }
                    battleOptions[lastIndexOption].Hover = false;
                    battleOptions[optionsIndex].Hover = true;    
                }
                else if (selectingSkills)
                {
                    if (Input.downPressed())
                    {
                        lastIndexSkill = skillsIndex;
                        skillsIndex++;
                        if (skillsIndex >= skills.Count)
                            skillsIndex = 0;

                    }
                    else if (Input.upPressed())
                    {
                        lastIndexSkill = skillsIndex;
                        skillsIndex--;
                        if (skillsIndex < 0)
                            skillsIndex = skills.Count - 1;
                    }
                    skills[lastIndexSkill].Hover = false;
                    skills[skillsIndex].Hover = true;

                    if (Input.escapePressed())
                    {
                        options = true;
                        selectingSkills = false;
                    }
                }
                    
                if (Input.actionBarPressed())
                {
                    if (selectingSkills)
                    {
                        if (currentMP - actualMateria[skillsIndex].ManaCost > 0)
                        {
                            state = BattleState.SELECTING_SPRITE;
                        }
                    }

                    if (battleOptions[optionsIndex].Text.Equals("Skills") && !selectingSkills)
                    {
                        selectingSkills = true;
                        selectingItems = false;
                        options = false;
                    }
                    else if (battleOptions[optionsIndex].Text.Equals("Items") && !selectingItems)
                    {
                        selectingItems = true;
                        selectingSkills = false;
                        options = false;
                    }
                    else
                    {
                        state = BattleState.SELECTING_SPRITE;
                    }
                }
            }
            else if (state == BattleState.SELECTING_SPRITE)
            {
                //choose option here after picking someone on the field
                selectorRec.X = (int)(battleField[selectorIndex].Position.X - selectorRec.Width);
                selectorRec.Y = (int)(battleField[selectorIndex].Position.Y);
                if(battleField[selectorIndex].IsDead)
                {
                    for (int i = 0; i < battleField.Count; i++ )
                    {
                        if (battleField[i].GetType() == typeof(BattleEnemy) && !battleField[i].IsDead)
                        {
                            selectorIndex = i;
                        }
                    }
                }
                if (Input.rightPressed())
                {
                    selectorIndex++;
                    if (selectorIndex >= battleField.Count)
                        selectorIndex = 0;
                }
                else if (Input.leftPressed())
                {
                    selectorIndex--;
                    if (selectorIndex < 0)
                        selectorIndex = battleField.Count - 1;
                }

                if (Input.actionBarPressed() && !battleField[selectorIndex].IsDead)
                {
                    //check spells/mana here
                    if (!selectingSkills)
                    {
                        chooseBattleOption(battleOptions[optionsIndex].Text, battleField[selectorIndex]);
                    }
                    else
                    {
                        chooseBattleOption(skills[skillsIndex].Text, battleField[selectorIndex]);
                    }
                    state = BattleState.MOVING;
                    waitTimer = 0;
                }
            }
            else if (state == BattleState.MOVING)
            {
                //sit here till the animation is over, then trigger waiting
                state = BattleState.WAITING;
                options = true;
                selectingSkills = false;
            }

            base.update(gameTime, battleField);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            if (state == BattleState.SELECTING_SPRITE)
            {
                spriteBatch.Draw(selectorImage, selectorRec, Color.White);
            }
            base.draw(spriteBatch);
        }

        public void chooseBattleOption(string s, BattleSprite target)
        {
            switch(s)
            {
                case "Attack":
                    if (doubleStrike)
                    {
                        float chance = (float)rand.NextDouble();
                        if (chance < doubleStrikeChance)
                        {
                            attack(target);
                            attack(target);
                        }
                        else
                        {
                            attack(target);
                        }
                    }
                    else
                    {
                        attack(target);
                    }
                    state = BattleState.MOVING;
                    break;
                case "Fire"://todo: change name
                    //check mana cost here when selection is implemented
                    magicAttack(target, skillsIndex);
                    currentMP -= actualMateria[skillsIndex].ManaCost;
                    state = BattleState.MOVING;
                    break;
            }
        }

        public void attack(BattleSprite target)
        {
            target.damage(this.stats, this.equipment.wep);
        }

        public void magicAttack(BattleSprite target, int materiaIndex)
        {
            target.magicDamage(this.stats, actualMateria[materiaIndex]);//need materia selection
        }
    }
}
