using System.Collections.Generic;
using UnityEngine;

class FieldObjectManager {
    static private FieldObjectManager instance;

    static public FieldObjectManager Instance
    {
        get
        {
            if (instance == null) {
                instance = new FieldObjectManager();
            }

            return instance;
        }
    }

    private List<FieldObject> objList = new List<FieldObject>();

    public void Add(FieldObject obj)
    {
        objList.Add(obj);
    }

    public FieldObject FindClosest(Vector3 pos, float maxDistance)
    {
        FieldObject closest = null;
        float closestDistance = 0;

        var pos2D = new Vector2(pos.x, pos.z);

        foreach (var obj in objList) {
            var objPos2D = new Vector2(obj.transform.position.x, obj.transform.position.y);

            var distance = Vector2.Distance(pos2D, objPos2D);
            if (distance <= maxDistance && (closest == null || distance < closestDistance)) {
                closest = obj;
            }
        }

        return closest;
    }
}