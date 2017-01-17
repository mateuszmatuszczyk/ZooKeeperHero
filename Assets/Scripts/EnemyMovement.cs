using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public Transform[] patrolpoints;
    int currentPoint;
    public Transform target;
    public float speed;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("patrol");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if(transform.position.x== patrolpoints[currentPoint].position.x)
            {
                currentPoint++;
            }

            if(currentPoint >= patrolpoints.Length)
            {
                currentPoint = 0;
            }

        }

    }


}
