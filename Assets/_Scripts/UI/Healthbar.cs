using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{


    [SerializeField] private Slider slide;
    [SerializeField] private PlayerDamageReceiver playerHealth;

    private void Awake()
    {
        LoadComponents();
        playerHealth.OnHealthChange += PlayerHealthChange;
        PlayerHealthChange();
    }




    public void PlayerHealthChange()
    {
        slide.value = playerHealth.Health;
        slide.maxValue = playerHealth.MaxHealth;
        Debug.Log("Health: " + playerHealth.Health);
    }

    private void LoadComponents()
    {
        if (slide == null) slide = GetComponent<Slider>();
        if (playerHealth == null) playerHealth = FindObjectOfType<PlayerDamageReceiver>();
    }
    private void OnValidate()
    {
        LoadComponents();
    }
}
