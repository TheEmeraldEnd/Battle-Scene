using UnityEngine;
using TMPro;

/// <summary>
/// Display the time score
/// </summary>
public class DisplayTimeScore : MonoBehaviour
{
    //Instantiated variables
    [SerializeField] private TimeHold _timeScore;       //The time that has passed
    [SerializeField] private TMP_Text _text;            //The text element

    /// <summary>
    /// The update before the first update
    /// </summary>
    private void Awake()
    {
        //Get the text component
        _text = this.GetComponent<TMP_Text>();

        //Set the text
        _text.text = $"Your score was {_timeScore.timeScore:f2} seconds\n The lower, the better";
    }
}
