using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracks : MonoBehaviour
{
    public Shader _drawShader; 
    public Material _snowMaterial;
    private Material _drawMaterial; 
    //private RenderTexture _splatmap; 
    private RenderTexture _splatmap; 

    public GameObject[] terrain; 
    public Transform[] trackables; 
    RaycastHit groundHit; 
    int layerMask; 
    public float brushSize; 
    [Range (0, 1)]
    public float brushStrength; 


    // Start is called before the first frame update
    void Start()
    {
        terrain = GameObject.FindGameObjectsWithTag("Ground"); 
        layerMask = LayerMask.GetMask("Ground"); 
        _drawMaterial = new Material(_drawShader);    
        _splatmap = new RenderTexture(1024,1024,0,RenderTextureFormat.ARGBFloat);
        _snowMaterial.SetTexture("_Splat", _splatmap);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(Transform t in trackables){
            if(Physics.Raycast(t.position, -Vector3.up, out groundHit, 2, layerMask)){
            _drawMaterial.SetVector("_Coordinate", new Vector4(groundHit.textureCoord.x, groundHit.textureCoord.y, 0, 0)); 
            _drawMaterial.SetFloat("_Strength", brushStrength);
            _drawMaterial.SetFloat("_Size", 500/brushSize);
            
            RenderTexture temp = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(_splatmap, temp);
            Graphics.Blit(temp, _splatmap, _drawMaterial);
            RenderTexture.ReleaseTemporary(temp); 
            }
        }
    }
}
