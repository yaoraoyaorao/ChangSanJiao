using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVideoControl
{
    public string Name { get; }

    void Play();

    void Stop();

    void Rewind();

}
