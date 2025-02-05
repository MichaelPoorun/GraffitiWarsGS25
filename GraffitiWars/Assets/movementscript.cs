using UnityEngine;

public class movementscript : MonoBehaviour
{

    public float speed;
    Vector3 myDir;
    // Update is called once per frame
    void Update()
    {
        //Dir();
        transform.Translate(Dir());
        //simple keycode checks - start with w for up
        if (Input.GetKey(KeyCode.W))
        {
            //for now we'll just directly call the transform and translate it
            transform.Translate(0, 0, speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed);
        }

    if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * speed);
        }
        static Vector3 Dir()
        {
            Vector3 Dir;
            float z = Input.GetAxis("Vertical");
            float x = Input.GetAxis("Horizontal");
            Dir = new Vector3(x, 0, z);
            Debug.Log(Dir);
            return Dir; //return the value
        }

    }
}
