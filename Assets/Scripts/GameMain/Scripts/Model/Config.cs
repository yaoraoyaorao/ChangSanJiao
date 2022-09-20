using System.Collections;
using System.Collections.Generic;

public class Config
{
    public string Local_IP = "127.0.0.1";
    public int Local_Port = 20021;

    /// <summary>
    /// 192.168.1.8   udp ¶Ë¿ÚºÅ£º10089
    /// </summary>
    public string Send_IP;
    public int Send_Port;

    public string StartUp1 = "03 06 00 07 00 01 F8 29";
    public string StartUp2 = "03 06 00 07 00 00 39 E9";

    public string Point1_1 = "03 06 00 04 00 01 08 29";
    public string Point1_2 = "03 06 00 04 00 00 C9 E9";

    public string Point2_1 = "03 06 00 05 00 01 59 E9";
    public string Point2_2 = "03 06 00 05 00 00 98 29";

    public string Point3_1 = "03 06 00 06 00 01 A9 E9";
    public string Point3_2 = "03 06 00 06 00 00 68 29";

    public Dictionary<string, string> VideoPath = new Dictionary<string, string>();
}
