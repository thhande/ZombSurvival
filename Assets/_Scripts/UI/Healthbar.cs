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
        if (playerHealth != null) playerHealth.OnHealthChange += PlayerHealthChange;
        PlayerHealthChange();
    }

    private void Start()
    {
        LoadComponents();
        playerHealth.OnHealthChange += PlayerHealthChange;
        PlayerHealthChange();
    }

    public void SetPlayer(PlayerDamageReceiver newplayerHealth)
    {
        this.playerHealth = newplayerHealth;
        playerHealth.OnHealthChange += PlayerHealthChange;
        PlayerHealthChange();
    }






    public void PlayerHealthChange()
    {
        if (playerHealth == null) return;
        slide.value = playerHealth.Health;
        slide.maxValue = playerHealth.MaxHealth;
    }

    private void LoadComponents()
    {
        if (slide == null) slide = GetComponent<Slider>();
        if (playerHealth == null) playerHealth = FindObjectOfType<PlayerDamageReceiver>();
    }
    private void OnValidate()
    {

    }
}
