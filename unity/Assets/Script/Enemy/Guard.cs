using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Guard : Enemy
{
    public override void OnSpotPlayer(Transform player)
    {
        GameProgressManager.Instance.GameOver();

        agent.enabled = false;
        enabled = false;
    }

}
