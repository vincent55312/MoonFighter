using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MoonFighter
{
    public class Fighter
    {
        public Texture2D Texture { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public int Jump { get; set; }

        public Rectangle Element;

        public Fighter(int health, int speed, int jump, Rectangle element, Texture2D texture)
        {
            this.Health = health;
            this.Texture = texture;
            this.Speed = speed;
            this.Jump = jump;
            this.Element = element;
        }
    }
}