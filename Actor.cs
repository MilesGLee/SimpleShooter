using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace SimpleShooter
{
    class Actor
    {
        private string _name;
        //private Vector2 _position;
        private bool _started;
        private float _speed;
        private Vector2 _forward = new Vector2(1, 0);
        private Collider _collider;
        private Matrix3 _localTransform = Matrix3.Identity;
        public Matrix3 _translation = Matrix3.Identity;
        public Matrix3 _rotation = Matrix3.Identity;
        public Matrix3 _scale = Matrix3.Identity;
        private Actor[] _children = new Actor[0];
        private Actor _parent;
        private Sprite _sprite;
        private int rot = 0;

        public bool Started
        {
            get { return _started; }
        }

        public string Name
        {
            get { return _name; }
        }

        public float Speed
        {
            get { return _speed; }
        }

        public Vector2 Position
        {
            get { return new Vector2(_localTransform.M02, _localTransform.M12); }
            set { _localTransform.M02 = value.X; _localTransform.M12 = value.Y; }
        }

        public Vector2 Forward
        {
            get { return new Vector2(_rotation.M00, _rotation.M10); }
            set
            {
                Vector2 point = value.Normalized + Position;
                LookAt(point);
            }
        }

        public Vector2 WorldPosition
        {
            get { return new Vector2(); }
            set { }
        }

        public Matrix3 GlobalTransform
        {
            get; 
            set;
        }

        public Matrix3 LocalTransform
        {
            get { return _localTransform; }
            set { _localTransform = value; }
        }

        public Actor Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public Actor[] Children
        {
            get { return _children; }
            set { }
        }

        public Sprite Sprite 
        {
            get { return _sprite; }
            set { _sprite = value; }
        }

        public Collider Collider 
        {
            get { return _collider; }
            set { _collider = value; }
        }

        public Actor()
        {

        }
        /// <summary>
        /// takes the Actor constructor and add the float x and y but takes out y
        /// </summary>
        /// <param name="x">is the replace the Vector2</param>
        /// <param name="y">is the replacement for the veoctor2</param>
        public Actor(float x, float y, float speed, string name = "Actor", string path = "") :
            this(new Vector2 { X = x, Y = y }, speed, name, path)
        { }


        /// <summary>
        /// Is a constructor for the actor that hold is definition.
        /// </summary>
        /// <param name="icon">The icon that all this information applies to</param>
        /// <param name="position">is the loctation that the icon is in</param>
        /// <param name="name">current Actor name</param>
        /// <param name="color">The color that the neame or icon will be</param>
        public Actor(Vector2 position, float speed, string name = "Actor", string path = "")
        {
            SetTranslate(position.X, position.Y);
            _name = name;

            if(path != "")
                _sprite = new Sprite(path);
        }

        public void UpdateTransforms() 
        {

        }

        public void AddChild(Actor child) 
        {
            Actor[] temArray = new Actor[_children.Length + 1];

            for (int i = 0; i < _children.Length; i++)
            {
                temArray[i] = _children[i];
            }

            temArray[_children.Length] = child;

            _children = temArray;
        }

        public bool RemoveChild(Actor child) 
        {
            bool actorRemoved = false;

            Actor[] temArray = new Actor[_children.Length - 1];

            int j = 0;

            for (int i = 0; i < _children.Length; i++)
            {
                if (_children[i] != child)
                {
                    temArray[j] = _children[i];
                    j++;
                }
                else
                    actorRemoved = true;

            }

            if (actorRemoved)
            {
                _children = temArray;
                child.Parent = null;
            }

            return actorRemoved;
        }

        public virtual void Start()
        {
            _started = true;
            if (Parent != null)
                Parent.AddChild(this);
        }

        public virtual void Update(float deltaTime)
        {
            _localTransform = _translation * _rotation * _scale;
            Console.WriteLine(_name + ":" + Position.X + ":" + Position.Y);
            for (int i = rot; rot < 360; rot++)
            {
                Rotate(rot);
            }
            rot = 0;
        }

        public virtual void Draw()
        {
            if (_name == "Point")
            {
                AABBCollider myCol = (AABBCollider)Collider;
                Raylib.DrawRectangle((int)Position.X, (int)Position.Y, (int)myCol.Width, (int)myCol.Height, Color.MAGENTA);
            }
            else
            {
                if (_sprite != null)
                    _sprite.Draw(_localTransform);
                CircleCollider myCol = (CircleCollider)Collider;
            }
            
        }

        public void End()
        {

        }

        /// <summary>
        /// Startes when the player hits a target.
        /// </summary>
        /// 
        public virtual void OnCollision(Actor actor) 
        {
            if (_name == "Point" && actor is Player) 
            {
                Engine._currentScene.RemoveActor(this);
                Engine.Score++;
            }
        }
        public virtual bool CheckForCollision(Actor actor)
        {
            if (Collider == null || actor.Collider == null)
                return false;
            return Collider.CheckCollision(actor);
        }

        //sets the actors position
        public void SetTranslate(float translationX, float translationY)
        {
            _translation = Matrix3.CreateTranslation(translationX, translationY);
        }

        public void Translate(float translationX, float translationY) 
        {
            _translation *= Matrix3.CreateTranslation(translationX, translationY);
        }

        //Sets the actors rotation
        public void SetRotation(float radians)
        {
            _rotation = Matrix3.CreateRotation(radians);
        }
        public void Rotate(float radians)
        {
            _rotation *= Matrix3.CreateRotation(radians);
        }

        //sets the actors scale
        public void SetScale(float x, float y) 
        {
            _scale = Matrix3.CreateScale(x, y);
        }
        public void Scale(float x, float y)
        {
            _scale *= Matrix3.CreateScale(x, y);
        }

        //Rotates the actor to face the given position
        public void LookAt(Vector2 position) 
        {
            //Find the direction the actor should look in
            Vector2 direction = (position - Position).Normalized;
            //Use the dot product to find the angle the actor needs to rotate
            float dotProduct = Vector2.DotProduct(direction, Forward);

            if (dotProduct > 1)
                dotProduct = 1;

            float angle = (float)Math.Acos(dotProduct);
            //Find a perpendicular vector to the direction
            Vector2 perpendicularDirection = new Vector2(direction.Y, -direction.X);
            //Find the dor product of the perpendicular vector and the current forward
            float perpendicularDotProduct = Vector2.DotProduct(perpendicularDirection, Forward);

            //if the result isnt 0, use it to change the sign of the angle to be either positive or negative.
            if (perpendicularDotProduct != 0)
                angle *= -perpendicularDotProduct / Math.Abs(perpendicularDotProduct);

            Rotate(angle);
        }
    }
}
