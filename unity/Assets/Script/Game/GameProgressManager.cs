using UnityEngine;
using UnityEngine.SceneManagement;

class GameProgressManager {
    static private GameProgressManager instance;

    static public GameProgressManager Instance
    {
        get
        {
            if (instance == null) {
                instance = new GameProgressManager();
            }

            return instance;
        }
    }

    public void GameOver()
    {
        // TODO
        Debug.Log("Game over");
        RestartCurrentLevel();
    }

    public void RestartCurrentLevel()
    {
        GameObject.FindWithTag("Player").transform.position = ChangeFloor.lastTeleportPos;
        GameObject.FindWithTag("CameraSystem").GetComponent<PlayerCamera>().Reset();
        GameObject.FindWithTag("UI").GetComponent<PlayerUI>().Reset();
        Enemy.ResetAll();
    }
}