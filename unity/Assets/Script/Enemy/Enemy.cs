using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public GameObject wayPoints;
    Vector3[] wayPointPositions;
    Vector3 chaseTarget;
    protected NavMeshAgent agent;
    int currentDestination;

    const float requiredDistance = 0.1f;

    bool isFacingRight;
    bool isTouching;
    Transform graphics;

    PatrolState currentState;

    float walkSpeed = 2.0f;
    float chaseSpeed = 3.0f;

    bool isWaiting;
    float waitTimer;

    PlayerUI playerUI;

    float raycastHeight = 0.5f;

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
        playerUI = GameObject.FindWithTag("UI").GetComponent<PlayerUI>();

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
        agent.speed = walkSpeed;
        agent.SetDestination(wayPointPositions[currentDestination]);

        if (IsCloseEnough(agent.destination))
        {
            currentDestination++;
            if (currentDestination >= wayPointPositions.Length)
            {
                currentDestination = 0;
            }
        }
    }

    bool IsCloseEnough(Vector3 target)
    {
        Debug.DrawLine(transform.position, target, Color.blue);

        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetPos = new Vector2(target.x, target.z);
        float distance = Vector2.Distance(enemyPos, targetPos);
        return distance < requiredDistance;
    }

    bool Look()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * raycastHeight;
        float seeDistance = currentState == PatrolState.patrol ? 10.0f : 20.0f;
        float arcAngle = currentState == PatrolState.patrol ? 35.0f : 220.0f;
        int numLines = currentState == PatrolState.patrol ? 16 : 64;
        isTouching = false;

        Collider[] cols = Physics.OverlapSphere(transform.position, currentState == PatrolState.patrol ? 1.33f : 1.8f);
        for (int i = 0; i < cols.Length; ++i)
        {
            if (cols[i].transform.tag == "Player")
            {
                if (Physics.Linecast(transform.position + Vector3.up * raycastHeight, cols[i].transform.position, out hit))
                {
                    if (hit.transform.tag == "Player")
                    {
                        Debug.DrawLine(rayOrigin, hit.point, Color.red);
                        OnSpotPlayer(hit.transform);
                        isTouching = true;
                        return true;
                    }
                    else
                    {
                        Debug.DrawLine(rayOrigin, hit.point, Color.yellow);
                    }
                }
            }
        }

        for (int i = 0; i < numLines; ++i)
        {
            Vector3 rayDirection = Quaternion.AngleAxis(-1 * arcAngle / 2 + (i * arcAngle / numLines) + arcAngle / (2 * numLines), Vector3.up) * (isFacingRight ? Vector3.right : Vector3.left);

            if (Physics.Raycast(rayOrigin, rayDirection, out hit, seeDistance))
            {
                Debug.DrawLine(rayOrigin, hit.point, Color.yellow);

                if(hit.transform.tag == "Player")
                {
                    Debug.DrawLine(rayOrigin, hit.point, Color.red);
                    OnSpotPlayer(hit.transform);
                    return true;
                }
            }
            else
            {
                Debug.DrawLine(rayOrigin, rayOrigin + rayDirection * seeDistance, Color.green);
            }
        }

        return false;
        
    }

    void Chase()
    {
        Debug.DrawLine(transform.position + Vector3.up * raycastHeight, agent.destination + Vector3.up * raycastHeight, Color.red);

        agent.SetDestination(chaseTarget);
        agent.speed = isWaiting ? 0.0f : chaseSpeed;
        bool spotted = Look();

        if(isWaiting)
        {
            if (!spotted && Time.time > waitTimer)
            {
                isWaiting = false;
                currentState = PatrolState.patrol;
            }
        }
        else if (IsCloseEnough(agent.destination) || isTouching)
        {
            if(spotted || isTouching)
            {
                OnPlayerCollision();
            }
            else
            {
                isWaiting = true;
                waitTimer = Time.time + 0.5f;
            }
            
        }
    }

    void OnPlayerCollision()
    {
        playerUI.TakeDamage();
    }

    public virtual void OnSpotPlayer(Transform player)
    {
        currentState = PatrolState.chase;
        chaseTarget = player.position;
    }

    void LateUpdate()
    {
        graphics.rotation = Quaternion.identity;
        graphics.localScale = new Vector3(isFacingRight ? 1.0f : -1.0f, 1.0f, 1.0f);
    }
}
