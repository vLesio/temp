using UnityEngine;

namespace SO.Echos {
    [CreateAssetMenu(fileName = "Echo", menuName = "Scriptable Objects/Echo")]
    public class Echo : ScriptableObject {
        public float range;
        public float speed;
        public float width;
        public float yOffset;
        public float intensity;
        public Color color;
        public AudioClip soundEffect;
    }
}
