using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SaveBreakInput : MonoBehaviour
{
        public InputField breakInput;
        public InputField timeInput;

        //to be printed on the button
        private string newActivity = "";
        private float hours = 0;

        //things needed to instantiate
        public GameObject canvas;
        public GameObject button;
        public BreakButton scroll;

        //for Task input field
        public void GetTaskInput(string activity)
        {
            newActivity = activity;
            Debug.Log("Entered " + newActivity);
        }

        //for Time input field
        public void GetTimeInput(string time)
        {
            hours = float.Parse(time);
            Debug.Log("Entered " + time);
        }

        // When Add button is clicked use the method in scroll to create button
        public void DisplayInput()
        {
        Debug.Log("no string");
            if (newActivity != "")
            {
                Debug.Log("instruct make button");
                scroll.MakeButton(newActivity, hours, canvas);
                breakInput.text = "";
                timeInput.text = "";
                newActivity = "";
                hours = 0;
            }
        }

    }
