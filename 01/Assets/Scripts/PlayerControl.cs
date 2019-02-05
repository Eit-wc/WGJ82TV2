using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
     public GameObject lightningObject;
     public TMPro.TMP_Text TextLight;
     public int LightMax;
     private int LightHave;
     public int coolDownLevel;
     public float coolDownStart;
     
     public float Cooldown;
     
     private float CountCooldown;
    public GameObject upgradeCoolDownButtonText;
    public GameObject upgradeLightningButtonText;
     
    RaycastHit hit; 
    Ray ray;
    EventSystem eventSystem;
    private Vector3 scale = Vector3.zero;
    public float scaleFromScore = 10.0f;

    public float initialCamera;
    private bool fireable = true;

    private StringBuilder strBuild;    
    void Start()
    {
        //   Debug.Log("Player");
        strBuild = new StringBuilder();
        GameObject EVSystem = GameObject.Find("EventSystem");
        
        if(EVSystem == null)
        {
            Cooldown = 0.05f;
            LightHave = LightMax;
            
        }
        else
        {
            eventSystem = EVSystem.GetComponent<EventSystem>();
            Cooldown = coolDownStart/(float)coolDownLevel;
        
            CountCooldown = 0.0f;
            LightHave = LightMax;
            initialCamera = Camera.main.orthographicSize;

            upgradeCoolDownButtonText.GetComponent<TMPro.TMP_Text>().text = "Cooldown("+this.coolDownLevel.ToString()+")";
            upgradeLightningButtonText.GetComponent<TMPro.TMP_Text>().text = "Lightning("+ this.LightMax.ToString()+")";
    
        }
    }

    IEnumerator Fire()
    {
        //Debug.Log("Fire in player");
        //Debug.Log(lightningParticle);
        //Debug.Log( particleObject.GetComponent<ParticleSystem>());
        
        fireable = false;
        yield return new WaitForSeconds(0.1f);
        fireable = true;
       // Physics.OverlapCapsule(Point0.transform.position,Point1.transform.position,Point0.transform.lossyScale.x,LayerMask.GetMask("Building"));

    }
    // Update is called once per frame
    private void FixedUpdate() {
       CountCooldown += Time.deltaTime;
       if(CountCooldown> this.Cooldown)
       {
           if(LightHave>= LightMax)
           {
               LightHave = LightMax;
           }else
           {
               LightHave += 1;
           }
           CountCooldown=0.0f;
       }

       strBuild.Clear();
       strBuild.Append(LightHave);
       strBuild.Append("/");
       strBuild.Append(LightMax);

       TextLight.text = strBuild.ToString();

    // size from score
    scale.x = 1 + Global.score/scaleFromScore;
    scale.y = 1 + Global.score/scaleFromScore;
    scale.z = 1 + Global.score/scaleFromScore;
    this.transform.localScale = scale;

    Camera.main.orthographicSize =  scale.x * initialCamera;


//click fire

        if(Input.GetButton("Fire1")&&(!eventSystem.IsPointerOverGameObject()))
        {
            // Debug.Log("Fire");

            if(LightHave>0 && fireable)
            {
                LightHave -=1 ;
                ray = Camera.main.ScreenPointToRay(Input.mousePosition); 

                if( Physics.Raycast (ray,out hit,500,Global.LayerCanActive)) {
                    // Summon Light upper hit.point
                    Instantiate(lightningObject,hit.point + (Vector3.up*3),Quaternion.identity);
                    StartCoroutine("Fire");
                    //particleObject.transform.position = hit.point + (Vector3.up*3) ;
                    //particleObject.GetComponent<ParticleSystem>().Play();

                    //Debug.Log("You selected the " + hit.point); // ensure you picked right object
                    //GameObject go  = GameObject.CreatePrimitive(    PrimitiveType.Cube);
                    //go.transform.position = hit.point + (Vector3.up*2);                    
                }
            }
        }
    }


#region GUIEvent
    public void upgradeCoolDown()
    {

        if(Global.score > coolDownLevel)
        {
            Global.score -= this.coolDownLevel;
            coolDownLevel +=1;
            Cooldown = coolDownStart/(float)coolDownLevel;
            strBuild.Clear();
            strBuild.Append("Cooldown(");
            strBuild.Append(coolDownLevel);
            strBuild.Append(")");
            upgradeCoolDownButtonText.GetComponent<TMPro.TMP_Text>().text = strBuild.ToString();
        }
    }

    public void upgradeLightning()
    {
        if(Global.score > this.LightMax )
        {
            
            Global.score -= this.LightMax-2;
            LightMax +=1;
            strBuild.Clear();
            strBuild.Append("Lightning(");
            strBuild.Append(this.LightMax);
            strBuild.Append(")");
            upgradeLightningButtonText.GetComponent<TMPro.TMP_Text>().text = strBuild.ToString();
        }
    }
#endregion
}
