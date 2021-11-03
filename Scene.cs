using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShooter
{
    class Scene
    {
        /// <summary>
        /// Array made for the actors in the scene
        /// </summary>
        public static Actor[] _actors;

        /// <summary>
        /// Makes actor in a Scene
        /// </summary>
        public Scene()
        {
            _actors = new Actor[0];
        }

        /// <summary>
        /// calls start for all of the actors in the actors array
        /// </summary>
        public virtual void Start()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Start();
        }

        /// <summary>
        /// calls the update for the actors in the actors array
        /// </summary>
        public virtual void Update(float deltaTime)
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (!_actors[i].Started)
                    _actors[i].Start();

                _actors[i].Update(deltaTime);

                if (i >= _actors.Length)
                    i--;
                //incremtns thorgh the actors array
                for (int j = 0; j < _actors.Length; j++)
                {
                    if (j >= _actors.Length)
                        j--;
                    if (_actors[i].Collider != null && _actors[i].Collider.CheckCollision(_actors[j]))
                    {
                        //then start on Collision for actor 1 by making actor 2 be collied with.
                        _actors[j].OnCollision(_actors[i]);

                    }
                }
            }
        }

        /// <summary>
        /// calls Draw for the actors in the actors array
        /// </summary>
        public virtual void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Draw();
        }

        /// <summary>
        /// calls end for actors in the actors array
        /// </summary>
        public virtual void End()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].End();
        }

        /// <summary>
        /// makes a array and adds it to the actors array 
        /// </summary>
        /// <param name="actor">The actor to add to the scene</param>
        public void AddActor(Actor actor)
        {
            //makes a new array called temArray and mades it the lengh of actors + a nother spot
            Actor[] temArray = new Actor[_actors.Length + 1];

            //incremens through the actors array
            for (int i = 0; i < _actors.Length; i++)
            {
                temArray[i] = _actors[i];
            }

            //sets temArray to the actors array and set it to actor
            temArray[_actors.Length] = actor;

            //then sets actors to temarray
            _actors = temArray;

        }

        /// <summary>
        /// makes a new array then subtracts a existing actor form that array
        /// </summary>
        /// <param name="actor">the actor in that scene</param>
        /// <returns>returns a bool called actorRemoved</returns>
        public virtual bool RemoveActor(Actor actor)
        {
            //create a varialbe to store if the removal was successful
            bool actorRemoved = false;

            //created a new array that is small than the original array.
            Actor[] temArray = new Actor[_actors.Length - 1];

            //is there to the second array and not have space from removed actor.
            int j = 0;

            //incremens through the temArray
            for (int i = 0; i < _actors.Length; i++)
            {
                //sais that if actor is not equal to the actor that is choosen then dont go into but..
                if (_actors[i] != actor)
                {
                    //make temArray have j and make it equal to actors with i so there is no left over space in the array.
                    temArray[j] = _actors[i];
                    //increment j
                    j++;
                }
                //if none of that is needed return true.
                else
                    actorRemoved = true;

            }

            //will only happen if the actor is being removed and will the set actors with temArray.
            if (actorRemoved)
                _actors = temArray;

            //...then returns
            return actorRemoved;
        }
    }
}
