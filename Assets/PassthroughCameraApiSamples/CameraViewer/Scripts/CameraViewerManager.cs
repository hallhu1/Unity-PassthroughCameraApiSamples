// Copyright (c) Meta Platforms, Inc. and affiliates.

using System.Collections;
using Meta.XR.Samples;
using UnityEngine;
using UnityEngine.UI;

namespace PassthroughCameraSamples.CameraViewer
{
    [MetaCodeSample("PassthroughCameraApiSamples-CameraViewer")]
    public class CameraViewerManager : MonoBehaviour
    {
        // Create a field to attach the reference to the WebCamTextureManager prefab
        [SerializeField] private WebCamTextureManager m_webCamTextureManager;
        [SerializeField] private Text m_debugText;
        [SerializeField] private RawImage m_image;

        private IEnumerator Start()
        {
            while (m_webCamTextureManager.WebCamTexture == null)
            {
                yield return null;
            }
            m_debugText.text += "\nWebCamTexture Object ready and playing.";
            // Set WebCamTexture GPU texture to the RawImage Ui element
            m_image.texture = m_webCamTextureManager.WebCamTexture;
        }
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
        private void Update() {
             m_debugText.text = PassthroughCameraPermissions.HasCameraPermission == true ? "Permission granted." : "No permission granted.";
             if (Input.GetKeyDown(KeyCode.S))
            {
                SaveImageToDisk(ConvertWebCamTextureToTexture2D(m_webCamTextureManager.WebCamTexture));
            }
             
        }
        
    }
}
