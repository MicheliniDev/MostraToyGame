using UnityEngine;

namespace ToyGame
{
    public class BackgroundMove : MonoBehaviour
    {
        public MeshRenderer mesh;
        public float speed;

        void Update()
        {
            mesh.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
        }
    }
}
