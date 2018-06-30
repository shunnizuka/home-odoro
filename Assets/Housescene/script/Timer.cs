using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    
    //attributes for timer
    private float totaltime;
    private float countdown;
    public Text timertext;
    private bool paused = true;

    //to change pause button sprite
    public Sprite pauseSprite;
    public Sprite playSprite;
    public Button pauseButton;

    //panel to open after timer is done
    public GameObject timerendedPanel;

    //data to update
    public Data datatime;

    //initialise the timer to countdown
    public void SetTime(float time)
    {
        //setup the panel
        paused = true;
        pauseButton.image.sprite = playSprite;
        totaltime = time;
        countdown = totaltime * 60 * 60; // convert to seconds
        
        int seconds = (int)(countdown % 60);
        int minutes = (int)(countdown / 60) % 60;
        int hours = (int)(countdown / 3600);

        string timerstring = string.Format("{0:0}:{1:00}:{2:00}", hours, minutes, seconds);
        timertext.text = timerstring;
    }

    //count down and update the timer text
    private void Update()
    {
        if (!paused)
        {
            countdown -= Time.deltaTime;
            int seconds = (int)(countdown % 60);
            int minutes = (int)(countdown / 60) % 60;
            int hours = (int)(countdown / 3600);

            string timerstring = string.Format("{0:0}:{1:00}:{2:00}", hours, minutes, seconds);
            timertext.text = timerstring;

            
            if (countdown < 0)
            {
                paused = !paused;
                Debug.Log("timer ends" + paused);
                ContinueOrEnd();
            }
        } 
    }

    //to pause the timer
    public void PauseTimer()
    {
        //pause the time
        paused = !paused;
        Debug.Log("pause" + paused);
        //change the button image
        if(paused)
        {
            pauseButton.image.sprite = playSprite;
        }
        else
        {
            pauseButton.image.sprite = pauseSprite;
        }
    }

    //to be called when timer ends
    private void ContinueOrEnd()
    {
        timerendedPanel.SetActive(true);
        datatime.Updatehour(totaltime);
        datatime.DisplayTotaltimeText();
    }

    public void Endtimer()
    {
        timerendedPanel.SetActive(false);
        this.gameObject.SetActive(false);
        Debug.Log("close timer panel");
    }
}
