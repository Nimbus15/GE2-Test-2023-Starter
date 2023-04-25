using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public GameObject missile;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        spawnMissiles();
    }

    IEnumerator spawnMissiles()
    {
        Instantiate(missile);
        yield return new WaitForSeconds(10);
    }
}
