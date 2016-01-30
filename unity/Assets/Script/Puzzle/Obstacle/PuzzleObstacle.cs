using UnityEngine;

abstract class PuzzleObstacle : MonoBehaviour {
    protected virtual void Start()
    {
        PuzzleObstacleManager.Instance.Add(this);
    }

    public abstract void Action(Player player);
}