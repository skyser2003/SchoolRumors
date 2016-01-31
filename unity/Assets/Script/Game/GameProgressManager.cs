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
        GameObject UIObject = GameObject.FindWithTag("UI");
        UIObject.GetComponent<PlayerUI>().Reset();
        UIObject.GetComponent<Cutscene>().StartCutscene(UIObject.GetComponent<CutsceneContainer>().cutsceneSlides);
        UIObject.GetComponent<AudioSource>().Play();
        Enemy.ResetAll();
        RitualItem.ResetAll();
        foreach(DoorPuzzleObstacle door in GameObject.FindObjectsOfType<DoorPuzzleObstacle>())
        {
            door.Close();
        }
    }
}