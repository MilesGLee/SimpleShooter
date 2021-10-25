using System;
using System.Collections.Generic;
using System.Text;
using MathLibaray;

namespace SimpleShooter
{
    class AABBCollider : Collider
    {
        private float _width;
        private float _height;

        public AABBCollider(float width, float height, Actor owner) : base(owner, ColliderType.AABB)
        {
            _width = width;
            _height = height;
        }

        //Checking the collisisons for boxes. :)
        public override bool CheckCollisionAABB(AABBCollider other) 
        {
            //If the object is overlapping itself, return false.
            if (other.Owner == Owner)
                return false;
            //Return true if there is an overlap between boxes.
            if (other.Left <= Right &&
                other.Top <= Bottom &&
                Left <= other.Right &&
                Top <= other.Bottom)
                return true;
            return false;
        }

        public override bool CheckCollisionCircle(CircleCollider other)
        {
            return other.CheckCollisionAABB(this);
        }

        //Width of the box collision
        public float Width 
        {
            get { return _width; }
            set { _width = value; }
        }

        //Height of the box collision
        public float Height 
        {
            get { return _height; }
            set { _height = value; }
        }

        //The left face of the box
        public float Left 
        {
            get 
            {
                return Owner.Postion.X - (Width / 2);
            }
        }

        //The right face of the box
        public float Right 
        {
            get 
            {
                return Owner.Postion.X + (Width / 2);
            }
        }

        //The top face of the box
        public float Top 
        {
            get 
            {
                return Owner.Postion.Y - (Height / 2);
            }
        }

        //The bottom face of the box
        public float Bottom 
        {
            get 
            {
                return Owner.Postion.Y + (Height / 2);
            }
        }
    }
}
