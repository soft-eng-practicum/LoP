using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

namespace Completed

{

    public class BoardManager3 : MonoBehaviour
    {
        // Using Serializable allows us to embed a class with sub properties in the inspector.
        [Serializable]
        public class Count
        {
            public int minimum;             //Minimum value for our Count class.
            public int maximum;             //Maximum value for our Count class.

            //Assignment constructor.
            public Count(int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }


        public int columns;                                         //Number of columns in our game board.
        public int rows;                                            //Number of rows in our game board.
        public Count wallCount = new Count(5, 9);                      //Lower and upper limit for our random number of walls per level.
        public Count foodCount = new Count(4, 8);                      //Lower and upper limit for our random number of food items per level.
        public GameObject exit;                                         //Prefab to spawn for exit.
        public GameObject[] floorTiles;                                 //Array of floor prefabs.
        public GameObject[] wallTiles;                                  //Array of wall prefabs.
        public GameObject[] foodTiles;                                  //Array of food prefabs.
        public GameObject[] enemyTiles;                                 //Array of enemy prefabs.
        public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.

        private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
        private List<Vector3> gridPositions = new List<Vector3>();   //A list of possible locations to place tiles.


        //Clears our list gridPositions and prepares it to generate a new board.
        void InitialiseList()
        {
            //Clear our list gridPositions.
            gridPositions.Clear();

            //Loop through x axis (columns).
            for (int x = 1; x < columns - 1; x++)
            {
                //Within each column, loop through y axis (rows).
                for (int y = 1; y < rows - 1; y++)
                {
                    //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                    gridPositions.Add(new Vector3(x, y, 0f));
                }
            }
        }


        //Sets up the outer walls and floor (background) of the game board.
        void BoardSetup()
        {
            //Instantiate Board and set boardHolder to its transform.
            boardHolder = new GameObject("Board").transform;

            //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
            for (int x = -1; x < columns + 1; x++)
            {
                //Loop along y axis, starting from -1 to place floor or outerwall tiles.
                for (int y = -1; y < rows + 1; y++)
                {
                    //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                    //Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
                    if (x == -1 || x == columns || y == -1 || y == rows)
                        toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

                    //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                    GameObject instance =
                        Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                    instance.transform.SetParent(boardHolder);
                }
            }
        }


        //RandomPosition returns a random position from our list gridPositions.
        Vector3 RandomPosition()
        {
            //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
            int randomIndex = Random.Range(0, gridPositions.Count);

            //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
            Vector3 randomPosition = gridPositions[randomIndex];

            //Remove the entry at randomIndex from the list so that it can't be re-used.
            gridPositions.RemoveAt(randomIndex);

            //Return the randomly selected Vector3 position.
            return randomPosition;
        }


        //LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
        void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
        {
            //Choose a random number of objects to instantiate within the minimum and maximum limits
            int objectCount = Random.Range(minimum, maximum + 1);

            //Instantiate objects until the randomly chosen limit objectCount is reached
            for (int i = 0; i < objectCount; i++)
            {
                //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
                Vector3 randomPosition = RandomPosition();

                //Choose a random tile from tileArray and assign it to tileChoice
                GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

                //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation   
                Instantiate(tileChoice, randomPosition, Quaternion.identity);

            }
        }

        public void makeWalls3(GameObject[] tileArray)
        {
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

            Instantiate(tileChoice, new Vector3(0, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(0, 2, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(1, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(1, 2, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(2, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 2, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 17, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 18, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 19, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(3, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(3, 2, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(4, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(4, 2, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(4, 15, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(5, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(5, 2, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(5, 8, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(6, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(6, 2, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(6, 6, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(6, 7, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(6, 8, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(7, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(7, 2, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(9, 0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(9, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(9, 2, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(10, 0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(10, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(10, 2, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(10, 4, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(11, 0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(11, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(11, 2, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(11, 4, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(11, 9, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(11, 11, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(12, 0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(12, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(12, 2, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(12, 4, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(13, 0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(13, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(13, 2, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(14, 0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(14, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(14, 2, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(15, 0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(15, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(15, 2, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(15, 7, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(15, 15, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(16, 0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(16, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(16, 2, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(16, 13, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(16, 14, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(17, 0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(17, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(17, 2, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(17, 13, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(18, 0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18, 2, 0f), Quaternion.identity);

            Instantiate(tileChoice, new Vector3(19, 0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(19, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(19, 2, 0f), Quaternion.identity);



        }

        public void SetupScene3(int level)
        {
            //Creates the outer walls and floor.
            BoardSetup();

            //Reset our list of gridpositions.
            InitialiseList();

            //Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
            // LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);

            makeWalls3(wallTiles);

            //Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
            LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);

            //Determine number of enemies based on current level number, based on a logarithmic progression
            int enemyCount = (int)Mathf.Log(level, 2f);

            //Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
            LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);

           
        }
    }
}