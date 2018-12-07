using Maze; // Namespace Maze
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


    public delegate void BallTrigger();
    public static event BallTrigger BallManager;

    public delegate void CubeTrigger();
    public static event CubeTrigger CubeManager;

    public delegate void CapsuleTrigger();
    public static event CapsuleTrigger CapsuleManager;

    public delegate void FlashTrigger();
    public static event FlashTrigger FlashManager;

    // Singleton pattern
    private static GameController instance;
    // Maze generation
    private MazeGenerator generator;

    // Maze size
    public int columns = 5;
    public int rows = 5;

    // Room size, afects walls and floor

    public float roomSize = 5.0f;

    // Number of monsters
    public int numberOfMonsters;

    // Delay before start the spawn of monster after game starts
    public int startSpawnDelay;
    // Time between every spawn
    public int spawnDelay;

    // Game objects
    public GameObject wallPrefab = null;
    public GameObject floorPrefab = null;
    public GameObject monsterPrefab = null;

    // Monster container for clear hierarchy
    private GameObject monstersContainer;

    private readonly string MONSTERS = "monsters";
    private readonly string MONSTER = "monster";

    // Use this for initialization
    void Awake () {
        if (instance == null)
			instance = this;
		else if (instance != this) // If it's not this, destroy it
			Destroy(gameObject);
    }

    private void Start() {
      /*
        if (roomSize < 2.5f)
            throw new UnityException("room size is too small");

        generator = new MazeGenerator(columns, rows);
        generator.BuildInUnity(floorPrefab, wallPrefab, roomSize);
        monstersContainer = new GameObject();
        InitContainer();
        // After 10 seconds, the game will spawn a monster every min
        StartCoroutine(Spawn(startSpawnDelay, spawnDelay));
      */
    }

    private void InitContainer() {
        monstersContainer.transform.position = new Vector3();
        monstersContainer.name = MONSTERS;
    }

    private IEnumerator Spawn(float waitBeforeSpawn, float repeatInterval) {
        yield return new WaitForSeconds(waitBeforeSpawn);
        int i = 0;
        while (i < numberOfMonsters) {
            float range = (roomSize / 2) - 1;
            float randomOffsetX = Random.Range(-range, range);
            float randomOffsetZ = Random.Range(-range, range);
            float fixedCenter = wallPrefab.transform.localScale.z;
            // Position of the last cell + the middle of the cell, so it spawn in center
            float x = ((1/*Columns*/ - 1) * roomSize + roomSize / 2) + fixedCenter + randomOffsetX; // I put a 1 just for spawn monsters near camera
            float z = ((1/*rows*/ - 1) * roomSize + roomSize / 2) + fixedCenter + randomOffsetZ;
            Vector3 position = new Vector3(x, floorPrefab.transform.localScale.y + monsterPrefab.transform.localScale.y / 2, -z);
            GameObject monster = GameObject.Instantiate(monsterPrefab, position, Quaternion.identity, monstersContainer.transform);
            monster.name = MONSTER + "_" + i;
            i++;
            yield return new WaitForSeconds(repeatInterval);
        }
    }

    // This method trigger the event depending on the response
    public static void HandleResponse (int cases) {
          switch (cases) {
            case 0:
              BallManager();
              break;
            case 1:
              CubeManager();
              break;
            case 2:
              CapsuleManager();
              break;
            case 3:
              FlashManager();
              break;
            default:

              break;
          }
    }

}
