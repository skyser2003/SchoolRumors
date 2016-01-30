﻿using UnityEngine;

class PlayerCamera : MonoBehaviour {

    public Transform mainCamera;
    public BoxCollider cameraArea;
    public Transform player;
    PlayerMovement playerScript;
    public Vector3 DeltaPosToBound;

    void Start()
    {
        playerScript = player.GetComponent<PlayerMovement>();
        Reset();
    }

    public void Reset()
    {
        transform.position = player.position;
    }

    void FixedUpdate()
    {
        Bounds bounds = cameraArea.bounds;
        Vector3 target = transform.position;

        if (player.position.x > bounds.max.x )
        {
            target.x += playerScript.Speed * Time.fixedDeltaTime;
        }
        else if(player.position.x < bounds.min.x)
        {
            target.x -= playerScript.Speed * Time.fixedDeltaTime;
        }

        if (player.position.z > bounds.max.z)
        {
            target.z += playerScript.Speed * Time.fixedDeltaTime;
        }
        else if (player.position.z < bounds.min.z)
        {
            target.z -= playerScript.Speed * Time.fixedDeltaTime;
        }

        transform.position = target;

        mainCamera.localEulerAngles = new Vector3(
            mainCamera.localEulerAngles.x,
            (player.position.x - transform.position.x) * 3.0f,
            mainCamera.localEulerAngles.z);

        Vector3 newcampos = Vector3.Lerp(mainCamera.transform.position, cameraArea.transform.position + DeltaPosToBound, 0.5f);
        //raycast
        float distance = Vector3.Distance(cameraArea.transform.position, newcampos);
        Ray ray = new Ray(cameraArea.transform.position, (newcampos - cameraArea.transform.position).normalized);
        RaycastHit[] hits = Physics.RaycastAll(ray, distance);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.isStatic)
            {
                newcampos = hit.point;
                break;
            }
        }
        mainCamera.transform.position = newcampos;
    }

}