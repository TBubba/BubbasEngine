using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.GameStates;
using BubbasEngine.Engine.Input.Devices.Keyboards;
using BubbasEngine.Engine.Graphics.Drawables;
using SFML.Window;
using SFML.Graphics;

namespace ExampleGame.GameContent
{
    internal class LaunchGameState : GameState
    {
        private BSprite _beLogo;
        private BSprite _farseerLogo;
        private BSprite _sfmlLogo;

        private float _time;

        private KeyboardBindingCollection _keys;

        // Constructor(s)
        internal LaunchGameState()
        {
            //
            _keys = new KeyboardBindingCollection();
            _keys.AddOnPressed(Keyboard.Key.Space,
                new KeyboardBinding(new KeyboardInputDele(delegate
                {
                    Random random = new Random();
                    _beLogo.Color = new Color((byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255));
                })));
        }

        //
        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            // Define content paths
            const string bubbaLogoPath = @"GameContent\Intro\BubbaLogo.png";
            const string farseerLogoPath = @"GameContent\Intro\FarseerLogo.png";
            const string sfmlLogoPath = @"GameContent\Intro\SfmlLogo.png";

            // Define positioning
            float scrWidth = (float)_graphics.RenderWidth;
            float scrHeight = (float)_graphics.RenderHeight;

            // Load content
            Texture bubbaLogoTexture = _content.LoadContent(bubbaLogoPath).GetContent<Texture>();
            Texture farseerLogoTexture = _content.LoadContent(farseerLogoPath).GetContent<Texture>();
            Texture sfmlLogoTexture = _content.LoadContent(sfmlLogoPath).GetContent<Texture>();

            // Set up Bubbas Engine's logo
            _beLogo = new BSprite(bubbaLogoTexture);
            _beLogo.Position = new Vector2f(scrWidth * 0.5f, scrHeight * 0.5f);
            _beLogo.Origin = new Vector2f(bubbaLogoTexture.Size / 2u);
            _beLogo.Scale = new Vector2f(Math.Min(scrWidth / bubbaLogoTexture.Size.Y,
                                                  scrHeight / bubbaLogoTexture.Size.X)
                                                  * 0.7f);
            _layer.Renderables.Add(_beLogo);

            // Set up Farseer logo
            _farseerLogo = new BSprite(farseerLogoTexture);
            _farseerLogo.Position = new Vector2f(scrWidth * 0.25f, scrHeight * 0.85f);
            _farseerLogo.Origin = new Vector2f(farseerLogoTexture.Size / 2u);
            _farseerLogo.Scale = new Vector2f(Math.Min(scrWidth / farseerLogoTexture.Size.Y,
                                                       scrHeight / farseerLogoTexture.Size.X)
                                                       * 0.25f);
            _layer.Renderables.Add(_farseerLogo);

            // Set up SFML logo
            _sfmlLogo = new BSprite(sfmlLogoTexture);
            _sfmlLogo.Position = new Vector2f(scrWidth * 0.75f, scrHeight * 0.85f);
            _sfmlLogo.Origin = new Vector2f(sfmlLogoTexture.Size / 2u);
            _sfmlLogo.Scale = new Vector2f(Math.Min(scrWidth / sfmlLogoTexture.Size.Y,
                                                    scrHeight / sfmlLogoTexture.Size.X)
                                                    * 0.25f);
            _layer.Renderables.Add(_sfmlLogo);

            // Apply Keybindings
            _keys.Apply(_input.Keyboard);
        }

        public override void BeginFrame()
        {
            if (_time > 2f)
            {
                // Go to main game
                //_states.RemoveState(this);
                //_states.AddState(new MainMenu.MainMenuGameState());
            }
        }

        public override void Step()
        {
        }

        public override void Animate(float delta)
        {
            // Update time                                           
            _time += delta / 1000f;
        }

        public override void UnloadContent()
        {
            // Unload content
            //content.DEQUSET(this, @"intro\logo.png");

            // Remove graphics
            _layer.Renderables.Remove(_beLogo);
            _layer.Renderables.Remove(_farseerLogo);
            _layer.Renderables.Remove(_sfmlLogo);

            // Remove Keybindings
            _keys.Remove(_input.Keyboard);
        }
    }
}
