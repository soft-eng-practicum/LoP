using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

namespace Completed

{

    public class BoardManager : MonoBehaviour
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
        public GameObject exitHelp;
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

        public void makeWalls(GameObject[] tileArray)
        {
            //Choose a random tile from tileArray and assign it to tileChoice
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            List<Vector3> wallSpots = new List<Vector3>();

            //Column Zero 8 walls
            Instantiate(tileChoice, new Vector3(0, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(0, 2, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(0, 3, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(0, 4, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(0, 5, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(0, 6, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(0, 7, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(0, 17, 0f), Quaternion.identity);
            
            //Column One 17 walls
            Instantiate(tileChoice, new Vector3(1, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(1, 2, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(1, 3, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(1, 4, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(1, 5, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(1, 6, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(1, 7, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(1, 8, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(1, 9, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(1, 10, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(1, 11, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(1, 12, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(1, 14, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(1, 15, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(1, 17, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(1, 18, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(1, 19, 0f), Quaternion.identity);
           
            //Column Two 16 walls
            Instantiate(tileChoice, new Vector3(2, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 2, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 3, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 5, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 6, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 7, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 8, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 9, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 10, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 11, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 12, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 14, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 15, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 17, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 18, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(2, 19, 0f), Quaternion.identity);
          
            //Column Three 4 Walls
            Instantiate(tileChoice, new Vector3(3, 7, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(3, 8, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(3, 14, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(3, 15, 0f), Quaternion.identity);
            
            //Column Four 6 Walls
            Instantiate(tileChoice, new Vector3(4, 0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(4, 1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(4, 2, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(4, 7, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(4, 8, 0f), Quaternion.identity);  
            Instantiate(tileChoice, new Vector3(4, 14, 0f), Quaternion.identity);
            
            //Column Five 4 Walls
            Instantiate(tileChoice, new Vector3(5,0,0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(5,1,0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(5,2,0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(5,14,0f), Quaternion.identity);

            //Column Six 8 Walls
            Instantiate(tileChoice, new Vector3(6,0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(6,1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(6,7, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(6,12, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(6,13, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(6,14, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(6,15, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(6,16, 0f), Quaternion.identity);

            //Column Seven 14 Walls
            Instantiate(tileChoice, new Vector3(7,0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(7,1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(7,3, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(7,4, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(7,5, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(7,6, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(7,7, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(7,8, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(7,9, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(7,12, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(7,13, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(7,14, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(7,15, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(7,16, 0f), Quaternion.identity);

            //Column Eight
            Instantiate(tileChoice, new Vector3(8,0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(8,1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(8,5, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(8,6, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(8,7, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(8,12, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(8,13, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(8,14, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(8,15, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(8,16, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(8,18, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(8,19, 0f), Quaternion.identity);

            //Column Nine
            Instantiate(tileChoice, new Vector3(9,0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(9,1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(9,5, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(9,6, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(9,7, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(9,12, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(9,15, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(9,16, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(9,19, 0f), Quaternion.identity);

            //Column Ten
            Instantiate(tileChoice, new Vector3(10,0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(10,1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(10,2, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(10,3, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(10,5, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(10,6, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(10,7, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(10,14, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(10,15, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(10,16, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(10,19, 0f), Quaternion.identity);

            //Column Eleven
            Instantiate(tileChoice, new Vector3(11,6, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(11,7, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(11,8, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(11,9, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(11,10, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(11,12, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(11,14, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(11,15, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(11,16, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(11,17, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(11,19, 0f), Quaternion.identity);

            //Column Twelve
            Instantiate(tileChoice, new Vector3(12,5, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(12,6, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(12,7, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(12,8, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(12,9, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(12,10, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(12,11, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(12,12, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(12,14, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(12,15, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(12,19, 0f), Quaternion.identity);

            //Column Thirteen
            Instantiate(tileChoice, new Vector3(13,0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(13,1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(13,3, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(13,4, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(13,5, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(13,6, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(13,7, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(13,8, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(13,9, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(13,15, 0f), Quaternion.identity);

            //Column Fourteen
            Instantiate(tileChoice, new Vector3(14,0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(14,1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(14,3, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(14,4, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(14,5, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(14,6, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(14,7, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(14,8, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(14,9, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(14,10, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(14,15, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(14,17, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(14,18, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(14,19, 0f), Quaternion.identity);

            //Column Fifteen
            Instantiate(tileChoice, new Vector3(15,0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(15,1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(15,5, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(15,10, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(15,15, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(15,19, 0f), Quaternion.identity);

            //Column Sixteen
            Instantiate(tileChoice, new Vector3(16,0, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(16,1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(16,2, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(16,3, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(16,5, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(16,6, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(16,8, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(16,9, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(16,10, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(16,13, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(16,14, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(16,15, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(16,16, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(16,19, 0f), Quaternion.identity);

            //Column Seventeen
            Instantiate(tileChoice, new Vector3(17,8, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(17,9, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(17,16, 0f), Quaternion.identity);

            //Column Eighteen
            Instantiate(tileChoice, new Vector3(18,1, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18,2, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18,3, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18,4, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18,5, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18,6, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18,7, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18,8, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18,9, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18,10, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18,11, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18,12, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18,14, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18,15, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18,16, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18,18, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(18,19, 0f), Quaternion.identity);

            //Column Nineteen
            Instantiate(tileChoice, new Vector3(19,10, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(19,11, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(19,12, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(19,13, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(19,14, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(19,15, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(19,16, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(19,18, 0f), Quaternion.identity);
            Instantiate(tileChoice, new Vector3(19,19, 0f), Quaternion.identity);

        }

        //SetupScene initializes our level and calls the previous functions to lay out the game board
        public void SetupScene(int level)
        {
            //Creates the outer walls and floor.
            BoardSetup();

            //Reset our list of gridpositions.
            InitialiseList();

            //Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
            // LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);

            makeWalls(wallTiles);

            //Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
            LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);

            //Determine number of enemies based on current level number, based on a logarithmic progression
            int enemyCount = (int)Mathf.Log(level, 2f); 

            //Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
            LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);

            //Instantiate the exit tile 
            Instantiate(exit, new Vector3(18, 17, 0f), Quaternion.identity);
            Instantiate(exitHelp, new Vector3(19, 17, 0f), Quaternion.identity);
        }

  
    }
}