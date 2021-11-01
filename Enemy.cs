using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace SimpleShooter
{
    class Enemy : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Player _player;


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


        public Enemy(float x, float y, float speed, Player player, string name = "Enemy", string path = "")
            : base(x, y, speed, name, path)
        {
            //i need to the player = palyer I need to get the this.
            _speed = speed;
            _player = player;

        }

        public override void Update(float deltaTime)
        {

            //Create a vector tht stores the move input
            Vector2 moveDirection = new Vector2();
            if(_player != null)
                moveDirection = _player.Position - Position;

            //caculates the veclocity 
            Velocity = moveDirection.Normalized * Speed * deltaTime;
            if (GetTargetInSight())
                base.Translate(Velocity.X, Velocity.Y);
            else
                base.Translate(Velocity.X / 2, Velocity.Y / 2);
            LookAt(_player.Position);
            base.Update(deltaTime);
            
        }

        public bool GetTargetInSight()
        {
            Vector2 directionOfTarget = (_player.Position - Position).Normalized;
            return Vector2.DotProduct(directionOfTarget, Forward) > 0;
        }

        public override void OnCollision(Actor actor)
        {
            if (actor is Player) //If an enemy hits the player, end the game.
            {
                Engine.CloseApplication();
            }
        }
    }
}