import csv
import numpy as np

# 빈 리스트를 생성하여 데이터를 저장할 준비
data = []

# CSV 파일 읽기
with open(r'C:\Users\rlawl\OneDrive\문서\GitHub\WhiteRabbit\Assets\example.csv', newline='') as csvfile:
    reader = csv.reader(csvfile)
    
     # 각 열의 값을 담을 리스트 초기화
    column_data = [[] for _ in range(21)]

    # 한 번에 한 줄씩 읽어들이면서 처리
    for idx, row in enumerate(reader):
        # 각 열의 값을 가져와서 각 열의 리스트에 추가
        for i in range(21): 
            column_data[i].append(float(row[i]))  # 각 열의 값을 부동 소수점으로 변환하여 해당 열의 리스트에 추가

        # 3개의 줄씩 처리할 때마다 각 열의 값을 data 리스트에 추가하고 각 열의 리스트 비우기
        if (idx + 1) % 3 == 0:  # 3개의 줄씩 처리하고자 함
            for i in range(21):
                data.append(column_data[i])
                column_data[i] = []

# 리스트를 NumPy 배열로 변환
joint = np.array(data)
print(joint)
