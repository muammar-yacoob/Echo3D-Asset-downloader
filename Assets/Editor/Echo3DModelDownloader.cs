using System;
using UnityEngine;
using UnityEditor;
using System.IO;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using UnityEngine.Networking;

public class Echo3DModelDownloader : EditorWindow
{
    //https://console.echo3d.com/#/pages/contentmanager
    private string apiKey = "dry-rice-2801";
    private string secKey = "wNYXnx9VhJHVXDUWeNZ0gjZN";
    private string modelId = "05f679e5-11aa-4599-ab43-3a3d3f0b618a";
    private string fileFormat;
    private Texture2D windowIcon;
    private string defaultSavePath = "Assets/Echo3DModels/";

    private void OnFocus()
    {
        fileFormat = "gltf";
    }

    [MenuItem("Asset Management/Echo3D Model Downloader")]
    public static void ShowWindow()
    {
        Echo3DModelDownloader window = GetWindow<Echo3DModelDownloader>();
        window.titleContent = new GUIContent("Echo3D Model Downloader", window.windowIcon);
    }

    private void OnEnable()
    {
        windowIcon = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/res/echo3d-logo.png", typeof(Texture2D));
        titleContent.image = windowIcon;
    }

    void OnGUI()
    {
        GUILayout.Label("Echo3D Model Downloader", EditorStyles.boldLabel);

        //apiKey = EditorGUILayout.TextField("API Key", apiKey);
        modelId = EditorGUILayout.TextField("Model ID", modelId);
        //fileFormat = EditorGUILayout.TextField("File Format", fileFormat);

        if (string.IsNullOrEmpty(modelId) || modelId.Length < 13 || string.IsNullOrEmpty(apiKey))
        {
            GUI.enabled = false;
        }

        if (GUILayout.Button("Fetch Model"))
        {
            FetchAndDownloadModel().Forget();
        }

        GUI.enabled = true;
    }

    async UniTaskVoid FetchAndDownloadModel()
    {
        /// Example usage on: https://docs.echo3d.com/download
        string downloadUrl =
            $"https://api.echo3D.com/download/model?key={apiKey}&entry={modelId}&fileFormat={fileFormat}&convertMissing=true&secKey={secKey}";
        await DownloadModel(downloadUrl);
    }

    async UniTask DownloadModel(string url)
    {
        using (var request = UnityWebRequest.Get(url))
        {
            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                //you can only parse gltf file contents, DOH!
                var currentModelInfo = JsonUtility.FromJson<ModelInfo>(request.downloadHandler.text);
                byte[] modelBytes = request.downloadHandler.data;

                string fileName = currentModelInfo.asset.extras.title + $".{fileFormat}"; 
                Directory.CreateDirectory(defaultSavePath);
                string savePath = Path.Combine(defaultSavePath, fileName);

                File.WriteAllBytes(savePath, modelBytes);
                Debug.Log($"Model is downloaded to: " + defaultSavePath);
            }
            else
            {
                Debug.LogError("Failed to download the model: " + request.error);
            }
        }
    }
}

