using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchBGAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _intro;

    [SerializeField]
    private AudioClip _loop;

	// Use this for initialization
	void Start () 
    {
        _audioSource.clip = _intro;
        _audioSource.loop = false;
        _audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = _loop;
            _audioSource.loop = true;
            _audioSource.Play();
        }
	}
}
