using UnityEngine;

class PlayerCamera : MonoBehaviour {

    public Transform mainCamera;
    public BoxCollider cameraArea;
    public Transform player;
    PlayerMovement playerScript;

    void Start()
    {
        transform.position = player.position;
        playerScript = player.GetComponent<PlayerMovement>();
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
    }

}