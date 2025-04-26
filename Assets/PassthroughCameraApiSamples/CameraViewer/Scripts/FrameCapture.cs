using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils;
using PassthroughCameraSamples;


public class FrameCapture : MonoBehaviour
{
    public WebCamTextureManager m_webCamTextureManager;

    public Mat CaptureFrame()
    {
        Texture2D frameTexture = ConvertWebCamTextureToTexture2D(m_webCamTextureManager.WebCamTexture);

        // debug
        // SaveImageToDisk(frameTexture);

        Mat frameMat = new Mat(frameTexture.height, frameTexture.width, CvType.CV_8UC3);
        Utils.texture2DToMat(frameTexture, frameMat);

        Destroy(frameTexture);

        return frameMat;
    }
    public Mat GetIntrinsicMat()
    {
        // 1. Grab the intrinsics struct from the PCA utils:
        var intrinsics = PassthroughCameraUtils.GetCameraIntrinsics(PassthroughCameraEye.Left);
        //    └── focal lengths in pixels
        float fx = intrinsics.FocalLength.x;
        float fy = intrinsics.FocalLength.y;
        //    └── principal point in pixels (from top‑left of the image)
        float cx = intrinsics.PrincipalPoint.x;
        float cy = intrinsics.PrincipalPoint.y;
        //    └── sensor skew (almost always zero, but provided)
        float skew = intrinsics.Skew;
        //    └── resolution at which these intrinsics apply
        int width  = intrinsics.Resolution.x;
        int height = intrinsics.Resolution.y;

        // create a 3×3 CV_32F matrix
        Mat camMatrix = new Mat(3, 3, CvType.CV_32F);
        camMatrix.put(0, 0, fx);
        camMatrix.put(0, 1, skew);
        camMatrix.put(0, 2, cx);
        camMatrix.put(1, 0, 0f);
        camMatrix.put(1, 1, fy);
        camMatrix.put(1, 2, cy);
        camMatrix.put(2, 0, 0f);
        camMatrix.put(2, 1, 0f);
        camMatrix.put(2, 2, 1f);

        return camMatrix;
    }

    // public static Mat GetDistortionMat() {
    //     // after you’ve called EnsureInitialized() internally to populate the camera‑manager…
    //     var cameraId  = PassthroughCameraUtils.GetCameraIdByEye(PassthroughCameraEye.Left);
    //     var characteristics = s_cameraManager.Call<AndroidJavaObject>("getCameraCharacteristics", cameraId);

    //     // fetch radial (deprecated) or distortion array (API 31+)
    //     float[] distArray = characteristics.Call<float[]>("get",
    //         characteristics.GetStatic<AndroidJavaObject>("LENS_DISTORTION")); 
    //     // fallback if that’s null:
    //     if (distArray == null)
    //         distArray = characteristics.Call<float[]>("get",
    //             characteristics.GetStatic<AndroidJavaObject>("LENS_RADIAL_DISTORTION"));

    //     // pack into a 1×N Mat
    //     Mat distCoeffs = new Mat(1, distArray.Length, CvType.CV_32F);
    //     distCoeffs.put(0, 0, distArray);

    //     return distCoeffs;
    // }

    private Texture2D ConvertWebCamTextureToTexture2D(WebCamTexture webCamTexture)
        {
            if (webCamTexture == null)
            {
                Debug.LogError("ConvertWebCamTextureToTexture2D: webCamTexture is null!");
                return null;
            }
            Texture2D tex2D = new Texture2D(webCamTexture.width, webCamTexture.height, TextureFormat.RGB24, false);
            // Copy pixel data from the WebCamTexture into the Texture2D.
            tex2D.SetPixels(webCamTexture.GetPixels());
            tex2D.Apply();
            return tex2D;
        }

    // debug functions ------------------------------------------
    public void SaveImageToDisk(Texture2D projectedTexture)
    {
        if (projectedTexture != null)
        {
            byte[] pngData = projectedTexture.EncodeToPNG();
            string filePath = "C:/Users/hallhu/SketchGenie/Assets/Scenes/capturedFrame.png";
            System.IO.File.WriteAllBytes(filePath, pngData);
            Debug.Log("Saved image to: " + filePath);
        }
        else
        {
            Debug.LogWarning("No texture to save!");
        }
    }
    private IEnumerator Start()
    {
        Debug.Log("Manager reference: " + (m_webCamTextureManager == null ? "NULL" : "OK"));

        while (m_webCamTextureManager.WebCamTexture == null)
            {
                yield return null;
            }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            CaptureFrame();
        }
    }
}
