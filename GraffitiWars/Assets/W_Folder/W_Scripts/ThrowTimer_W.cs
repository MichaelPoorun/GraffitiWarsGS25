using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ThrowTimer_W : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn;
    public Text throwTimerTxt;
    public RawImage throwColor;

    void Start()
    {
        TimerOn = true;
        throwTimerTxt.enabled = true;
        throwColor.color = Color.gray;
    }
    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateThrowTimer(TimeLeft);
            }
        }
        else
        {
            TimerOn = false;
            throwTimerTxt.enabled = false;
        }
    }

    void updateThrowTimer(float currentTime)
    {
        currentTime += 1;
        float seconds = Mathf.FloorToInt(currentTime % 60);

        throwTimerTxt.text = string.Format("{00}", seconds);

        if (currentTime > 0)
        {
            throwColor.color = Color.gray;
        }
    }
}
