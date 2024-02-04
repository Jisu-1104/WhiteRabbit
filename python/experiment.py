import cv2
import numpy as np
import time

def get_gesture_response(label):
    # 라벨에 해당하는 제스처를 반환
    gesture_mapping = {
        0: 'ㄱ', 1: 'ㄴ', 2: 'ㄷ', 3: 'ㄹ', 4: 'ㅁ', 5: 'ㅂ', 6: 'ㅅ', 7: 'ㅇ',
        8: 'ㅈ', 9: 'ㅊ', 10: 'ㅋ', 11: 'ㅌ', 12: 'ㅍ', 13: 'ㅎ', 14: 'ㅏ', 15: 'ㅑ',
        16: 'ㅓ', 17: 'ㅕ', 18: 'ㅗ', 19: 'ㅛ', 20: 'ㅜ', 21: 'ㅠ', 22: 'ㅡ', 23: 'ㅣ',
        24: 'ㅐ', 25: 'ㅒ', 26: 'ㅔ', 27: 'ㅖ', 28: 'ㅢ', 29: 'ㅚ', 30: 'ㅟ'
    }

    return gesture_mapping.get(label, "Unknown Gesture")

def load_trained_model(model_filepath):
    # KNN 모델 로드
    knn = cv2.ml.KNearest_create()
    knn = knn.load(model_filepath)
    return knn

def process_landmark_data(message):
    landmark_data = [float(x) for x in message.split(',')]
    if len(landmark_data) != 63:
        print("Invalid landmark data format.")
        time.sleep(3)
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

    except Exception as e:
        print(f"An error occurred during prediction: {e}")

if __name__ == "__main__":
    # 학습된 모델 파일 경로
    model_filepath = r'trained_model.yml'

    # 모델 로드
    trained_model =  load_trained_model(model_filepath)
    print(trained_model)

    # 하드코딩된 landmark 데이터 (예시 데이터)
    hardcoded_message = "0.7399392,0.6396268,0.5696343,0.5307348,0.511135,0.6310843,0.5866882,0.5578044,0.5356174,0.6856714,0.6545372,0.6266471,0.6021307,0.7410411,0.7188329,0.6926796,0.6673013,0.7987934,0.8012974,0.7915003,0.7768581,0.8101081,0.7784361,0.6999842,0.6093031,0.5249171,0.5565302,0.4521163,0.3825617,0.320829,0.5243816,0.4023914,0.323635,0.2596858,0.5165454,0.3902571,0.3101223,0.2430195,0.5285105,0.423597,0.3507478,0.2853324,2.67897E-07,-0.03017016,-0.03862337,-0.04682495,-0.05385901,0.004100557,-0.01563423,-0.03622612,-0.0524838,8.671743E-05,-0.01733089,-0.03923368,-0.05577574,-0.01111412,-0.03512017,-0.05568114,-0.06890276,-0.02559046,-0.04956273,-0.06292136,-0.0710628"
    
    # 데이터를 직접 전달하여 결과 얻기
    angles = process_landmark_data(hardcoded_message)
    angles = angles.astype(np.float32)

    # 제스처 예측 및 응답
    predict_gesture(trained_model, angles)

    time.sleep(3)
