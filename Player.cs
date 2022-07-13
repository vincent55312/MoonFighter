using System;
using System.IO;
using System.Text.Json;

namespace MoonFighter
{
    public class Player : ISerializableElement
    {
        public int FighterSpeed { get; set; }
        public int FighterJump { get; set; }
        public int FighterXposition { get; set; }
        public int FighterYposition { get; set; }
        public int FighterHealth { get; set; }
        public int FramedUpdated { get; set; }
        public int LossLife { get; set; }
        public int Score { get; set; }
        public int IdBullet { get; set; }
        public int BoostGeneration { get; set; }
        public int BoostSpeedOnBullets { get; set; }


        public Player(int fighterSpeed, int fighterJump, int fighterXposition, int fighterYposition, int fighterHealth, int nFramedUpdated, int lossLife, int score, int idBullet, int boostGeneration, int boostSpeedOnBullets)
        {
            this.FighterSpeed = fighterSpeed;
            this.FighterJump = fighterJump;
            this.FighterXposition = fighterXposition;
            this.FighterYposition = fighterYposition;
            this.FighterHealth = fighterHealth;
            this.FramedUpdated = nFramedUpdated;
            this.LossLife = lossLife;
            this.Score = score;
            this.IdBullet = idBullet;
            this.BoostGeneration = boostGeneration;
            this.BoostSpeedOnBullets = boostSpeedOnBullets;
        }

        public Player() { }

        public string GetPath()
        {
            string fileName = "Save.json";
            string pathFolder = Path.Combine(Environment.CurrentDirectory, @"prod_data");
            Directory.CreateDirectory(pathFolder);

            return pathFolder + '/' + fileName;
        }

        public void Save()
        {
            File.WriteAllText(GetPath(), JsonSerializer.Serialize(this));
        }

        public static Player GetFromSave()
        {
            string fileName = "Save.json";
            string pathFolder = Path.Combine(Environment.CurrentDirectory, @"prod_data");
            string path = pathFolder + '/' + fileName;

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Player>(json);
        }

        public static bool SaveIsUp()
        {
            try
            {
                string fileName = "Save.json";
                string pathFolder = Path.Combine(Environment.CurrentDirectory, @"prod_data");
                string path = pathFolder + '/' + fileName;

                return File.Exists(path) ? true : false;
            }
            catch
            {
                return false;
            }
        }
    }
}