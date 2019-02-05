using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour
{
    public float startSound = 2.0f;
    public float stopSound = 2.2f;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioS = this.GetComponent<AudioSource>();
        audioS.pitch = Random.Range(0.5f,0.9f);
        Global.PlaySoundInterval(audioS,startSound,stopSound);
    }
}
