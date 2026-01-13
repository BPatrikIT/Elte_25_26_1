import socket
import select
import struct
import sys

HOST = "localhost"
PORT = 10001

# tantárgy -> { neptun -> jegy }
grades = {}


def handle_command(data):
    cmd, course, neptun, grade = struct.unpack("3s6s6sf", data)

    cmd = cmd.decode().strip("\x00")
    course = course.decode().strip("\x00")
    neptun = neptun.decode().strip("\x00")

    if cmd == "INS":
        if course not in grades:
            grades[course] = {}
        grades[course][neptun] = grade
        return grade

    elif cmd == "GET":
        return grades.get(course, {}).get(neptun, 0.0)

    elif cmd == "AVG":
        subject = grades.get(course, {})
        if not subject:
            return 0.0
        return sum(subject.values()) / len(subject)

    return 0.0


def main():
    if len(sys.argv) == 2:
        port = int(sys.argv[1])
    else:
        port = PORT

    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    server.bind((HOST, port))
    server.listen()

    server.setblocking(False)

    sockets = [server]

    print(f"Szerver fut a {HOST}:{port} címen")

    while True:
        readable, _, _ = select.select(sockets, [], [])

        for sock in readable:
            if sock is server:
                client, addr = server.accept()
                client.setblocking(False)
                sockets.append(client)
            else:
                try:
                    data = sock.recv(struct.calcsize("3s6s6sf"))
                    if not data:
                        sockets.remove(sock)
                        sock.close()
                        continue

                    result = handle_command(data)
                    sock.sendall(struct.pack("f", result))

                except ConnectionResetError:
                    sockets.remove(sock)
                    sock.close()


if __name__ == "__main__":
    main()
