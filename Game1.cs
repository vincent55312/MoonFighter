using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameState _gameState;
    private Map _map;
    private Rectangle cube;
    Texture2D pixel;
    private int speed;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _gameState = GameState.OnMenu;
        _map = new Map(1200, 720, 1, 1);
        speed = 10;
        cube = new Rectangle(0, 0, 30, 30);

        pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData<Color>(new Color[] { Color.White });

        _graphics.PreferredBackBufferWidth = _map.yPixel;
        _graphics.PreferredBackBufferHeight = _map.xPixel;
        _graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }
        else
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                if (cube.Y < 0)
                {
                    cube.Y += _map.yPixel;
                }
                cube.Y -= speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                if (cube.X < 0)
                {
                    cube.X += _map.yPixel;
                }
                cube.X -= speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (cube.X > _map.yPixel)
                {
                    cube.X -= _map.yPixel;
                }
                cube.X += speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if(cube.Y > _map.xPixel)
                {
                    cube.Y -= _map.yPixel;
                }
                cube.Y += speed;
            }

            base.Update(gameTime);
        }
    }

    private int xCheck(Rectangle rectangle)
    {
        if (rectangle.X < 0)
        {
            return _map.xPixel;
        }
        return rectangle.X;
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();
        _spriteBatch.Draw(pixel, cube, Color.GreenYellow);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}

