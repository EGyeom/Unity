using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Vector3 startPoint;
    [SerializeField]
    float SearchArea = 5.0f;
    [SerializeField]
    float AttackArea = 1.0f;

    GameObject player;
    NavMeshAgent nav;    
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        player = GameObject.Find("Player");
        nav = transform.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.transform.position);
        nav.speed = 1.0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startPoint, SearchArea);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackArea);
    }
}
