using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini : MonoBehaviour
{
    Rigidbody Minirigidbody;
    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        Minirigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(isActive);
            isActive = !isActive;
        }
    }
}
