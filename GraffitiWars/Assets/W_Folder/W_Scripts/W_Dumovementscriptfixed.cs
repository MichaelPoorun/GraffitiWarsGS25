using UnityEngine;

public class W_Dumovementscriptfixed : MonoBehaviour
{
    public W_PlayerBlockState Block;

    public float speed;

    public float jumpPower;

    public bool OnGround;

    public HealthSystem HP;
    public int damage = 25;

    public void Start()
    {
        W_PlayerStateManager playerStateManager = GetComponent<W_PlayerStateManager>();
        if (playerStateManager != null)
        {
            Block = playerStateManager.BlockState;
        }
        else
        {
            Debug.LogError("PlayerStateManager not found on player object!");
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hit_Player" && Block.IsBlocking == false)
        {
            Debug.Log("Player Took 25 Damage");
            HP.TakeDamage(damage);
        }
        else if (other.gameObject.tag == "Hit_Player" && Block.IsBlocking == true)
        {
            
            Debug.Log("Player Blocked Enemy Attack");
        }
    }
}