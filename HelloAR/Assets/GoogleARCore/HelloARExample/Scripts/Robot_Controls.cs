using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TechTweaking.Bluetooth;
using System;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;

public class Robot_Controls : MonoBehaviour
{

    // Use this for initialization
    [SerializeField]
    private Button _Up;
    [SerializeField]
    private Button _Down;
    [SerializeField]
    private Button _Left;
    [SerializeField]
    private Button _Right;

    [SerializeField]
    private Button _NeckExt;
    [SerializeField]
    private Button _NeckRetr;

    //[SerializeField]
    //private Button ;


    bool Forward = false;
    bool BackWards = false;
    bool Left = false;
    bool Right = false;
    bool NeckForw = false;
    bool NeckBack = false;


    
    //[SerializeField]
    //private string _port = "50001";
    private int Port = 4444;
    
    private string _ip = "172.30.35.145";//172.30.38.18//172.30 //"192.168.0.164" BrickPI wlan0 /"192.168.0.226" Pc /"192.168.0.177" BrickPiwlan1 //172.30.35.145
    private bool _socketReady = false;

    private TcpClient _tcpClient = null;
    private NetworkStream _netStream = null;
    private StreamWriter _socketWriter = null;
    //******************************// https://stackoverflow.com/questions/38816660/sending-data-from-unity-to-raspberry //****************************//

    void Start()
    {
        //Application.runInBackground = true;
        //Connect();


    }
    void OnApplicationQuit()
    {

        if (_tcpClient != null)
        {
            _socketWriter.WriteLine("Quit");
            _socketWriter.Flush();
            _socketWriter.Close();
            _netStream.Close();
            _tcpClient.Close();
        }
        // Update is called once per frame
    }
    
    private void Awake()
    {
        Application.runInBackground = true;
        Connect();
    }

    private void Connect()
    {
        IPAddress test = IPAddress.Parse(_ip);

        try
        {
            _tcpClient = new TcpClient();
            //int port = Int32.Parse(_port);

            _tcpClient.Connect(IPAddress.Parse(_ip), Port);
            _netStream = _tcpClient.GetStream();
            _socketWriter = new StreamWriter(_netStream);
            _socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e);
        }
    }
    public void RotateForwardOn()
    {
        Forward = true;
        _socketWriter.WriteLine("Forw");
        _socketWriter.Flush();
        //Debug.Log("send frow 'command' to robot");
    }
    public void RotateForwardOff()
    {
        Forward = false;

        _socketWriter.WriteLine("STOP");
        _socketWriter.Flush();
    }
    public void RotateBackOn()
    {
        BackWards = true;

        _socketWriter.WriteLine("Back");
        _socketWriter.Flush();
    }
    public void RotateBackOff()
    {
        BackWards = false;
        _socketWriter.WriteLine("STOP");
        _socketWriter.Flush();
    }

    public void RotateRightOn()
    {
        Right = true;

        _socketWriter.WriteLine("Right");
        _socketWriter.Flush();
    }
    public void RotateRightOff()
    {
        Right = false;
        _socketWriter.WriteLine("STOP");
        _socketWriter.Flush();
    }


    public void RotateLeftOn()
    {
        Left = true;

        _socketWriter.WriteLine("Left");
        _socketWriter.Flush();
    }
    public void RotateLeftOff()
    {
        Left = false;

        _socketWriter.WriteLine("STOP");
        _socketWriter.Flush();
    }
    public void NeckForwardOn()
    {
        NeckForw = true;

        _socketWriter.WriteLine("NeckF");
        _socketWriter.Flush();
    }
    public void NeckForwardOff()
    {
        NeckForw = false;

        _socketWriter.WriteLine("STOP");
        _socketWriter.Flush();
    }
    public void NeckBackOn()
    {
        NeckBack = true;

        _socketWriter.WriteLine("NeckB");
        _socketWriter.Flush();

    }
    public void NeckBackOff()
    {
        NeckBack = false;

        _socketWriter.WriteLine("STOP");
        _socketWriter.Flush();
    }
   
}


//BLUETOOTH
//public void openConnection()
//{
//    if (_UsedPort == null)
//    {
//        _UsedPort = new SerialPort(portname, speed, Parity.None, 8, StopBits.One);
//    }
//    if (_UsedPort != null)
//    {
//        if (!_UsedPort.IsOpen)
//        {
//            try
//            {
//                _UsedPort.Open();  // opens the connection
//                _UsedPort.ReadTimeout = 100;  // sets the timeout value before reporting error
//                Debug.Log("Port Open!");
//            }
//            catch (System.Exception e)
//            {
//                Debug.LogWarning(e.ToString());
//            }
//        }
//    }
//}
////close connexion
//public void closeConnection()
//{
//    if (_UsedPort != null && _UsedPort.IsOpen)
//    {
//        try
//        {
//            _UsedPort.Close();
//        }
//        catch (System.Exception e)
//        {
//            Debug.LogWarning(e.ToString());
//        }
//    }
//}
//public void sendCommand(string cmd)
//{
//    try
//    {
//        _UsedPort.Write(cmd); //ff hex commandos
//    }
//    catch (System.Exception e)
//    {
//        Debug.LogWarning(e.ToString());
//    }
//}




//#!/usr/bin/python
//import RPi.GPIO as GPIO
//import socket
//HOST = '192.168.0.106'
//PORT= 5002
//s= socket.socket(socket.AF_INET, socket.SOCK_STREAM)
//s.bind((HOST, PORT))
//s.listen(1)
//conn, addr=s.accept()
//s.setsockopt(socket.IPPROTO_TCP, socket.TCP_NODELAY, 1)
//print 'Connected by', addr
//GPIO.setmode(GPIO.BCM)
//GPIO.setup(04, GPIO.IN)
//GPIO.setup(17, GPIO.IN)
//GPIO.setup(27, GPIO.IN)
//while True:
//    if (GPIO.input(04)==True):
//        if (GPIO.input(17)==False):
//                if (GPIO.input(27)==False):
//                        conn.send('0')
//                elif(GPIO.input(27)==True):
//                        conn.send('1')
//        elif(GPIO.input(17)==True):
//                if (GPIO.input(27)==False):
//                        conn.send('2')
//                elif(GPIO.input(27)==True):
//                        conn.send('3')
//    elif(GPIO.input(04)==False):
//        conn.send('5')
//s.close()