using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Random rnd;

    private Map map;
    private Fighter fighter;
    private Dictionary<int, Bullet> instancesBullet = new Dictionary<int, Bullet>();
    private int idBullet = 0;
    private int lossLife = 0;
    private int boostedSpeedBullets = 0;
    private int boostedGeneration = 0;
    private int numberUpdated = 0;

    //Choix de l'état lors du démarrage de l'application
    private GameState _gameState = GameState.Game;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        map = new Map(1200, 720, 1, Content.Load<Texture2D>("background"));
        fighter = new Fighter(8, 12, new Rectangle(10, 720 - 150, 125, 75), Content.Load<Texture2D>("fighter"));

        _graphics.PreferredBackBufferWidth = map.yPixel;
        _graphics.PreferredBackBufferHeight = map.xPixel;
        _graphics.ApplyChanges();
        base.Initialize();
    }   

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        numberUpdated++;
        if (numberUpdated % 300 == 0)
        {
            boostedSpeedBullets++;
        }
        if (numberUpdated % 120 == 0)
        {
            boostedGeneration++;
        }
        if (numberUpdated % 3000 == 0)
        {
            fighter.speed++;
        }


        rnd = new Random();
        if (rnd.Next(0, 1000) < 10 + boostedGeneration)
        {
            instancesBullet.Add(idBullet, new Bullet(1 + boostedSpeedBullets, map, new SizeElement(30, 80), Content.Load<Texture2D>("meteor"), true));
            idBullet++;
        }

        if (rnd.Next(0, 1000) < 5 + boostedGeneration)
        {
            instancesBullet.Add(idBullet, new Bullet(1 + boostedSpeedBullets*2, map, new SizeElement(30, 80), Content.Load<Texture2D>("rocket")));
            idBullet++;
        }

        if (rnd.Next(0, 1000) < 2 + boostedGeneration)
        {
            instancesBullet.Add(idBullet, new Bullet(1 + boostedSpeedBullets*3, map, new SizeElement(120, 100), Content.Load<Texture2D>("alien")));
            idBullet++;
        }


        foreach (Bullet instance in instancesBullet.Values)
        {
            instance.element.Y += instance.speed;
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

            base.Update(gameTime);
        }
    }


    protected override void Draw(GameTime gameTime)
    {
        switch (_gameState)
        {
            case GameState.MainMenu:

                _spriteBatch.Begin();
                _spriteBatch.Draw(map.texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                
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
                        instancesBulletExpired.Add(instance.Key);
                    }
                }

                Score score = new Score(GraphicsDevice, lossLife, idBullet);

                if (score.percentScoreLeft <= 0)
                {
                    _gameState = GameState.MainMenu;
                }


                _spriteBatch.Draw(score.textureLossLife, score.elementLossLife, score.color);
                _spriteBatch.Draw(score.textureScore, score.elementScore, Color.White * 0.9f);

                foreach (int instanceExpired in instancesBulletExpired)
                {
                    instancesBullet.Remove(instanceExpired);
                }

                _spriteBatch.End();
                break;

            default:

                break;
        }

        
        base.Draw(gameTime);
    }
}

