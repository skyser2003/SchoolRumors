using UnityEngine;

abstract class PuzzleObstacle : MonoBehaviour {
    public float DelayTime;

    protected virtual void Start()
    {
        PuzzleObstacleManager.Instance.Add(this);
    }

    public abstract bool CheckIfActionIsPossible(Player player);
    public abstract void Action(Player player);
}