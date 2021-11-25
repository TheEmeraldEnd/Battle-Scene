using UnityEngine;

public class MusicBoxScript : MonoBehaviour
{
    //The music box variable
    [Tooltip("The music box component that will play the music")]
    [SerializeField] private AudioSource _musicBox;

    //Start before the first update
    private void Awake()
    {
        _musicBox = this.GetComponent<AudioSource>();
    }

    /// <summary>
    /// Start the music method
    /// </summary>
    /// <param name="volume"></param>
    ///     The volume of the music
    /// <param name="timeDelay"></param>
    ///     The time delay of the music in seconds
    public void StartMusic(float volume, float timeDelay = 0f)
    {
        //Play the music (start
        UpdateVolume(volume);
        _musicBox.Play((ulong)(timeDelay * 44100f));
    }

    /// <summary>
    /// Update the volume of the music player
    /// </summary>
    /// <param name="volume"></param>
    ///     The volume the thing should be set to
    public void UpdateVolume(float volume)
    {
        //Set the music volume
        _musicBox.volume = volume;
    }
}
