﻿using UnityEngine;
using System.Collections;

public class SpeechText : MonoBehaviour {
    public CreateObjects createObjects;

    private const string BOTTOM_TEXT = "put text on bottom";
    private const string MIDDLE_TEXT = "put text on middle";
    private const string TOP_TEXT = "put text on top";
    private const string SPEECH_BUBBLE_TEXT = "put text on speech bubble";

    private bool bottom;
    private bool middle;
    private bool top;
    private bool speechBubble;
    private string currentText;

    private SoundObject soundObject;
    //Use this for initialization

   void Start () {
        currentText = "Microphone is Recording";
        bottom = false;
        middle = true;
        top = false;
        speechBubble = false;
        createObjects = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CreateObjects>();
	}
	
	// Update is called once per frame
	void Update () {
        updateLocation(GetComponent<TextMesh>().text);
        
        faceUser();
    }

    
    

    /// <summary>
    /// Makes sure that the text is always facing the user
    /// </summary>
    private void faceUser()
    {
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward * 5;

        if (bottom)
        {

            Vector3 temp = new Vector3(headPosition.x, headPosition.y - 0.5f, headPosition.z);
            transform.position = temp + gazeDirection;
            transform.TransformDirection(gazeDirection);
        }
        else if (middle)
        {
            transform.position = headPosition + gazeDirection;
            transform.TransformDirection(gazeDirection);
        }
        else if (top)
        {
            Vector3 temp = new Vector3(headPosition.x, headPosition.y + 0.5f, headPosition.z);
            transform.position = temp + gazeDirection;
            transform.TransformDirection(gazeDirection);
        }
        else if(speechBubble && soundObject == null) //If speech bubble is selected and a sound object exists
        {
            soundObject = createObjects.getFirstObject();
            if(soundObject != null)
            {
                transform.position = soundObject.transform.position;
                Debug.Log("SOUNDOBJECT POSITION" + soundObject.transform.position);
            }
            else
            {
                Vector3 temp = new Vector3(headPosition.x - 1f, headPosition.y, headPosition.z);
                transform.position = headPosition + gazeDirection;
                transform.TransformDirection(gazeDirection);
            }

        }
    }

    /// <summary>
    /// Update the 3D text
    /// </summary>
    /// <param name="text"></param>
    public void updateText(string text)
    {
        GetComponent<TextMesh>().text = text;
        updateLocation(text);
      
    }

    /// <summary>
    /// Updates the boolean variables representing where the text should be located
    /// </summary>
    /// <param name="text"></param>
    private void updateLocation(string text)
    {
        if (text.ToLower().Contains(BOTTOM_TEXT))
        {
            bottom = true;
            middle = false;
            top = false;
            speechBubble = false;
        }
        else if (text.ToLower().Contains(MIDDLE_TEXT))
        {
            bottom = false;
            middle = true;
            top = false;
            speechBubble = false;
        }
        else if (text.ToLower().Contains(TOP_TEXT))
        {
            bottom = false;
            middle = false;
            top = true;
            speechBubble = false;
        }
        else if (text.ToLower().Contains(SPEECH_BUBBLE_TEXT))
        {
            bottom = false;
            middle = false;
            top = false;
            speechBubble = true;
        }
    }

}