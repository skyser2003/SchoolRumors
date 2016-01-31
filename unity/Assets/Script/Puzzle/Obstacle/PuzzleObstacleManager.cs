using System.Collections.Generic;
using UnityEngine;

class PuzzleObstacleManager {
    static private PuzzleObstacleManager instance;

    static public PuzzleObstacleManager Instance
    {
        get
        {
            if (instance == null) {
                instance = new PuzzleObstacleManager();
            }

            return instance;
        }
    }

    private List<PuzzleObstacle> obstacleList = new List<PuzzleObstacle>();

    public void Add(PuzzleObstacle obstacle)
    {
        obstacleList.Add(obstacle);
    }

    public PuzzleObstacle FindClosest(Vector3 pos, float maxDistance)
    {

        PuzzleObstacle closest = null;
        float closestDistance = 0;

        var pos2D = new Vector2(pos.x, pos.z);

        foreach (var obstacle in obstacleList) {
            var obstaclePos2D = new Vector2(obstacle.transform.position.x, obstacle.transform.position.z);

            var distance = Vector2.Distance(pos2D, obstaclePos2D);
            if (distance <= maxDistance && (closest == null || distance < closestDistance)) {
                closest = obstacle;
            }
        }

        return closest;
    }
}