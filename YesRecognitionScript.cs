using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Convai.Scripts.Utils;

public class YesRecognitionScript : MonoBehaviour
{
    // Start is called before the first frame update
    private string userName;
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

    public void saveUsersPhoto()
    {
        ConvaiNPCManager.Instance.activeConvaiNPC.SendTextDataAsync("<speak>, that is a nice name. I will be sure to remember it for the future. </speak>");
        // Find the pythonGO game object and get the script path and filename
        PythonIntegration pythonIntegration = GameObject.Find("pythonGO").GetComponent<PythonIntegration>();
        string scriptPath = pythonIntegration.getScriptPath();
        string fileName = pythonIntegration.getFilename();

        // Gets the directory path of the scriptPath
        string directoryPath = Path.GetDirectoryName(scriptPath); 

        // Combines the path with "picture" folder and fileName to get the path to the picture
        string picturesPath = Path.Combine(directoryPath, "picture", fileName);

        // If the file exists:
        if (File.Exists(picturesPath))
        {
            // Get the new filename using the player's name (with an extension, assuming the original file's extension)
            string newFileName = userName + Path.GetExtension(fileName);
            
            // Create the path to move the file into the "test" folder with the new name
            string testFolderPath = Path.Combine(directoryPath, "test", newFileName);

            // Move the file to the "test" folder and rename it
            File.Move(picturesPath, testFolderPath);
        }
        else
        {
            Debug.LogError("Tried to move a file that doesn't exist.");
        }
    }
}
