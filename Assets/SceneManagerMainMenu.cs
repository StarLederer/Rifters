﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToSpellsScene()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToCharacterSelection()
    {
        SceneManager.LoadScene(3);
    }
}