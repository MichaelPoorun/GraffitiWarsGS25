using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DuHealthBar :MonoBehaviour{
    
    public HealthSystem hp;
    public Image HealthBar;
    public void Updatehp(float maxHealth, float currentHealth)
    {
        float perc = currentHealth / maxHealth;
        HealthBar.transform.localScale = new Vector3(perc * 10, 0.3f, 1);
    }
    
      
}
