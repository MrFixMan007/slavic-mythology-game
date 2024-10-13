using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseSettings : MonoBehaviour
{
    // —сылка на объект панели настроек
    public GameObject SettingsPanel;

    // —сылка на кнопку, котора€ активирует панель настроек
    public Button settingsButton;

    public void CloseSettingsPanel()
    {
        SettingsPanel.SetActive(false);
    }
}
