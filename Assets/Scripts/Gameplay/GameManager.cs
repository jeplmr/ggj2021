using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour
{

    public Image ftb;
    [Range(1f, 10f)]
    public float fadeLength; 
    private Color opaque = new Color(0f, 0f, 0f, 1f);
    private Color clear = new Color(0f, 0f, 0f, 0f);
    public GameObject player; 
    public vThirdPersonCamera playerCamera; 
    //public GameObject menuCamera; 
    private Vector3 startPos; 
    private bool _dead; 
    private Warmth playerWarmth;
    private Tracks tracksReference; 
    public NPC child; 
    public bool hasWon; 
    public GameObject endScreen; 

    public static GameManager GmInstance { get; private set; }

    // Start is called before the first frame update
    void Awake(){
        if (GmInstance != null && GmInstance != this) {
            Destroy(this.gameObject);
        } else {
            GmInstance = this;
        }
    }

    void Start()
    {
        //StartCoroutine(FadeFromBlack(Time.time));
        ftb.color = opaque; 
        _dead = false; 
        startPos = player.transform.position; 
        playerWarmth = player.GetComponent<Warmth>(); 
        tracksReference = player.GetComponent<Tracks>(); 
        //player.SetActive(false);
        playerCamera.enabled = false; 
        //menuCamera.SetActive(true); 
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
        hasWon = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(_dead){
            StartCoroutine(FadeToBlack(Time.time));
            _dead = false;
        }
    
        if(hasWon){
            EndGame(); 
            hasWon = false; 
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            GTFO(); 
        }

    }


    void FadeFrom(){
        ftb.color = opaque; 
        ftb.color = Color.Lerp(opaque, clear, fadeLength); 
    }


    IEnumerator FadeFromBlack(float timestamp){
        ftb.color = opaque; 
        float temp = 1f;
        float rate = (1f/fadeLength)/100; 
        while(Time.time - timestamp < fadeLength){
            temp -= rate; 
            ftb.color = new Color(0, 0, 0, temp);
            yield return new WaitForEndOfFrame(); 
        }
        ftb.color = clear; 
        yield return null;
    }

    IEnumerator FadeToBlack(float timestamp){
        ftb.color = clear; 
        float temp = 0f;
        float fadeLengthTemp = fadeLength/4f;
        float rate = (1f/fadeLengthTemp)/100; 
        while(Time.time - timestamp < fadeLengthTemp){
            temp += rate; 
            ftb.color = new Color(0, 0, 0, temp);
            yield return new WaitForEndOfFrame(); 
        }
        ftb.color = opaque;
        ResetPlayer(); 
        yield return null; 
    }

    IEnumerator FadeToEnd(float timestamp){
        ftb.color = clear; 
        float temp = 0f;
        float rate = (1f/fadeLength)/100; 
        while(Time.time - timestamp < fadeLength){
            temp += rate; 
            ftb.color = new Color(0, 0, 0, temp);
            yield return new WaitForEndOfFrame(); 
        }
        ftb.color = opaque;
        yield return null; 
    }

    private void ResetPlayer(){
        player.transform.position = startPos;
        playerWarmth.Reset();
        _dead = false;
        tracksReference.ResetSnow(); 
        child.Reset(); 
        StartCoroutine(FadeFromBlack(Time.time));
    }

    public void MarkPlayerAsDead(){
        if(!_dead){
            _dead = true; 
        }
    }

    public void StartGame(){
        playerCamera.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(FadeFromBlack(Time.time)); 
    }

    public void EndGame(){
        //playerCamera.lockCamera = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        endScreen.SetActive(true); 
        StartCoroutine(FadeToEnd(Time.time)); 
    }

    public void SeldgehammerPlayAgain(){
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0); 
    }

    public void EnableMouseStuff(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void GTFO(){
        Application.Quit(); 
    }

}
