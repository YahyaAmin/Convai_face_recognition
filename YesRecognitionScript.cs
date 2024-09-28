using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Convai.Scripts.Utils;
using System.Threading;

public class YesRecognitionScript : MonoBehaviour
{
    // Start is called before the first frame update
    public string userName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUserName(string name)
    {
        userName = name;
    }

    public void userSaysRecognizeMe()
    {
        ConvaiNPCManager.Instance.activeConvaiNPC.SendTextDataAsync($"<speak>{userName}</speak>");
    }

    public void saveUsersPhoto()
    {
    
        // Find the pythonGO game object and get the script path and filename
        PythonIntegration pythonIntegration = GameObject.Find("pythonGO").GetComponent<PythonIntegration>();
        string scriptPath = pythonIntegration.getScriptPath();
        string fileName = pythonIntegration.getFilename();

        // Gets the directory path of the scriptPath
        string directoryPath = Path.GetDirectoryName(scriptPath); 

        // Combines the path with "picture" folder and fileName to get the path to the picture
        string picturesPath = Path.Combine(directoryPath, "picture", fileName);
        Debug.Log(picturesPath);

        // If the file exists:
        if (File.Exists(picturesPath))
        {
            // Get the new filename using the player's name (with an extension, assuming the original file's extension)
            string newFileName = userName + Path.GetExtension(fileName);
            Debug.Log(newFileName);
            Debug.Log(userName);

            // Create the path to move the file into the "test" folder with the new name
            string testFolderPath = Path.Combine(directoryPath, "test", newFileName);
            Debug.Log(testFolderPath);

            // Move the file to the "test" folder and rename it
            File.Move(picturesPath, testFolderPath);
            ConvaiNPCManager.Instance.activeConvaiNPC.SendTextDataAsync("<speak>What a nice name!</speak>");

             /* Get all files in the directory
                string[] files = Directory.GetFiles(picturesPath);

                // Check if there are any files in the directory
                if (files.Length > 0){
                    // Loop through and delete each file
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }
                } */


        }
        else
        {
            Debug.LogError("Tried to move a file that doesn't exist.");
        }


    }
}
