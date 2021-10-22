using System;
using System.Collections.Generic;
using System.Text;
using MathLibaray;
using Raylib_cs;

namespace SimpleShooter
{
    class Player : Actor
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

        public Player(char icon, float x, float y, float speed, Color color, string name = "Player")
            : base(icon, x, y, speed, color, name)
        {
            _speed = speed;

        }



        public override void Update(float deltaTime)
        {
            //get the player input direction
            int xDiretion = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDiretion = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                SetIcon('>');
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                SetIcon('<');
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
                SetIcon('^');
            if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                SetIcon('V');
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) 
            {
                Bullet pellet = new Bullet('-', Postion.X, Postion.Y, 500, Color.RED, "Bullet");
                Engine._currentScene.AddActor(pellet);
            }

            //Create a vector tht stores the move input
            Vector2 moveDirection = new Vector2(xDiretion, yDiretion);

            //caculates the veclocity 
            Velocity = moveDirection.Normalized * Speed * deltaTime;

            base.Update(deltaTime);
            //moves the player
            Postion += Velocity;

        }

        public override void OnCollision(Actor actor)
        {

        }
    }
}
