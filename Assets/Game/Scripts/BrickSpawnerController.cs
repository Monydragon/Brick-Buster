using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawnerController : MonoBehaviour
{
    public int currentlevel = 1;
    public int NumberOfBlocksToSpawn = 4;
    public int LastLevelNumberOfBlocks = 4;
    public Vector2 LeftBounds;
    public Vector2 RightBounds;
    public GameObject RootGameobject;
    public GameObject[] bricksToSpawn;
    private List<GameObject> Blocks = new List<GameObject>();
    private List<Vector2> UsedPositions = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        SpawnBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBlocks()
    {
        for (int i = 0; i < NumberOfBlocksToSpawn; i++)
        {
            var randBlock = bricksToSpawn[Random.Range(0, bricksToSpawn.Length)];
            if (randBlock != null)
            {
            GETRANDOMBOUND:
                var randLeft = Mathf.RoundToInt(Random.Range(LeftBounds.x, RightBounds.x));
                var randRight = Mathf.RoundToInt(Random.Range(LeftBounds.y, RightBounds.y));
                var randomBound = new Vector2(randLeft, randRight);
                //var randomBound = new Vector2(Random.Range(LeftBounds.x, RightBounds.x), Random.Range(LeftBounds.y, RightBounds.y));
                if (UsedPositions.Contains(randomBound))
                {
                    goto GETRANDOMBOUND;
                }
                UsedPositions.Add(randomBound);
                var obj = Instantiate(randBlock, randomBound, Quaternion.identity);
                obj.transform.parent = RootGameobject.transform;
                Blocks.Add(obj);

            }
        }
    }

    public void ResetLevel()
    {
        UsedPositions.Clear();
        Blocks.Clear();
        for (int i = 0; i < RootGameobject.transform.childCount; i++)
        {
            Destroy(RootGameobject.transform.GetChild(i).gameObject);
        }
        currentlevel = 1;
        EventManager.LevelFail();
        LastLevelNumberOfBlocks = 4;
        NumberOfBlocksToSpawn = 4;
        SpawnBlocks();
        EventManager.GameReset();
    }

    public void NextLevel()
    {
        UsedPositions.Clear();
        Blocks.Clear();
        for (int i = 0; i < RootGameobject.transform.childCount; i++)
        {
            Destroy(RootGameobject.transform.GetChild(i).gameObject);
        }
        currentlevel++;
        NumberOfBlocksToSpawn = Random.Range(LastLevelNumberOfBlocks, LastLevelNumberOfBlocks + currentlevel * 3);
        if(NumberOfBlocksToSpawn >= 100)
        {
            NumberOfBlocksToSpawn = 100;
        }
        LastLevelNumberOfBlocks = NumberOfBlocksToSpawn;
        GameManager.Instance.currentLevelIndex++;
        SpawnBlocks();
    }
}
