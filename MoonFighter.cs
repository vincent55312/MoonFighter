using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

public class MoonFighter : Game
{
    private SpriteFont font { get; set; }
    private SpriteFont fontLittle { get; set; }
    private GraphicsDeviceManager _graphics { get; set; }
    private SpriteBatch spriteBatch { get; set; }
    private Texture2D background { get; set; }
    private Texture2D podium { get; set; }
    private Texture2D commands { get; set; }
    private Texture2D commands2 { get; set; }
    private Texture2D story { get; set; }
    private Map map { get; set; }
    private Fighter fighter { get; set; }
    private List<Bullet> instancesBullet { get; set; } = new List<Bullet>();
    private List<Button> menu { get; set; } = new List<Button>();
    private List<int> scores { get; set; } = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int idBullet { get; set; } = 0;
    private int lossLife { get; set; } = 0;
    private int boostSpeedBullets { get; set; } = 0;
    private int boostGeneration { get; set; } = 0;
    private int nFrameUpdated { get; set; } = 0;
    private int maxEntityGeneration { get; set; } = 80;
    private double elapsedTime { get; set; } = 0f;
    private bool isEnterOnGame { get; set; } = true;
    private bool isEnterOnSave { get; set; } = false;
    private Score score { get; set; }
    private GameState _gameState { get; set; } = GameState.MainMenu;

    public MoonFighter()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        bool saveIsExisting = Player.saveIsExisting();
        if (saveIsExisting)
        {
            menu.Add(new Button(new Rectangle(490, 230, 220, 60), 490, 230, GameState.Reload, Color.Gold, "Reload", GraphicsDevice));
        }
        menu.Add(new Button(new Rectangle(490, 130, 220, 60), 490, 130, GameState.Game, Color.Gold, "Play", GraphicsDevice));
        menu.Add(new Button(new Rectangle(490, 330, 220, 60), 490, 330, GameState.Story, Color.Gold, "Story", GraphicsDevice));
        menu.Add(new Button(new Rectangle(490, 430, 220, 60), 490, 430, GameState.howPlay, Color.Gold, "How to Play", GraphicsDevice));
        menu.Add(new Button(new Rectangle(490, 530, 220, 60), 490, 530, GameState.Score, Color.Gold, "Score", GraphicsDevice));
        menu.Add(new Button(new Rectangle(490, 630, 220, 60), 490, 630, GameState.Quit, Color.Gold, "Quit", GraphicsDevice));

