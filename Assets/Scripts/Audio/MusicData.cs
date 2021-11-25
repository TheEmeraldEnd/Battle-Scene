using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains the music data to be able to change through multiple scenes
/// </summary>
[CreateAssetMenu(fileName = "Music Data", menuName = "new music data")]
public class MusicData : ScriptableObject
{
    //The music volume
    [SerializeField][Range(0f, 1f)] public float musicVolume;

    /// <summary>
    /// Easy alterations with scroll bars
    /// </summary>
    public float MusicVolume{ get { return musicVolume; } set { musicVolume = value; } }
}
