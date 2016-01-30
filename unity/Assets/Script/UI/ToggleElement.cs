using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleElement : MonoBehaviour
{
    private bool visible = false; //show or hide at start
    public GameObject go;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            OnClick();
        });
        OnClick(); //set initial state
    }

    void OnClick()
    {
        visible = !visible;
        go.SetActive(!visible);
    }
}
