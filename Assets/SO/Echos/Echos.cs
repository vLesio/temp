using UnityEngine;

namespace SO.Echos {
    [CreateAssetMenu(fileName = "Echo", menuName = "Scriptable Objects/Echo")]
    public class Echo : ScriptableObject {
        public float range;
        public float duration;
        public float desiredAngle;
        public float width;
        public float yOffset;
        public float intensity;
        public AnimationCurve echoFlowFunction;
        public AnimationCurve echoEaseOutFunction;
        public bool shouldCastTrigger;
        public bool enlightIgnoresWalls;
        public Color color;
        public AudioClip soundEffect;
    }
}
