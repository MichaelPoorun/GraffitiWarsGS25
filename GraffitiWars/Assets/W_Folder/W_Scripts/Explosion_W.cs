using System.Collections;
using UnityEngine;

public class Explosion_W : MonoBehaviour
{

    public AudioSource AS;

    public GameObject AOEHitBox;
    void Awake()
    {
        AOEHitBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        AOEHitBox.SetActive(true);

        if(AOEHitBox == true)
        {
            AS.Play();
            Destroy(gameObject, 1.5f);
        }

    }

    /*IEnumerator Explosion()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }*/
}
