using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEditor;

public class FinalSceneController : MonoBehaviour
{
    public Animator animator;
    public VideoPlayer video;
    private bool started;
    public float timer;
    public float duration = 10f;
    public float fadeOut = 10f;
    // Start is called before the first frame update
    void Start()
    {
        started = false;
        video.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            timer += 1 * Time.deltaTime;

            if(timer >= duration)
            {
                Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else if(timer >= fadeOut)
            {
                animator.SetBool("end", true);
            }
        }
    }

    public void StartVideo()
    {
        video.Play();
       
        video.Pause();
    }

    public void UnPause()
    {
        video.Play();
        started = true;
    }
}
