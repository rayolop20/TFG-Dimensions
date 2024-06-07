using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsInfo : MonoBehaviour
{
    
    public bool MinimapActive;
    public bool CamsSwap;
    public Toggle miniMap;
    public Toggle cam1;
    public Toggle cam2;

    void Start()
    {
       // DontDestroyOnLoad(this.gameObject);
        if (miniMap != null)
        {
            // Añade un listener al evento onValueChanged
            miniMap.onValueChanged.AddListener(delegate {
                ToggleValueChanged(miniMap);
            });
        }
        cam1.onValueChanged.AddListener(delegate { OnToggleValueChanged(cam1); });
        cam2.onValueChanged.AddListener(delegate { OnToggleValueChanged(cam2); });
    }


    void ToggleValueChanged(Toggle change)
    {
        MinimapActive = change.isOn; // Cambia el valor del bool según el estado del Toggle
    }

     void OnToggleValueChanged(Toggle toggle)
    {
        // Si se activa toggleA, desactiva toggleB
        if (toggle == cam1 && cam1.isOn)
        {
            cam2.isOn = false;
        }
        // Si se activa toggleB, desactiva toggleA
        else if (toggle == cam2 && cam2.isOn)
        {
            cam1.isOn = false;
        }
    }

    void closeOptionsMenu() { 
        gameObject.SetActive(false);
    }
        
}
