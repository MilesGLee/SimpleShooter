﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShooter
{

    enum ColliderType 
    {
        CIRCLE,
        AABB
    }

    class Collider
    {
        private Actor _owner;
        private ColliderType _colliderType;

        public Actor Owner 
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public ColliderType ColliderType 
        {
            get { return _colliderType; }
        }

        public Collider(Actor owner, ColliderType colliderType) 
        {
            _owner = owner;
            _colliderType = colliderType;
        }

        //The bool that is checking the collision of circles and AABB boxes alike.s
        public bool CheckCollision(Actor other) 
        {
            if (other.Collider.ColliderType == ColliderType.CIRCLE) 
                return CheckCollisionCircle((CircleCollider)other.Collider);
            if (other.Collider.ColliderType == ColliderType.AABB)
                return CheckCollisionAABB((AABBCollider)other.Collider);
            return false;
        }

        public virtual bool CheckCollisionCircle(CircleCollider other) 
        {
            return false;
        }

        public virtual bool CheckCollisionAABB(AABBCollider other)
        {
            return false;
        }
    }
}
