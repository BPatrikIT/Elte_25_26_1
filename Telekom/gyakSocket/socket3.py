import socket
import select
import struct
import sys

# ===== BEÁLLÍTÁSOK =====
PROXY_HOST = "localhost"
PROXY_PORT = 10002

FEEDBACK_HOST = "localhost"
FEEDBACK_PORT = 10001

BUFFER_SIZE = 1024

# ======================

def main():
    # TCP szerver socket
    tcp_server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    tcp_server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    tcp_server.bind((PROXY_HOST, PROXY_PORT))
    tcp_server.listen()
    tcp_server.setblocking(False)

    # UDP socket a feedback szerverhez
    udp_sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    udp_sock.settimeout(2)

    sockets = [tcp_server]
    client_type = {}  # socket -> "student" | "admin"

    print(f"Proxy fut: {PROXY_HOST}:{PROXY_PORT}")

    while True:
        readable, _, _ = select.select(sockets, [], [])

        for sock in readable:
            # Új TCP kliens
            if sock is tcp_server:
                client, addr = tcp_server.accept()
                client.setblocking(False)
                sockets.append(client)

            else:
                try:
                    data = sock.recv(struct.calcsize("6s6si"))
                    if not data:
                        sockets.remove(sock)
                        sock.close()
                        continue

                    neptun, subject, score = struct.unpack("6s6si", data)
                    neptun = neptun.decode().strip("\x00")
                    subject_str = subject.decode().strip("\x00")

                    # kliens típusa
                    is_admin = (neptun == "AAA000")
                    client_type[sock] = "admin" if is_admin else "student"

                    # hibás pontszám
                    if score >= 6:
                        sock.sendall("Hibás értékelési pontszám".encode())
                        continue

                    # UDP továbbítás
                    udp_data = struct.pack("6si", subject, score)
                    udp_sock.sendto(
                        udp_data,
                        (FEEDBACK_HOST, FEEDBACK_PORT)
                    )

                    response, _ = udp_sock.recvfrom(BUFFER_SIZE)
                    response_str = response.decode()

                    # Admin válasz prefixelése
                    if is_admin:
                        response_str = f"{subject_str} {response_str}"

                    sock.sendall(response_str.encode())

                except (ConnectionResetError, socket.timeout):
                    sockets.remove(sock)
                    sock.close()


if __name__ == "__main__":
    main()
