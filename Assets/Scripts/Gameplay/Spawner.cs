using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] spawnPositions; 
    public Spawner[] triggers; 
    public NPC child; 
    private bool _triggered; 

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player" && !_triggered){
            child.Spawn(spawnPositions[Mathf.RoundToInt(Random.Range(0, 1))].transform.position); 
            _triggered = true;
            
            foreach(Spawner s in triggers){
                s.SetAsTriggered(); 
            }
        }
    }

    public void SetAsTriggered(){
        _triggered = true; 
    }
    
}
