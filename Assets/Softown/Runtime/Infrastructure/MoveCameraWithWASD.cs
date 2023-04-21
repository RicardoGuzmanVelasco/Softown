using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public class MoveCameraWithWASD : MonoBehaviour
    {
        public float Speed = 1f;
        public float ZoomSpeed = 1f;
        public float ZoomMin = 1f;
        public float ZoomMax = 100f;
        public float Zoom = 10f;
        public float ZoomStep = 1f;
        
        void Update()
        {
            var zoom = Zoom;
            if(Input.mouseScrollDelta.y != 0)
                zoom = Mathf.Clamp(Zoom + Input.mouseScrollDelta.y * ZoomSpeed, ZoomMin, ZoomMax);
            
            var zoomFactor = zoom / Zoom;
            var speed = Speed * zoomFactor;
            var zoomStep = ZoomStep * zoomFactor;
            
            var position = transform.position;
            if(Input.GetKey(KeyCode.W))
                position += Vector3.forward * speed;
            if(Input.GetKey(KeyCode.S))
                position += Vector3.back * speed;
            if(Input.GetKey(KeyCode.A))
                position += Vector3.left * speed;
            if(Input.GetKey(KeyCode.D))
                position += Vector3.right * speed;
            if(Input.GetKey(KeyCode.Q))
                zoom = Mathf.Clamp(Zoom - zoomStep, ZoomMin, ZoomMax);
            if(Input.GetKey(KeyCode.E))
                zoom = Mathf.Clamp(Zoom + zoomStep, ZoomMin, ZoomMax);
            
            transform.position = position;
            transform.LookAt(Vector3.zero);
            Camera.main.orthographicSize = zoom;
            
            Zoom = zoom;
        }
    }
}