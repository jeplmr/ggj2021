using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Warmth : MonoBehaviour
{
    [SerializeField]
    private bool _isWarm;
    private float warmth; 
    [Range(0.01f, 0.2f)]
    public float warmRate = 0.05f;
    //USE 0.05f!!!!

    public Image warmthGuage;
    private bool _triggered; 

    void Start(){
        warmth = 50;    
        //this has a direct relationship to fixed timestep, which updates at 50hz
        //warmrate of 0.067 lasts about 30 seconds 
        //warmrate of 0.2 lasts about 10 seconds
        //warmrate of 0.1 lasts about 20 seconds 
        //warmrate of 0.05 lasts about 40 seconds
        //DO NOT CHANGE WARMTH VALUE, I HAVE A BUNCH OF MAGIC NUMBERS HERE #GAMEJAMCODE
        _triggered = false; 
    }

    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag == "Fire"){
            _isWarm = true; 
        }
    }

    void OnTriggerExit(Collider c){
        if(c.gameObject.tag == "Fire"){
            _isWarm = false; 
        }
    }

    void FixedUpdate(){
        CalcWarmth(); 
        UpdateUI(); 
    }

    void CalcWarmth(){
        if(_isWarm){
            warmth += warmRate; 
        } else {
            warmth -= warmRate/2;
        }

        if(warmth <= 0){
            warmth = 0;
            if(!_triggered){
                GameManager.GmInstance.MarkPlayerAsDead();
                _triggered = true; 
            }
        }

        if(warmth >= 50){
            warmth = 50; 
        }
    }
    void UpdateUI(){
        warmthGuage.fillAmount = warmth/50f; 
    }

    public void Reset(){
        warmth = 50f;
        _triggered = false; 
    }

}
