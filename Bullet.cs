using System;
using System.Collections.Generic;
using System.Text;
using MathLibaray;
using Raylib_cs;

namespace SimpleShooter
{
    class Bullet : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Vector2 _direction = new Vector2 { X = 1, Y = 0}; 

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }


        public Bullet(char icon, float x, float y, float speed, Color color, Vector2 direction, string name = "Bullet")
            : base(icon, x, y, speed, color, name)
        {
            _speed = speed;
            _direction = direction;
        }

        public override void Update(float deltaTime)
        {

            //Create a vector tht stores the move input
            Vector2 moveDirection = new Vector2();
            moveDirection = _direction;

            //caculates the veclocity 
            Velocity = moveDirection.Normalized * Speed * deltaTime;

            base.Update(deltaTime);
            Postion += Velocity;


            if (Postion.X > 800) //Destroy the bullet if it is off screen.
                Engine._currentScene.RemoveActor(this);
            if (Postion.X < 0)
                Engine._currentScene.RemoveActor(this);
            if (Postion.Y > 450)
                Engine._currentScene.RemoveActor(this);
            if (Postion.Y < 0)
                Engine._currentScene.RemoveActor(this);

            
        }

        public override void OnCollision(Actor actor)
        {
            if (actor is Enemy)
            {
                Engine._currentScene.RemoveActor(actor);
                Engine._currentScene.RemoveActor(this);
            }
        }

        public override void Draw()
        {
            CircleCollider myCol = (CircleCollider)Collider;
            Raylib.DrawCircleLines((int)Postion.X, (int)Postion.Y, myCol.CollisionRadius, Color.YELLOW);
        }
    }
}
