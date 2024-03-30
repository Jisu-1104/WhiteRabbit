import cv2
import socket
import numpy as np

def get_gesture_response(label):
    # 라벨에 해당하는 제스처를 반환
    gesture_mapping = {
        0: 'ㄱ', 1: 'ㄴ', 2: 'ㄷ', 3: 'ㄹ', 4: 'ㅁ', 5: 'ㅂ', 6: 'ㅅ', 7: 'ㅇ',
        8: 'ㅈ', 9: 'ㅊ', 10: 'ㅋ', 11: 'ㅌ', 12: 'ㅍ', 13: 'ㅎ', 14: 'ㅏ', 15: 'ㅑ',
        16: 'ㅓ', 17: 'ㅕ', 18: 'ㅗ', 19: 'ㅛ', 20: 'ㅜ', 21: 'ㅠ', 22: 'ㅡ', 23: 'ㅣ',
        24: 'ㅐ', 25: 'ㅒ', 26: 'ㅔ', 27: 'ㅖ', 28: 'ㅢ', 29: 'ㅚ', 30: 'ㅟ'
    }

    return gesture_mapping.get(label, "Unknown Gesture")

def start_server(model):

    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.bind(("127.0.0.1", 8888))
    server.listen(1)

    print("Python server is waiting for connection...")
    conn, addr = server.accept()
    print(f"Connected to {addr}")

    try:
        while True:
            data = conn.recv(4096)
            if not data:
                break
            message = data.decode('utf-8')

            angles = process_landmark_data(message)
            if angles is not None:
                predicted_gesture = predict_gesture(model, angles)
                conn.send(predicted_gesture.encode('utf-8'))

    except Exception as e:
        print(f"An error occurred: {e}")

    finally:
        conn.close()
        server.close()


def load_trained_model(model_filepath):
    # KNN 모델 로드
    knn = cv2.ml.KNearest_create()
    knn = knn.load(model_filepath)
    return knn

def process_landmark_data(message):
    landmark_data = [float(x) for x in message.split(',')]
    if len(landmark_data) != 63:
        print("Invalid landmark data format.")
        return None
    transposed_joint = np.array(landmark_data).reshape((3, 21))
    joint = np.transpose(transposed_joint)
    v1 = joint[[0,1,2,3,0,5,6,7,0, 9,10,11, 0,13,14,15, 0,17,18,19],:]
    v2 = joint[[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20],:]
    v = v2 - v1
    v_normalized = v / np.linalg.norm(v, axis=1)[:, np.newaxis]
    compareV1 = v_normalized[[0,1,2,4,5,6,8,9,10,12,13,14,16,17,18],:]
    compareV2 = v_normalized[[1,2,3,5,6,7,9,10,11,13,14,15,17,18,19],:]
    angles = np.degrees(np.arccos(np.einsum('nt,nt->n', compareV1, compareV2)))
    return angles

def predict_gesture(model, input_data):
    try:
        # 입력 데이터 형식 및 열 수 조정
        input_data = input_data.astype(np.float32)
        input_data = np.reshape(input_data, (1, -1))

        # 입력 데이터를 예측
        _, result, _, _ = model.findNearest(input_data, k=1)

        # 결과를 정수로 변환하여 해당하는 제스처를 찾음
        predicted_label = int(result[0, 0])

        # 제스처에 대한 응답 출력
        gesture_response = get_gesture_response(predicted_label)
        print(f"Predicted Gesture: {gesture_response}")

        return gesture_response

    except Exception as e:
        print(f"An error occurred during prediction: {e}")

if __name__ == "__main__":
    # 학습된 모델 파일 경로
    model_filepath = r'trained_model.yml'
    # 모델 로드
    trained_model =  load_trained_model(model_filepath)
    
    start_server(trained_model)
