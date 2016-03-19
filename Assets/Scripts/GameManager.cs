using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Completed
{
    using System.Collections.Generic;       //Allows us to use Lists. 

    public class GameManager : MonoBehaviour
    {
        public float turnDelay = .1f;
        public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
        private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
        public int playerFoodPoints = 100;
        [HideInInspector]  public bool playersTurn = true;

        private int level = 3;  //Current level number, expressed in game as "Day 1".
        private List<Enemy> enemies; //Keep Track of Enemies, send orders to move
        private bool enemiesMoving; //set enemies 


        //Awake is always called before any Start functions
        void Awake()
        {
            //Check if instance already exists
            if (instance == null)

                //if not, set instance to this
                instance = this;

            //If instance already exists and it's not this:
            else if (instance != this)

                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);

            enemies = new List<Enemy>(); 
            //Get a component reference to the attached BoardManager script
            boardScript = GetComponent<BoardManager>();

            //Call the InitGame function to initialize the first level 
            InitGame();
        }

        //Initializes the game for each level.
        void InitGame()
        {
            enemies.Clear(); //Use to clear enemies from previous level.
            //Call the SetupScene function of the BoardManager script, pass it current level number.
            boardScript.SetupScene(level);

        }
        
        public void GameOver()
        {
            enabled = false;
        }



        //Update is called every frame.
        void Update()
        {
            if (playersTurn || enemiesMoving)
                return;
            StartCoroutine(MoveEnemies());
        }

        public void AddEnemyToList(Enemy script)
        {
            enemies.Add(script);
        }
        IEnumerator MoveEnemies()
        {
            enemiesMoving = true;
            yield return new WaitForSeconds(turnDelay);
            if(enemies.Count == 0)
            {
                yield return new WaitForSeconds(turnDelay);
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].MoveEnemy();
                yield return new WaitForSeconds(enemies[i].moveTime);
            }
            playersTurn = true;

            enemiesMoving = false; 
        }
    }
}