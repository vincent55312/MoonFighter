using System.IO;
using System.Text.Json;

public class Player: SerializableElement
{
    public int fighterSpeed { get; set; }
    public int fighterJump { get; set; }

    public int fighterXposition { get; set; }
    public int fighterYposition { get; set; }
    public int fighterHealth { get; set; }
    public int nFramedUpdated { get; set; }
    public int lossLife { get; set; }
    public int score { get; set; }
    public int idBullet { get; set; }
    public int boostGeneration { get; set; }
    public int boostSpeedOnBullets { get; set; }


    public Player(int fighterSpeed, int fighterJump, int fighterXposition, int fighterYposition, int fighterHealth, int nFramedUpdated, int lossLife, int score, int idBullet, int boostGeneration, int boostSpeedOnBullets)
    {
        this.fighterSpeed = fighterSpeed;
        this.fighterJump = fighterJump;
        this.fighterXposition = fighterXposition;
        this.fighterYposition = fighterYposition;
        this.fighterHealth = fighterHealth;
        this.nFramedUpdated = nFramedUpdated;
        this.lossLife = lossLife;
        this.score = score;
        this.idBullet = idBullet;
        this.boostGeneration = boostGeneration;
        this.boostSpeedOnBullets = boostSpeedOnBullets;
    }

    public Player(){}
    
    public void save()
    {
        File.WriteAllText(@"./save.json", JsonSerializer.Serialize(this));
    }

    public object getFromSave()
    {
        string json = File.ReadAllText(@"./save.json");
        return JsonSerializer.Deserialize<Player>(json);
    }
}
