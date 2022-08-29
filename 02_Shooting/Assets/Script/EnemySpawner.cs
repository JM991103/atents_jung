using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;

    IEnumerator enemysp;

    private void Awake()
    {
        enemysp = enemy();
    }

    void Start()
    {
        StartCoroutine(enemysp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator enemy()
    {
        while(true)
        {
            transform.position = new (11.0f, Random.Range(-4.5f, 4.5f), 0.0f);
            Instantiate(Enemy,transform.position,Quaternion.identity);
            yield return new WaitForSeconds(3.0f);
        }
    }
}