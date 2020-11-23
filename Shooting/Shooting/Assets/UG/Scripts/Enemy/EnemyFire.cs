using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject bulletFactory;
    public GameObject FirePoint;
    public float FireCount = 1f;
    public float Count = 0;
    public GameObject Player;
    Vector3 dir;
   
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.transform.position.z < transform.position.z)
        {
            Fire();
        }
        
    }

    private void Fire()
    {
        Count += Time.deltaTime;
        if (FireCount < Count)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = FirePoint.transform.position;
            dir = Player.transform.position - transform.position;
            dir.Normalize();
            bullet.transform.forward = dir;
            Count = 0;
        }
    }
}
