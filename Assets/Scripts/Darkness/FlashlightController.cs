using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    // Cambiar a algo mas optimo, que se ejecute cuando se mueva el raton por ejemplo
    void Update()
    {
        Vector2 brazoPosition = transform.position;
        float actualX = transform.position.x;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - brazoPosition;
        transform.right = direction * transform.parent.localScale.x;

        if ((mousePosition.x < actualX && transform.parent.localScale.x > 0) || (mousePosition.x >= actualX && transform.parent.localScale.x < 0))
            transform.parent.localScale = new Vector3(-transform.parent.localScale.x, transform.parent.localScale.y, transform.parent.localScale.z);
    }
}
