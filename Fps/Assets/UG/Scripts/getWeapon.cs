using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class getWeapon : MonoBehaviour
{
    public GameObject bt;
    public GameObject bottom_weapon;
    public GameObject wearing_weapon;
    public GameObject point;
    public Transform glasstf;
    bool isbuttonShow = true;
    // Start is called before the first frame update
    void Start()
    {
        bt.SetActive(false);
        wearing_weapon.SetActive(false);
        point.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, bottom_weapon.transform.position);
        if(isbuttonShow)
        {
            if (distance < 2f)
            {
                bt.SetActive(true);
            }
            else
            {
                bt.SetActive(false);
            }
        }

        if(bt.activeSelf)
        {
            if(Input.GetKey(KeyCode.F))
            {
                bottom_weapon.SetActive(false);
                wearing_weapon.SetActive(true);
                point.SetActive(true);

                bt.SetActive(false);
                isbuttonShow = false;
                wearing_weapon.transform.position = new Vector3(glasstf.position.x + 0.19f, glasstf.position.y, glasstf.position.z);
            }
        }
    }
}
