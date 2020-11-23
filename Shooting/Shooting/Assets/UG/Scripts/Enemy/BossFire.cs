using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFire : MonoBehaviour
{
    public GameObject bulletFactory;
    public GameObject bulletFactory2;
 
    public GameObject FirePointLeft;
    public GameObject FirePointRight;
    public GameObject FirePointCenter;
    public float FireCount = 2f;
    public float SkillCount = 5f;
    float SCount = 0;
    float Count = 0;
    float angle = 90.0f;
    public int bulletCount = 12;
    int number = 0;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Fire();

        Skill();
    }

    private void Skill()
    {
        SCount += Time.deltaTime;
        if (SCount > SkillCount)
        {
            //총알공장에서 총알생성
            GameObject bullet1 = Instantiate(bulletFactory2);
            GameObject bullet2 = Instantiate(bulletFactory2);
            GameObject bullet3 = Instantiate(bulletFactory2);
            GameObject bullet4 = Instantiate(bulletFactory2);
            //총알생성 위치
            bullet1.transform.position = transform.position;
            bullet2.transform.position = transform.position;
            bullet3.transform.position = transform.position;
            bullet4.transform.position = transform.position;
            //360도 방향으로 총알발사
            bullet1.transform.eulerAngles = new Vector3(0, angle, 0);
            bullet2.transform.eulerAngles = new Vector3(0, angle+90, 0);
            bullet3.transform.eulerAngles = new Vector3(0, angle+180, 0);
            bullet4.transform.eulerAngles = new Vector3(0, angle+270, 0);

            angle += 5f;
            if(angle >= 810f)
            {
                angle = 90f;
                SCount = 0;
            }
        }

    }

    private void Fire()
    {
        Count += Time.deltaTime;
        if (FireCount < Count)
        {
            GameObject bullet1 = Instantiate(bulletFactory);
            bullet1.transform.position = FirePointLeft.transform.position;
            GameObject bullet2 = Instantiate(bulletFactory);
            bullet2.transform.position = FirePointRight.transform.position;
            GameObject bullet3 = Instantiate(bulletFactory);
            bullet3.transform.position = FirePointCenter.transform.position;
            Count = 0;
        }
    }

}
