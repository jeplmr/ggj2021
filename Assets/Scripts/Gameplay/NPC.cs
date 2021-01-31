using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class NPC : MonoBehaviour
{
    private bool isFollowing;
    public GameObject followPoint; 
    private NavMeshAgent nma; 
    private Vector3 startPos; 
    // Start is called before the first frame update
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        isFollowing = false;
        StartCoroutine(CalcPath()); 
        startPos = transform.position; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //nma.SetDestination(followPoint.transform.position); 
    }


    public void ResetPosition(){
        isFollowing = false; 
    }

    IEnumerator CalcPath(){
        while(true){
            if(isFollowing){
                if(Vector3.Distance(followPoint.transform.position, transform.position) >= 1.5f){
                    nma.isStopped = false; 
                    nma.SetDestination(followPoint.transform.position);
                } else {
                    nma.isStopped = true;
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player" && !isFollowing){
            isFollowing = true; 
        }
    }

    public void Reset(){
        isFollowing = false;
        nma.isStopped = true; 
        transform.position = startPos; 
    }

    public void Spawn(Vector3 position){
        //TRYING TO SPAWN THING
        transform.position = position; 
        nma.Warp(position);
        startPos = position; 
    }

}
