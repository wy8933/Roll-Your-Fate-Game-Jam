using UnityEngine;

namespace Utils
{
    public class CameraFacer : MonoBehaviour
    {
        Camera camera;
        void Start()
        {
            camera = Camera.main;
        }

        void Update()
        {
            transform.LookAt(camera.gameObject.transform.position);
        }
    }
}