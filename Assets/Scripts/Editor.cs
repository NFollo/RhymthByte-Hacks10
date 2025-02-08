using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Editor : MonoBehaviour
{
    private Conductor conductor;
    private void Start() {
        conductor = GetComponent<Conductor>();
    }


    String StringBuilder(String name, List<float> array) {
        // If array is 0, return generic empty array string.
        if(array.Count == 0) {
            String emptyString = "GenericPropertyJSON:{\"name\":\"" + name + "\",\"type\":-1,\"arraySize\":0,\"arrayType\":\"float\",\"children\":[{\"name\":\"Array\",\"type\":-1,\"arraySize\":0,\"arrayType\":\"float\",\"children\":[{\"name\":\"size\",\"type\":12,\"val\":0}]}]}";
            return emptyString;
        }
        // If array is not 0, build the proper string.
        String returnString = "GenericPropertyJSON:{\"name\":\"";
        returnString += name;
        returnString += "\",\"type\":-1,\"arraySize\":";
        returnString += array.Count;
        returnString += ",\"arrayType\":\"float\",\"children\":[{\"name\":\"Array\",\"type\":-1,\"arraySize\":";
        returnString += array.Count;
        returnString += ",\"arrayType\":\"float\",\"children\":[{\"name\":\"size\",\"type\":12,\"val\":";
        returnString += array.Count;
        returnString += "},";
        for (int i = 0; i < array.Count; i++)
        {
            returnString += "{\"name\":\"data\",\"type\":2,\"val\":" + array[i] +"},";
            if((i+1) < array.Count) {
                returnString += ",";
            }
        }
        returnString += "]}]}";
        return returnString;
    }

    void CreateText(String name, List<float> array) {
        //Path of the file
        string path = Application.dataPath + "/" + name + ".txt";
        Debug.Log(path);
        //Create File if it doesn't exist
        // if (!File.Exists(path)) {
        //     File.WriteAllText(path, "Login log \n\n");
        // }
        //Add some to text to it
        File.WriteAllText(path, StringBuilder(name, array));
    }

    private List<float> topNotes = new List<float>();
    void OnTop() {
        topNotes.Add(Mathf.Round(conductor.songPositionInBeats*2)/2);
    }

    private List<float> bottomNotes = new List<float>();
    void OnBottom() {
        bottomNotes.Add(Mathf.Round(conductor.songPositionInBeats*2)/2);
    }

    void OnSave() {
        CreateText("notesTop", topNotes);
        CreateText("notesBottom", bottomNotes);
    }
}
