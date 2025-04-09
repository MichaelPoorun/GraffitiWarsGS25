using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar_W : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void Start()
    {
        if (slider == null)
        {
            Debug.Log("Slider not asssigned to PlayerHealthBar Script");
        }
    }
    public void UpdateBossHealthBar(float currentHealth, float maxHealth)
    {
        slider.value = currentHealth / maxHealth;
    }

    void Update()
    {

    }
}
