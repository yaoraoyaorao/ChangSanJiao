using GF;
using GF.Data;

public class DataManager : Singleton<DataManager>
{
    public Config config;
    public DataManager()
    {
        config = JsonMgr.Instance.LoadData<Config>("Config");
    }

    public void SaveConfig()
    {
        JsonMgr.Instance.SaveData(config,"Config",SavePath.streamingAssetsPath);
    }
}
