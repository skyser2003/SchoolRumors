using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleElement : MonoBehaviour
{
    public GameObject go;
    public AudioSource audioSource;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            OnClick();
        });
    }

    void OnClick()
    {
        if(audioSource != null)
        {
            audioSource.Play();
        }

        go.SetActive(!go.activeSelf);
    }
}
