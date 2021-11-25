using UnityEngine;
using TMPro;

/// <summary>
/// Display all of the credits in a text box
/// </summary>
public class DisplayCredits : MonoBehaviour
{
    //Instantiated variables
    [SerializeField] private Credits[] _credits;        //The credits
    [SerializeField] private TMP_Text _text;            //The text

    /// <summary>
    /// Everything before the first frame
    /// </summary>
    private void Awake()
    {
        //Get the text component
        _text = this.GetComponent<TMP_Text>();

        //Start the text string
        string textString = "";

        //Gather the data to cache the text string
        foreach(Credits credit in _credits)
        {
            //The text string with data
            textString += $"{credit.CreditsType}: {credit.CreditFor} by {credit.Author} at \n{credit.Website}\n\n";
        }

        //Set the text string to text
        _text.text = textString;
    }
}
