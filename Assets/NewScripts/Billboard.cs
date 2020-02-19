using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera mainCamera;

    private void LateUpdate()
    {
        transform.forward = mainCamera.transform.forward;
    }
}
