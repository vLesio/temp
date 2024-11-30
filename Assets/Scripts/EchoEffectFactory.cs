using SO.Echos;
using UnityEngine;
using Utility;

public class EchoEffectFactory : MonoBehaviour {
        [SerializeField] private GameObject echoLightPrefab;
            
        public void CreateAndCastEchoEffect(Vector3 position, float range, float speed, float intensity, Color color){
            var echoLight = Instantiate(echoLightPrefab, position, Quaternion.identity).GetComponent<EchoLight>();
            echoLight.SetLight(range, speed, intensity, color);
            echoLight.transform.position = position;
            echoLight.CastLight();
        }
        
        public void CreateAndCastEchoEffect(Vector3 position, Echo echo) {
            var positionToSpawn = position + Vector3.up * echo.yOffset;
            var echoLight = Instantiate(echoLightPrefab, positionToSpawn, Quaternion.identity).GetComponent<EchoLight>();
            echoLight.SetLight(echo.range, echo.speed, echo.intensity, echo.color);
            echoLight.transform.position = positionToSpawn;
            echoLight.CastLight();
        }
        public void CreateAndCastEchoEffect(Vector3 position){
            var echoLight = Instantiate(echoLightPrefab, position, Quaternion.identity).GetComponent<EchoLight>();
            echoLight.transform.position = position;
            echoLight.CastLight();
        }
    }