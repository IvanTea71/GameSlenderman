using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class LoadMenu : MonoBehaviour
{
    VideoPlayer _videoPlayer;

    void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.Prepare();

        _videoPlayer.loopPointReached += _videoPlayer_loopPointReached;

        Invoke("play", 0);
    }

    private void _videoPlayer_loopPointReached(VideoPlayer source)
    {
        MenuGame.Load();
    }

    private void play()
    {
        _videoPlayer.Play();
    }
}
