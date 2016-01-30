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

    void Awake()
    {
        isInCutscene = false;
    }

    public void StartCutscene(CutsceneSlide[] newSlides)
    {
        slides = newSlides;
        currentSlide = 0;
        isInCutscene = true;
        ShowSlide(currentSlide);
        cutsceneObjects.SetActive(true);
    }

    void ShowSlide(int slideNum)
    {
        image.sprite = slides[slideNum].image;
        dialog.text = slides[slideNum].dialog;
    }

	void Update ()
    {
	    if(isInCutscene)
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
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
