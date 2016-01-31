﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VisitRoof : MonoBehaviour
{
    public static bool hasVisited = false;

    //public Text tooltip1;
    //public Text tooltip2;
    //public Text tooltip3;

    void OnTriggerEnter (Collider col)
    {
	    if(col.transform.tag == "Player")
        {
            hasVisited = true;
            gameObject.SetActive(false);
            //tooltip1.text = "Changed text 1";
            //tooltip2.text = "Changed text 2";
            //tooltip3.text = "Changed text 3";
        }
	}
	
}
