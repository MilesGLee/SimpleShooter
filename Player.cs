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

        public Player(char icon, float x, float y, float speed, Color color, string name = "Player")
            : base(icon, x, y, speed, color, name)
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

            //This if statement repeats multiple times with small differences, this gives the player the ability to shoot in 8 directions.
            if (_canShoot && Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && Raylib.IsKeyUp(KeyboardKey.KEY_LEFT) && Raylib.IsKeyUp(KeyboardKey.KEY_UP) && Raylib.IsKeyUp(KeyboardKey.KEY_DOWN)) 
            {
                Bullet pellet = new Bullet(' ', Postion.X, Postion.Y, 500, Color.RED, new Vector2 { X = 1, Y = 0}, "Bullet"); //Spawns the bullet with the proper direction to move in.
                CircleCollider bulletCollider = new CircleCollider(10, pellet);
                pellet.Collider = bulletCollider;
                Engine._currentScene.AddActor(pellet);
                _canShoot = false;
            }
            if (_canShoot && Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && Raylib.IsKeyUp(KeyboardKey.KEY_RIGHT) && Raylib.IsKeyUp(KeyboardKey.KEY_UP) && Raylib.IsKeyUp(KeyboardKey.KEY_DOWN))
            {
                Bullet pellet = new Bullet(' ', Postion.X, Postion.Y, 500, Color.RED, new Vector2 { X = -1, Y = 0 }, "Bullet");
                CircleCollider bulletCollider = new CircleCollider(10, pellet);
                pellet.Collider = bulletCollider;
                Engine._currentScene.AddActor(pellet);
                _canShoot = false;
            }
            if (_canShoot && Raylib.IsKeyDown(KeyboardKey.KEY_UP) && Raylib.IsKeyUp(KeyboardKey.KEY_LEFT) && Raylib.IsKeyUp(KeyboardKey.KEY_RIGHT) && Raylib.IsKeyUp(KeyboardKey.KEY_DOWN))
            {
                Bullet pellet = new Bullet(' ', Postion.X, Postion.Y, 500, Color.RED, new Vector2 { X = 0, Y = -1 }, "Bullet");
                CircleCollider bulletCollider = new CircleCollider(10, pellet);
                pellet.Collider = bulletCollider;
                Engine._currentScene.AddActor(pellet);
                _canShoot = false;
            }
            if (_canShoot && Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) && Raylib.IsKeyUp(KeyboardKey.KEY_UP) && Raylib.IsKeyUp(KeyboardKey.KEY_LEFT) && Raylib.IsKeyUp(KeyboardKey.KEY_RIGHT))
            {
                Bullet pellet = new Bullet(' ', Postion.X, Postion.Y, 500, Color.RED, new Vector2 { X = 0, Y = 1 }, "Bullet");
                CircleCollider bulletCollider = new CircleCollider(10, pellet);
                pellet.Collider = bulletCollider;
                Engine._currentScene.AddActor(pellet);
                _canShoot = false;
            }
            if (_canShoot && Raylib.IsKeyDown(KeyboardKey.KEY_UP) && Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && Raylib.IsKeyUp(KeyboardKey.KEY_LEFT) && Raylib.IsKeyUp(KeyboardKey.KEY_DOWN))
            {
                Bullet pellet = new Bullet(' ', Postion.X, Postion.Y, 500, Color.RED, new Vector2 { X = 1, Y = -1 }, "Bullet");
                CircleCollider bulletCollider = new CircleCollider(10, pellet);
                pellet.Collider = bulletCollider;
                Engine._currentScene.AddActor(pellet);
                _canShoot = false;
            }
            if (_canShoot && Raylib.IsKeyDown(KeyboardKey.KEY_UP) && Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && Raylib.IsKeyUp(KeyboardKey.KEY_RIGHT) && Raylib.IsKeyUp(KeyboardKey.KEY_DOWN))
            {
                Bullet pellet = new Bullet(' ', Postion.X, Postion.Y, 500, Color.RED, new Vector2 { X = -1, Y = -1 }, "Bullet");
                CircleCollider bulletCollider = new CircleCollider(10, pellet);
                pellet.Collider = bulletCollider;
                Engine._currentScene.AddActor(pellet);
                _canShoot = false;
            }
            if (_canShoot && Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) && Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && Raylib.IsKeyUp(KeyboardKey.KEY_LEFT) && Raylib.IsKeyUp(KeyboardKey.KEY_UP))
            {
                Bullet pellet = new Bullet(' ', Postion.X, Postion.Y, 500, Color.RED, new Vector2 { X = 1, Y = 1 }, "Bullet");
                CircleCollider bulletCollider = new CircleCollider(10, pellet);
                pellet.Collider = bulletCollider;
                Engine._currentScene.AddActor(pellet);
                _canShoot = false;
            }
            if (_canShoot && Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) && Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && Raylib.IsKeyUp(KeyboardKey.KEY_RIGHT) && Raylib.IsKeyUp(KeyboardKey.KEY_UP))
            {
                Bullet pellet = new Bullet(' ', Postion.X, Postion.Y, 500, Color.RED, new Vector2 { X = -1, Y = 1 }, "Bullet");
                CircleCollider bulletCollider = new CircleCollider(10, pellet);
                pellet.Collider = bulletCollider;
                Engine._currentScene.AddActor(pellet);
                _canShoot = false;
            }

            //Create a vector tht stores the move input
            Vector2 moveDirection = new Vector2(xDiretion, yDiretion);

            //caculates the veclocity 
            Velocity = moveDirection.Normalized * Speed * deltaTime;

            base.Update(deltaTime);
            //moves the player
            Postion += Velocity;
            if (!_canShoot) //A cooldown for shooting.
                _shootTime += deltaTime;
        }

        public override void Draw() //I like the circles for all my actors, they look nice.
        {
            CircleCollider myCol = (CircleCollider)Collider;
            Raylib.DrawCircleLines((int)Postion.X, (int)Postion.Y, myCol.CollisionRadius, Color.GREEN);
        }
    }
}
