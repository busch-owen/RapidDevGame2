using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiHide : MonoBehaviour
{

    public GameObject Ui;
    public GameObject OtherButtons;

    public void HideUi()
    {
        Ui.SetActive(false);
        OtherButtons.transform.localPosition = new Vector3(400, 0, 0);
    }

    public void ShowUi()
    {
        Ui.SetActive(true);
        OtherButtons.transform.localPosition = new Vector3(0, 0, 0);
    }
}
