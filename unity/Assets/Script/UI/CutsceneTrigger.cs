using UnityEngine;
using System.Collections;

public class CutsceneTrigger : MonoBehaviour
{
    public Cutscene.CutsceneSlide[] cutsceneSlides;

	void OnTriggerEnter (Collider col)
    {
	    if(col.transform.tag == "Player")
        {
            if (cutsceneSlides != null)
            {
                GameObject.FindWithTag("UI").GetComponent<Cutscene>().StartCutscene(cutsceneSlides);
                gameObject.SetActive(false);
            }
        }
	}
}
