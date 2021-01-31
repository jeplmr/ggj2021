using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowNoise : MonoBehaviour
{
    public Shader _snowFallShader; 
    private Material _snowFallMat; 
    private MeshRenderer _meshRenderer;
    [Range(0.001f, 0.1f)]
    public float flakeAmount;
    [Range(0f, 1f)]
    public float flakeOpacity;    
    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>(); 
        _snowFallMat = new Material(_snowFallShader); 
    }

    // Update is called once per frame

    void Update()
    {
        _snowFallMat.SetFloat("_FlakeAmount", flakeAmount);
        _snowFallMat.SetFloat("_FlakeOpacity", flakeOpacity);  
        RenderTexture snow = (RenderTexture) _meshRenderer.material.GetTexture("_Splat");
        RenderTexture temp = RenderTexture.GetTemporary(snow.width, snow.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(snow, temp, _snowFallMat);
        Graphics.Blit(temp, snow);
        _meshRenderer.material.SetTexture("_Splat", snow);
        RenderTexture.ReleaseTemporary(temp); 
    }
    
}
