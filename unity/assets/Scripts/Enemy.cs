﻿using UnityEngine;
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

    float walkSpeed = 2.0f;
    float chaseSpeed = 3.0f;

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
        Vector3 enemyPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
        Vector3 targetPos = new Vector3(target.x, 0.0f, target.z);
        return Vector3.Distance(enemyPos, targetPos) < requiredDistance;
    }

    bool Look()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up;
        float seeDistance = currentState == PatrolState.patrol ? 10.0f : 20.0f;
        float arcAngle = currentState == PatrolState.patrol ? 35.0f : 220.0f;
        int numLines = currentState == PatrolState.patrol ? 16 : 64;

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
        Debug.DrawLine(transform.position + Vector3.up, agent.destination + Vector3.up, Color.red);

        agent.speed = chaseSpeed;
        bool spotted = Look();

        if (IsCloseEnough(agent.destination))
        {
            if(spotted)
            {
                OnPlayerCollision();
            }
            else
            {
                currentState = PatrolState.patrol;
            }
            
        }
    }

    void OnPlayerCollision()
    {
        Debug.Log("Hit");
    }

    public virtual void OnSpotPlayer(Transform player)
    {
        currentState = PatrolState.chase;
        agent.SetDestination(player.position);
    }

    void LateUpdate()
    {
        graphics.rotation = Quaternion.identity;
        graphics.localScale = new Vector3(isFacingRight ? 1.0f : -1.0f, 1.0f, 1.0f);
    }
}