using UnityEngine;
using UnityEditor;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

public class Echo3DModelDownloader : EditorWindow
{
    private string modelId = "644da764-822d-luaaa-b2db-4f2da35d48db";
    private string apiKey = "dark-art-8233";
    private string secKey = "72ap8O5TuHAAKkOQEoJnsTFB";
    private string fileFormat = "obj";
    private Texture2D windowIcon;

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

        apiKey = EditorGUILayout.TextField("API Key", apiKey);
        modelId = EditorGUILayout.TextField("Model ID", modelId);
        fileFormat = EditorGUILayout.TextField("File Format", fileFormat);

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
        await DownloadModel();
    }

    /// <summary>
    /// Example usage on: https://docs.echo3d.com/download
    /// </summary>
    async UniTask DownloadModel()
    {
        string downloadUrl =
            $"https://api.echo3D.com/download/model?key={apiKey}&entry={modelId}&fileFormat={fileFormat}&convertMissing=true&secKey={secKey}";
        using (var request = UnityWebRequest.Get(downloadUrl))
        {
            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                byte[] modelBytes = request.downloadHandler.data;

                string savePath = EditorUtility.SaveFilePanel("Save Model", "", modelId, fileFormat);
                if (!string.IsNullOrEmpty(savePath))
                {
                    File.WriteAllBytes(savePath, modelBytes);
                    Debug.Log("Model downloaded and saved to: " + savePath);
                }
            }
            else
            {
                Debug.LogError("Failed to download the model: " + request.error);
            }
        }
    }
}
