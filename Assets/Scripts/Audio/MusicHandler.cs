using UnityEngine;

/// <summary>
/// Handles the music in the battle scene
/// </summary>
public class MusicHandler : MonoBehaviour
{

    #region music related variables
    //Music box Game object
    [SerializeField] private string _musicBoxTag;               //Music box tag

    [SerializeField] private MusicBoxScript _musicBoxScript;    //Music box scripts
    [SerializeField] private MusicData _musicData;              //Contain the music volume

    [Tooltip("Time delay of the music in seconds")]
    [SerializeField] private float _timeDelay;                  //The time delay of the start in seconds

    //To reduce frame usage
    private int _frameCounter = 0;                              //THe frame counter for music
    [SerializeField] private int _frameUpdate;                  //Update music volume every {_fameUpdate}th frame
    #endregion

    //What happens on the first update
    private void Start()
    {
        //Get script
        _musicBoxScript = GameObject.FindGameObjectWithTag(_musicBoxTag).GetComponent<MusicBoxScript>();

        //Start the music
        _musicBoxScript.StartMusic(_musicData.musicVolume, _timeDelay);
    }

    //What happens every update
    private void Update()
    {
        //Update the volume
        if (_frameCounter % _frameUpdate == 0)
        {
            _musicBoxScript.UpdateVolume(_musicData.musicVolume);
        }

        //Add one to the counter
        _frameCounter++;
    }
}
