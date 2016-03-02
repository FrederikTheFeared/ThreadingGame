using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleGame
{
    class Bank : GameObject
    {
        private Vector2D startPosition;
        public Bank(string imagePath, Vector2D startPosition) : base(imagePath, startPosition)
        {
            this.startPosition = startPosition;
        }
        public override void OnCollision(GameObject other)
        {
            
        }
        public override void Update(float currentFPS)
        {
            base.Update(currentFPS);
        }
        public override void Draw(Graphics dc)
        {
            dc.TranslateTransform(startPosition.X, startPosition.Y);
            dc.DrawImage(sprite, 0-sprite.Width/2, 0-sprite.Height/2, sprite.Width, sprite.Height);
            dc.DrawRectangle(new Pen(Brushes.Red), 0 - sprite.Width / 2, 0 - sprite.Height / 2, CollisionBox.Width, CollisionBox.Height);
            dc.ResetTransform();
        }
    }
}
