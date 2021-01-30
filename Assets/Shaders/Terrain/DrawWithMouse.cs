using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWithMouse : MonoBehaviour
{

    public Camera _cam; 
    public Shader _drawShader; 

    private RenderTexture _splatmap; 

    private Material _snowMaterial;
    private Material _drawMaterial; 

    private RaycastHit _hit; 

    [Range(1, 500)]
    public float brushSize; 
    [Range (0, 1)]
    public float brushStrength; 

    void Start(){
        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); 
        _drawMaterial = new Material(_drawShader);
        _drawMaterial.SetVector("_Color", Color.red);
        _snowMaterial = GetComponent<MeshRenderer>().material; 
        _splatmap = new RenderTexture(512,512,0,RenderTextureFormat.ARGBFloat); 
        _snowMaterial.SetTexture("_Splat", _splatmap); 
    } 

    void Update(){
        if(Input.GetKey(KeyCode.Mouse0)){
            if(Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out _hit)){
                _drawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0)); 
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
