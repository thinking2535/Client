using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClipper : MonoBehaviour
{
    public new Camera camera;
    void OnPreCull()
    {
        GL.Clear(false, true, Color.black);
    }
    public void clip(float aspectRatio) // width divided by height
    {
        var screenAspectRatio = (float)Screen.width / (float)Screen.height;
        float x = 0.0f;
        float y = 0.0f;
        float width = 1.0f;
        float height = 1.0f;

        if (screenAspectRatio > aspectRatio)
        {
            width = aspectRatio / screenAspectRatio;
            x = (1.0f - width) * 0.5f;
        }
        else
        {
            height = screenAspectRatio / aspectRatio;
            y = (1.0f - height) * 0.5f;
        }

        camera.rect = new Rect(x, y, width, height);
    }
}
