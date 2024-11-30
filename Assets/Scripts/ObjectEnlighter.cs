using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEnlighter : MonoBehaviour
{
    [SerializeField] private float visibleTime = 5;
    [Header("Pick One That Works")]
    [SerializeField] private SkinnedMeshRenderer skinnedMesh;
    [SerializeField] private MeshRenderer outlinedObject;
    private Coroutine lightCor;

    private void Awake()
    {
        SetAllMaterialsOpacity(0);
    }

    private void SetAllMaterialsOpacity(float value){
        Debug.Log("changed");
        if(skinnedMesh){
            for(int i = 0; i < skinnedMesh.materials.Length; i++){
                skinnedMesh.materials[i].SetFloat("_Opacity" , value);
            }
        }
        else if(outlinedObject){
            for(int i = 0; i < outlinedObject.materials.Length; i++){
                outlinedObject.materials[i].SetFloat("_Opacity" , value);
            }
        }
    }

    public void Enlight(){
        if(lightCor != null){ 
            StopCoroutine(lightCor);
            lightCor = StartCoroutine(EnlightCoroutine(100f));
        }
        else{
            lightCor = StartCoroutine(EnlightCoroutine(0f));
        }
    }

    private IEnumerator EnlightCoroutine(float fadeInTimer){
        float time = visibleTime;
        float fadeOut = Math.Min(3f , visibleTime * 0.35f);
        float opacity;
        while(time > 0f){
            yield return null;
            time -= Time.deltaTime;
            fadeInTimer += Time.deltaTime;
            opacity = Mathf.Min(1f , time / fadeOut, fadeInTimer);
            SetAllMaterialsOpacity(opacity);
        }
        SetAllMaterialsOpacity(0);
        lightCor = null;
        yield break;
    }
}
