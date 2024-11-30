using UnityEngine;

public class EffectSetup : MonoBehaviour
{
    [SerializeField] private GameObject[] effects;

    private void Start()
    {
        EnableEffects();
    }

    private void EnableEffects(){
        foreach(GameObject effect in effects){
            effect.SetActive(true);
        }
    }
}
