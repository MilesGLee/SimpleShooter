using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace SimpleShooter
{
    class Player : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private float _shootTimer = 0.5f;
        private float _shootTime = 0;
        private bool _canShoot = true;

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

        public Player(float x, float y, float speed, string name = "Player", string path = "")
            : base(x, y, speed, name, path)
        {
            _speed = speed;

        }



        public override void Update(float deltaTime)
        {
            if (_shootTime > _shootTimer)
            {
                _shootTime = 0;
                _canShoot = true;
            }
            //get the player input direction
            int xDiretion = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDiretion = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            int bulletDirectionX = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT));
            int bulletDirectionY = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN));

            //this gives the player the ability to shoot in 8 directions.
            if (_canShoot && (bulletDirectionX != 0 || bulletDirectionY != 0)) 
            {
                Bullet pellet = new Bullet(Position.X, Position.Y, 500, new Vector2 { X = bulletDirectionX, Y = bulletDirectionY}, "Bullet", "bullet.png"); //Spawns the bullet with the proper direction to move in.
                pellet.SetScale(25, 25);
                CircleCollider bulletCollider = new CircleCollider(10, pellet);
                pellet.Collider = bulletCollider;
                Engine._currentScene.AddActor(pellet);
                _canShoot = false;
            }

            //Create a vector tht stores the move input
            Vector2 moveDirection = new Vector2(xDiretion, yDiretion);
            Vector2 bulletDiretion = new Vector2(bulletDirectionX, bulletDirectionY);

            //caculates the veclocity 
            Velocity = moveDirection.Normalized * Speed * deltaTime;

            if (Velocity.Magnitude > 0)
                Forward = Velocity.Normalized;
            if (bulletDiretion.Magnitude > 0)
                Forward = bulletDiretion.Normalized;

            //moves the player
            base.Translate(Velocity.X, Velocity.Y);

            if (!_canShoot) //A cooldown for shooting.
                _shootTime += deltaTime;

            base.Update(deltaTime);


        }
    }
}
