using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject[] healthSprites;
    int health;
    public int MaxHealth { get { return healthSprites.Length; } }

    float damagedTimer;
    float damageRate = 0.75f;

    public Text errorMessage;
    private float fadeOutTime;

    public GameObject[] ritualItemSprites;

    public AudioSource audioDamage;

    private Image gaugeBack;
    private Image gaugeFront;
    private float gaugeFillTime;
    private float gaugeLeftTime;

    void Awake()
    {
        Reset();
    }

    void Update()
    {
        var dt = Time.deltaTime;

        if (fadeOutTime != 0) {
            fadeOutTime -= dt;

            if(fadeOutTime <= 0) {
                fadeOutTime = 0;
                errorMessage.text = "";
            }
        }

        if(gaugeFillTime != -1) {
            gaugeLeftTime -= dt;

            if(gaugeLeftTime <= 0) {
                gaugeBack.gameObject.SetActive(false);
                gaugeFront.gameObject.SetActive(false);
                gaugeFillTime = -1;
                gaugeLeftTime = -1;
            }
            else {
                gaugeFront.fillAmount = (gaugeFillTime - gaugeLeftTime) / gaugeFillTime;
            }
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage();
        }
    }

    public void Reset()
    {
        health = healthSprites.Length - 1;

        for (int i = 0; i < healthSprites.Length; ++i)
        {
            healthSprites[i].SetActive(true);
        }

        errorMessage.text = "";
        fadeOutTime = 0;

        for (int i = 0; i < 3; ++i)
        {
            SetRitualItem(i, false);
        }

        gaugeBack = transform.FindChild("GaugeBack").GetComponent<Image>();
        gaugeFront = transform.FindChild("GaugeFront").GetComponent<Image>();

        gaugeBack.gameObject.SetActive(false);
        gaugeFront.gameObject.SetActive(false);
        gaugeFillTime = -1;
        gaugeLeftTime = -1;
    }

    void OnLevelWasLoaded(int level)
    {
        Reset();
    }

    public void TakeDamage()
    {
        if (Time.time > damagedTimer)
        {
            Debug.Log("Player got damaged!");

            audioDamage.Play();

            damagedTimer = Time.time + damageRate;
            health--;

            for(int i = 0; i < healthSprites.Length; ++i)
            {
                healthSprites[i].SetActive(health >= i);
            }

            if (health < 0)
            {
                GameProgressManager.Instance.GameOver();
            }
        }
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        health = Mathf.Min(health, MaxHealth);

        for (int i = 0; i < healthSprites.Length; ++i) {
            healthSprites[i].SetActive(health >= i);
        }
    }

    public void SetErrorMessage(string text, float fadeOutTime)
    {
        errorMessage.text = text;
        this.fadeOutTime = fadeOutTime;
    }

    public void SetRitualItem(int id, bool hasItem)
    {
        ritualItemSprites[id].SetActive(hasItem);
    }

    public void SetGaugeFillTime(float time)
    {
        gaugeFillTime = time;
        gaugeLeftTime = time;
        gaugeFront.fillAmount = 0;
        gaugeBack.gameObject.SetActive(true);
        gaugeFront.gameObject.SetActive(true);
    }
}
