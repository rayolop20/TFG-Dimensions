using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    
        public Camera mainCamera; // Asigna tu c�mara principal en el Inspector
        public Camera arround3D; // Asigna tu c�mara secundaria en el Inspector
        public Camera arround2D; // Asigna tu c�mara secundaria en el Inspector

        public bool CameraFCamActve;

        void Start()
        {
            // Aseg�rate de que solo una c�mara est� activa al inicio
            mainCamera.enabled = true;
            arround3D.enabled = false;
            arround2D.enabled = false;
        }

        void Update()
        {
            // Cambiar de c�mara al presionar la tecla C
            if (Input.GetKeyDown(KeyCode.LeftShift) && CameraFCamActve == true)
            {
                mainCamera.enabled = !mainCamera.enabled;
                arround3D.enabled = !arround3D.enabled;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && CameraFCamActve == false)
            {
                mainCamera.enabled = !mainCamera.enabled;
                arround2D.enabled = !arround2D.enabled;
            }
           // else if ( Input.GetKeyUp(KeyCode.LeftShift))
           // {
           //     mainCamera.enabled = !mainCamera.enabled;
           //     arround3D.enabled = !arround3D.enabled;
           // }
        }
    
}
