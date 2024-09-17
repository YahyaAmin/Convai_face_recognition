import face_recognition
import cv2
import os
from datetime import datetime
import sys
import warnings

# Disable MSMF backend
os.environ["OPENCV_VIDEOIO_MSMF_ENABLE"] = "0"

# Suppress warnings
warnings.filterwarnings("ignore")


def load_known_faces(known_image_dir):
    known_face_encodings = []
    known_face_names = []

    for filename in os.listdir(known_image_dir):
        if filename.endswith(".jpg") or filename.endswith(".jpeg") or filename.endswith(".png"):
            image_path = os.path.join(known_image_dir, filename)
            name = os.path.splitext(filename)[0]
            image = face_recognition.load_image_file(image_path)
            face_encodings = face_recognition.face_encodings(image)

            if face_encodings:
                known_face_encodings.append(face_encodings[0])
                known_face_names.append(name)
                
    return known_face_encodings, known_face_names


def capture_face(known_face_encodings, known_face_names, unknown_image_dir):
    # Force OpenCV to use the DirectShow backend
    video_capture = cv2.VideoCapture(0, cv2.CAP_DSHOW)  # Use CAP_DSHOW instead of MSMF

    if not video_capture.isOpened():
        print("Error: Unable to open video capture.")
        return "error_no_frame"  # No frame captured

    ret, frame = video_capture.read()
    video_capture.release()

    # Add destroyAllWindows here to close any OpenCV windows
    cv2.destroyAllWindows()

    if not ret:
        print("Error: No frame captured.")
        return "error_no_frame"

    rgb_frame = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
    face_locations = face_recognition.face_locations(rgb_frame)
    face_encodings = face_recognition.face_encodings(rgb_frame, face_locations)

    if len(face_locations) != 1:
        return "error_multiple_faces_or_no_face_detected"  # Multiple faces or no face detected

    for (top, right, bottom, left), face_encoding in zip(face_locations, face_encodings):
        matches = face_recognition.compare_faces(known_face_encodings, face_encoding)
        face_distances = face_recognition.face_distance(known_face_encodings, face_encoding)
        best_match_index = face_distances.argmin()

        if matches[best_match_index]:
            name = known_face_names[best_match_index]
            return f"recognized:{name}"
        else:
            # Save the unknown face for potential future use
            os.makedirs(unknown_image_dir, exist_ok=True)
            timestamp = datetime.now().strftime("%Y%m%d_%H%M%S")
            filename = f"unknown_{timestamp}.jpg"
            filepath = os.path.join(unknown_image_dir, filename)
            cv2.imwrite(filepath, frame[top:bottom, left:right])
            return f"unrecognized:{filename}"


if __name__ == "__main__":
    # Get the directories from command-line arguments
    if len(sys.argv) < 3:
        print("Usage: python main.py <known_faces_directory> <unknown_faces_directory>")
        sys.exit(1)

    known_image_dir = sys.argv[1]
    unknown_image_dir = sys.argv[2]

    known_face_encodings, known_face_names = load_known_faces(known_image_dir)
    result = capture_face(known_face_encodings, known_face_names, unknown_image_dir)
    print(result)
