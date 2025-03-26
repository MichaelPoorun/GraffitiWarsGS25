using UnityEngine;
using UnityEngine.UI;
public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    void Start()
    {
        camera = Camera.main;
    }
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        slider.value = currentHealth / maxHealth;
    }

    void Update()
    {
        transform.rotation = camera.transform.rotation; // keeps the health bar facing the camera
        transform.position = target.position + offset; //updates bar position relative to the target

    }
}
