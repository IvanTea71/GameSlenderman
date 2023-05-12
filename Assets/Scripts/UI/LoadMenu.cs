using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class LoadMenu : MonoBehaviour
{
    private VideoPlayer _videoPlayer;

    void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.Prepare();

        _videoPlayer.loopPointReached += VideoPlayerLoopPointReached;

        Invoke(nameof(Play), 0);
    }

    private void VideoPlayerLoopPointReached(VideoPlayer source)
    {
        MenuGame.Load();
    }

    private void Play()
    {
        _videoPlayer.Play();
    }
}
