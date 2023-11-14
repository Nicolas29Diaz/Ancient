using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinalAnim : MonoBehaviour
{
    public AudioSource cameraAudioSource;
    public AudioClip alarmSound;
    public AudioClip[] footSounds;
    public Animator fadeOutAnim;

    private void Awake()
    {
        fadeOutAnim.enabled = false;
    }
    public void AlarmSound()
    {
        cameraAudioSource.PlayOneShot(alarmSound,0.2f);
    }

    public void FootSounds(int i)
    {
        cameraAudioSource.PlayOneShot(footSounds[i],1);
    }

    public void FinalScene()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void FadeOut()
    {
        fadeOutAnim.enabled = true;
    }
}
