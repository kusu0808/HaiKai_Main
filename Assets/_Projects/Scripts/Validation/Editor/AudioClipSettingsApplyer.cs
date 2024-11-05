#if UNITY_EDITOR

using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using General;

public sealed class AudioClipSettingsApplyer : EditorWindow
{
    private const string FOLDERS_ROOT_PATH = "Assets/_Projects/Sounds";
    private const string MENU_NAME = "Audio Clip Settings";

    [MenuItem("Tools/Validation/" + MENU_NAME)]
    public static void ShowWindow() => GetWindow<AudioClipSettingsApplyer>(MENU_NAME);

    private void OnGUI()
    {
        EditorGUILayout.LabelField("オーディオクリップ(mp3)", EditorStyles.boldLabel);

        if (GUILayout.Button("Soundsフォルダ内の\n全オーディオクリップに設定を適用する"))
        {
            Execute();
        }
    }

    private static void Execute()
    {
        try
        {
            ApplyOrThrow("MonoBGM", AudioClipLoadType.Streaming, AudioCompressionFormat.Vorbis, true);
            ApplyOrThrow("MonoVoice", AudioClipLoadType.CompressedInMemory, AudioCompressionFormat.Vorbis, true);
            ApplyOrThrow("MonoSE", AudioClipLoadType.DecompressOnLoad, AudioCompressionFormat.Vorbis, true);
            ApplyOrThrow("MonoSERough", AudioClipLoadType.DecompressOnLoad, AudioCompressionFormat.ADPCM, true);

            ApplyOrThrow("StereoBGM", AudioClipLoadType.Streaming, AudioCompressionFormat.Vorbis, false);
            ApplyOrThrow("StereoVoice", AudioClipLoadType.CompressedInMemory, AudioCompressionFormat.Vorbis, false);
            ApplyOrThrow("StereoSE", AudioClipLoadType.DecompressOnLoad, AudioCompressionFormat.Vorbis, false);
            ApplyOrThrow("StereoSERough", AudioClipLoadType.DecompressOnLoad, AudioCompressionFormat.ADPCM, false);

            "処理終了".Tell("00ffff");
        }
        catch (Exception e) { e.Error(); }
    }

    private static void ApplyOrThrow(string folderName, AudioClipLoadType loadType, AudioCompressionFormat compressionFormat, bool forceToMono)
    {
        string folderPath = Path.Combine(FOLDERS_ROOT_PATH, folderName);
        if (Directory.Exists(folderPath) is false) throw new($"{folderPath}が存在しません");

        string[] filePathes = Directory.GetFiles(folderPath, "*.mp3", SearchOption.AllDirectories);
        foreach (string filePath in filePathes)
        {
            string assetPath = filePath.Replace(Application.dataPath, "").Replace('\\', '/');
            AudioImporter importer = AssetImporter.GetAtPath(assetPath) as AudioImporter;
            if (importer == null) continue;

            AudioImporterSampleSettings settings = importer.defaultSampleSettings;
            settings.loadType = loadType;
            settings.compressionFormat = compressionFormat;
            importer.defaultSampleSettings = settings;
            importer.forceToMono = forceToMono;

            importer.SaveAndReimport();
        }
    }
}

#endif
