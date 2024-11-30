using Unity.Mathematics;
using UnityEngine;

public class SoundAlert : MonoBehaviour
{
    [SerializeField] private float soundDistance = 5.5f;
    [SerializeField] private LayerMask enemyMask;

    public void AlertEnemies(){
        Collider[] enemies = Physics.OverlapSphere(transform.position , soundDistance , enemyMask);
        for(int i = 0 ; i < enemies.Length; i++){
            NoiseEnemy noise_en = enemies[i].GetComponent<NoiseEnemy>();
            if(noise_en){
                noise_en.PathFindToNoise(transform.position);
                continue;
            }
        }
    }
}
