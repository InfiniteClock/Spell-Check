using UnityEngine;
using System.IO.Ports;

public class Wand : MonoBehaviour
{
    SerialPort serialPort;
    Vector3 accel;
    Vector3 gyro;
    Quaternion orientation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        serialPort = new SerialPort("COM3",115200);
        serialPort.ReadTimeout = 1000;
        serialPort.Open();
    }

    // Update is called once per frame
    void Update()
    {
        string data = serialPort.ReadLine();
        if (data.StartsWith("SensorDataReading:")) {
            string[] splitData = data.Split('\t');

            accel = new Vector3(float.Parse(splitData[1]), float.Parse(splitData[2]), float.Parse(splitData[3]));
            gyro = new Vector3(float.Parse(splitData[4]), float.Parse(splitData[5]), float.Parse(splitData[6]));
            orientation = new Quaternion(float.Parse(splitData[7]), float.Parse(splitData[8]), float.Parse(splitData[9]), float.Parse(splitData[10]));

            Vector3 euler = orientation.eulerAngles;

            Debug.Log("Acceleration = " + accel);
            Debug.Log("Gyro = " + gyro);
            Debug.Log("Orientation = " + euler);
        }
        else
        {
            Debug.Log(data);
        }
    }

    private void OnApplicationQuit()
    {
        serialPort.Close();
    }
}
