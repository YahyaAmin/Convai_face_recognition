# AI Virtual Receptionist

## Table of Contents

1. [Project Overview](#project-overview)
2. [Methodology](#methodology)
3. [Deliverables](#deliverables)
4. [Project Schedule](#project-schedule)
5. [Risk Factors](#risk-factors)
6. [Installation](#installation)
   - [Linux/Mac](#linuxmac)
   - [Windows](#windows)
7. [Usage](#usage)
8. [Configuration](#configuration)
9. [Windows-Specific Requirements](#windows-specific-requirements)
10. [Unity Integration](#unity-integration)
11. [References](#references)
12. [Appendix](#appendix)

## Project Overview

This project aims to develop an AI-driven virtual receptionist for Edith Cowan University (ECU) Joondalup campus. The AI receptionist is designed to handle 24/7 customer service, improving response times and enhancing user experience, especially during peak times such as orientation week and university open days.

## Methodology

The project follows an Agile methodology to support rapid development, iterative progress, and continuous feedback. The virtual receptionist is developed using AI and machine learning systems, with a focus on user interaction, speech recognition, and facial recognition.

## Deliverables

1. Initial research and selection of project components.
2. Development of an AI-powered virtual receptionist prototype.
3. Integration of speech and text recognition, knowledge bank, and facial recognition.
4. Final demonstration and project report.

## Project Schedule

The project is structured into four phases:  
1. **Research** - Identifying and evaluating potential components.  
2. **Development** - Iterative development and testing.  
3. **Integration** - Combining all components into a functional prototype.  
4. **Presentation** - Final presentation and documentation.

## Risk Factors

- **Integration Complexity**: Challenges in incorporating multiple AI technologies.
- **Scalability**: Potential issues as the number of users increases.
- **Environmental Sensitivity**: Factors like lighting and accents affecting system performance.
- **Dependence on Individuals and Third-Party Services**: Risks associated with a small team and reliance on external APIs.

## Installation

### Linux/Mac

1. **Clone the Repository**:  
   ```bash
   git clone https://github.com/YahyaAmin/Convai_face_recognition.git
   ```

2. **Navigate to the Project Directory**:  
   ```bash
   cd virtual-assistant
   ```

3. **Create and Activate a Virtual Environment**:  
   ```bash
   python -m venv env
   source env/bin/activate
   ```

4. **Install Dependencies**:  
   ```bash
   pip install -r requirements.txt
   ```

### Windows

1. **Clone the Repository**:  
   ```bash
   git clone https://github.com/YahyaAmin/Convai_face_recognition.git
   ```

2. **Navigate to the Project Directory**:  
   ```bash
   cd virtual-assistant
   ```

3. **Create and Activate a Virtual Environment**:  
   ```bash
   python -m venv env
   env\Scripts\activate
   ```

4. **Install Dependencies**:  
   ```bash
   pip install -r requirements.txt
   ```

## Usage

### Face Recognition and Webcam

To run the face recognition system using your webcam:

1. **Prepare Known Faces**: Place images of known individuals in the `test` directory. Each image should be named after the individual (e.g., `john_doe.jpg`).

2. **Run the Webcam Script**:  
   ```bash
   python main.py "path of test folder" "path of picture folder"
   ```
   - The script will start the webcam feed.
   - If multiple faces are detected, a message `Please be one person in front of the camera` will be displayed, and the system will not proceed until only one person is detected.
   - When an unknown face is detected, a screenshot of the face will be saved automatically to the `picture` directory.
   - Press `'q'` to quit or `'s'` to save the video and quit.

## Configuration

- **KNOWN_IMAGE_DIR**: Directory where known face images are stored.  
   - Linux/Mac example: `./test/`  
   - Windows example: `test\\`

- **UNKNOWN_IMAGE_DIR**: Directory where images of unknown faces will be saved.  
   - Linux/Mac example: `./picture/`  
   - Windows example: `picture\\`

- **VIDEO_PATH**: Set to `0` for the default webcam.

- **OUTPUT_VIDEO_PATH**: Path where the processed video will be saved.  
   - Linux/Mac example: `./output/video.mp4`  
   - Windows example: `output\\video.mp4`

## Windows-Specific Requirements

If you're running this project on Windows, you may need additional setup for dependencies like `dlib`. Follow these steps:

1. **Install Visual Studio Build Tools**:  
   - Download and install the *Visual Studio Build Tools* from the [official Microsoft website](https://visualstudio.microsoft.com/visual-cpp-build-tools/).  
   - During installation, ensure the *"Desktop development with C++"* workload is selected, which includes the MSVC compiler and CMake.

2. **Install CMake**:  
   - If not included with Visual Studio, download and install it from the [CMake website](https://cmake.org/download/).  
   - Ensure CMake is added to your system's PATH.

3. **Upgrade pip** (optional but recommended):  
   ```bash
   python -m pip install --upgrade pip
   ```

4. **Install `dlib`**:  
   After the above installations, retry installing `dlib` using:  
   ```bash
   pip install dlib
   ```

### Using a Pre-Compiled Wheel

If you want to avoid building from source:

1. **Download a Pre-compiled Wheel**:  
   Pre-compiled wheels can be found on [Unofficial Windows Binaries for Python Extension Packages](https://www.lfd.uci.edu/~gohlke/pythonlibs/#dlib). Download the correct wheel file corresponding to your Python version.

2. **Install the Wheel**:  
   ```bash
   pip install path_to_downloaded_whl_file
   ```

## Unity Integration

This section explains how to integrate the Python face recognition script with Unity using C#. By following these steps, you'll be able to trigger Python-based face recognition functionality from within Unity.

### Requirements

1. **Python**: Ensure that Python is installed on your system.
2. **Unity**: Any version that supports C# scripting.
3. **OpenCV and face_recognition for Python**: Already covered in the installation steps of the Python script.

### Steps to Set Up Unity Integration (not applicable for the exe file. Skip these for .exe file

1. **Add the C# Script to Your Unity Project**:
   - Place the C# script (`PythonIntegration.cs`) into your Unity project's `Assets/Scripts` folder.

2. **Configure Python Path and Script Path**:
   - In Unity, create an empty GameObject in your scene.
   - Attach the `PythonIntegration.cs` script to this GameObject.
   - In the Unity Inspector, set the following fields:
     - **pythonPath**: Set this to the path of your Python interpreter. For example:
       ```
       C:\Users\YourUsername\AppData\Local\Programs\Python\Python39\python.exe
       ```
     - **scriptPath**: Set this to the full path of the Python face recognition script. For example:
       ```
       C:\Path\To\Your\Repo\scripts\python\face_recognition_script.py
       ```

3. **Running the Unity Project**:
   - Once you press the play button in Unity, the Python script will be executed.
   - If the face recognition script detects unknown faces, they will be saved in the specified folder.
   - The output and potential errors from the Python script will be displayed in the Unity console.

### Folder Structure

```bash
root/
├── README.md
├── requirements.txt
├── scripts/
│   ├── python/
│   │   └── face_recognition_script.py
│   └── unity/
│       └── PythonIntegration.cs
└── test/  # For known faces images
```

## References

- Edith Cowan University (2023). Pocket Statistics 2023.
- Mamun, K. A., et al. (2024). Smart reception: An AI-driven receptionist system.
- Project Management Institute (2017). Agile Practice Guide.

## Appendix

- Gantt Chart detailing the project timeline.
