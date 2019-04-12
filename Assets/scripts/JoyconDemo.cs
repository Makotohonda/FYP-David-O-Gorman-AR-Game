using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


/* I used this script in early iterations of my pproject
 * edited heavily to support what I needed for it
 * The Hololens does not suppport multithreading so JoyCons physically will not work with the Hololens
 * I Implemented necessary lines to support transfer of data to server
 * I intended to transfer the gyroscope and rotation data to a server which would then send to the Hololens
 * This method is a work around for the multithreading, However I took the project in a different direction
 * and abandoned this method due to limitations and time management
 */
public class JoyconDemo : MonoBehaviour {
	
	private List<Joycon> joycons;

    // Values made available via Unity
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public Vector3 middle;
    public int jc_ind = 0;
    public Quaternion orientation;

    void Start()
    {
        //A correct website page.
        StartCoroutine(GetRequest("https://sleepy-scrubland-59454.herokuapp.com/"));

        //A non-existing page.
        //StartCoroutine(GetRequest("https://error.html"));
        middle = new Vector3(180, 180, 180);
        //gyro = new Vector3(180, 180, 180);
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        //get the public Joycon array attached to the JoyconManager in scene
         joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind+1){
        	Destroy(gameObject);
        }
        StartCoroutine(Upload());
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            //Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }

   // void Start()
  //  {
        //      gyro = new Vector3(0, 0, 0);
        //      accel = new Vector3(0, 0, 0);
        //      // get the public Joycon array attached to the JoyconManager in scene
        //      joycons = JoyconManager.Instance.j;
        //if (joycons.Count < jc_ind+1){
        //	Destroy(gameObject);
        //}
  //      StartCoroutine(Upload());
 //   }


        //set up address to send data to the server I created using Heroku
    IEnumerator Upload()
    {
        byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");
        using (UnityWebRequest www = UnityWebRequest.Put("https://sleepy-scrubland-59454.herokuapp.com", myData))
        {
          //  UnityWebRequest www = UnityWebRequest.Put("https://sleepy-scrubland-59454.herokuapp.com", myData);
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Upload complete!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        // make sure the Joycon only gets checked if attached
        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];
            // GetButtonDown checks if a button has been pressed (not held)
            if (j.GetButtonDown(Joycon.Button.SHOULDER_2))
            {
                Debug.Log("Shoulder button 2 pressed");
                // GetStick returns a 2-element vector with x/y joystick components
                Debug.Log(string.Format("Stick x: {0:N} Stick y: {1:N}", j.GetStick()[0], j.GetStick()[1]));

                // Joycon has no magnetometer, so it cannot accurately determine its yaw value. Joycon.Recenter allows the user to reset the yaw value.
                j.Recenter();
               // j.GetGyro();
            }
            // GetButtonDown checks if a button has been released
            if (j.GetButtonUp(Joycon.Button.SHOULDER_2))
            {
                Debug.Log("Shoulder button 2 released");
            }
            // GetButtonDown checks if a button is currently down (pressed or held)
            if (j.GetButton(Joycon.Button.SHOULDER_2))
            {
                Debug.Log("Shoulder button 2 held");
            }

            if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
            {
                Debug.Log("Rumble");

                // Rumble for 200 milliseconds, with low frequency rumble at 160 Hz and high frequency rumble at 320 Hz. For more information check:
                // https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/rumble_data_table.md

                j.SetRumble(160, 320, 0.6f, 200);

                // The last argument (time) in SetRumble is optional. Call it with three arguments to turn it on without telling it when to turn off.
                // (Useful for dynamically changing rumble values.)
                // Then call SetRumble(0,0,0) when you want to turn it off.
            }

            stick = j.GetStick();

            // Gyro values: x, y, z axis values (in radians per second)
            gyro = j.GetGyro();

            // Accel values:  x, y, z axis values (in Gs)
            accel = j.GetAccel();

           // this.gameObject.transform.Translate(0, 0, accel.z/ 500);
            orientation = j.GetVector();
            if (j.GetButton(Joycon.Button.DPAD_UP))
            {
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
            gameObject.transform.rotation = orientation;

        }
    }
}