        map = new Map(1200, 720, 1, Content.Load<Texture2D>("background"));
        fighter = new Fighter(100, 8, 12, new Rectangle(map.yPixel / 2, map.xPixel / 2, 125, 75), Content.Load<Texture2D>("fighter"));
        background = Content.Load<Texture2D>("gameOver");
        podium = Content.Load<Texture2D>("podium");
        commands = Content.Load<Texture2D>("commands");
        commands2 = Content.Load<Texture2D>("commands2");
        story = Content.Load<Texture2D>("story");
        font = Content.Load<SpriteFont>("score");
        fontLittle = Content.Load<SpriteFont>("scoreLittle");
        _graphics.PreferredBackBufferWidth = map.yPixel;
        _graphics.PreferredBackBufferHeight = map.xPixel;
        _graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
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
                    instancesBullet.Add(new Bullet(1 + boostSpeedBullets, map, new SizeElement(30, 80), Content.Load<Texture2D>("meteor"), true));
                    idBullet++;
                }

                if (rnd.Next(0, 1000) < 5 + boostGeneration)
                {
                    instancesBullet.Add(new Bullet(1 + boostSpeedBullets * 2, map, new SizeElement(30, 80), Content.Load<Texture2D>("rocket")));
                    idBullet++;
                }

                if (rnd.Next(0, 1000) < 1 + boostGeneration)
                {
                    instancesBullet.Add(new Bullet(1 + boostSpeedBullets * 3, map, new SizeElement(120, 100), Content.Load<Texture2D>("alien")));
                    idBullet++;
                }

                if (rnd.Next(0, 1000) < 1 + boostGeneration)
                {
                    instancesBullet.Add(new Bullet(2 + boostSpeedBullets * 2, map, new SizeElement(300, 225), Content.Load<Texture2D>("doge"), false, false));
                    idBullet++;
                }

                if (rnd.Next(0, 1500) < 1 + boostGeneration)
                {
                    instancesBullet.Add(new Bullet(2 + boostSpeedBullets * 7, map, new SizeElement(400, 225), Content.Load<Texture2D>("tesla"), false, false));
                    idBullet++;
                }
            }


            foreach (Bullet instance in instancesBullet)
            {
                if (instance.mouvementIsVertial)
                {
                    instance.element.Y += instance.speed;
                }
                else
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
                else if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    Player p = new Player(fighter.speed, fighter.jump, fighter.element.X, fighter.element.Y, fighter.health, nFrameUpdated, lossLife, score.score, idBullet, boostGeneration, boostSpeedBullets);
                    p.save();
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
        switch (_gameState)
        {
            case GameState.MainMenu:
                MouseState mouse = Mouse.GetState();
                var mousePosition = new Point(mouse.X, mouse.Y);

                spriteBatch.Begin();
                spriteBatch.Draw(map.texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                spriteBatch.DrawString(fontLittle, "MoonFighter", new Vector2(400, 40), Color.Gold);
                menu.ForEach(delegate (Button button)
                {
                    spriteBatch.Draw(button.texture2D, new Vector2(button.positionX, button.positionY),
                        button.rectangle, Color.BlanchedAlmond);
                    spriteBatch.DrawString(Content.Load<SpriteFont>("File"), button.text, new Vector2(button.positionX, button.positionY), Color.Black);

                    if (button.rectangle.Contains(mousePosition))
                    {
                        if (mouse.LeftButton == ButtonState.Pressed)
                        {
                            _gameState = button.gameState;
                        }
                    }
                });

                spriteBatch.End();
                break;
            case GameState.Reload:
                isEnterOnGame = false;
                isEnterOnSave = true;
                _gameState = GameState.Game;
                break;
            case GameState.Game:
                if (isEnterOnGame)
                {
                    fighter.speed = 8;
                    fighter.jump = 12;
                    fighter.element = new Rectangle(map.yPixel / 2, map.xPixel / 2, 125, 75);
                    fighter.health = 100;
                    nFrameUpdated = 0;
                    lossLife = 0;
                    idBullet = 0;
                    boostGeneration = 0;
                    boostSpeedBullets = 0;
                    isEnterOnGame = false;
                    instancesBullet.Clear();
                    elapsedTime = 0;
                }

                if (isEnterOnSave)
                {
                    Player p = Player.getFromSave();
                    fighter.speed = p.fighterSpeed;
                    fighter.jump = p.fighterJump;
                    fighter.element = new Rectangle(p.fighterXposition, p.fighterYposition, 125, 75);
                    fighter.health = p.fighterHealth;
                    nFrameUpdated = p.nFramedUpdated;
                    lossLife = p.lossLife;
                    idBullet = p.idBullet;
                    boostGeneration = p.boostGeneration;
                    boostSpeedBullets = p.boostSpeedOnBullets;
                    isEnterOnSave = false;
                    instancesBullet.Clear();
                    elapsedTime = 0;
                }

                spriteBatch.Begin();
                spriteBatch.Draw(map.texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                spriteBatch.Draw(fighter.texture, fighter.element, Color.White);

                instancesBullet.ForEach(delegate (Bullet instance)
                {
                    spriteBatch.Draw(instance.texture, instance.element, Color.White);
                    if (instance.element.Intersects(fighter.element))
                    {
                        lossLife++;
                    }

                    if (instance.element.Y > map.xPixel)
                    {
                        instance.element.Y -= map.yPixel * 5;
                    }

                    if (instance.element.X > map.yPixel)
                    {
                        instance.element.X -= map.yPixel * 5;
                    }
                });

                score = new Score(GraphicsDevice, lossLife, nFrameUpdated);

                if (score.percentScoreLeft <= 0)
                {
                    scores.Add(score.score);
                    _gameState = GameState.GameOver;
                }
                spriteBatch.Draw(score.textureLossLife, score.elementLossLife, score.color);
                spriteBatch.Draw(score.textureScore, score.elementScore, Color.White * 0.9f);
                spriteBatch.DrawString(Content.Load<SpriteFont>("File"), score.getScore(), new Vector2(1100, 50), Color.Black);
                spriteBatch.End();
                break;

            case GameState.GameOver:
                spriteBatch.Begin();
                spriteBatch.Draw(map.texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Red);
                spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Red);


                elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

                if (elapsedTime >= 2000)
                {
                    elapsedTime = 0;
                    isEnterOnGame = true;
                    _gameState = GameState.MainMenu;
                }

                spriteBatch.End();
                break;
            case GameState.Score:
                spriteBatch.Begin();
                spriteBatch.Draw(map.texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                spriteBatch.Draw(podium, new Rectangle(50, 420, 1000, 300), Color.White);


                scores.Sort();
                scores.Reverse();
                spriteBatch.DrawString(font, scores.Max().ToString(), new Vector2(530, 280), Color.Gold);
                spriteBatch.DrawString(font, scores[1].ToString(), new Vector2(180, 380), Color.Gold);
                spriteBatch.DrawString(font, scores[2].ToString(), new Vector2(850, 420), Color.Gold);

                spriteBatch.DrawString(fontLittle, scores[3].ToString(), new Vector2(1100, 20), Color.White);
                spriteBatch.DrawString(fontLittle, scores[4].ToString(), new Vector2(1100, 120), Color.White);
                spriteBatch.DrawString(fontLittle, scores[5].ToString(), new Vector2(1100, 220), Color.White);
                spriteBatch.DrawString(fontLittle, scores[6].ToString(), new Vector2(1100, 320), Color.White);
                spriteBatch.DrawString(fontLittle, scores[7].ToString(), new Vector2(1100, 420), Color.White);
                spriteBatch.DrawString(fontLittle, scores[8].ToString(), new Vector2(1100, 520), Color.White);
                spriteBatch.DrawString(fontLittle, scores[9].ToString(), new Vector2(1100, 620), Color.White);

                if (Keyboard.GetState().IsKeyDown(Keys.M))
                {
                    _gameState = GameState.MainMenu;
                }

                spriteBatch.End();

                break;
            case GameState.howPlay:
                spriteBatch.Begin();
                spriteBatch.Draw(map.texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Gray);
                spriteBatch.Draw(commands, new Rectangle(0, 0, 1200, 900), Color.White);
                spriteBatch.Draw(commands2, new Rectangle(0, 440, 1200, 900), Color.White);


                if (Keyboard.GetState().IsKeyDown(Keys.M))
                {
                    _gameState = GameState.MainMenu;
                }

                spriteBatch.End();

                break;
            case GameState.Story:
                spriteBatch.Begin();
                spriteBatch.Draw(map.texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Gray);
                spriteBatch.DrawString(fontLittle, "MoonFighter", new Vector2(400, 40), Color.Gold);
                spriteBatch.Draw(story, new Rectangle(100, 80, 1000, 700), Color.White);

                if (Keyboard.GetState().IsKeyDown(Keys.M))
                {
                    _gameState = GameState.MainMenu;
                }

                spriteBatch.End();

                break;
            default:
                break;
        }

        base.Draw(gameTime);
    }
}

