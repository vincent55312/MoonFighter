using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonFighter
{
    public class MoonFighter : Game
    {
        private SpriteFont _font { get; set; }
        private SpriteFont _fontLittle { get; set; }
        private GraphicsDeviceManager _graphics { get; set; }
        private SpriteBatch _spriteBatch { get; set; }
        private Texture2D _background { get; set; }
        private Texture2D _podium { get; set; }
        private Texture2D _commands { get; set; }
        private Texture2D _commands2 { get; set; }
        private Texture2D _story { get; set; }
        private Chunk _chunk { get; set; }
        private Fighter _fighter { get; set; }
        private List<Bullet> _instancesBullet { get; set; } = new List<Bullet>();
        private List<Button> _menu { get; set; } = new List<Button>();
        private List<int> _scores { get; set; } = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private int _idBullet { get; set; } = 0;
        private int _lossLife { get; set; } = 0;
        private int _boostSpeedBullets { get; set; } = 0;
        private int _boostGeneration { get; set; } = 0;
        private int _nFrameUpdated { get; set; } = 0;
        private int _maxEntityGeneration { get; set; } = 80;
        private double _elapsedTime { get; set; } = 0f;
        private bool onEnterGame { get; set; } = true;
        private bool onEnterSave { get; set; } = false;
        private Score _score { get; set; }
        private GameState _gameState { get; set; } = GameState.MainMenu;

        public MoonFighter()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _menu.Add(new Button(new Rectangle(490, 130, 220, 60), 490, 130, GameState.Game, Color.Gold, "Play", GraphicsDevice));
            _menu.Add(new Button(new Rectangle(490, 330, 220, 60), 490, 330, GameState.Story, Color.Gold, "Story", GraphicsDevice));
            _menu.Add(new Button(new Rectangle(490, 430, 220, 60), 490, 430, GameState.howPlay, Color.Gold, "How to Play", GraphicsDevice));
            _menu.Add(new Button(new Rectangle(490, 530, 220, 60), 490, 530, GameState.Score, Color.Gold, "Score", GraphicsDevice));
            _menu.Add(new Button(new Rectangle(490, 630, 220, 60), 490, 630, GameState.Quit, Color.Gold, "Quit", GraphicsDevice));

            _chunk = new Chunk(1200, 720, 1, Content.Load<Texture2D>("background"));
            _fighter = new Fighter(100, 8, 12, new Rectangle(_chunk.Ypixels / 2, _chunk.Xpixels / 2, 125, 75), Content.Load<Texture2D>("fighter"));
            _background = Content.Load<Texture2D>("gameOver");
            _podium = Content.Load<Texture2D>("podium");
            _commands = Content.Load<Texture2D>("commands");
            _commands2 = Content.Load<Texture2D>("commands2");
            _story = Content.Load<Texture2D>("story");
            _font = Content.Load<SpriteFont>("Score");
            _fontLittle = Content.Load<SpriteFont>("scoreLittle");
            _graphics.PreferredBackBufferWidth = _chunk.Ypixels;
            _graphics.PreferredBackBufferHeight = _chunk.Xpixels;
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
            if (_gameState == GameState.Quit)
            {
                Exit();
            }
            if (_gameState == GameState.Game)
            {
                _nFrameUpdated++;
                if (_nFrameUpdated % 300 == 0)
                {
                    _boostSpeedBullets++;
                }
                if (_nFrameUpdated % 120 == 0)
                {
                    _boostGeneration++;
                }
                if (_nFrameUpdated % 600 == 0)
                {
                    _fighter.Speed++;
                }

                Random rnd = new Random();
                if (_idBullet < _maxEntityGeneration)
                {
                    if (rnd.Next(0, 1000) < 10 + _boostGeneration)
                    {
                        _instancesBullet.Add(new Bullet(1 + _boostSpeedBullets, _chunk, new SizeElement(30, 80), Content.Load<Texture2D>("meteor"), true));
                        _idBullet++;
                    }

                    if (rnd.Next(0, 1000) < 5 + _boostGeneration)
                    {
                        _instancesBullet.Add(new Bullet(1 + _boostSpeedBullets * 2, _chunk, new SizeElement(30, 80), Content.Load<Texture2D>("rocket")));
                        _idBullet++;
                    }

                    if (rnd.Next(0, 1100) < 1 + _boostGeneration)
                    {
                        _instancesBullet.Add(new Bullet(1 + _boostSpeedBullets * 3, _chunk, new SizeElement(120, 100), Content.Load<Texture2D>("alien")));
                        _idBullet++;
                    }

                    if (rnd.Next(0, 1200) < 1 + _boostGeneration)
                    {
                        _instancesBullet.Add(new Bullet(2 + _boostSpeedBullets * 2, _chunk, new SizeElement(300, 225), Content.Load<Texture2D>("doge"), false, false));
                        _idBullet++;
                    }

                    if (rnd.Next(0, 1500) < 1 + _boostGeneration)
                    {
                        _instancesBullet.Add(new Bullet(2 + _boostSpeedBullets * 7, _chunk, new SizeElement(400, 225), Content.Load<Texture2D>("tesla"), false, false));
                        _idBullet++;
                    }
                }


                foreach (Bullet instance in _instancesBullet)
                {
                    if (instance.MouvementIsVertial)
                    {
                        instance.Element.Y += instance.Speed;
                    }
                    else
                    {
                        instance.Element.X += instance.Speed;
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
                        if ((_fighter.Element.Y - 20) < 0 == false)
                        {
                            _fighter.Element.Y -= 20;
                        }
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Q))
                    {
                        if (_fighter.Element.X + _fighter.Element.Width < 0)
                        {
                            _fighter.Element.X += _chunk.Ypixels;
                        }
                        _fighter.Element.X -= _fighter.Speed;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        if (_fighter.Element.X > _chunk.Ypixels)
                        {
                            _fighter.Element.X -= _chunk.Ypixels;
                        }
                        _fighter.Element.X += _fighter.Speed;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        if (!((_fighter.Element.Y + _fighter.Element.Height + _fighter.Speed) > _chunk.Xpixels))
                        {
                            _fighter.Element.Y += _fighter.Speed;
                        }
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        Player p = new Player(_fighter.Speed, _fighter.Jump, _fighter.Element.X, _fighter.Element.Y, _fighter.Health, _nFrameUpdated, _lossLife, _score.scoreNumber, _idBullet, _boostGeneration, _boostSpeedBullets);
                        p.Save();
                    }

                    if (!((_fighter.Element.Y + _fighter.Element.Height + _chunk.Gravity) > _chunk.Xpixels))
                    {
                        _fighter.Element.Y += _chunk.Gravity;
                    }
                }
                base.Update(gameTime);
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            switch (_gameState)
            {
                case GameState.MainMenu:
                    bool saveIsExisting = Player.SaveIsUp();
                    if (saveIsExisting)
                    {
                        _menu.Add(new Button(new Rectangle(490, 230, 220, 60), 490, 230, GameState.Reload, Color.Gold, "Reload", GraphicsDevice));
                    }

                    MouseState mouse = Mouse.GetState();
                    var mousePosition = new Point(mouse.X, mouse.Y);

                    _spriteBatch.Begin();
                    _spriteBatch.Draw(_chunk.Texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    _spriteBatch.DrawString(_fontLittle, "MoonFighter", new Vector2(400, 40), Color.Gold);
                    _menu.ForEach(delegate (Button button)
                    {
                        _spriteBatch.Draw(button.Texture2D, new Vector2(button.PositionX, button.PositionY),
                            button.Rectangle, Color.BlanchedAlmond);
                        _spriteBatch.DrawString(Content.Load<SpriteFont>("File"), button.Text, new Vector2(button.PositionX, button.PositionY), Color.Black);

                        if (button.Rectangle.Contains(mousePosition))
                        {
                            if (mouse.LeftButton == ButtonState.Pressed)
                            {
                                _gameState = button.GameState;
                            }
                        }
                    });

                    _spriteBatch.End();
                    break;
                case GameState.Reload:
                    onEnterGame = false;
                    onEnterSave = true;
                    _gameState = GameState.Game;
                    break;
                case GameState.Game:
                    if (onEnterGame)
                    {
                        _fighter.Speed = 8;
                        _fighter.Jump = 12;
                        _fighter.Element = new Rectangle(_chunk.Ypixels / 2, _chunk.Xpixels / 2, 125, 75);
                        _fighter.Health = 100;
                        _nFrameUpdated = 0;
                        _lossLife = 0;
                        _idBullet = 0;
                        _boostGeneration = 0;
                        _boostSpeedBullets = 0;
                        onEnterGame = false;
                        _instancesBullet.Clear();
                        _elapsedTime = 0;
                    }

                    if (onEnterSave)
                    {
                        Player p = Player.GetFromSave();
                        _fighter.Speed = p.FighterSpeed;
                        _fighter.Jump = p.FighterJump;
                        _fighter.Element = new Rectangle(p.FighterXposition, p.FighterYposition, 125, 75);
                        _fighter.Health = p.FighterHealth;
                        _nFrameUpdated = p.FramedUpdated;
                        _lossLife = p.LossLife;
                        _idBullet = p.IdBullet;
                        _boostGeneration = p.BoostGeneration;
                        _boostSpeedBullets = p.BoostSpeedOnBullets;
                        onEnterSave = false;
                        _instancesBullet.Clear();
                        _elapsedTime = 0;
                        _idBullet = 0;
                    }

                    _spriteBatch.Begin();
                    _spriteBatch.Draw(_chunk.Texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    _spriteBatch.Draw(_fighter.Texture, _fighter.Element, Color.White);

                    _instancesBullet.ForEach(delegate (Bullet instance)
                    {
                        _spriteBatch.Draw(instance.Texture, instance.Element, Color.White);
                        if (instance.Element.Intersects(_fighter.Element))
                        {
                            _lossLife++;
                        }

                        if (instance.Element.Y > _chunk.Xpixels)
                        {
                            instance.Element.Y -= _chunk.Ypixels * 5;
                        }

                        if (instance.Element.X > _chunk.Ypixels)
                        {
                            instance.Element.X -= _chunk.Ypixels * 5;
                        }
                    });

                    _score = new Score(GraphicsDevice, _lossLife, _nFrameUpdated);

                    if (_score.PercentScoreLeft <= 0)
                    {
                        _scores.Add(_score.scoreNumber);
                        _gameState = GameState.GameOver;
                    }
                    _spriteBatch.Draw(_score.TextureLossLife, _score.ElementLossLife, _score.Color);
                    _spriteBatch.Draw(_score.TextureScore, _score.ElementScore, Color.White * 0.9f);
                    _spriteBatch.DrawString(Content.Load<SpriteFont>("File"), _score.GetScore(), new Vector2(1100, 50), Color.Black);
                    _spriteBatch.End();
                    break;

                case GameState.GameOver:
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(_chunk.Texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Red);
                    _spriteBatch.Draw(_background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Red);


                    _elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (_elapsedTime >= 2000)
                    {
                        _elapsedTime = 0;
                        onEnterGame = true;
                        _gameState = GameState.MainMenu;
                    }

                    _spriteBatch.End();
                    break;
                case GameState.Score:
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(_chunk.Texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    _spriteBatch.Draw(_podium, new Rectangle(50, 420, 1000, 300), Color.White);


                    _scores.Sort();
                    _scores.Reverse();
                    _spriteBatch.DrawString(_font, _scores.Max().ToString(), new Vector2(460, 280), Color.Gold);
                    _spriteBatch.DrawString(_font, _scores[1].ToString(), new Vector2(160, 380), Color.Gold);
                    _spriteBatch.DrawString(_font, _scores[2].ToString(), new Vector2(830, 420), Color.Gold);

                    _spriteBatch.DrawString(_fontLittle, _scores[3].ToString(), new Vector2(1100, 20), Color.White);
                    _spriteBatch.DrawString(_fontLittle, _scores[4].ToString(), new Vector2(1100, 120), Color.White);
                    _spriteBatch.DrawString(_fontLittle, _scores[5].ToString(), new Vector2(1100, 220), Color.White);
                    _spriteBatch.DrawString(_fontLittle, _scores[6].ToString(), new Vector2(1100, 320), Color.White);
                    _spriteBatch.DrawString(_fontLittle, _scores[7].ToString(), new Vector2(1100, 420), Color.White);
                    _spriteBatch.DrawString(_fontLittle, _scores[8].ToString(), new Vector2(1100, 520), Color.White);
                    _spriteBatch.DrawString(_fontLittle, _scores[9].ToString(), new Vector2(1100, 620), Color.White);

                    if (Keyboard.GetState().IsKeyDown(Keys.M))
                    {
                        _gameState = GameState.MainMenu;
                    }

                    _spriteBatch.End();

                    break;
                case GameState.howPlay:
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(_chunk.Texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Gray);
                    _spriteBatch.Draw(_commands, new Rectangle(0, 0, 1200, 900), Color.White);
                    _spriteBatch.Draw(_commands2, new Rectangle(0, 440, 1200, 900), Color.White);


                    if (Keyboard.GetState().IsKeyDown(Keys.M))
                    {
                        _gameState = GameState.MainMenu;
                    }

                    _spriteBatch.End();

                    break;
                case GameState.Story:
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(_chunk.Texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Gray);
                    _spriteBatch.DrawString(_fontLittle, "MoonFighter", new Vector2(400, 40), Color.Gold);
                    _spriteBatch.Draw(_story, new Rectangle(100, 80, 1000, 700), Color.White);

                    if (Keyboard.GetState().IsKeyDown(Keys.M))
                    {
                        _gameState = GameState.MainMenu;
                    }

                    _spriteBatch.End();

                    break;
                default:
                    break;
            }

            base.Draw(gameTime);
        }
    }
}