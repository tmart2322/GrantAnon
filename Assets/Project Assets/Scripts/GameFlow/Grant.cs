﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grant : MonoBehaviour
{
    [SerializeField] float CaptureTime = 10.0f;

    int[] CapturingTeams;
    int CurrentTimerOwner;
    float Countdown;

    // Start is called before the first frame update
    void Start()
    {
        CapturingTeams = new int[MainGameManager.instance.PlayerCount];
        Countdown = CaptureTime;
        CurrentTimerOwner = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int NumCapturing = 0;
        bool MultipleCapturing = false;

        for(int i = 0; i < CapturingTeams.Length; i++)
        {
            //Check if any units are capturing
            if(CapturingTeams[i] > 0)
            {
                //If there are, check if anyone else is doing so
                if (NumCapturing == 0)
                {
                    NumCapturing = i + 1;
                }
                else
                {
                    //If so, set flag
                    MultipleCapturing = true;
                }
            }
        }

        //If multiple people capturing, do nothing
        if (!MultipleCapturing)
        {
            // Check if no one is capturing grant
            if (NumCapturing > 0)
            {
                //See if capturing player was the same as last check
                if (CurrentTimerOwner == NumCapturing)
                {
                    //Count down timer
                    Countdown -= Time.deltaTime;

                    if (Countdown <= 0.0f)
                    {
                        //Claim Grant
                        MainGameManager.instance.ScoreGrant(CurrentTimerOwner);
                        Destroy(gameObject);
                    }

                }
                else
                {
                    //Set new timer owner and start countdown
                    Countdown = CaptureTime;
                    Countdown -= Time.deltaTime;
                    CurrentTimerOwner = NumCapturing;
                }
            }
            else
            {
                //Reset Timer
                if (Countdown != CaptureTime)
                {
                    Countdown = CaptureTime;
                }
            }
        }

        //Debug.Log("Countdown: " + Countdown.ToString());
    }

    void OnTriggerEnter(Collider other)
    {
        Unit unit = other.GetComponent<Unit>();
        if (unit != null)
        {
            CapturingTeams[unit.OwningPlayerNum - 1]++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Unit unit = other.GetComponent<Unit>();
        if (unit != null)
        {
            CapturingTeams[unit.OwningPlayerNum - 1]--;
        }
    }
}
