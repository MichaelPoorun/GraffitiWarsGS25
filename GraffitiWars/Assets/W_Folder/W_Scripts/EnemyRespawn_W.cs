using UnityEngine;
using UnityEngine.Timeline;

public class EnemyRespawn_W : MonoBehaviour
{
    public GameObject Enemy;
    public HealthSystem HP;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(Enemy, new Vector3(35, .5f, 0), Quaternion.Euler(0, 90, 0));
        HP = Enemy.GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP.Alive == false)
        {
            Debug.Log("SPAWNED");
            Instantiate(Enemy, new Vector3(35, .5f, 0), Quaternion.Euler(0, 90, 0));
            HP.Alive = true;
        }
    }
}
