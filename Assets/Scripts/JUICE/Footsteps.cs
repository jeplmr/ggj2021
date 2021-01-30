using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity; 

public class Footsteps : MonoBehaviour
{
    private StudioEventEmitter emitter; 
    private Collider _col; 
    // Start is called before the first frame update
    void Start()
    {
        emitter = GetComponent<StudioEventEmitter>();
        _col = GetComponent<SphereCollider>(); 
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Ground"){
            emitter.Play();  
        }
    }

}
