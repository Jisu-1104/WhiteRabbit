import cv2
import numpy as np

#gesture. 추가해야 함
gesture = {0:'ㄱ'}

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
