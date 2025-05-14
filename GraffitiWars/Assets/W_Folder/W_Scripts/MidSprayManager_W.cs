using System.Collections;
using UnityEngine;

public class MidSprayManager_W : MonoBehaviour
{
    public CameraLocks_W CLW;
    private string Event;

    public Renderer rendererM1;
    public Renderer rendererM2;
    public Renderer rendererM3;
    public Renderer rendererM4;
    public Renderer rendererM5;
    public Renderer rendererM6;
    public Renderer rendererM7;
    public Renderer rendererM8;
    public Renderer rendererM9;
    public Material changedM1;
    public Material changedM2;
    public Material changedM3;

    public GameObject S1;
    public GameObject S2;
    public GameObject S3;
    public GameObject S4;
    public GameObject S5;
    public GameObject S6;
    public GameObject S7;
    public GameObject S8;
    public GameObject S9;


    public bool Spray1 = false;
    public bool Spray2 = false;
    public bool Spray3 = false;
    public bool Spray4 = false;
    public bool Spray5 = false;
    public bool Spray6 = false;
    public bool Spray7 = false;
    public bool Spray8 = false;
    public bool Spray9 = false;
    public bool P1 = false;
    public bool P2 = false;
    public bool P3 = false;
    public bool rdyP1 = false;
    public bool rdyP2 = false;
    public bool rdyP3 = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Spray1 == true && Spray2 == true && Spray3 == true && P1 == false)
        {
            rdyP1 = true;
            P1 = true;
        }

        if (rdyP1 == true)
        {
            Event = "Part1End";
            CLW.HandleEvent(Event);
            rdyP1 = false;
        }

        if (Spray4 == true && Spray5 == true && Spray6 == true && P2 == false)
        {
            rdyP2 = true;
            P2 = true;
        }

        if (rdyP2 == true)
        {
            Event = "Part2End";
            CLW.HandleEvent(Event);
            rdyP2 = false;
        }

        if (Spray7 == true && Spray8 == true && Spray9 == true && P3 == false)
        {
            rdyP3 = true;
            P3 = true;
        }

        if (rdyP3 == true)
        {
            Event = "Part3End";
            CLW.HandleEvent(Event);
            rdyP3 = false;
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Spray1") && (Input.GetButtonDown("Interact")))
        {
            Spray1 = true;
            CLW.Arrow1.SetActive(false);
            rendererM1.material = changedM1;
        }
        else if (other.CompareTag("Spray2") && (Input.GetButtonDown("Interact")))
        {
            Spray2 = true;
            CLW.Arrow2.SetActive(false);
            rendererM2.material = changedM2;
        }
        else if (other.CompareTag("Spray3") && (Input.GetButtonDown("Interact")))
        {
            Spray3 = true;
            CLW.Arrow3.SetActive(false);
            rendererM3.material = changedM3;
        }
        else if (other.CompareTag("Spray4") && (Input.GetButtonDown("Interact")))
        {
            Spray4 = true;
            rendererM4.material = changedM1;
        }
        else if (other.CompareTag("Spray5") && (Input.GetButtonDown("Interact")))
        {
            Spray5 = true;
            rendererM5.material = changedM2;
        }
        else if (other.CompareTag("Spray6") && (Input.GetButtonDown("Interact")))
        {
            Spray6 = true;
            rendererM6.material = changedM3;
        }
        else if (other.CompareTag("Spray7") && (Input.GetButtonDown("Interact")))
        {
            Spray7 = true;
            rendererM7.material = changedM1;
        }
        else if (other.CompareTag("Spray8") && (Input.GetButtonDown("Interact")))
        {
            Spray8 = true;
            rendererM8.material = changedM2;
        }
        else if (other.CompareTag("Spray9") && (Input.GetButtonDown("Interact")))
        {
            Spray9 = true;
            rendererM9.material = changedM3;
        }
    }
}
