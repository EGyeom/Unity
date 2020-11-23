//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMgr : MonoBehaviour
{
    public GameObject EnemyFactory;
    public GameObject BossFactory;
    public Transform CreatePointLeft;
    public Transform CreatePointRight;
    public float EnemyCreateCount = 2.0f;
    public float BossCreateCount = 20.0f;
    bool BossIsCreated = false;
    float Count = 0;
    float BossCount = 0;
    // Start is called before the first frame update
    void Start()
    {
       //CreatePointLeft = GameObject.Find("Left").transform;
       //CreatePointRight = GameObject.Find("Right").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(BossIsCreated)
        {

        }
        else
        {
            EnemyCreate();
            BossCreate();
        }
        
    }

    private void BossCreate()
    {
        BossCount += Time.deltaTime;
        if(BossCount > BossCreateCount)
        {
            GameObject boss = Instantiate(BossFactory);
            boss.transform.position = new Vector3(0f, 0f, 6f);
            BossIsCreated = true;
            BossCount = 0;
        }
    }

    private void EnemyCreate()
    {
        Count += Time.deltaTime;
        if (EnemyCreateCount < Count)
        {
            GameObject Enemy = Instantiate(EnemyFactory);
            Enemy.transform.position = new Vector3(Random.Range(CreatePointLeft.position.x, CreatePointRight.position.x),
                                                    transform.position.y,
                                                    transform.position.z);
            Count = 0;
        }
    }


}
