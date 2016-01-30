using UnityEngine;
using UnityEngine.SceneManagement;

class Floor : MonoBehaviour {
    public int StageLevel;

    private void Start()
    {
        // TODO : opening event scene
    }

    private bool CheckCondition(Player player)
    {
        return RitualItem.HaveAllRitualItems(player.Inventory, StageLevel);
    }

    public void ProceedToNextFloor(Player player)
    {
        if (CheckCondition(player) == false) {
            // TODO : show error message
            Debug.Log("Item count condition not met : Current item count = " + player.Inventory.Count(StageLevel) + ", required item count = " + RitualItem.Count(StageLevel));
        }
        else {
            Debug.Log("Proceed to next level!");
            SceneManager.LoadScene("Stage" + (StageLevel + 1));
        }
    }
}