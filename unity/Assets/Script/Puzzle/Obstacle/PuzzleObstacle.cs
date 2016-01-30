using UnityEngine;

abstract class PuzzleObstacle : MonoBehaviour {
    private void Start()
    {
        PuzzleObstacleManager.Instance.Add(this);
    }

    public abstract void Action(Player player);
}