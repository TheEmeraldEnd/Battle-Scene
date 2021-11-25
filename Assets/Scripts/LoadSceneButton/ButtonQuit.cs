using UnityEngine;

/// <summary>
/// Used to quit the game on button press
/// </summary>
public class ButtonQuit : MonoBehaviour
{
    /// <summary>
    /// Quit on button press
    /// </summary>
    public void Quit()
    {
        //Quick the game
        Application.Quit();
    }
}
