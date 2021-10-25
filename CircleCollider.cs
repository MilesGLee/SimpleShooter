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

        public override bool CheckCollisionAABB(AABBCollider other)
        {
            //return false if colliding with itself
            if (other.Owner == Owner)
                return false;

            //Get and clamp the direction from the collider to the aabb
            Vector2 direction = Owner.Postion - other.Owner.Postion;
            direction.X = Math.Clamp(direction.X, -other.Width / 2, other.Width / 2);
            direction.Y = Math.Clamp(direction.Y, -other.Height / 2, other.Height / 2);

            //find the closest point between the AABB and the collider
            Vector2 closestPoint = other.Owner.Postion + direction;
            float distanceFromClosestPoint = Vector2.Distance(Owner.Postion, closestPoint);

            //Return true if the colliders are colliding.
            return distanceFromClosestPoint <= CollisionRadius;
        }
    }
}
