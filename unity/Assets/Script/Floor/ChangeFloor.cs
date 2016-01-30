using UnityEngine;
using System.Collections;

public class ChangeFloor : MonoBehaviour
{
	void OnTriggerEnter (Collider col)
    {
	    if(col.transform.tag == "Player")
        {
            col.transform.position = transform.GetChild(0).position;
        }
	}
}
