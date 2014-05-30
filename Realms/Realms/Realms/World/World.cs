using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Realms
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class World : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Grid g;

        public Grid Grid
        {
            get { return g; }
        }

        public World(Game game)
            : base(game)
        {
            g = new TestDungeon(10, 10);
            g.Player = new Assassin(Image.Particle, Location.Zero, 98, 0);
            g.Player.giveQuest(new TalkToNpc());
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }
        
        public override void Update(GameTime gameTime)
        {
            g.update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, rs, null, Game1.Camera.Transform);
            g.draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void changeMap(Grid newPlace)
        {
            if (newPlace != null)
            {
                newPlace.Player = g.Player;
                newPlace.Player.setLocation(newPlace, newPlace.StartLoc);
                g = newPlace;
            }
        }
    }
}
