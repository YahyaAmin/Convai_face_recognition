using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using System.IO;
using System.Linq;
using Convai.Scripts.Utils;

public class NoRecognitionScript : MonoBehaviour
{
    private PythonIntegration pythonIntegration;
    private string scriptPath;
    private string fileName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deleteUsersPhoto()
    {
        // Get the script path and filename from PythonIntegration   
        pythonIntegration = GameObject.Find("pythonGO").GetComponent<PythonIntegration>();
        scriptPath = pythonIntegration.getScriptPath();
        fileName = pythonIntegration.getFilename();

        // Alter scriptPath to point to the pictures folder
        string directoryPath = Path.GetDirectoryName(scriptPath); // Gets the directory path of the scriptPath
        string picturesPath = Path.Combine(directoryPath, "picture", fileName); // Combines the path with "picture" folder and fileName

        // Debug.Log("The filename is: " + picturesPath);
        //Delete the file if it exists
        if (File.Exists(picturesPath))
        {
            File.Delete(picturesPath);
        }
       // else
       // {
         //  ConvaiNPCManager.Instance.activeConvaiNPC.SendTextDataAsync("<speak>That's strange—when I tried to forget your face, my memory of it was already gone. </speak>");
       // }
    }
}
