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
    float damageRate = 2.0f;

    public Text errorMessage;
    private float fadeOutTime;

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
    }

    public void Reset()
    {
        health = healthSprites.Length - 1;

        for (int i = 0; i < healthSprites.Length; ++i)
        {
            healthSprites[i].SetActive(true);
        }

        fadeOutTime = 0;
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
}
