using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSettings : MonoBehaviour
{
    // ������ �� ������ ������ ��������
    public GameObject SettingsPanel;

    // ������ �� ������, ������� ���������� ������ ��������
    public Button settingsButton;

    public void ShowSettingsPanel()
    {
        SettingsPanel.SetActive(true);
    }
}
