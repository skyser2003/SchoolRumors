using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cutscene : MonoBehaviour
{
    [System.Serializable]
    public class CutsceneSlide
    {
        public Sprite image;
        [Multiline] public string dialog;
    }

    public GameObject cutsceneObjects;
    CutsceneSlide[] slides;
    int currentSlide;
    public static bool isInCutscene;

    public Image image;
    public Text dialog;

    AudioSource audioSource;

    void Awake()
    {
        isInCutscene = false;
        audioSource = cutsceneObjects.GetComponent<AudioSource>();
    }

    public void StartCutscene(CutsceneSlide[] newSlides)
    {
        slides = newSlides;
        currentSlide = 0;
        isInCutscene = true;
        ShowSlide(currentSlide);
        cutsceneObjects.SetActive(true);
        audioSource.Play();
    }

    void ShowSlide(int slideNum)
    {
        image.sprite = slides[slideNum].image;
        image.gameObject.SetActive(image.sprite != null);
        dialog.text = slides[slideNum].dialog;
    }

	void Update ()
    {
	    if(isInCutscene)
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                audioSource.Play();

                currentSlide++;

                if (currentSlide >= slides.Length)
                {
                    isInCutscene = false;
                    cutsceneObjects.SetActive(false);
                }
                else
                {
                    ShowSlide(currentSlide);
                }
            }
        }
	}
}
