using UnityEngine;
using System.Collections;

public class SaveRitualItem : MonoBehaviour
{
    public int ritualItemID;

	void OnTriggerEnter (Collider col)
    {
	    if(col.transform.tag == "Player")
        {
            RitualItem.savedItems[ritualItemID] = true;
            gameObject.SetActive(false);
        }
	}
}
