using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class EchoLightTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask triggeredBy;
    private SphereCollider objCollider;

    List<Collider> collidedWith = new List<Collider>(0);

    private void Awake()
    {
        objCollider = gameObject.GetComponent<SphereCollider>();
    }

    private void OnDrawGizmos()
    {
        if(objCollider != null){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position , objCollider.radius);
        }
    }
    public void SetRadius(float radius){
        objCollider.radius = radius;
    }

    public void AddRadius(float amount){
        objCollider.radius += amount;
    }

    private bool IsInLayerMask(GameObject obj)
    {
        int objLayer = obj.layer;
        return (triggeredBy.value & (1 << objLayer)) != 0;
    }

    private void OnTriggerEnter(Collider other)
    {   
        if(!IsInLayerMask(other.gameObject)) return;
        if(collidedWith.Contains(other)) return;
        Debug.Log(other.name);
        collidedWith.Add(other);
        RunOtherColliderLogic(other);
    }

    public void RunOtherColliderLogic(Collider other){
        ObjectEnlighter obEnlight = other.gameObject.GetComponent<ObjectEnlighter>();
        if(obEnlight) obEnlight.Enlight();
    }
}
