using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public class CameraSwitcher : MonoBehaviour
    {
        public Camera mainCamera; // Asigna tu cámara principal en el Inspector
        public Camera arround3D; // Asigna tu cámara secundaria en el Inspector
        public Camera arround2D; // Asigna tu cámara secundaria en el Inspector

        void Start()
        {
            // Asegúrate de que solo una cámara esté activa al inicio
            mainCamera.enabled = true;
            arround3D.enabled = false;
        }

        void Update()
        {
            // Cambiar de cámara al presionar la tecla C
            if (Input.GetKeyDown(KeyCode.C))
            {
                mainCamera.enabled = !mainCamera.enabled;
                arround3D.enabled = !arround3D.enabled;
            }
        }
    }
}
