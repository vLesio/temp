using SO.Echos;
using UnityEngine;
using Utility;

public class EchoEffectFactory : MonoBehaviour {
        [SerializeField] private GameObject echoLightPrefab;
        public void CreateAndCastEchoEffect(Vector3 position, Echo echo) {
            var positionToSpawn = position + Vector3.up * echo.yOffset;
            var echoLight = Instantiate(echoLightPrefab, positionToSpawn, Quaternion.identity).GetComponent<EchoLight>();
            echoLight.SetLight(echo);
            echoLight.transform.position = positionToSpawn;
            echoLight.CastLight();
        }
    }