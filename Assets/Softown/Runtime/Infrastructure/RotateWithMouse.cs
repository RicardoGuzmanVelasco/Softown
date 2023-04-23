using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public class RotateWithMouse : MonoBehaviour
    {
        [SerializeField] float sensitivity = 10f;
        [SerializeField] float maxYAngle = 80f;
        
        Vector2 currentRotation;
        
        void Update()
        {
            if(!Input.GetMouseButton(0))
                return;
            
            currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
            currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
            
            transform.rotation = Quaternion.Euler(currentRotation.y,currentRotation.x,0);
        }
    }
}