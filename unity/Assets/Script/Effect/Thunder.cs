using UnityEngine;
using System.Collections;

public class Thunder : MonoBehaviour
{
    public Color color;
    public AnimationCurve curve;
    public float speed = 0.1f;
    AudioSource audio;

    public ThunderWindow Window;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

	void OnTriggerEnter (Collider col)
    {
	    if(col.transform.tag == "Player" && !audio.isPlaying)
        {
            audio.Play();
            StartCoroutine("ThunderAnim");
        }
	}

    IEnumerator ThunderAnim()
    {
        if (Window != null)
        {
            Window.ShowGhostWindow();
        }

        Color originalColor = RenderSettings.ambientLight;
        float originalIntensity = RenderSettings.ambientIntensity;

        float v = 0.0f;

        while(v < 1.0f)
        {
            if(v > 0.2f)
            {
                if (Window != null)
                {
                    Window.ShowNormalWindow();
                }
            }
            RenderSettings.ambientLight =  Color.Lerp(originalColor, color, curve.Evaluate(v));
            RenderSettings.ambientIntensity = Mathf.Lerp(originalIntensity, 2.0f, curve.Evaluate(v));
            v += Time.deltaTime * speed;
            yield return null;
        }

        RenderSettings.ambientLight = originalColor;
        RenderSettings.ambientIntensity = originalIntensity;

        gameObject.SetActive(false);

    }
}
