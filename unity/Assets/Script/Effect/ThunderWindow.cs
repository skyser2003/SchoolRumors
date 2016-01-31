using UnityEngine;
using System.Collections;

public class ThunderWindow : MonoBehaviour {

    public GameObject NormalWindow;
    public GameObject GhostWindow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowNormalWindow()
    {
        NormalWindow.gameObject.SetActive(true);
        GhostWindow.gameObject.SetActive(false);
    }

    public void ShowGhostWindow()
    {
        NormalWindow.gameObject.SetActive(false);
        GhostWindow.gameObject.SetActive(true);
    }
}
