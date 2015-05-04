using System;
using BubbasEngine.Engine.GameStates;
using BubbasEngine.Engine.Generic;
using BubbasEngine.Engine.Graphics;
using BubbasEngine.Engine.Graphics.Drawables;
using BubbasEngine.Engine.Input.Devices;

namespace ExampleGame.GameContent
{
    internal class LaunchGameState : GameState
    {
        private Sprite _beLogo;
        private Sprite _farseerLogo;
        private Sprite _sfmlLogo;

        private float _time;

        // Constructor(s)
        internal LaunchGameState()
        {
        }

        //
        public override void Initialize()
        {
            //
            _keyboard.AddOnPressed(Keyboard.Key.Space, delegate
                {
                    Random random = new Random();
                    _beLogo.Color = new Color((byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255));
                });
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
            _beLogo = new Sprite(bubbaLogoTexture);
            _beLogo.Position = new Vector2f(scrWidth * 0.5f, scrHeight * 0.5f);
            _beLogo.Origin = new Vector2f(bubbaLogoTexture.Size / 2u);
            _beLogo.Scale = new Vector2f(Math.Min(scrWidth / bubbaLogoTexture.Size.Y,
                                                  scrHeight / bubbaLogoTexture.Size.X)
                                                  * 0.7f);
            _layer.Renderables.Add(_beLogo);

            // Set up Farseer logo
            _farseerLogo = new Sprite(farseerLogoTexture);
            _farseerLogo.Position = new Vector2f(scrWidth * 0.25f, scrHeight * 0.85f);
            _farseerLogo.Origin = new Vector2f(farseerLogoTexture.Size / 2u);
            _farseerLogo.Scale = new Vector2f(Math.Min(scrWidth / farseerLogoTexture.Size.Y,
                                                       scrHeight / farseerLogoTexture.Size.X)
                                                       * 0.25f);
            _layer.Renderables.Add(_farseerLogo);

            // Set up SFML logo
            _sfmlLogo = new Sprite(sfmlLogoTexture);
            _sfmlLogo.Position = new Vector2f(scrWidth * 0.75f, scrHeight * 0.85f);
            _sfmlLogo.Origin = new Vector2f(sfmlLogoTexture.Size / 2u);
            _sfmlLogo.Scale = new Vector2f(Math.Min(scrWidth / sfmlLogoTexture.Size.Y,
                                                    scrHeight / sfmlLogoTexture.Size.X)
                                                    * 0.25f);
            _layer.Renderables.Add(_sfmlLogo);
        }

        public override void BeginFrame()
        {
            if (_time > 2f)
            {
                // Go to main game
                _states.RemoveState(this);
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
        }
    }
}
