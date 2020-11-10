using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniFire : MonoBehaviour
{
    public GameObject bulletFactory; //총알 공장 (프리팹)
    public GameObject firePoint;    //총알 발사위치

    public float count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        count += Time.deltaTime;

        if (count > 0.5f)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = firePoint.transform.position;
            count = 0f;
        }
    }
}
