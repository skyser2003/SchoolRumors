using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Title : MonoBehaviour
{
    public Sprite title1;
    public Sprite title2;
    Image image;

    float timer;
    float rate = 0.6f;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update ()
    {
	    if(Time.time > timer)
        {
            timer = Time.time + rate;

            if(image.sprite == title1)
            {
                image.sprite = title2;
            }
            else
            {
                image.sprite = title1;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Stage");
        }
	}
}
