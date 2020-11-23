using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory;
    public GameObject FirePoint1;
    public GameObject FirePoint2;

    public Queue<GameObject> bulletPool;
    //오브젝트 풀링
    //오브젝트 풀링에 사용할 최대 총알갯수
    int poolSize = 20;
    int fireIndex = 0;

    float FireCount = 1;
    float Count = 0;
    // Start is called before the first frame update
    void Start()
    {
        InitObjectPooling();
        //bulletFactory = GameObject.Find("Bullet");
        FirePoint1 = GameObject.Find("1");
        FirePoint2 = GameObject.Find("2");
    }
    private void InitObjectPooling()
    {
        //1. 배열
        //bulletPool = new GameObject[poolSize];
        //for(int i = 0;i < poolSize; i++)
        //{
        //    GameObject bullet = Instantiate(bulletFactory);
        //    bullet.SetActive(false);
        //    bulletPool[i] = bullet;
        //}

        //2. 리스트
        //bulletPool = new List<GameObject>();
        //for(int i = 0;i < poolSize; i++)
        //{
        //    GameObject bullet = Instantiate(bulletFactory);
        //    bullet.SetActive(false);
        //    bulletPool.Add(bullet);
        //}

        //3. 큐
        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }
        // Update is called once per frame
        void Update()
    {

    }

    public void OnFireButtonClick()
    {
        Fire();
    }

    private void Fire()
    {
        //if(Input.GetMouseButtonDown(1))
        //{
            //GameObject bullet1 = Instantiate(bulletFactory);
            //GameObject bullet2 = Instantiate(bulletFactory);
            //
            //bullet1.transform.position = FirePoint1.transform.position;
            //bullet2.transform.position = FirePoint2.transform.position;

            if (bulletPool.Count > 0)
            {
                GameObject bullet1 = bulletPool.Dequeue();
                bullet1.SetActive(true);
                bullet1.transform.position = FirePoint1.transform.position;
                bullet1.transform.position = FirePoint1.transform.position;
                bullet1.transform.up = FirePoint1.transform.up;
                GameObject bullet2 = bulletPool.Dequeue();
                bullet2.SetActive(true);
                bullet2.transform.position = FirePoint2.transform.position;
                bullet2.transform.position = FirePoint2.transform.position;
                bullet2.transform.up = FirePoint2.transform.up;
            }
            else//오브젝트풀이 비어서 오브젝트가 하나도 없으니 풀크기를 늘려준다
            {
                GameObject bullet = Instantiate(bulletFactory);
                bullet.SetActive(false);
                //오브젝트 풀에 추가한다
                bulletPool.Enqueue(bullet);
            }
        //}

    }
}
