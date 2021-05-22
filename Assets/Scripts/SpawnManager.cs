using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private Vector3 spawnPos = new Vector3(26, 1, 0);
    private float startDelay = 2;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        //invoke first spawn
        Invoke("SpawnObstacle", startDelay);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle() {
        //spawn obstacle
        int obstacleIndex = Random.Range(0, 2);
        if (playerControllerScript.gameOver == false) {
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);
        }

        //invoke next spawn
        float spawnDelay = Random.Range(0.55f, 1.5f);
        Invoke("SpawnObstacle", spawnDelay);
    }
}
