using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCursor : MonoBehaviour
{
    private void Awake()//aflæsir cursor
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
