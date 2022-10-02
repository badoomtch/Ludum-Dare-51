using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
/*         if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemy();
        } */
    }

    public void SpawnEnemy()
    {
        StartCoroutine(DelayBetweenSpawns());
    }

    IEnumerator DelayBetweenSpawns()
    {
        //for 4 times
        for (int i = 0; i < 5; i++)
        {
            //spawn an enemy at a random spawn point
            Instantiate(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
            //wait 1 second
            yield return new WaitForSeconds(1f);
        }
    }
}
