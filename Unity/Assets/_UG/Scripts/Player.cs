﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody myRigidbody;
    float speed = 5f;
    Vector3 movement;
    float h;
    float v;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }
   
    void Start()
    {
       // myRigidbody.AddForce(0, 500, 0);
        Debug.Log("START");

    }

    // Update is called once per frame
    void Update()
    {
        
        /*
         * switch(Input.inputString)
        {
            case "A":
                myRigidbody.AddForce(10, 0, 0);
                Debug.Log("A");
                break;
            case "W":
                myRigidbody.AddForce(0, 0, 10);
                Debug.Log("W");
                break;
            case "D":
                myRigidbody.AddForce(-10, 0, 0);
                Debug.Log("D");
                break;
            case "S":
                myRigidbody.AddForce(0, 0, -10);
                Debug.Log("S");
                break;
        }
        */
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //????? 여긴 왜 있는거임? ㅋㅋ루삥뽕
        }
           // myRigidbody.AddForce(Vector3.up * 3f, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //movement.Set(h, 0f, v);
        //movement = movement.normalized * speed * Time.deltaTime;
        //myRigidbody.MovePosition(transform.position + movement);
        Vector3 vec = new Vector3(h * speed, myRigidbody.velocity.y, v * speed);
        myRigidbody.velocity = vec;

        //transform.Translate(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime);


        //범위지정으로 충돌처리
        /*
        if (transform.position.x >= 4.5f) transform.position = new Vector3(4.5f,transform.position.y, transform.position.z);
        if (transform.position.x <= -4.5f) transform.position = new Vector3(-4.5f, transform.position.y, transform.position.z);
        if (transform.position.z >= 4.5f) transform.position = new Vector3(transform.position.x, transform.position.y, 4.5f);
        if (transform.position.z <= -4.5f) transform.position = new Vector3(transform.position.x, transform.position.y, -4.5f);

        Vector3 temp = new Vector3(Mathf.Clamp(transform.position.x, -4.5f, 4.5f),
                        transform.position.y,
                        Mathf.Clamp(transform.position.z, -4.5f, 4.5f));
        
        transform.position = temp;
        */

        //카메라 범위 밖으로 안나가도록 하는것
        
         Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
         float ratioX = GetComponent<Collider>().bounds.extents.x / Screen.width;
         float ratioZ = GetComponent<Collider>().bounds.extents.z / Screen.height;
         viewPos.x = Mathf.Clamp(viewPos.x,ratioX,1f-ratioX);
         viewPos.y = Mathf.Clamp(viewPos.y, ratioZ, 1f - ratioZ);
         Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
         transform.position = worldPos;
       // if(Input.GetMouseButtonDown(0))
       // {
       //     Vector2 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
       //     position = Camera.main.ScreenToWorldPoint(position);
       //
       //     transform.position = position;
       // }
        //Cursor.SetCursor()

        /*
        float HalfsizeX = Camera.main.orthographicSize * Screen.width / Screen.height;
        float HalfsizeY = Camera.main.orthographicSize;
        
        float objsizeX = GetComponent<Collider>().bounds.extents.x;
        float objsizeY = GetComponent<Collider>().bounds.extents.z;
        Vector3 objsize = GetComponent<Collider>().bounds.extents;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -HalfsizeX + objsize.x, HalfsizeX - objsize.x),
                                         transform.position.y, 
                                         Mathf.Clamp(transform.position.z, -HalfsizeY + objsize.z, HalfsizeY - objsize.z));
        */

        //if (pos.x < 0.1f) pos.x = 0.1f;
        //
        //if (pos.x > 0.9f) pos.x = 0.9f;
        //
        //if (pos.y < 0.1f) pos.y = 0.1f;
        //
        //if (pos.y > 0.9f) pos.y = 0.9f;
    }
}
