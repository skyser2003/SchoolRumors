using System.Collections.Generic;
using UnityEngine;

class PlayerAction : MonoBehaviour {
    private Player player;
    private PlayerMovement playerMove;

    private bool isSearching;
    private float curSearchTime;
    private FieldObject searchingObject;

    private MonoBehaviour prevNearestObj;

    private void Start()
    {
        player = GetComponent<Player>();
        playerMove = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        var dt = Time.deltaTime;

        if (isSearching == true) {
            // Keep searching
            if (playerMove.Direction.magnitude == 0) {
                curSearchTime -= dt;
                if (curSearchTime <= 0) {
                    searchingObject.Action(player);
                }
            }
            // Cancel
            else {
                isSearching = false;
                searchingObject = null;
            }
        }

        var nearestObj = GetClosestActionObject();

        if (nearestObj != null && nearestObj != prevNearestObj) {
            if (prevNearestObj != null) {
                prevNearestObj.GetComponentInChildren<Renderer>().material.shader = Shader.Find("Standard");
            }
            nearestObj.GetComponentInChildren<Renderer>().material.shader = Shader.Find("Unlit/Transparent Cutout");

            prevNearestObj = nearestObj;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (nearestObj != null) {
                var fieldObject = nearestObj as FieldObject;
                var handheldItem = nearestObj as HandheldItem;
                var puzzleObstacle = nearestObj as PuzzleObstacle;

                if (fieldObject != null) {
                    if (fieldObject.item != null) {
                        searchingObject = fieldObject;
                        isSearching = true;
                        curSearchTime = fieldObject.DelayTime;
                    }
                }
                else if (handheldItem != null) {
                    handheldItem.GetPickedUp(player);
                    handheldItem.GetComponentInChildren<Renderer>().material.shader = Shader.Find("Standard");
                }
                else if (puzzleObstacle != null) {
                    puzzleObstacle.Action(player);
                }
            }
        }
    }

    public MonoBehaviour GetClosestActionObject()
    {
        var fieldObject = FieldObjectManager.Instance.FindClosest(transform.position, 1);
        var handheldItem = HandheldItemManager.Instance.FindClosest(transform.position, 1);
        var puzzleObstacle = PuzzleObstacleManager.Instance.FindClosest(transform.position, 1);

        var list = new List<MonoBehaviour>();
        if (fieldObject != null) {
            list.Add(fieldObject);
        }
        if (handheldItem != null) {
            list.Add(handheldItem);
        }
        if (puzzleObstacle != null) {
            list.Add(puzzleObstacle);
        }

        if (list.Count == 0) {
            return null;
        }

        var pos2D = new Vector2(transform.position.x, transform.position.z);

        MonoBehaviour ret = null;
        float minDistance = -1;

        foreach (var obj in list) {
            var obj2D = new Vector2(obj.transform.position.x, obj.transform.position.z);

            float objDist = Vector2.Distance(pos2D, obj2D);
            if (minDistance == -1 || objDist < minDistance) {
                ret = obj;
                minDistance = objDist;
            }
        }

        return ret;
    }
}