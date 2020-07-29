using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinHandler : MonoBehaviour
{
    [SerializeField] private Canvas winCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        winCanvas.enabled = false;
    }

    public void HandleWin()
    {
        winCanvas.enabled = true;
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
