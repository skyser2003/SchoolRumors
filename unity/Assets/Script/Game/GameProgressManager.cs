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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}