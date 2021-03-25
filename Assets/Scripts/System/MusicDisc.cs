using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDisc : MonoBehaviour
{
    [SerializeField] private bool playOnStart = false;
    [SerializeField] private List<AudioClip> musicClips = default;
    private MusicJockey musicJockey = default;

    private void Start()
    {
        musicJockey = MusicJockey.MusicJockeyInstance;
        CheckPlayOnStart();
    }

    /// <summary>
    /// Startのタイミングで、再生許可があるならMusicをJockeyに渡す。
    /// </summary>
    private void CheckPlayOnStart()
    {
        if (!playOnStart)
        {
            return;
        }

        PlayDisc();
    }

    /// <summary>
    /// Discに登録したMusicをJockeyに渡し、再生する。
    /// </summary>
    private void PlayDisc()
    {
        if(musicJockey == null)
        {
            return;
        }

        if (musicClips.Count == 0)
        {
            return;
        }

        musicJockey.PlayMusic(musicClips[0]);
    }
}
