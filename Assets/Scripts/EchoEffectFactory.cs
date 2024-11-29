using UnityEngine;
using Utility;

public class EchoEffectFactory : MonoBehaviour {
        [SerializeField] private GameObject echoLightPrefab;
            
        public void CreateAndCastEchoEffect(Vector3 position, float range, float speed, Color color){
            var echoLight = Instantiate(echoLightPrefab, position, Quaternion.identity).GetComponent<EchoLight>();
            echoLight.SetLight(range, speed, color);
            echoLight.transform.position = position;
            echoLight.CastLight();
        }
        public void CreateAndCastEchoEffect(Vector3 position){
            Debug.Log("Echo Factory started Creating");
            var echoLight = Instantiate(echoLightPrefab, position, Quaternion.identity).GetComponent<EchoLight>();
            echoLight.transform.position = position;
            echoLight.CastLight();
        }
    }