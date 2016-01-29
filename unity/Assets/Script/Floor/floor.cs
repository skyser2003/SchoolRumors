using UnityEngine;
using UnityEngine.SceneManagement;

class Floor : MonoBehaviour {
    public int StageLevel;
    public int FloorItemCount;

    private void Start()
    {
        // TODO : opening event scene

    }

    private bool CheckCondition(Player player)
    {
        if (player.Inventory.Count == FloorItemCount) {
            return true;
        }
        else {
            return false;
        }
    }

    public void ProceedToNextFloor(Player player)
    {
        if (CheckCondition(player) == true) {
            // TODO : show error message
        }
        else {
            SceneManager.LoadScene("Stage" + (StageLevel + 1));
        }
    }
}