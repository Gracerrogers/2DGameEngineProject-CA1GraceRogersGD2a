using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Menuscript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");   
    }

    
    public void Quit()
    {
        Application.Quit();
    }
}
