using System.Collections.Generic;
using UnityEngine;

class HandheldItemManager {
    static private HandheldItemManager instance;

    static public HandheldItemManager Instance
    {
        get
        {
            if (instance == null) {
                instance = new HandheldItemManager();
            }

            return instance;
        }
    }

    private List<HandheldItem> handheldItemList = new List<HandheldItem>();

    public void Add(HandheldItem handheldItem)
    {
        handheldItemList.Add(handheldItem);
    }

    public void Remove(HandheldItem handheldItem)
    {
        handheldItemList.Remove(handheldItem);
    }

    public HandheldItem FindClosest(Vector3 pos, float maxDistance)
    {
        //remove all nulls
        for (int iter = 0; iter < handheldItemList.Count;)
        {
            if (handheldItemList[iter] == null)
            {
                handheldItemList.RemoveAt(iter);
            }
            else
            {
                iter++;
            }
        }

        HandheldItem closest = null;
        float closestDistance = 0;

        var pos2D = new Vector2(pos.x, pos.z);

        foreach (var handheldItem in handheldItemList) {
            var handheldItemPos2D = new Vector2(handheldItem.transform.position.x, handheldItem.transform.position.z);

            var distance = Vector2.Distance(pos2D, handheldItemPos2D);
            if (distance <= maxDistance && (closest == null || distance < closestDistance)) {
                closest = handheldItem;
            }
        }

        return closest;
    }
}