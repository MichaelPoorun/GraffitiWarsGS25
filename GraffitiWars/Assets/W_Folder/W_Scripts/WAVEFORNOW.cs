using UnityEngine;

public class WAVEFORNOW : MonoBehaviour
{
    public GameObject EnemyP1_1;
    public GameObject EnemyP1_2;
    public GameObject EnemyP1_3;
    public GameObject EnemyP1_4;
    public GameObject EnemyP1_5;
    public GameObject EnemyP1_6;
    public GameObject EnemyP1_7;
    public GameObject EnemyP1_8;
    public GameObject EnemyP1_9;
    public GameObject P1Wall1;
    public GameObject P1Wall2;
    public GameObject P1Wall3;
    public GameObject P1Wall4;
    public GameObject P1Wall5;
    public GameObject P1Wall6;
    public GameObject UI;

    private bool EK1 = false;
    private bool EK2 = false;
    private bool EK3 = false;
    private bool EK4 = false;
    private bool EK5 = false;
    private bool EK6 = false;
    private bool EK7 = false;
    private bool EK8 = false;
    private bool EK9 = false;


    private void Awake()
    {
        P1Wall1.SetActive(false);
        P1Wall2.SetActive(false);
        P1Wall3.SetActive(false);
        P1Wall4.SetActive(false);
        P1Wall5.SetActive(false);
        P1Wall6.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyP1_1.SetActive(false);
        EnemyP1_2.SetActive(false);
        EnemyP1_3.SetActive(false);
        EnemyP1_4.SetActive(false);
        EnemyP1_5.SetActive(false);
        EnemyP1_6.SetActive(false);
        EnemyP1_7.SetActive(false);
        EnemyP1_8.SetActive(false);
        EnemyP1_9.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyP1_1 == null)
        {
            EK1 = true;
        }
        if (EnemyP1_2 == null)
        {
            EK2 = true;
        }
        if (EnemyP1_3 == null)
        {
            EK3 = true;
        }
        if (EnemyP1_4 == null)
        {
            EK4 = true;
        }
        if (EnemyP1_5 == null)
        {
            EK5 = true;
        }
        if (EnemyP1_6 == null)
        {
            EK6 = true;
        }
        if (EnemyP1_7 == null)
        {
            EK7 = true;
        }
        if (EnemyP1_8 == null)
        {
            EK8 = true;
        }
        if (EnemyP1_9 == null)
        {
            EK9 = true;
        }

        if (EK1 == true && EK2 == true && EK3 == true)
        {
            P1Wall1.SetActive(false);
            P1Wall2.SetActive(false);
            UI.SetActive(true);
        }
        if (EK4 == true && EK5 == true && EK6 == true)
        {
            P1Wall3.SetActive(false);
            P1Wall4.SetActive(false);
            UI.SetActive(true);
        }
        if (EK7 == true && EK8 == true && EK9 == true)
        {
            P1Wall5.SetActive(false);
            P1Wall6.SetActive(false);
            UI.SetActive(true);
        }
    }
}
