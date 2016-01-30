using UnityEngine;
using System.Collections;

public class ChangeFloor : MonoBehaviour
{
    public static Vector3 lastTeleportPos;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

	void OnTriggerEnter (Collider col)
    {
	    if(col.transform.tag == "Player")
        {
            audioSource.Play();
            lastTeleportPos = transform.GetChild(0).position;
            col.transform.position = lastTeleportPos;
            GameObject.FindWithTag("CameraSystem").GetComponent<PlayerCamera>().Reset();
            Enemy.ResetAll();
        }
	}
}
