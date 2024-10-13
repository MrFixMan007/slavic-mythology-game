using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseSettings : MonoBehaviour
{
    // ������ �� ������ ������ ��������
    public GameObject SettingsPanel;

    // ������ �� ������, ������� ���������� ������ ��������
    public Button settingsButton;

    public void CloseSettingsPanel()
    {
        SettingsPanel.SetActive(false);
    }
}
