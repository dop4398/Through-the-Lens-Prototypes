using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Code Source: https://answers.unity.com/questions/22954/how-to-save-a-picture-take-screenshot-from-a-camer.html
/// </summary>
public class ScreenshotMaker : MonoBehaviour
{
    public Camera camera;

    private bool takeHiResShot = false;

    public static string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/Resources/Screenshots/screen_{1}x{2}_{3}.png",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void TakeHiResShot()
    {
        takeHiResShot = true;
    }

    void LateUpdate()
    {
        takeHiResShot |= Input.GetKeyDown("k");
        if (takeHiResShot)
        {
            RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
            camera.targetTexture = rt;
            int size = (int)(Screen.width * 500f / 1256f); // #### Custom Ratio ####
            Texture2D screenShot = new Texture2D(size, size, TextureFormat.RGB24, false);
            camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect((Screen.width - size) / 2, (Screen.height - size) / 2, (Screen.width - size) / 2 + size, (Screen.height - size) / 2 + size), 0, 0);
            camera.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToPNG();
            string filename = ScreenShotName(size, size);
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
            takeHiResShot = false;
        }
    }
}
