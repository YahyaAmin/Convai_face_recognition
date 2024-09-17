
# AI Virtual Receptionist

## Table of Contents

1. [Project Overview](#project-overview)
2. [Methodology](#methodology)
3. [Deliverables](#deliverables)
4. [Project Schedule](#project-schedule)
5. [Risk Factors](#risk-factors)
6. [Installation](#installation)
7. [Usage](#usage)
8. [Configuration](#configuration)
9. [References](#references)
10. [Appendix](#appendix)

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

1. **Clone the Repository**:  
   ```bash
   git clone https://github.com/YahyaAmin/virtual-assistant.git
   ```

2. **Navigate to the Project Directory**:  
   ```bash
   cd virtual-assistant
   ```

3. **Create and Activate a Virtual Environment**:  
   ```bash
   python -m venv env
   source env/bin/activate  # On Windows use `env\Scripts\activate`
   ```

4. **Install Dependencies**:  
   ```bash
   pip install -r requirements.txt
   ```

## Usage

### Face Recognition and Webcam

To run the face recognition system using your webcam:

1. **Prepare Known Faces**: Place images of known individuals in the `picture` directory. Each image should be named after the individual (e.g., `john_doe.jpg`).

2. **Run the Webcam Script**:  
   ```bash
   python main.py
   ```
   - The script will start the webcam feed.
   - Press `'q'` to quit or `'s'` to save the video and quit.

## Configuration

- **KNOWN_IMAGE_DIR**: Directory where known face images are stored.
- **VIDEO_PATH**: Set to `0` for the default webcam.
- **OUTPUT_VIDEO_PATH**: Path where the processed video will be saved.

## References

- Edith Cowan University (2023). Pocket Statistics 2023.
- Mamun, K. A., et al. (2024). Smart reception: An AI-driven receptionist system.
- Project Management Institute (2017). Agile Practice Guide.

## Appendix

- Gantt Chart detailing the project timeline.
```
