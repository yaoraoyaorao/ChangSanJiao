using System.Collections.Generic;

namespace GF
{
    public class VideoManager : SingletonMono<VideoManager>
    {
        private Dictionary<string, IVideoControl> videoDic = new Dictionary<string, IVideoControl>();

        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                videoDic.Add(transform.GetChild(i).name, transform.GetChild(i).GetComponent<IVideoControl>());
            }
        }

        public IVideoControl GetControl(string str)
        {
            if(videoDic.TryGetValue(str,out IVideoControl value))
            {
                return value;
            }
            return null;
        }
    }
}

