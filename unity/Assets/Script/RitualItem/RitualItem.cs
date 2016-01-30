using UnityEngine;
using System.Collections.Generic;

class RitualItem : MonoBehaviour
{
    public static List<RitualItem> allItems = new List<RitualItem>();
    Vector3 startPos;

    void Awake()
    {
        allItems.Add(this);
    }

    public static void ResetAll()
    {
        for(int i = 0; i < allItems.Count; ++i)
        {
            allItems[i].Reset();
        }
    }

    public void Reset()
    {
        transform.position = startPos;
    }

    public int id;
}