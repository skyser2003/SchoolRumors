using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Guard : Enemy
{
    public Transform spotlight;

    public override void OnSpotPlayer(Transform player)
    {
        GameProgressManager.Instance.GameOver();

        agent.enabled = false;
        enabled = false;
    }

    public override void LateUpdate()
    {
        base.LateUpdate();

        spotlight.localEulerAngles = new Vector3(spotlight.localEulerAngles.x, isFacingRight ? 90.0f : 270.0f , spotlight.localEulerAngles.z);
    }

}
