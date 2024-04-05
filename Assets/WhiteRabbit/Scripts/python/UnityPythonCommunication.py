import socket
import cv2
import numpy as np

# Gesture dictionary
gesture = {
    0: 'ㄱ', 1: 'ㄴ', 2: 'ㄷ', 3: 'ㄹ', 4: 'ㅁ', 5: 'ㅂ', 6: 'ㅅ', 7: 'ㅇ',
    8: 'ㅈ', 9: 'ㅊ', 10: 'ㅋ', 11: 'ㅌ', 12: 'ㅍ', 13: 'ㅎ', 14: 'ㅏ', 15: 'ㅑ',
    16: 'ㅓ', 17: 'ㅕ', 18: 'ㅗ', 19: 'ㅛ', 20: 'ㅜ', 21: 'ㅠ', 22: 'ㅡ', 23: 'ㅣ',
    24: 'ㅐ', 25: 'ㅒ', 26: 'ㅔ', 27: 'ㅖ', 28: 'ㅢ', 29: 'ㅚ', 30: 'ㅟ'
}

def start_server():
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.bind(("127.0.0.1", 8888))
    server.listen(1)

    print("Python server is waiting for connection...")
    conn, addr = server.accept()
    print(f"Connected to {addr}")

    while True:
        data = conn.recv(4096)
        message = data.decode('ascii')

        # Process the data (modify as needed)
        processed_data = str(message + " rec")

        # Send the processed data back to Unity
        conn.send(processed_data.encode('ascii'))

    conn.close()
    server.close()

if __name__ == "__main__":
    start_server()
