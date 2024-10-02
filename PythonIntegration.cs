using UnityEngine;
using System.Diagnostics;
using System.IO;
using System.Collections;
using Convai.Scripts.Runtime;
using Convai.Scripts.Utils;
using System.Linq;
using System;

public class PythonIntegration : MonoBehaviour
{
    private string pythonPath = GetPythonPathFromWhere(); 
    // Define paths dynamically using the found "convai_face_recognition" folder
    private static string convaiFolder = SearchForConvaiFolder();
    private static string scriptPath = convaiFolder != null ? Path.Combine(convaiFolder, @"main.py") : null;
    private static string knownImageDir = convaiFolder != null ? Path.Combine(convaiFolder, @"test") : null;
    private static string unknownImageDir = convaiFolder != null ? Path.Combine(convaiFolder, @"picture") : null;

    private string data;
    private string filename = "";

    public void Start()
    {
        
    }

    public string getScriptPath()
    {
        return scriptPath;
    }

    public string getFilename()
    {
        return filename;
    }

    public void RunPythonScript()
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = pythonPath;
        start.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\"", scriptPath, knownImageDir, unknownImageDir); // Pass the directories as arguments to the script
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        start.RedirectStandardError = true;
        start.CreateNoWindow = true;

        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string data = reader.ReadToEnd();
                HandlePythonResult(data.Trim());
                UnityEngine.Debug.Log("Handle python function being called");
                UnityEngine.Debug.Log("Known Image Directory: " + knownImageDir);
                UnityEngine.Debug.Log("UnKnown Image Directory: " + unknownImageDir);
                UnityEngine.Debug.Log("ScriptPath: " + scriptPath);
            }

            using (StreamReader reader = process.StandardError)
            {
                string error = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(error))
                {
                    UnityEngine.Debug.LogError(error);
                }
            }
        }
    }

    private void HandlePythonResult(string result)
    {
        if (result.StartsWith("recognized:"))
        {
            string name = result.Substring("recognized:".Length);
            data = $"<speak>{name}! How are you doing? </speak>";
            ConvaiNPCManager.Instance.activeConvaiNPC.SendTextDataAsync(data);
        }
        else if (result.StartsWith("unrecognized:"))
        {
            filename = result.Substring("unrecognized:".Length);
            UnityEngine.Debug.Log("Face not recognized.");
            UnityEngine.Debug.Log("Press 'y' to save the face for future recognition or 'n' to discard.");
            // StartCoroutine(HandleUserInput(filename));
            ConvaiNPCManager.Instance.activeConvaiNPC.SendTextDataAsync("The face was not recognized.");
            UnityEngine.Debug.Log("Face not being recognized sent to NPC");
        }
        else if (result == "error_no_frame")
        {
            ConvaiNPCManager.Instance.activeConvaiNPC.SendTextDataAsync("<speak>, I am sorry but there is no frame to process. Please ensure the camera is working properly then try again. </speak>");
        }
        else if (result == "error_multiple_faces_or_no_face_detected")
        {
            ConvaiNPCManager.Instance.activeConvaiNPC.SendTextDataAsync("<speak>, I am sorry, but either I was unable to detect a face or I detected multiple faces. Please ensure only one face is visible then try again. </speak>");
        }
    }

    static string GetPythonPathFromWhere()
    {
        try
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c where python",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrWhiteSpace(output))
                {
                    return output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error finding python.exe: " + ex.Message);
        }

        // Ensure that a value is returned even if an error occurs or no python.exe is found
        return null;  // or return an empty string "" based on your preference
    
    
    }

   // Find the "convai_face_recognition" folder starting from specified directories and searching subdirectories
    private static string FindConvaiFolder(string startDirectory)
    {
        try
        {
            foreach (var directory in Directory.GetDirectories(startDirectory, "*", SearchOption.AllDirectories))
            {
                if (Path.GetFileName(directory) == "virtual-assistant")
                {
                    return directory;  // Folder found
                }
            }
        }
        catch (UnauthorizedAccessException) 
        {
            // Handle any access exceptions (e.g., if certain directories cannot be accessed)
        }
        
        return null;  // Folder not found
    }

    // Search both Desktop and MyDocuments folders and their subdirectories
    private static string SearchForConvaiFolder()
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // Search in Desktop folder
        string foundInDesktop = FindConvaiFolder(desktopPath);
        if (foundInDesktop != null) return foundInDesktop;

        // Search in MyDocuments folder
        string foundInDocuments = FindConvaiFolder(documentsPath);
        if (foundInDocuments != null) return foundInDocuments;

        // Folder not found in both locations
        return null;
    }
    
}
