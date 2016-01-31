using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

    public string SceneName = "Stage_RoofToBasement";

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            //load scene
            Application.LoadLevel(SceneName);
        }
    }

}
