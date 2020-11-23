using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 5.0f;
    // Start is called before the first frame update
    Vector3 dir;
    public GameObject fxFactory;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);   
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
       // Destroy(collision.gameObject);
        ShowEffect();
        ScoreManager.Instance.AddScore();
    }

    private void ShowEffect()
    {
        GameObject fx = Instantiate(fxFactory);
        fx.transform.position = transform.position;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
