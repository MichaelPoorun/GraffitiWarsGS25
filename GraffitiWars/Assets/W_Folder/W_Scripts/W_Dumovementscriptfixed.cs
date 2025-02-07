using UnityEngine;

public class W_Dumovementscriptfixed : MonoBehaviour
{

    public float speed;

    // Update is called once per frame
    void Update()
    {
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        transform.Translate (new Vector3(x * speed, 0, z * speed) * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * speed);
        }
    }
}