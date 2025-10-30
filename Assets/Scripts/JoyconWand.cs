using System.Collections.Generic;
using UnityEngine;

public enum Direction { up, down, left, right, forward, back}
public class JoyconWand : MonoBehaviour
{
    private List<Joycon> joycons;

    // Values made available via Unity
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public int jc_index = 0;
    public Quaternion orientation;
    

    

    //public Vector3 velocity;
    //private Vector3 prevAccel;
    void Start()
    {
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        //velocity = new Vector3(0, 0, 0);
        //prevAccel = new Vector3(0, 0, 0);

        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_index + 1)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // make sure the Joycon only gets checked if attached
        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_index];
           
            // Temporary recalibration input
            if (j.GetButtonDown(Joycon.Button.SHOULDER_2))  Recalibrate();
            

            stick = j.GetStick();

            // Gyro values: x, y, z axis values (in radians per second)
            gyro = j.GetGyro();

            // Accel values:  x, y, z axis values (in Gs)
            accel = j.GetAccel();
            orientation = Quaternion.Euler(90f, 0f, 0f) * j.GetVector();

            gameObject.transform.rotation = orientation;

            Direction dir = GetDirection();
            Debug.Log(dir);
        }
    }

    public void Recalibrate()
    {
        if (joycons.Count > 0) 
        { 
            Joycon j = joycons[jc_index];
            j.Recenter(); 
        }
    }

    public Direction GetDirection()
    {
        if (transform.eulerAngles.x <= 45 || transform.eulerAngles.x >= 315)
        {
            if (transform.eulerAngles.y > 45 && transform.eulerAngles.y < 135) return Direction.right;
            else if (transform.eulerAngles.y < 315 && transform.eulerAngles.y > 225) return Direction.left;
            else if (transform.eulerAngles.y > 315 || transform.eulerAngles.y < 45) return Direction.forward;
            else return Direction.back;
        }
        else if (transform.eulerAngles.x > 45 && transform.eulerAngles.x < 135) return Direction.down;
        else if (transform.eulerAngles.x < 315 && transform.eulerAngles.x > 225) return Direction.up;
        else return Direction.back;
    }
    
}
