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
        return player.Inventory.Count == RitualItem.allItems.Count;
    }

    public void ProceedToNextFloor(Player player)
    {
        if (CheckCondition(player) == false) {
            // TODO : show error message
            Debug.Log("Item count condition not met : Current item count = " + player.Inventory.Count + ", required item count = " + RitualItem.allItems.Count);
        }
        else {
            Debug.Log("Proceed to next level!");
            SceneManager.LoadScene("Stage" + (StageLevel + 1));
        }
    }
}