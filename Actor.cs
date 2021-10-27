using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace SimpleShooter
{
    /// <summary>
    /// is there so i can hold the type for icon for actors or player
    /// </summary>
    struct Icon
    {
        public char Symbol;
        public Color color;
    }
    class Actor
    {
        private Icon _icon;
        private string _name;
        //private Vector2 _position;
        private bool _started;
        private float _speed;
        private Vector2 _forward = new Vector2(1, 0);
        private Collider _collider;
        private Matrix3 _transform = Matrix3.Identity;

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
        public Icon Icon
        {
            get { return _icon; }
        }

        public Vector2 Forward
        {
            get { return _forward; }
            set { _forward = value; }
        }

        public Collider Collider 
        {
            get { return _collider; }
            set { _collider = value; }
        }

        public void SetIcon(char symbol) 
        {
            _icon.Symbol = symbol;
        }

        public Actor()
        {

        }
        /// <summary>
        /// takes the Actor constructor and add the float x and y but takes out y
        /// </summary>
        /// <param name="x">is the replace the Vector2</param>
        /// <param name="y">is the replacement for the veoctor2</param>
        public Actor(char icon, float x, float y, float speed, Color color, string name = "Actor") :
            this(icon, new Vector2 { X = x, Y = y }, color, speed, name)
        { }


        /// <summary>
        /// Is a constructor for the actor that hold is definition.
        /// </summary>
        /// <param name="icon">The icon that all this information applies to</param>
        /// <param name="position">is the loctation that the icon is in</param>
        /// <param name="name">current Actor name</param>
        /// <param name="color">The color that the neame or icon will be</param>
        public Actor(char icon, Vector2 position, Color color, float speed, string name = "Actor")
        {
            //updatede the Icon with the struct and made it take a symbol and a color
            _icon = new Icon { Symbol = icon, color = color };
            Position = position;
            _name = name;
        }


        public virtual void Start()
        {
            _started = true;
        }

        public virtual void Update(float deltaTime)
        {

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
                Raylib.DrawText(Icon.Symbol.ToString(), (int)Position.X, (int)Position.Y, 50, Icon.color);
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

        public void SetScale(float x, float y) 
        {
            _transform.M00 = x;
            _transform.M11 = y;
        }
    }
}
