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

    private List<KeyHandheldItem> handheldItemList = new List<KeyHandheldItem>();

    public void Add(KeyHandheldItem handheldItem)
    {
        handheldItemList.Add(handheldItem);
    }

    public void Remove(KeyHandheldItem handheldItem)
    {
        handheldItemList.Remove(handheldItem);
    }

    public KeyHandheldItem FindClosest(Vector3 pos, float maxDistance)
    {
        KeyHandheldItem closest = null;
        float closestDistance = 0;

        var pos2D = new Vector2(pos.x, pos.z);

        foreach (var handheldItem in handheldItemList) {
            var handheldItemPos2D = new Vector2(handheldItem.transform.position.x, handheldItem.transform.position.y);

            var distance = Vector2.Distance(pos2D, handheldItemPos2D);
            if (distance <= maxDistance && (closest == null || distance < closestDistance)) {
                closest = handheldItem;
            }
        }

        return closest;
    }
}