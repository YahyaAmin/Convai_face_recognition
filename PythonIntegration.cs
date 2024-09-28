using UnityEngine;
using System.Diagnostics;
using System.IO;
using System.Collections;
using Convai.Scripts.Runtime;
using Convai.Scripts.Utils;

public class PythonIntegration : MonoBehaviour
{
    private string pythonPath = @"C:\Users\abc\AppData\Local\Programs\Python\Python311\python.exe"; 
    private string scriptPath = @"C:\Users\abc\Desktop\Face_recognition_demo\virtual-assistant\main.py";
    private string knownImageDir = @"C:\Users\abc\Desktop\Face_recognition_demo\virtual-assistant\test";
    private string unknownImageDir = @"C:\Users\abc\Desktop\Face_recognition_demo\virtual-assistant\picture";
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

    private IEnumerator HandleUserInput(string filename)
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                UnityEngine.Debug.Log("Enter your name:");
                string userName = ""; // Get the user's name from input
                string newFilePath = Path.Combine(knownImageDir, userName + ".jpg");
                File.Move(Path.Combine(unknownImageDir, filename), newFilePath);
                UnityEngine.Debug.Log($"Face saved as {newFilePath}");
                break;
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                File.Delete(Path.Combine(unknownImageDir, filename));
                UnityEngine.Debug.Log("Face discarded.");
                break;
            }

            yield return null;
        }
    }
}
