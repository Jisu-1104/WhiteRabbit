import cv2
import numpy as np

#gesture. 추가해야 함
gesture = {0:'ㄱ'}
gesture = {1:'ㄴ'}
gesture = {2:'ㄷ'}
gesture = {3:'ㄹ'}
gesture = {4:'ㅁ'}
gesture = {5:'ㅂ'}
gesture = {6:'ㅅ'}
gesture = {7:'ㅇ'}
gesture = {8:'ㅈ'}
gesture = {9:'ㅊ'}
gesture = {10:'ㅋ'}
gesture = {11:'ㅌ'}
gesture = {12:'ㅍ'}
gesture = {13:'ㅎ'}
gesture = {14:'ㅏ'}
gesture = {15:'ㅑ'}
gesture = {16:'ㅓ'}
gesture = {17:'ㅕ'}
gesture = {18:'ㅗ'}
gesture = {19:'ㅛ'}
gesture = {20:'ㅜ'}
gesture = {21:'ㅠ'}
gesture = {22:'ㅡ'}
gesture = {23:'ㅣ'}
gesture = {24:'ㅐ'}
gesture = {25:'ㅒ'}
gesture = {26:'ㅔ'}
gesture = {27:'ㅖ'}
gesture = {28:'ㅢ'}
gesture = {29:'ㅚ'}
gesture = {30:'ㅟ'}

#학습시키기
file = np.genfromtxt(r'test.txt', delimiter=',')
angleFile = file[:,:-1]
labelFile = file[:,-1]
angle = angleFile.astype(np.float32)
label = labelFile.astype(np.float32)

knn = cv2.ml.KNearest_create()
knn.train(angle, cv2.ml.ROW_SAMPLE,label)

"""
#유니티에서 데이터를 받아와야 함!!
data = np.array([angle], dtype=np.float32)

#index(=gesture 번호)
ret, results, neighbours, dist = knn.findNearest(data, 3) # k가 3일 때의 값 구함
idx = int(results[0][0])
"""
