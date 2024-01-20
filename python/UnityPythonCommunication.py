import socket

def start_server():
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.bind(("127.0.0.1", 8888))
    server.listen(1)

    print("Python server is waiting for connection...")
    conn, addr = server.accept()
    print(f"Connected to {addr}")

    while True:
        data = conn.recv(4096)
        if not data:
            break
        message = data.decode('ascii')

        # Process the data (modify as needed)
        processed_data = str(int(message) * 2)

        # Send the processed data back to Unity
        conn.send(processed_data.encode('ascii'))

    conn.close()
    server.close()

if __name__ == "__main__":
    start_server()
