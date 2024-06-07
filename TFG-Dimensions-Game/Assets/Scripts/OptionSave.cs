using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSave : MonoBehaviour
{
    // Start is called before the first frame update
    SettingsInfo SettingsInfo;
    public bool camera1On;
    public bool miniMap;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SettingsInfo = GameObject.Find("Options").gameObject.GetComponent<SettingsInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        camera1On = SettingsInfo.cam1;
        miniMap = SettingsInfo.miniMap;
    }
}
