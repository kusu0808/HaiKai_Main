#if UNITY_EDITOR

using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using General;

namespace Validation
{
    public sealed class AudioClipSettingsApplyer : EditorWindow
    {
        private const string FOLDERS_ROOT_PATH = "Assets/_Projects/Sounds";
        private const string MENU_NAME = "Audio Clip Settings";

        [MenuItem("Tools/Validation/" + MENU_NAME)]
        public static void ShowWindow() => GetWindow<AudioClipSettingsApplyer>(MENU_NAME);

        private void OnGUI()
        {
            EditorGUILayout.LabelField("オーディオクリップ", EditorStyles.boldLabel);

            if (GUILayout.Button("【mp3】\nSoundsフォルダ内の\n全オーディオクリップに設定を適用する")) Execute(SoundFileType.MP3);
            if (GUILayout.Button("【wav】\nSoundsフォルダ内の\n全オーディオクリップに設定を適用する")) Execute(SoundFileType.WAV);
        }

        private static void Execute(SoundFileType type)
        {
            try
            {
                ApplyOrThrow("MonoBGM", type, AudioClipLoadType.Streaming, AudioCompressionFormat.Vorbis, true);
                ApplyOrThrow("MonoVoice", type, AudioClipLoadType.CompressedInMemory, AudioCompressionFormat.Vorbis, true);
                ApplyOrThrow("MonoSE", type, AudioClipLoadType.DecompressOnLoad, AudioCompressionFormat.Vorbis, true);
                ApplyOrThrow("MonoSERough", type, AudioClipLoadType.DecompressOnLoad, AudioCompressionFormat.ADPCM, true);

                ApplyOrThrow("StereoBGM", type, AudioClipLoadType.Streaming, AudioCompressionFormat.Vorbis, false);
                ApplyOrThrow("StereoVoice", type, AudioClipLoadType.CompressedInMemory, AudioCompressionFormat.Vorbis, false);
                ApplyOrThrow("StereoSE", type, AudioClipLoadType.DecompressOnLoad, AudioCompressionFormat.Vorbis, false);
                ApplyOrThrow("StereoSERough", type, AudioClipLoadType.DecompressOnLoad, AudioCompressionFormat.ADPCM, false);

                "処理終了".Tell("00ffff");
            }
            catch (Exception e) { e.Error(); }
        }

        private static void ApplyOrThrow(string folderName, SoundFileType type, AudioClipLoadType loadType, AudioCompressionFormat compressionFormat, bool forceToMono)
        {
            string folderPath = Path.Combine(FOLDERS_ROOT_PATH, folderName);
            if (Directory.Exists(folderPath) is false) throw new($"{folderPath}が存在しません");

            string[] filePathes = Directory.GetFiles(folderPath, ToSearchPattern(type), SearchOption.AllDirectories);
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

        private enum SoundFileType
        {
            MP3,
            WAV
        }

        private static string ToSearchPattern(SoundFileType type) => type switch
        {
            SoundFileType.MP3 => "*.mp3",
            SoundFileType.WAV => "*.wav",
            _ => string.Empty
        };
    }
}

#endif
