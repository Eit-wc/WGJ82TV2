using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsScript : MonoBehaviour
{
    public bool haveTV = true;
    [ColorUsageAttribute(false,true,0f,8f,0.125f,3f)]
    public Color colorHaveTV;
    [ColorUsageAttribute(false,true,0f,8f,0.125f,3f)]
    
    public Color colorHaveNotTV;

    public float noiseSpeed = 4.0f;
    public float noiseRate = 0.2f;

    [ColorUsageAttribute(false,true,0f,8f,0.125f,3f)]
    private Color currentColor;
    private Material windowsMat;
    private float noiseTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Material item in GetComponent<MeshRenderer>().materials)
        {
            //Debug.Log(item.name);
            //Debug.Log(string.Compare(item.name,"Windows (Instance)"));
            if(string.Compare(item.name,"Windows (Instance)") == 0)
            {
                windowsMat = item;
                windowsMat.EnableKeyword("_EMISSION");
                break;
            }
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(haveTV)
        {
            if(Random.value < noiseRate)
            {
                //Debug.Log("TV noise");
                noiseTime = 0.0f;
            }
            currentColor = Color.Lerp(colorHaveNotTV,colorHaveTV,noiseTime);
            noiseTime += Time.deltaTime * noiseSpeed;
            windowsMat.EnableKeyword("_EMISSION");
            windowsMat.SetColor("_EmissionColor",currentColor);

        }else
        {
            windowsMat.SetColor("_EmissionColor",colorHaveNotTV);
        }
    }
}
