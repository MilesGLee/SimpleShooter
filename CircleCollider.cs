using System;
using System.Collections.Generic;
using System.Text;
using MathLibaray;

namespace SimpleShooter
{
    class CircleCollider : Collider
    {
        private float _collisionRadius;

        public float CollisionRadius 
        {
            get { return _collisionRadius; }
            set { _collisionRadius = value; }
        }
        public CircleCollider(float collisionRadius, Actor owner) : base(owner, ColliderType.CIRCLE) 
        {
            _collisionRadius = collisionRadius;
        }

        //Checking for a collision between circles.
        public override bool CheckCollisionCircle(CircleCollider other)
        {
            if (other.Owner == Owner) //If the colliding circle is not the owner of this circle.
                return false;
            float distance = Vector2.Distance(other.Owner.Postion, Owner.Postion); //Distance between both circles
            float combinedRadii = other.CollisionRadius + CollisionRadius; //The combined radii of the circles

            return distance <= combinedRadii; //if the distance is less than the combined radii, a collision occured.
        }
    }
}
