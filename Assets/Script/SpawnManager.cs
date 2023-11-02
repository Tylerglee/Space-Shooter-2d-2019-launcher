using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnRoutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawn game objects every 5 seconds
    //Create a coroutine of type IEnumerator -- Yield Events 
    //while loop

    //yield return null; //wait 1 frame

    //then this line is called

    //yield return new WaitForSeconds(5.0f);

    //then this line is called
    //while loop (infinite loop)
    //Instiate enemy prefab
    //yield wait for 5 seconds

    IEnumerator SpawnRoutine()
    {
        while (true) 
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

}
