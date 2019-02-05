using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Global
{
    public static int score = 0;
    public static string arrester = "Arrester";
    public static int LayerCanActive = LayerMask.GetMask("Building")| LayerMask.GetMask("Ground");
    public static int GUILayer = LayerMask.GetMask("UI");

    public static bool Gameover = false;

    public static void PlaySoundInterval(AudioSource audioSource,float fromSeconds, float toSeconds)
    {
        audioSource.time = fromSeconds;
        audioSource.Play();
        audioSource.SetScheduledEndTime(AudioSettings.dspTime+(toSeconds-fromSeconds));
    }
}
