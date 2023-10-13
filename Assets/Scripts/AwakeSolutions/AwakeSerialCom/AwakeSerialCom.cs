using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace AwakeSolutions
{
    public class AwakeSerialCom : MonoBehaviour
    {
        public UnityEvent<string> onMessageGot = new UnityEvent<string>();

        public int baudrate;

        private SerialPort serialPort;

        private void OnGUI()
        {
            if (serialPort != null)
                if (serialPort.IsOpen)
                    return;

            string[] ports = SerialPort.GetPortNames();

            GUILayout.Label("[AwakeSerialCom] Choose Serial port:");

            foreach (string port in ports)
                if (GUILayout.Button(port))
                    Open(port);
        }

        void Update()
        {
            if (serialPort != null)
                if (serialPort.IsOpen)
                    ReadSerialPort();
        }

        private void OnDestroy()
        {
            serialPort.Close();
        }

        private void Open(string port)
        {
            serialPort = new SerialPort(port, baudrate);
            serialPort.ReadTimeout = 0;
            serialPort.Open();
        }

        private async void ReadSerialPort()
        {
            try
            {
                string value = null;

                await Task.Run(() =>
                {
                    value = serialPort.ReadLine();
                });

                if (value == null)
                    return;

                OnMessageGot(value.Trim());
            }
            catch (TimeoutException e)
            {

            }
        }

        void OnMessageGot(string message)
        {
            onMessageGot?.Invoke(message);

            Debug.Log("[AwakeSerialCom] Serial port message received: " + message);
        }

        public void Send(string message)
        {
            serialPort.WriteLine(message);

            Debug.Log("[AwakeSerialCom] Serial port message sent: " + message);
        }
    }
}