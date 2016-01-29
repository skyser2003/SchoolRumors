using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Guard : Enemy
{
    public override void OnSpotPlayer(Transform player)
    {
        Debug.Log("Game over!");
        // TODO: Implement game over
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        agent.enabled = false;
        enabled = false;
    }

}
