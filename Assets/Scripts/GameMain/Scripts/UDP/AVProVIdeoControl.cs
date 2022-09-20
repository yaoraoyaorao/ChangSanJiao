using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MediaPlayer))]
public class AVProVIdeoControl : MonoBehaviour, IVideoControl
{
    private MediaPlayer _mediaPlayer;

    public string Name =>transform.name;
    
    public MediaPlayer Player => _mediaPlayer;

    public bool Loop
    {
        get
        {
            return _mediaPlayer.Loop;
        }
        set
        {
            _mediaPlayer.Loop = value;  
        }
    }

    private void Start()
    {
        _mediaPlayer = GetComponent<MediaPlayer>();
    }

    public void Play()
    {
        if(_mediaPlayer == null)
            _mediaPlayer = GetComponent<MediaPlayer>();
        _mediaPlayer.Play();
    }

    public void Rewind()
    {
        if (_mediaPlayer == null)
            _mediaPlayer = GetComponent<MediaPlayer>();
        _mediaPlayer.Rewind(true);
    }

    public void Stop()
    {
        if (_mediaPlayer == null)
            _mediaPlayer = GetComponent<MediaPlayer>();
        _mediaPlayer.Stop();
    }
}
