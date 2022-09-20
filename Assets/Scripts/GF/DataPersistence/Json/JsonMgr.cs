/*-----------------------------------------------
 // 作    者：RCY
 // 文 件 名：JsonData
 // 创建日期：2022/4/14 22:42:12
 // 功能描述：
 // 修改日期：
 // 修改描述：
-----------------------------------------------*/

using LitJson;
using System;
using System.IO;
using UnityEngine;

namespace GF.Data
{
    public enum JsonType
    {
        JsonUtlity,
        LitJson
    }

    public enum SavePath
    {
        persistentDataPath,
        streamingAssetsPath
    }

    public class JsonMgr : Singleton<JsonMgr>
    {
        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="fileName">文件名</param>
        /// <param name="type">Json类型</param>
        public void SaveData(object data, string fileName,SavePath savePath = SavePath.persistentDataPath, JsonType type = JsonType.LitJson)
        {
            string path;

            switch (savePath)
            {
                case SavePath.persistentDataPath:
                    path = SavePerPath(fileName);
                    break;
                case SavePath.streamingAssetsPath:
                    path = SaveStreamPath(fileName);
                    break;
                default:
                    path = SavePerPath(fileName);
                    break;
            }

            
            string jsonStr = "";
            switch (type)
            {
                case JsonType.JsonUtlity:
                    jsonStr = JsonUtility.ToJson(data);
                    break;
                case JsonType.LitJson:
                    jsonStr = JsonMapper.ToJson(data);
                    break;
            }

            File.WriteAllText(path, jsonStr);
        }


        /// <summary>
        /// 读取数据
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="fileName">文件名</param>
        /// <param name="type">Json类型</param>
        /// <returns></returns>
        public T LoadData<T>(string fileName,JsonType type = JsonType.LitJson)where T:new()
        {
            string path = SaveStreamPath(fileName);

            if (!File.Exists(path))
            {
                path = SavePerPath(fileName);
            }

            if (!File.Exists(path))
            {
                return new T();
            }

            string jsonStr = File.ReadAllText(path);
            T data = default(T);
            switch (type)
            {
                case JsonType.JsonUtlity:
                    data = JsonUtility.FromJson<T>(jsonStr);
                    break;
                case JsonType.LitJson:
                    data = JsonMapper.ToObject<T>(jsonStr);
                    break;
            }
            return data;
        }

        public object LoadData(string fileName,Type type,JsonType jsontype = JsonType.LitJson)
        {
            string path = SaveStreamPath(fileName);

            if (!File.Exists(path))
            {
                path = SavePerPath(fileName);
            }

            if (!File.Exists(path))
            {
                return new object();
            }

            string jsonStr = File.ReadAllText(path);
            object data = new object();
            switch (jsontype)
            {
                case JsonType.JsonUtlity:
                    data = JsonUtility.FromJson(jsonStr,type);
                    break;
                case JsonType.LitJson:
                    data = JsonMapper.ToObject(jsonStr,type);
                    break;
            }
            return data;
        }

        private string SavePerPath(string fileName)
        {
            return Application.persistentDataPath + "/" + fileName + ".json";
        }
        private string SaveStreamPath(string fileName)
        {
            return Application.streamingAssetsPath + "/" + fileName + ".json";
        }
    }


}

