﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MenuButton
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Loads new level
     */
    override public void onClick()
    {
        //replace sample scene with the game
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
