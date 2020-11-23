using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject FxFactory;
    public VariableJoystick joystick;
    public float speed = 5.0f;
    Transform tr;
    float h, v;
    float HalfHeight;
    float HalfWidth;
    Vector3 halfofObj;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        halfofObj = GetComponent<Collider>().bounds.extents;
        HalfHeight = Camera.main.orthographicSize;
        HalfWidth = Camera.main.orthographicSize / Screen.height * Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        if( h==0 && v==0)
        {
            h = joystick.Horizontal;
            v = joystick.Vertical;
        }
        Vector3 dir = new Vector3(h, 0, v);
        transform.Translate(dir * speed * Time.deltaTime);

        movescreen();
    }

    private void movescreen()
    {
        Vector3 temp = new Vector3(Mathf.Clamp(transform.position.x, -HalfWidth + halfofObj.x, HalfWidth- halfofObj.x),
                                    transform.position.y,
                                    Mathf.Clamp(transform.position.z, -HalfHeight + halfofObj.z, HalfHeight- halfofObj.z));
        transform.position = temp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        showEffect();
    }

    private void showEffect()
    {
        GameObject fx = Instantiate(FxFactory);
        fx.transform.position = transform.position;
    }
}
