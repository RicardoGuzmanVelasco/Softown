using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public class MoveWithWASD : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        
        void Update()
        {
            var horizontal = Input.GetAxis("Horizontal") * speed;
            var vertical = Input.GetAxis("Vertical") * speed;
            var forward = Input.GetAxis("Mouse ScrollWheel") * speed * 10;
            transform.Translate(horizontal, forward, vertical);
        }
    }
}