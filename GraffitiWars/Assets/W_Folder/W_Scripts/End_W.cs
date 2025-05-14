using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_W : MonoBehaviour
{
    bool ready = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ready = false;
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (ready == true)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(1);
            }

            if (Input.GetButtonDown("Punch") || Input.GetButtonDown("Kick") || Input.GetButtonDown("Jump") || Input.GetButtonDown("Block") || Input.GetButtonDown("Spray") || Input.GetButtonDown("Throw"))
            {
                SceneManager.LoadScene(1);
            }
        }
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        ready = true;
    }
}
