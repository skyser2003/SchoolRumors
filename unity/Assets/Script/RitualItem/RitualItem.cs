using UnityEngine;
using System.Collections.Generic;

class RitualItem : MonoBehaviour {
    public static List<RitualItem> allItems = new List<RitualItem>();
    Vector3 startPos;

    static public bool HaveAllRitualItems(RitualItemInventory inventory, int floorLevel)
    {
        int floorCount = Count(floorLevel);
        int playerCount = inventory.Count(floorLevel);

        return floorCount == playerCount;
    }

    static public int Count(int floorLevel)
    {
        int count = 0;

        foreach (var item in allItems) {
            if (item.FloorLevel == floorLevel) {
                ++count;
            }
        }

        return count;
    }

    void Awake()
    {
        allItems.Add(this);
        startPos = transform.position;
    }

    public static void ResetAll()
    {
        if (VisitRoof.hasVisited)
            return;

        for (int i = 0; i < allItems.Count; ++i) {
            allItems[i].Reset();
        }
    }

    public void Reset()
    {
        transform.position = startPos;
    }

    public int FloorLevel;
    public int id;
}