import cv2
import numpy as np
import time
import traceback  # traceback 모듈을 추가

def train_knn_model(model_filepath):
    gesture = {
        0: 'ㄱ', 1: 'ㄴ', 2: 'ㄷ', 3: 'ㄹ', 4: 'ㅁ', 5: 'ㅂ', 6: 'ㅅ', 7: 'ㅇ',
        8: 'ㅈ', 9: 'ㅊ', 10: 'ㅋ', 11: 'ㅌ', 12: 'ㅍ', 13: 'ㅎ', 14: 'ㅏ', 15: 'ㅑ',
        16: 'ㅓ', 17: 'ㅕ', 18: 'ㅗ', 19: 'ㅛ', 20: 'ㅜ', 21: 'ㅠ', 22: 'ㅡ', 23: 'ㅣ',
        24: 'ㅐ', 25: 'ㅒ', 26: 'ㅔ', 27: 'ㅖ', 28: 'ㅢ', 29: 'ㅚ', 30: 'ㅟ',
        31 : "Unknown"
    }

    try:
        # 학습 데이터 로드
        file = np.genfromtxt(r'dataset.txt', delimiter=',')
        angleFile = file[:, :-1]
        labelFile = file[:, -1]
        angle = angleFile.astype(np.float32)
        label = labelFile.astype(np.float32)

        # KNN 모델 훈련
        knn = cv2.ml.KNearest_create()
        knn.train(angle, cv2.ml.ROW_SAMPLE, label)

        # 모델 저장
        knn.save(model_filepath)
        print(f"Model trained and saved to {model_filepath}")

    except Exception as e:
        # 예외가 발생한 경우 오류 메시지와 스택 트레이스를 출력
        print(f"An error occurred: {e}")
        traceback.print_exc()

    time.sleep(3)

if __name__ == "__main__":
    # 모델 파일 경로
    model_filepath = r'trained_model.yml'

    # KNN 모델 훈련 및 모델 파일 저장
    train_knn_model(model_filepath)
