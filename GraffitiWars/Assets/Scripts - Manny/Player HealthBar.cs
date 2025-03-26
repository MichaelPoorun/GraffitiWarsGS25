using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    void Start()
    {
        if (slider == null)
        {
            Debug.Log("Slider not asssigned to PlayerHealthBar Script");
        }
    }
    public void UpdatePlayerHealthBar(float currentHealth, float maxHealth)
    {
        slider.value = currentHealth / maxHealth;
    }

    void Update()
    {

    }
}
