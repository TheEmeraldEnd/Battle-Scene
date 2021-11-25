using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Load the screen on click
/// </summary>
public class ButtonLoadScene : MonoBehaviour
{
    //Instantiate variables
    [SerializeField] private string _sceneName;     //The new load sreen name

    /// <summary>
    /// Used to load scene on click
    /// </summary>
    public void LoadScene()
    {
        //Load the scene
        SceneManager.LoadScene(_sceneName);
    }
}
