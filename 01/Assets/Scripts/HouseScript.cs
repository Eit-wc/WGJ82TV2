using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class HouseScript : MonoBehaviour
{
    // Start is called before the first frame update
    private WindowsScript[] windowsScripts;
    public int Score = 1;

    [SerializeField]
    public GameObject[] boomParticles;

    Transform arresterTransform;
    bool haveArrester =false;
    public GameObject ReturnLight;
    bool haveTV = true;

    public Color[] RandomColor;

    public AudioSource audioS;

    void Start()
    {
        //Random house color
        
        if(RandomColor.Length > 0)
        {
            this.GetComponent<MeshRenderer>().material.SetColor("_Color", RandomColor[Random.Range(0,RandomColor.Length)]);
        }

        //get Audio
        audioS = this.GetComponent<AudioSource>();

        //Random HaveTV
        windowsScripts = GetComponentsInChildren<WindowsScript>();
        if(Random.value < 0.05f)
        {
            this.setHaveNotTV();
            StartCoroutine("Reborn");
        }else
        {
            this.setHaveTV(); 
        }

        arresterTransform = transform.Find(Global.arrester);
        arresterTransform.gameObject.SetActive(false);
        haveArrester = false;
        //if( arresterTransform != null)
        //{
        //    haveArrester = true;
        //}
    }


    void setHaveTV()
    {
        foreach (WindowsScript item in windowsScripts)
        {
            item.haveTV = true;
        }

        haveTV = true;
    }

    void setHaveNotTV()
    {
        foreach (WindowsScript item in windowsScripts)
        {
            item.haveTV = false;
        }  
        haveTV = false;
    }
    public bool boom()
    {
        // off TV for all windows
        if(haveTV)
        {
            if(haveArrester)
            {
                Instantiate(ReturnLight,arresterTransform.position,Quaternion.identity);
                return false;
            }else
            {
                setHaveNotTV();
                // Boom particle
                foreach (GameObject item in boomParticles)
                {
                    item.GetComponent<ParticleSystem>().Play(true);
                }
                // camera shake
                CameraShaker.Instance.ShakeOnce(4f/(float)Global.score ,4f,.1f,1f);
                //Play boom audio
                audioS.pitch = Random.Range(0.6f,0.9f);
                //Global.PlaySoundInterval(audioS,0.0f,0.5f);
                audioS.Play();

                Global.score += this.Score ;
                StartCoroutine("Reborn");
                return true;
            }
        }else
        {
            return false;
        }

    }
    IEnumerator Reborn()
    {
        yield return new WaitForSeconds(Random.Range(5,15));
        setHaveTV();
        // random setup arrester
        if(Random.value<0.03f)
        {
            arresterTransform.gameObject.SetActive(true);
            haveArrester = true;
        }
    }

    private void OnParticleCollision(GameObject other) {
        
        if(other.gameObject.layer == LayerMask.NameToLayer("Lightning"))
        {
            this.boom();
        }
    }



}
