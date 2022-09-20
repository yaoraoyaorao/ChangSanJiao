using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DisplayUGUI))]
public class AVProVIdeoControl : MonoBehaviour, IVideoControl
{

    private DisplayUGUI _displayUGUI;
   

    private void Start()
    {
        _displayUGUI = GetComponent<DisplayUGUI>();
    }

    public void Play()
    {
        _displayUGUI.CurrentMediaPlayer?.Play();
    }

    public void Rewind()
    {
        _displayUGUI.CurrentMediaPlayer?.Rewind(true);
    }

    public void Stop()
    {
        _displayUGUI.CurrentMediaPlayer?.Stop();
    }

    public void Pause()
    {
        _displayUGUI.CurrentMediaPlayer?.Pause();
    }
}
