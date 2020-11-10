using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletM : MonoBehaviour
{
    public GameObject Lturret;
    public GameObject Rturret;
    bool isActive = false;

    void Start()
    {
        Lturret = GameObject.Find("sub1");    
        Rturret = GameObject.Find("sub2");    
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (isActive)
            {
                Lturret.SetActive(false);
                Rturret.SetActive(false);
            }

            else
            {
                Lturret.SetActive(true);
                Rturret.SetActive(true);
            }

            isActive = !isActive;
        }
    }
}
