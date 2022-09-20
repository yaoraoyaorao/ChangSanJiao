using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using GF;

public class GFMenu : EditorWindow
{
    [MenuItem("GF/��ʼ����Ŀ")]
    private static void InitProject()
    {
        if (!File.Exists(GFPath.UI))
        {
            Directory.CreateDirectory(GFPath.UI);
        }

        if (!File.Exists(GFPath.Animator))
        {
            Directory.CreateDirectory(GFPath.Animator);
        }

        if (!File.Exists(GFPath.StreamAssets))
        {
            Directory.CreateDirectory(GFPath.StreamAssets);
        }

        if (!File.Exists(GFPath.GameMain))
        {
            Directory.CreateDirectory(GFPath.GameMain);
        }
        AssetDatabase.Refresh();
    }

    [MenuItem("GF/����·��/persistentDataPath")]
    private static void PerPath()
    {
        Debug.Log("==GF==:   " + Application.persistentDataPath);
    }

    [MenuItem("GF/����·��/streamingAssetsPath")]
    private static void PerStream()
    {
        Debug.Log("==GF==:   " + Application.streamingAssetsPath);
    }

    [MenuItem("GF/�����ļ�/streamingAssetsPath")]
    private static void CreateStream()
    {
        if(!File.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
            Debug.Log("==GF==:   �����ɹ�");
            AssetDatabase.Refresh();
        }
        else
        {
            Debug.Log("==GF==:   �ļ����Ѵ���");
        }
            
        
    }

}
