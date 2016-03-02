using System.Collections.Generic;
using System.Drawing;

namespace IdleGame
{
    abstract class GameObject
    {
        public Image sprite;
        protected Vector2D position;
        protected List<Image> animationFrames = new List<Image>();
        protected float currentFrameIndex;
        private RectangleF collisionBox;
        public RectangleF CollisionBox
        {
            get
            {
                return new RectangleF(position.X, position.Y, sprite.Width, sprite.Height);
            }
        }

        public Vector2D Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public GameObject(string imagePath, Vector2D startPosition)
        {
            string[] imagePaths = imagePath.Split(';');
            this.animationFrames = new List<Image>();

            foreach (string path in imagePaths)
            {
                animationFrames.Add(Image.FromFile(path));
            }
            this.sprite = this.animationFrames[0];
            this.position = startPosition;
        }

        public abstract void StartThread();

        public virtual void Update(float currentFPS)
        {
            CheckCollosion();
        }

        public virtual void Draw(Graphics dc)
        {
            dc.DrawImage(sprite, position.X, position.Y, sprite.Width, sprite.Height);
            dc.DrawRectangle(new Pen(Brushes.Red), CollisionBox.X, CollisionBox.Y, CollisionBox.Width, CollisionBox.Height);
        }

        public bool IsCollidingWith(GameObject other)
        {
            if ((CollisionBox.IntersectsWith(other.CollisionBox)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CheckCollosion()
        {
            foreach (GameObject go in GameWorld.Objs)
            {
                if (go != this)
                {
                    if (this.IsCollidingWith(go))
                    {
                        OnCollision(go);
                    }
                }
            }
        }

        public abstract void OnCollision(GameObject other);
    }
}