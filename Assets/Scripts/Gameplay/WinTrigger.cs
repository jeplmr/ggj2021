using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private bool triggered; 
    void Start(){
        triggered = false; 
    }
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Child" && triggered == false){
            GameManager.GmInstance.hasWon = true;
            triggered = true; 
        }
    }


}
