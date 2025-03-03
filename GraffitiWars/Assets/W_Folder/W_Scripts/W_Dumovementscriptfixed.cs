using UnityEngine;

public class W_Dumovementscriptfixed : MonoBehaviour
{

    public float speed;

    public float jumpPower;

    public bool OnGround;

    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        transform.Translate (new Vector3(x * speed, 0, z * speed) * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && OnGround == true)
        {
            transform.Translate(new Vector3(0,  1 * jumpPower, 0) * Time.deltaTime);
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            OnGround = true;
        }
    }

    public void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            OnGround = false;
        }
    }
}