using UnityEngine;
using System.Collections;

public class ThunderEffect : MonoBehaviour {

    public bool IsPlaying;
    public float TotalAnimTime;
    public float CurAnimTime;
    public AnimationCurve LightAnimation;
    public Light MyLight;
    public float MaxIntensity;
	// Use this for initialization
	void Start () {
        StartThunder();
    }
	
	// Update is called once per frame
	void Update () {
        if (IsPlaying)
        {
            CurAnimTime += Time.deltaTime;
            MyLight.intensity = LightAnimation.Evaluate(CurAnimTime / TotalAnimTime) * MaxIntensity;

            if(CurAnimTime > TotalAnimTime)
            {
                IsPlaying = false;
            }
        }
        
	}

    public void StartThunder()
    {
        gameObject.SetActive(true);
        IsPlaying = true;
        CurAnimTime = 0f;
    }
}
