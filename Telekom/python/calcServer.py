from socket import socket,AF_INET, SOCK_STREAM, timeout, SOL_SOCKET, SO_REUSEADDR
import struct
from select import select

server_addr = ('', 10000)
unpacker = struct.Struct('I I 1s')  #int, int, char[1]

with socket(AF_INET, SOCK_STREAM) as server:
    server.bind(server_addr)
    server.listen(1)
    server.setsockopt(SOL_SOCKET, SO_REUSEADDR, 1)

    sockets = [ server ]

    while True:
        r,w,e = select.select(sockets, [], [], 1)
        
        if not (r or w or e):
            continue
        
        for s in r:
            if s is server:
                client, client_addr = s.accept()
                socket.append(client)
                print("Kapcsolodott:", client_addr)
            else:
                data = s.recv(unpacker.size)
                if not data:
                    sockets.remove(s)
                    s.cloe()
                    print("Kapcsolat bontva")
                else:
                    print("Kapott adat:", data)
                    unp_data = unpacker.unpack(data)
                    print("Szetszedett adat:", unp_data)
                    x = eval(str(unp_data[0]) + unp_data[2].decode() + str(unp_data[1]))
                    print("Szamitott ertek:", x)
                    s.sendall(str(x).encode())