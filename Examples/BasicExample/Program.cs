using Basil.Core;
using Basil.Engine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace BasilExamples.BasicExample
{
    //class CharacterRenderer : Component
    //{
    //    public char character { get; private set; }

    //    private SFML.Graphics.Text sfmlText;
    //    // TODO: Move to Font class
    //    private static SFML.Graphics.Font bigBlueFont = new SFML.Graphics.Font("Resources/Fonts/BigBlue_TerminalPlus.ttf");

    //    float time = 0;
    //    static int nextTick = 500;

    //    public CharacterRenderer(char character)
    //    {
    //        this.character = character;

    //        sfmlText = new SFML.Graphics.Text(this.character.ToString(), bigBlueFont)
    //        {
    //            // TODO: Expose these
    //            FillColor = SFML.Graphics.Color.White,
    //            CharacterSize = 24,
    //        };

    //        time = nextTick;
    //    }

    //    public override void Update()
    //    {
    //        List<Color> colors = new List<Color>
    //        {
    //            Color.White,
    //            Color.Red,
    //            Color.Green,
    //            Color.Blue,
    //            Color.Yellow,
    //            Color.Magenta,
    //            Color.Cyan
    //        };

    //        sfmlText.FillColor = colors[new Random().Next(colors.Count)];

    //        Vector2 gridPosition = entity.gridTransform.position;

    //        sfmlText.Position = new SFML.System.Vector2f(gridPosition.X, gridPosition.Y);
            
    //    }

    //    override public void Render()
    //    {
    //        Game.Instance.window.Draw(sfmlText);
    //    }
    //}

    class Player : Component
    {
        Text text;
        public Player()
        {
            Font font = new SFML.Graphics.Font("Resources/Fonts/BigBlue_TerminalPlus.ttf");
            text = new Text("Hello World", font)
            {
                FillColor = SFML.Graphics.Color.White,
                CharacterSize = 24,
            };
        }

        override public void Render()
        {
            Game.Instance.window.Draw(text);
        }
    }

    class BasicExampleScene : Scene
    {
        Entity CreatePlayer()
        {
            Entity player = new Entity("Player");
            //player.AddComponent(new CharacterRenderer('a'));
            player.AddComponent(new Player());

            return player;
        }

        public BasicExampleScene() : base()
        {
            AddEntity(CreatePlayer());
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            // Basic Example
            // This example displays the string Hello World somewhere randomly on the screen
            // and moves it in a random direction.
            // When the text collides with a side of the screen, it bounces off in an inverse
            // direction and changes colour.
            // If the text bounces in the corner, it flashes different colours for 2 seconds.

            // TODO:
            //   - Display "Hello World" string on screen at random position
            //   - Move string in random direction
            //   - Detect when string is outside of screen bounds
            //       - Invert direction
            //       - Change to random colour
            //   - Detect if screen bounced near corner of bounds
            //       - Flash random colours for 2 seconds
            Game.Instance.AddScene(new BasicExampleScene());
            Game.Instance.Run("Basic Example - Basil");
        }
    }
}