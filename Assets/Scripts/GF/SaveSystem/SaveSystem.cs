/*-----------------------------------------------
// 作    者：RCY
// 文 件 名：SaveManager
// 创建日期：2022/5/12 21:11:52
// 功能描述：存档系统
// 修改日期：
// 修改描述：
-----------------------------------------------*/

using GF.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


namespace GF
{
    public enum E_FileType
    {
        Json,
        XML,
        Binary,
        PlayerPrefab,

    }

    public class SaveSystem
    {
        public string MainFolderName { get; set; }
        public string Suffix { get; set; }
        public string Path => Application.persistentDataPath + "/" + MainFolderName;
        public E_FileType CurrentType;
        public List<SaveInfo> saveInfos = new List<SaveInfo>();
        public List<string> fileList = new List<string>();

        public SaveSystem(string mainFolderName, List<string> file, E_FileType type = E_FileType.Json, string suffix = "json")
        {
            MainFolderName = mainFolderName;
            CurrentType = type;
            Suffix = suffix;
            fileList = file;
            Create(Path);
        }

        public T LoadData<T>(string fileName, string folderName = "") where T : IDataModel, new()
        {
            string savePath = Path + "/" + folderName;

            if (!string.IsNullOrEmpty(folderName))
                Create(savePath);

            string filePath = MainFolderName + "/" + folderName + "/" + fileName;
            switch (CurrentType)
            {
                case E_FileType.Json:
                    return JsonMgr.Instance.LoadData<T>(filePath);
                case E_FileType.XML:
                    break;
                case E_FileType.Binary:
                    break;
                case E_FileType.PlayerPrefab:
                    break;

            }
            return null;
        }

        public void Save(object obj, string fileName, string folderName = "")
        {
            string savePath = Path + "/" + folderName;

            if (!string.IsNullOrEmpty(folderName))
                Create(savePath);

            string filePath = MainFolderName + "/" + folderName + "/" + fileName;
            switch (CurrentType)
            {
                case E_FileType.Json:
                    JsonMgr.Instance.SaveData(obj, filePath);
                    break;
                case E_FileType.XML:
                    break;
                case E_FileType.Binary:
                    break;
                case E_FileType.PlayerPrefab:
                    break;
            }
        }

        public bool DeleteSave(string fileName)
        {
            DirectoryInfo mainFolder = new DirectoryInfo(Path);
            DirectoryInfo[] subFolder = mainFolder.GetDirectories();
            for (int i = 0; i < subFolder.Length; i++)
            {
                if (subFolder[i].Name == fileName)
                {
                    FileInfo[] fileInfos = subFolder[i].GetFiles();
                    for (int j = 0; j < fileInfos.Length; j++)
                    {
                        File.Delete(fileInfos[j].FullName);
                    }
                    subFolder[i].Delete(true);
                    return true;
                }
            }
            return false;
        }

        public bool CheckSaveInfoName(string fileName)
        {
            DirectoryInfo mainFolder = new DirectoryInfo(Path);
            DirectoryInfo[] subFolder = mainFolder.GetDirectories();
            for (int i = 0; i < subFolder.Length; i++)
            {
                if (subFolder[i].Name == fileName)
                {
                    return true;
                }
            }
            return false;
        }

        public List<SaveInfo> GetSaveInfo()
        {
            DirectoryInfo mainFolder = new DirectoryInfo(Path);
            DirectoryInfo[] subFolder = mainFolder.GetDirectories();
            List<SaveInfo> saveInfos = new List<SaveInfo>();

            List<string> temp = new List<string>();
            SaveInfo saveInfo;
            bool flag = true;
            for (int i = 0; i < subFolder.Length; i++)
            {
                FileInfo[] infos = subFolder[i].GetFiles();
                List<string> fileNames = new List<string>();
                for (int j = 0; j < infos.Length; j++)
                {
                    string[] fileInfo = infos[j].Name.Split('.');

                    if (fileInfo[1] == Suffix)
                    {
                        fileNames.Add(fileInfo[0]);
                    }

                }

                temp = fileList.Intersect(fileNames).ToList();
                if (temp.Count == fileList.Count)
                {
                    for (int k = 0; k < temp.Count; k++)
                    {
                        if (!fileList.Contains(temp[k]))
                        {

                            flag = false;
                            break;
                        }
                    }
                }
                else
                {
                    flag = false;
                }

                if (flag)
                {
                    saveInfo = new SaveInfo();
                    saveInfo.SaveName = subFolder[i].Name;
                    saveInfo.SaveTime = subFolder[i].CreationTime.ToString();
                    saveInfos.Add(saveInfo);
                }
            }

            return saveInfos;
        }

        private void Create(string path)
        {
            if (!Directory.Exists(path))
            {
                Debug.Log("存档文件创建成功");
                Directory.CreateDirectory(path);
            }
        }



    }
}

