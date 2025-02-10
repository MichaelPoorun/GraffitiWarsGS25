using UnityEngine;

public class DuHealthBar{
    [SerializeField] private ImageConversion _healthbarsprite;

    public void UpdateHealthBar(float maxHealth, float currentHealth);
    _healthbarsprite.fillAmount = CurrentHealth/ MaxHealth;
}
