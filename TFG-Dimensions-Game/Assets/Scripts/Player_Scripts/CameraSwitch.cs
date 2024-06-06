using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public class CameraSwitcher : MonoBehaviour
    {
        public Camera mainCamera; // Asigna tu c�mara principal en el Inspector
        public Camera arround3D; // Asigna tu c�mara secundaria en el Inspector
        public Camera arround2D; // Asigna tu c�mara secundaria en el Inspector

        void Start()
        {
            // Aseg�rate de que solo una c�mara est� activa al inicio
            mainCamera.enabled = true;
            arround3D.enabled = false;
        }

        void Update()
        {
            // Cambiar de c�mara al presionar la tecla C
            if (Input.GetKeyDown(KeyCode.C))
            {
                mainCamera.enabled = !mainCamera.enabled;
                arround3D.enabled = !arround3D.enabled;
            }
        }
    }
}
