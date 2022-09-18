using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float swaySensitivity = 0.02f;
    public float swayClamp = 20f;
    public float swaySmoothness = 20f;

    private Vector3 startPosition;
    private Vector3 nextPosition;
    private Vector3 currentVelocity = Vector3.zero;
    void Start()
    {
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("MouseX") * swaySensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("MouseY") * swaySensitivity * Time.deltaTime;

        mouseX = Mathf.Clamp(mouseX, -swayClamp, swayClamp);
        mouseY = Mathf.Clamp(mouseY, -swayClamp, swayClamp);

        nextPosition = new Vector3(mouseX, mouseY, 0);

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, nextPosition + startPosition, ref currentVelocity, swaySmoothness * Time.deltaTime);
    }
}
