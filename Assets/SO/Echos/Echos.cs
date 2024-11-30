using UnityEngine;

namespace SO.Echos {
    [CreateAssetMenu(fileName = "Echo", menuName = "Scriptable Objects/Echo")]
    public class Echo : ScriptableObject {
        public float range;
        public float speed;
        public Color color;
    }
}
