using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using MathLibrary;
using Raylib_cs;

namespace SimpleShooter
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
        private static int _currentSceneIndex;
        private Scene[] _scenes = new Scene[0];
        private Stopwatch _stopwatch = new Stopwatch();
        public static Scene _currentScene;
        private float _spawnEnemyTimer = 0f;
        private float _spawnEnemyMaxTimer = 3f;
        public static int Score = 0;


        /// <summary>
        /// is the call to start the application
        /// </summary>
        public void Run()
        {
            //calles the entrire application
            Start();

            //made the three float for delta time to function
            float currentTime = 0;
            float lastTime = 0;
            float deltaTime = 0;


            //loops till application is done
            while (!_applicationShouldClose || Raylib.WindowShouldClose())
            {
                //getss the time from the Stopwatch timer
                currentTime = _stopwatch.ElapsedMilliseconds / 1000.0f;

                //uses the last time that is at the end of the loop to subtact from the currentTime...
                //... to get the deltaTime.
                deltaTime = currentTime - lastTime;

                Update(deltaTime);

                Draw();

                //gets the currentTime and saves it
                lastTime = currentTime;
            }


            //is the call to end the entire appliction
            End();
        }

        /// <summary>
        /// Called when the applicaiton starts
        /// </summary>
        private void Start()
        {
            //created a window using raylib
            Raylib.InitWindow(800, 450, "The math for game. ");
            Raylib.SetTargetFPS(0);

            _stopwatch.Start();

            //prevously made a function to hold the actors and players to make...
            //the Start function smaller
            _currentScene = new Scene();


            Player player = new Player(400, 225, 150, "Player", "player.png");
            player.SetScale(50, 50);
            CircleCollider playerCollider = new CircleCollider(25, player);
            player.Collider = playerCollider;
            Enemy enemy = new Enemy(110, 0, 100, player, "Enemy", "enemy.png");
            enemy.SetScale(50, 50);
            CircleCollider enemyCollider = new CircleCollider(25, enemy);
            enemy.Collider = enemyCollider;
            Actor point = new Actor(200, 200, 0, "Point");
            AABBCollider pointCollider = new AABBCollider(15, 15, point);
            point.Collider = pointCollider;


            //adds the actor to the scene and takes in that actor
            _currentScene.AddActor(enemy);
            _currentScene.AddActor(player);
            _currentScene.AddActor(point);
            _currentSceneIndex = AddScene(_currentScene);

            _scenes[_currentSceneIndex].Start();


        }

        /// <summary>
        /// Updates the Engine when it is called
        /// </summary>
        private void Update(float deltaTime)
        {

            _scenes[_currentSceneIndex].Update(deltaTime);

            while (Console.KeyAvailable)
                Console.ReadKey(true);

            if (_spawnEnemyTimer > _spawnEnemyMaxTimer) //Loop to spawn enemies constantly.
            {
                _spawnEnemyTimer = 0;
                //SpawnEnemy(200);
                SpawnPoint(150);
            }
            _spawnEnemyTimer += deltaTime;
        }

        /// <summary>
        ///call the current Scene.
        /// </summary>
        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);

            //add all of the icons back to the buffer
            _scenes[_currentSceneIndex].Draw();

            Raylib.EndDrawing();

        }
        /// <summary>
        /// end the appliction 
        /// </summary>
        private void End()
        {
            _scenes[_currentSceneIndex].End();
            Raylib.CloseWindow();
        }

        /// <summary>
        /// Creats a array that is teparay then adds all the old arrays vaules to it..
        /// then sets the last index of that array to be the scene.
        /// </summary>
        /// <param name="scene">The scene that will be added to the scene array</param>
        /// <returns>The index where the new scene is located</returns>
        public int AddScene(Scene scene)
        {

            //craets a new temporary array
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //copy all values of old array then...
            for (int i = 0; i < _scenes.Length; i++)
            {
                //puts it in side the array.
                tempArray[i] = _scenes[i];
            }

            //set the last index to be the scene.
            tempArray[_scenes.Length] = scene;

            //set the old array to e the new array
            _scenes = tempArray;

            //returns the last array.
            return _scenes.Length - 1;
        }

        /// <summary>
        /// get the next key that was typed in the input stream.
        /// </summary>
        /// <returns>The key that was pressed</returns>
        public static ConsoleKey GetNewtKey()
        {
            //if there are no keys being pressed...
            if (!Console.KeyAvailable)
                //...return
                return 0;
            //Return the current key being pressed
            return Console.ReadKey(true).Key;
        }
        /// <summary>
        /// Closes the application
        /// </summary>
        public static void CloseApplication()
        {
            _applicationShouldClose = true;
        }

        private void SpawnEnemy(float radius) 
        {
            var rand = new Random(); //These lines are to give the new enemy a random spawn point with a given radius.
            
            float t = rand.Next(361);
            double x = 400 + radius * Math.Cos(t);
            double y = 225 + radius * Math.Sin(t);

            for (int i = 0; i < Scene._actors.Length; i++)
            {
                if (Scene._actors[i] is Player) //Sifts through all the actors to get the player, to make them the new enemies target.
                {
                    Enemy enemy = new Enemy((float)x, (float)y, 100, (Player)Scene._actors[i], "Enemy");
                    enemy.SetScale(50, 50);
                    CircleCollider enemyCollider = new CircleCollider(25, enemy);
                    enemy.Collider = enemyCollider;
                    _currentScene.AddActor(enemy);

                }
            }
        }

        private void SpawnPoint(float radius)
        {
            var rand = new Random(); //These lines are to give the new enemy a random spawn point with a given radius.

            var angle = rand.NextDouble() * Math.PI * 2;
            var rad = Math.Sqrt(rand.NextDouble()) * radius;
            var x = 400 + rad * Math.Cos(angle);
            var y = 225 + rad * Math.Sin(angle);

            Actor point = new Actor((float)x, (float)y, 0, "Point");
            AABBCollider pointCollider = new AABBCollider(15, 15, point);
            point.Collider = pointCollider;
            _currentScene.AddActor(point);
        }
    }
}