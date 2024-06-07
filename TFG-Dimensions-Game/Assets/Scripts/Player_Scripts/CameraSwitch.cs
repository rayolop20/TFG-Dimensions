using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraSwitch : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera mainCamera;
    public Camera arround3D;
    public Camera arround2D;
    public SettingsInfo options;
    public GameObject miniMap;

    public bool cameraFCamActve;
    public bool cameraActive;

    void Start()
    {

        options.MinimapActive = true;
        options.cam1.isOn = true;
        options.cam2.isOn = false;

        mainCamera.enabled = true;
        arround3D.enabled = false;
        arround2D.enabled = false;
    }

    void Update()
    {

        miniMap.SetActive(options.MinimapActive);

        if (options.cam1.isOn == true)
        {
            cameraFCamActve = true;
        }
        if (options.cam2.isOn == true)
        {
            cameraFCamActve = false;
        }


        if (Input.GetKeyDown(KeyCode.LeftShift) && cameraFCamActve == true)
        {
            mainCamera.enabled = !mainCamera.enabled;
            arround3D.enabled = !arround3D.enabled;
            cameraActive = !cameraActive;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && cameraFCamActve == false)
        {
            mainCamera.enabled = !mainCamera.enabled;
            arround2D.enabled = !arround2D.enabled;
            cameraActive = !cameraActive;
        }

    }

}
