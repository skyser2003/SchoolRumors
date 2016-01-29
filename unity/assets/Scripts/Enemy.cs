using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public GameObject wayPoints;
    Vector3[] wayPointPositions;
    NavMeshAgent agent;
    int currentDestination;

    const float requiredDistance = 0.1f;

    bool isFacingRight;
    Transform graphics;

    PatrolState currentState;

    enum PatrolState
    {
        patrol,
        chase
    }

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        graphics = transform.FindChild("Graphics");
        currentState = PatrolState.patrol;

        if (wayPoints == null)
        {
            Debug.LogError("No waypoints found!", gameObject);
            enabled = false;
        }

        wayPointPositions = new Vector3[wayPoints.transform.childCount];

        for (int i = 0; i < wayPoints.transform.childCount; ++i)
        {
            if (wayPoints.transform.FindChild(i.ToString()) != null)
            {
                wayPointPositions[i] = wayPoints.transform.FindChild(i.ToString()).position;
            }
        }

        currentDestination = 0;
    }
	
	void Update ()
    {
        isFacingRight = agent.velocity.x > 0.0f;

        switch (currentState)
        {
            case PatrolState.patrol:
                Patrol();
                Look();
                break;

            case PatrolState.chase:
                Chase();
                break;
        }
	}

    void Patrol()
    {
        agent.SetDestination(wayPointPositions[currentDestination]);

        Vector3 enemyPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
        Vector3 targetPos = new Vector3(agent.destination.x, 0.0f, agent.destination.z);

        if (Vector3.Distance(enemyPos, targetPos) < requiredDistance)
        {
            currentDestination++;
            if (currentDestination >= wayPointPositions.Length)
            {
                currentDestination = 0;
            }
        }
    }

    void Look()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up;
        float seeDistance = 5.0f;
        float arcAngle = 30.0f;
        int numLines = 16;

        for (int i = 0; i < numLines; ++i)
        {
            Vector3 rayDirection = Quaternion.AngleAxis(-1 * arcAngle / 2 + (i * arcAngle / numLines) + arcAngle / (2 * numLines), Vector3.up) * (isFacingRight ? Vector3.right : Vector3.left);

            if (Physics.Raycast(rayOrigin, rayDirection, out hit, seeDistance))
            {
                Debug.DrawLine(rayOrigin, hit.point, Color.yellow);

                if(hit.transform.tag == "Player")
                {
                    Debug.DrawLine(rayOrigin, hit.point, Color.red);
                    OnSpotPlayer();
                }
            }
            else
            {
                Debug.DrawLine(rayOrigin, rayOrigin + rayDirection * seeDistance, Color.green);
            }
        }
        
    }

    void Chase()
    {

    }

    public virtual void OnSpotPlayer()
    {
        currentState = PatrolState.chase;
    }

    void LateUpdate()
    {
        graphics.rotation = Quaternion.identity;
        graphics.localScale = new Vector3(isFacingRight ? 1.0f : -1.0f, 1.0f, 1.0f);
    }
}
