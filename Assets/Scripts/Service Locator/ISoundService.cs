using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoundService : IGameService
{
    public abstract void PlaySound(AudioClip clip, Vector3 position, float volume);

}
