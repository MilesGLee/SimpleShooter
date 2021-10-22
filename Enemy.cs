using System;
using System.Collections.Generic;
using System.Text;
using MathLibaray;
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


        public Enemy(char icon, float x, float y, float speed, Player player, Color color, float radi, string name = "Enemy")
            : base(icon, x, y, speed, color, radi,name)
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
                moveDirection = _player.Postion - Postion;

            //caculates the veclocity 
            Velocity = moveDirection.Normalized * Speed * deltaTime;

            base.Update(deltaTime);
            if(GetTargetInSight())
                Postion += Velocity;
        }

        public bool GetTargetInSight()
        {
            Vector2 directionOfTarget = (_player.Postion - Postion).Normalized;
            return Vector2.DotProduct(directionOfTarget, Forward) > 0;
        }

        public override void Draw()
        {
            Raylib.DrawCircleLines((int)Postion.X, (int)Postion.Y, CollisionRadius, Color.RED);
        }
    }
}