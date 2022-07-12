using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

public class MoonFighter : Game
{
    private GraphicsDeviceManager _graphics { get; set; }
    private SpriteBatch _spriteBatch { get; set; }
    private Texture2D background { get; set; }
    private Map map { get; set; }
    private Fighter fighter { get; set; }
    private Dictionary<int, Bullet> instancesBullet { get; set; } = new Dictionary<int, Bullet>();
    private Dictionary<int, Button> menu { get; set; } = new Dictionary<int, Button>();

    private int idBullet { get; set; } = 0;
    private int lossLife { get; set; } = 0;
    private int boostSpeedBullets { get; set; } = 0;
    private int boostGeneration { get; set; } = 0;
    private int nFrameUpdated { get; set; } = 0;
    private int nDrawUpdated { get; set; } = 0;
    private bool onGameOver { get; set; } = false;
    private int maxEntityGeneration { get; set; } = 80;

    private GameState _gameState { get; set; } = GameState.MainMenu;

    public MoonFighter()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {

        menu.Add(1, new Button(new Rectangle(450, Window.ClientBounds.Height/4, 200, 50), 450, Window.ClientBounds.Height / 4, GameState.Game, Color.AntiqueWhite, "Play", GraphicsDevice));
        menu.Add(2, new Button(new Rectangle(450, Window.ClientBounds.Height / 3, 200, 50), 450, Window.ClientBounds.Height / 3, GameState.GameOver, Color.AntiqueWhite, "Reload", GraphicsDevice));
        menu.Add(3, new Button(new Rectangle(450, Window.ClientBounds.Height / 2, 200, 50), 450, Window.ClientBounds.Height / 2, GameState.Score, Color.AntiqueWhite, "Score", GraphicsDevice));
        menu.Add(4, new Button(new Rectangle(450, Window.ClientBounds.Height / 1, 200, 50), 450, Window.ClientBounds.Height / 1, GameState.Quit, Color.AntiqueWhite, "Quit", GraphicsDevice));

        map = new Map(1200, 720, 1, Content.Load<Texture2D>("background"));
        fighter = new Fighter(100, 8, 12, new Rectangle(map.yPixel/2, map.xPixel/2, 125, 75), Content.Load<Texture2D>("fighter"));
        background = Content.Load<Texture2D>("gameOver");
        _graphics.PreferredBackBufferWidth = map.yPixel;
        _graphics.PreferredBackBufferHeight = map.xPixel;
        _graphics.ApplyChanges();
        base.Initialize();
    }   

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        MediaPlayer.Play(Content.Load<Song>("music"));
        MediaPlayer.IsRepeating = true;
    }
    protected override void Update(GameTime gameTime)
    {
        if ( _gameState == GameState.Quit)
        {
            Exit();
        }
        if (_gameState == GameState.Game)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                _gameState = GameState.MainMenu;
            }
            nFrameUpdated++;
            if (nFrameUpdated % 300 == 0)
            {
                boostSpeedBullets++;
            }
            if (nFrameUpdated % 120 == 0)
            {
                boostGeneration++;
            }
            if (nFrameUpdated % 600 == 0)
            {
                fighter.speed++;
            }

            Random rnd = new Random();
            if (idBullet < maxEntityGeneration)
            {
                if (rnd.Next(0, 1000) < 10 + boostGeneration)
                {
                    instancesBullet.Add(idBullet, new Bullet(1 + boostSpeedBullets, map, new SizeElement(30, 80), Content.Load<Texture2D>("meteor"), true));
                    idBullet++;
                }

                if (rnd.Next(0, 1000) < 5 + boostGeneration)
                {
                    instancesBullet.Add(idBullet, new Bullet(1 + boostSpeedBullets * 2, map, new SizeElement(30, 80), Content.Load<Texture2D>("rocket")));
                    idBullet++;
                }

                if (rnd.Next(0, 1000) < 1 + boostGeneration)
                {
                    instancesBullet.Add(idBullet, new Bullet(1 + boostSpeedBullets * 3, map, new SizeElement(120, 100), Content.Load<Texture2D>("alien")));
                    idBullet++;
                }

                if (rnd.Next(0, 1000) < 1 + boostGeneration)
                {
                    instancesBullet.Add(idBullet, new Bullet(2 + boostSpeedBullets * 2, map, new SizeElement(300, 225), Content.Load<Texture2D>("doge"), false, false));
                    idBullet++;
                }
            }


            foreach (Bullet instance in instancesBullet.Values)
            {
                if (instance.mouvementIsVertial)
                {
                    instance.element.Y += instance.speed;
                } else
                {
                    instance.element.X += instance.speed;
                }
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if ((fighter.element.Y - 20) < 0 == false)
                    {
                        fighter.element.Y -= 20;
                    }
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    if (fighter.element.X + fighter.element.Width < 0)
                    {
                        fighter.element.X += map.yPixel;
                    }
                    fighter.element.X -= fighter.speed;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    if (fighter.element.X > map.yPixel)
                    {
                        fighter.element.X -= map.yPixel;
                    }
                    fighter.element.X += fighter.speed;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    if ((fighter.element.Y + fighter.element.Height + fighter.speed) > map.xPixel == false)
                    {
                        fighter.element.Y += fighter.speed;
                    }
                }

                // Gravity set up
                if ((fighter.element.Y + fighter.element.Height + map.gravity) > map.xPixel == false)
                {
                    fighter.element.Y += map.gravity;
                }
            }
            base.Update(gameTime);
        }
    }


    protected override void Draw(GameTime gameTime)
    {
        nDrawUpdated++;
        switch (_gameState)
        {
            case GameState.MainMenu:
                MouseState mouse = Mouse.GetState();
                var mousePosition = new Point(mouse.X, mouse.Y);

                _spriteBatch.Begin();
                _spriteBatch.Draw(map.texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

                foreach (KeyValuePair<int, Button> button in menu)
                {
                    _spriteBatch.Draw(button.Value.texture2D, new Vector2(button.Value.positionX, button.Value.positionY),
                        button.Value.rectangle, Color.BlanchedAlmond);
                    _spriteBatch.DrawString(Content.Load<SpriteFont>("File"), button.Value.text, new Vector2(button.Value.positionX, button.Value.positionY), Color.Black);

                    if (button.Value.rectangle.Contains(mousePosition))
                    {
                        if (mouse.LeftButton == ButtonState.Pressed)
                        {
                            _gameState = button.Value.gameState;
                        }
                    }
                }

                _spriteBatch.End();
                break;

            case GameState.Game:
                _spriteBatch.Begin();
                _spriteBatch.Draw(map.texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                _spriteBatch.Draw(fighter.texture, fighter.element, Color.White);

                List<int> instancesBulletExpired = new List<int>();
                foreach (KeyValuePair<int, Bullet> instance in instancesBullet)
                {
                    _spriteBatch.Draw(instance.Value.texture, instance.Value.element, Color.White);
                    if (instance.Value.element.Intersects(fighter.element))
                    {
                        lossLife++;
                    }

                    if (instance.Value.element.Y > map.xPixel)
                    {
                        instance.Value.element.Y -= map.yPixel * 2;
                    }

                    if (instance.Value.element.X > map.yPixel)
                    {
                        instance.Value.element.X -= map.yPixel * 2;
                    }
                }

                Score score = new Score(GraphicsDevice, lossLife, nFrameUpdated);

                if (score.percentScoreLeft <= 0)
                {
                    _gameState = GameState.GameOver;
                }

                _spriteBatch.Draw(score.textureLossLife, score.elementLossLife, score.color);
                _spriteBatch.Draw(score.textureScore, score.elementScore, Color.White * 0.9f);
                _spriteBatch.DrawString(Content.Load<SpriteFont>("File"), score.getScore(), new Vector2(1100, 50), Color.Black);


                _spriteBatch.End();
                break;

            case GameState.GameOver:

                _spriteBatch.Begin();
                _spriteBatch.Draw(map.texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Red);
                _spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Red);
                
                int numberBase = 0;
                if (onGameOver == false)
                {
                    numberBase = nDrawUpdated;
                    onGameOver = true;
                }

                if (nDrawUpdated > (450 + numberBase))
                {
                    _gameState = GameState.MainMenu;
                }

                _spriteBatch.End();
                break;
            case GameState.Score:
                _spriteBatch.Begin();
                _spriteBatch.Draw(map.texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                _spriteBatch.End();

                break;
            default:
                break;
        }

        base.Draw(gameTime);
    }
}

