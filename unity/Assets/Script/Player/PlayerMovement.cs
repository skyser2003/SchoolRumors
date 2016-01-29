using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    private Vector3 direction = new Vector3();

    public float Speed;

    private void FixedUpdate()
    {
        var dt = Time.fixedDeltaTime;

        transform.localPosition += direction * Speed * dt;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            direction.x = -1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            direction.x = 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            direction.z = -1;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            direction.z = 1;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            direction.x = 0;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow)) {
            direction.x = 0;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow)) {
            direction.z = 0;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow)) {
            direction.z = 0;
        }
    }
}
