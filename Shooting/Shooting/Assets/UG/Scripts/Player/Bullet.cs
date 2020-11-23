using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("총알충돌");
        if(collision.gameObject.name == "Boss")
        {
            print("보스충돌");

            Destroy(gameObject);

        }
        else
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }
    }
}
