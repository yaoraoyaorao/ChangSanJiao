using GF;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class UDPMgr:SingletonMono<UDPMgr>
{
    private Socket socket;
    private Config config;

    private EndPoint Local;
    private EndPoint Send;

	private byte[] data;

    protected override void Awake()
    {
        base.Awake();

		config = DataManager.Instance.config;
        Local = new IPEndPoint(IPAddress.Parse(config.Local_IP), config.Local_Port);
        Send = new IPEndPoint(IPAddress.Parse(config.Send_IP), config.Send_Port);

        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        
        //绑定本机地址
        socket.Bind(Local);
    }

	public void SendData(string str1,string str2,float time = 0.5f)
    {
		StartCoroutine(SendDataCorutine(str1, str2, time));
    }

	private void SendHex(string sendStr)
    {
		data = new byte[512];
		data = Hex16StringToHex16Byte(sendStr);
		socket.SendTo(data,Send);
    }


	private byte[] Hex16StringToHex16Byte(string _hex16String)
	{
		//去掉字符串中的空格。
		_hex16String = _hex16String.Replace(" ", "");
		if (_hex16String.Length / 2 == 0)
		{
			_hex16String += " ";
		}
		//声明一个字节数组，其长度等于字符串长度的一半。
		byte[] buffer = new byte[_hex16String.Length / 2];
		for (int i = 0; i < buffer.Length; i++)
		{
			//为字节数组的元素赋值。
			buffer[i] = Convert.ToByte((_hex16String.Substring(i * 2, 2)), 16);
		}
		//返回字节数组。
		return buffer;
	}


	IEnumerator SendDataCorutine(string str1, string str2, float time = 0.5f)
    {
		SendHex(str1);
		yield return new WaitForSeconds(time);
		SendHex(str2);
    }
}
