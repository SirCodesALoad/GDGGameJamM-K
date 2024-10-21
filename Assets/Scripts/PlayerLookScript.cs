using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookScript : MonoBehaviour
{
    
    [SerializeField] private float xMin = -8, yMin = -4, xMax = 8, yMax = 12;
    public float sensitivity = 100f;
    private float yRotation = 0;
    private float xRotation = 0;

    
    void Start()
    {
    }

    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * -sensitivity * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, yMin, yMax);
        xRotation -= mouseX;
        xRotation = Mathf.Clamp(xRotation, xMin, xMax);

        transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0);
        
    }
}
