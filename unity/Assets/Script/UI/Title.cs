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

    bool isSplashing;
    public Image spash;

    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine("SplashAnim");
    }

    IEnumerator SplashAnim()
    {
        isSplashing = true;

        yield return new WaitForSeconds(3.0f);

        float v = 1.0f;

        while(v > 0.0f)
        {
            if(spash != null)
                spash.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Clamp01(v));

            v -= Time.deltaTime * 3.0f;
            yield return null;
        }

        if(spash != null)
            spash.gameObject.SetActive(false);

        isSplashing = false;
    }

    void Update ()
    {
        if(isSplashing)
            return;

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
