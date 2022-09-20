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
        
        //�󶨱�����ַ
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
		//ȥ���ַ����еĿո�
		_hex16String = _hex16String.Replace(" ", "");
		if (_hex16String.Length / 2 == 0)
		{
			_hex16String += " ";
		}
		//����һ���ֽ����飬�䳤�ȵ����ַ������ȵ�һ�롣
		byte[] buffer = new byte[_hex16String.Length / 2];
		for (int i = 0; i < buffer.Length; i++)
		{
			//Ϊ�ֽ������Ԫ�ظ�ֵ��
			buffer[i] = Convert.ToByte((_hex16String.Substring(i * 2, 2)), 16);
		}
		//�����ֽ����顣
		return buffer;
	}


	IEnumerator SendDataCorutine(string str1, string str2, float time = 0.5f)
    {
		SendHex(str1);
		yield return new WaitForSeconds(time);
		SendHex(str2);
    }
}
