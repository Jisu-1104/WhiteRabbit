import csv
import numpy as np
import time

# 빈 리스트를 생성하여 데이터를 저장할 준비
rawdata = []

# CSV 파일 읽기. 파일 이름은 다를 수 있다. 
with open(r'C:\Users\rlawl\OneDrive\문서\GitHub\WhiteRabbit\Assets\None.csv', newline='') as csvfile:
    reader = csv.reader(csvfile)
    
    # 각 열의 값을 담을 리스트 초기화
    column_data = [[] for _ in range(21)]  # 3개의 열이 있다고 가정

    # 한 번에 한 줄씩 읽어들이면서 처리
    for idx, row in enumerate(reader):
        # 각 열의 값을 가져와서 각 열의 리스트에 추가
        for i in range(21):  
            column_data[i].append(float(row[i]))  # 각 열의 값을 부동 소수점으로 변환하여 해당 열의 리스트에 추가

        # 3개의 줄씩 처리할 때마다 각 열의 값을 data 리스트에 추가하고 각 열의 리스트 비우기
        if (idx + 1) % 3 == 0:  # 3개의 줄씩 처리하고자 함
            for i in range(21):  
                rawdata.append(column_data[i])
                column_data[i] = []
            # 리스트를 NumPy 배열로 변환
            joint = np.array(rawdata)
            print(joint)
            print(rawdata)

            # 벡터를 구하기 위해 생성하는 v1,v2 (v2에서 v2을 빼면 v백터가 된다)
            v1 = joint[[0,1,2,3,0,5,6,7,0, 9,10,11, 0,13,14,15, 0,17,18,19],:] 
            v2 = joint[[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20],:]

            # 정규화
            v = v2-v1
            v = v / np.linalg.norm(v,axis=1)[:,np.newaxis]

            #각 벡터의 각도를 비교하기 위해 생성하는 compare벡터
            compareV1 = v[[0,1,2,4,5,6,8,9,10,12,13,14,16,17,18],:] 
            compareV2 = v[[1,2,3,5,6,7,9,10,11,13,14,15,17,18,19],:]

            # compare벡터를 사용하여 각도를 구함
            angle = np.arccos(np.einsum('nt,nt->n',compareV1,compareV2)) 
            angle = np.degrees(angle)

            #dataset 저장할 파일 만들기
            f = open(r'dataset.txt', 'a')

            #파일에 dataset 집어넣기
            for i in angle :
                num = round(i, 6)
                f.write(str(num))
                f.write(',')
            f.write("7.000000") #gesture 번호
            f.write("\n")

            f.close();
            rawdata = []



