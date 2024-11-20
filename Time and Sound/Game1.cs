using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Time_and_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bomb, pilers, boom, reset;
        SpriteFont font;    
        SoundEffect explode;
        


        Rectangle bombRectangle;
        MouseState mouseState;
        int kaBoom = 0;
        float seconds;
        bool explosionVisible = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            bombRectangle = new Rectangle(50, 50, 700, 400);
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.ApplyChanges();
            seconds = 0f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            bomb = Content.Load<Texture2D>("bomb");
            pilers = Content.Load<Texture2D>("pilers");
            font = Content.Load<SpriteFont>("TimeFont");
            explode = Content.Load<SoundEffect>("explosion");
            boom = Content.Load<Texture2D>("boom");
            reset = Content.Load<Texture2D>("reset");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            this.Window.Title = $"x = {mouseState.X} y= {mouseState.Y}";
            if (seconds < 15)
            {
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (seconds >= 15 || kaBoom == 1)
            {
                explosionVisible = true;
            }
        
            //reset button (make it reset only when curosr is on the reset button and it has to be clicked and make sure the number countdown to go to -0.00)
            if (mouseState.X >= 600 && mouseState.X <= 750 && mouseState.Y >= 400 && mouseState.Y <= 550)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    kaBoom = 0;
                    seconds = 0f;
                    explosionVisible = false;
                }
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override async void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Pink);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(bomb, bombRectangle, Color.White);
            _spriteBatch.Draw(reset, new Rectangle(600, 400, 150, 150), Color.White);
            _spriteBatch.DrawString(font, (15 - seconds).ToString("00.0"), new Vector2(270, 200), Color.Black);
          
            if (explosionVisible)
            {
                _spriteBatch.Draw(boom, new Rectangle(50, 50, 700, 400), Color.White);
            }
            //pilers follow mouse using mousestate
            _spriteBatch.Draw(pilers, new Rectangle((mouseState.X), (mouseState.Y), 100, 100), Color.White);

            //draw rect for the red and and the green fire and make it stop or explode when cutting


            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
