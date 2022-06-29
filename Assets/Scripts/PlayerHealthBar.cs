using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : Bar
{
    private void OnEnable()
    {
        Player.TookDamage += OnValueChanged;
    }

    private void OnDisable()
    {
        Player.TookDamage -= OnValueChanged;
    }
    
    private void OnValueChanged(Player player)
    {
        Slider.enabled = player.Health < player.MaxHealth;
        OnValueChanged(player.Health, player.MaxHealth);
    }
}
