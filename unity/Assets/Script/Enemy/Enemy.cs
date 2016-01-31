using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public static List<Enemy> enemies = new List<Enemy>();

    public GameObject wayPoints;
    Vector3[] wayPointPositions;
    Vector3 chaseTarget;
    protected NavMeshAgent agent;
    int currentDestination;

    const float requiredDistance = 0.1f;

    protected bool isFacingRight;
    bool isTouching;
    Transform graphics;

    protected PatrolState currentState;

    Vector3 startPos;
    public float walkSpeed = 2.0f;
    public float chaseSpeed = 3.0f;

    bool isWaiting;
    float waitTimer;

    PlayerUI playerUI;

    float raycastHeight = 0.5f;

    public AnimationCurve walkCurveBounce;
    public AnimationCurve walkCurveWave;
    public float animWalkSpeed = 1.0f;
    public float animChaseSpeed = 2.0f;

    public AudioSource audioFoot;
    float footStepTimer;
    public float footStepRate = 0.25f;

    public enum PatrolState
    {
        patrol,
        chase
    }

	void Awake ()
    {
        enemies.Add(this);
        startPos = transform.position;

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

    public void Reset()
    {
        transform.position = startPos;
        currentState = PatrolState.patrol;
        currentDestination = 0;
        agent.SetDestination(wayPointPositions[currentDestination]);
        isWaiting = false;
        StartCoroutine("WaitAndEnable"); // Hax to fix a bug: agent and this script get disabled sometimes randomly and this re-enables them
    }

    public static void ResetAll()
    {
        for (int i = 0; i < Enemy.enemies.Count; ++i)
        {
            Enemy.enemies[i].GetComponent<NavMeshAgent>().enabled = true;
            Enemy.enemies[i].enabled = true;
            Enemy.enemies[i].Reset();
        }
    }

    IEnumerator WaitAndEnable()
    {
        yield return new WaitForSeconds(1.0f);
        Enemy.EnableAll();
    }

    public static void EnableAll()
    {
        for (int i = 0; i < Enemy.enemies.Count; ++i)
        {
            Enemy.enemies[i].GetComponent<NavMeshAgent>().enabled = true;
            Enemy.enemies[i].enabled = true;
        }
    }
	
	void Update ()
    {
        if (Cutscene.isInCutscene)
            return;

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
        UpdateWalkAnim(animWalkSpeed);
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

    public float PatrolSeeDistance = 10f;
    public float DetectedSeeDistance = 20f;
    public float ArcAngleMin = 40f;
    public float ArcAngleMax = 220f;
    bool Look()
    {
        RaycastHit hit;
        float seeDistance = currentState == PatrolState.patrol ? PatrolSeeDistance : DetectedSeeDistance;
        float arcAngle = currentState == PatrolState.patrol ? ArcAngleMin : ArcAngleMax;
        int numLines = currentState == PatrolState.patrol ? 16 : 64;
        isTouching = false;

        Collider[] cols = Physics.OverlapSphere(transform.position, currentState == PatrolState.patrol ? 1.33f : 1.8f);
        for (int i = 0; i < cols.Length; ++i)
        {
            if (cols[i].transform.tag == "Player")
            {
                if (Physics.Linecast(transform.position + Vector3.up * 0.5f, cols[i].transform.position, out hit))
                {
                    if (hit.transform.tag == "Player")
                    {
                        Debug.DrawLine(transform.position + Vector3.up * 0.5f, hit.point, Color.red);
                        OnSpotPlayer(hit.transform);
                        isTouching = true;
                        return true;
                    }
                    else
                    {
                        Debug.DrawLine(transform.position + Vector3.up * 0.5f, hit.point, Color.yellow);
                    }
                }
            }
        }

        for (int h = 0; h < 2; ++h)
        {
            raycastHeight = h == 0 ? 0.5f : 0.95f;
            Vector3 rayOrigin = transform.position + Vector3.up * raycastHeight;

            for (int i = 0; i < numLines; ++i)
            {
                Vector3 rayDirection = Quaternion.AngleAxis(-1 * arcAngle / 2 + (i * arcAngle / numLines) + arcAngle / (2 * numLines), Vector3.up) * (isFacingRight ? Vector3.right : Vector3.left);

                if (Physics.Raycast(rayOrigin, rayDirection, out hit, seeDistance))
                {
                    Debug.DrawLine(rayOrigin, hit.point, Color.yellow);

                    if (hit.transform.tag == "Player")
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
        }

        return false;
        
    }

    void Chase()
    {
        Debug.DrawLine(transform.position + Vector3.up * raycastHeight, agent.destination + Vector3.up * 0.5f, Color.red);

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

        UpdateWalkAnim(isWaiting ? 0.0f : animWalkSpeed);
    }

    float animV;
    void UpdateWalkAnim(float speed)
    {
        animV += speed * Time.deltaTime;
        if(animV > 1.0f)
        {
            animV = 0.0f;
        }

        if (speed > 0.0f && audioFoot != null)
        {
            footStepTimer += Time.deltaTime;

            if (footStepTimer > footStepRate)
            {
                footStepRate = 0.25f * Random.Range(0.95f, 1.05f);
                footStepTimer = 0.0f;
                audioFoot.pitch = Random.Range(0.9f, 1.1f);
                audioFoot.Play();
            }
        }

        if (speed <= 0.0f)
        {
            animV -= Time.deltaTime;
            animV = Mathf.Clamp01(animV);
        }
         
        graphics.localPosition = Vector3.up * walkCurveBounce.Evaluate(animV);
        graphics.eulerAngles = new Vector3(0.0f, 0.0f, walkCurveWave.Evaluate(animV) * 10.0f);
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

    public virtual void LateUpdate()
    {
        graphics.localScale = new Vector3(isFacingRight ? 1.0f : -1.0f, 1.0f, 1.0f);
    }
}
