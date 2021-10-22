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


        public Bullet(char icon, float x, float y, float speed, Color color, string name = "Bullet")
            : base(icon, x, y, speed, color, name)
        {
            //i need to the player = palyer I need to get the this.
            _speed = speed;

        }

        public override void Update(float deltaTime)
        {

            //Create a vector tht stores the move input
            Vector2 moveDirection = new Vector2();
            moveDirection = new Vector2 { X = 1, Y = 0};

            //caculates the veclocity 
            Velocity = moveDirection.Normalized * Speed * deltaTime;

            base.Update(deltaTime);
            Postion += Velocity;

            Actor me = this;

            if (Postion.X > 800)
                Engine._currentScene.RemoveActor(me);
        }
    }
}
