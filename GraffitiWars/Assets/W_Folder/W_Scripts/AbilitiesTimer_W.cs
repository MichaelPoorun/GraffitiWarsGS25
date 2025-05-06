using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesTimer_W : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn;
    public Text TimerTxt;
    public RawImage sprayColor;

    void Start()
    {
        TimerOn = true;
        TimerTxt.enabled = true;
        sprayColor.color = Color.gray;
    }
    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateSprayTimer(TimeLeft);
            }
        }
        else
        {
            TimerOn = false;
            TimerTxt.enabled = false;
        }
    }

    void updateSprayTimer(float currentTime)
    {
        currentTime += 1;
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{00}", seconds);

        if (currentTime > 0)
        {
            sprayColor.color = Color.gray;
        }
    }
}
