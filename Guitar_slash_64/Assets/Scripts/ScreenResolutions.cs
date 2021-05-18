using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolutions : MonoBehaviour
{
    public Dropdown dropdown;
    Resolution[] res;

    void Start()
    {
        Resolution[] resolution = Screen.resolutions;
        res = resolution.Distinct().ToArray();
        string[] strRes = new string[res.Length];
        for (int i = 0; i < res.Length; i++)
        {
            strRes[i] = res[i].width.ToString() + "x" + res[i].height.ToString();
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(strRes.ToList());
        dropdown.value = res.Length - 1;
    }

    public void SetRes()
    {
        Screen.SetResolution(res[dropdown.value].width, res[dropdown.value].height, true);
    }
}
