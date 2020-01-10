using UnityEngine;
using System.Collections;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;
    [HideInInspector]
    public AudioSource source;
}
