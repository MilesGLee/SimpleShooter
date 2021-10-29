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
        private Matrix3 _transform = Matrix3.Identity;
        public Matrix3 _translation = Matrix3.Identity;
        public Matrix3 _rotation = Matrix3.Identity;
        public Matrix3 _scale = Matrix3.Identity;
        private Sprite _sprite;

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
            get { return new Vector2(_transform.M02, _transform.M12); }
            set { _transform.M02 = value.X; _transform.M12 = value.Y; }
        }

        public Vector2 Forward
        {
            get { return _forward; }
            set { _forward = value; }
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


        public virtual void Start()
        {
            _started = true;
        }

        public virtual void Update(float deltaTime)
        {
            _transform = _translation * _rotation * _scale;
            Console.WriteLine(_name + ":" + Position.X + ":" + Position.Y);
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
                    _sprite.Draw(_transform);
                CircleCollider myCol = (CircleCollider)Collider;
                Raylib.DrawCircleLines((int)Position.X, (int)Position.Y, myCol.CollisionRadius, Color.GREEN);
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
    }
}
