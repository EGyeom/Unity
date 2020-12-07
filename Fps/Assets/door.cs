using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public Transform player;
    public GameObject bt;
    float speed = 1.0f;
    bool isopen = false;
    bool isclose = false;
    bool isclosed = true;
    bool isopened = false;
    float count = 0;
    // Start is called before the first frame update
    void Start()
    {
        bt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        print(distance);
        if(distance < 5f)
        {
            bt.SetActive(true);
            if (Input.GetKey(KeyCode.F))
            {
                if (isopened) isclose = true;
                else if (isclosed) isopen = true;
            }
        }
        else
        {
            bt.SetActive(false);
        }

        doorMove();
    }

    private void doorMove()
    {
        if(isopen)
        {
            count += Time.deltaTime;
            if(count < 5f)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else
            {
                count = 0;
                isopen = false;
                isopened = true;
                isclosed = false;
            }
        }

        if(isclose)
        {
            count += Time.deltaTime;
            if (count < 5f)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else
            {
                count = 0;
                isclose = false;
                isclosed = true;
                isopened = false;
            }
        }
    }
}
