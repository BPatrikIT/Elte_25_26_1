import socket
import struct
import sys

# --- Állandók ---
ISBN = "2399960172659"
IGENYELT_NAPOK = 20

# !!! ÍRD BE A SAJÁT NEPTUN KÓDOD !!!
NEPTUN = "ABC123"  # 6 karakter legyen

# --- Segédfüggvény ---
def send_message(sock, neptun, isbn, msg_type):
    data = struct.pack(
        "6s13s6s",
        neptun.encode("utf-8"),
        isbn.encode("utf-8"),
        msg_type.encode("utf-8")
    )
    sock.sendall(data)

def recv_two_ints(sock):
    data = sock.recv(struct.calcsize("ii"))
    return struct.unpack("ii", data)

def recv_string(sock, size):
    data = sock.recv(size)
    return data.decode("utf-8").strip("\x00")


def main():
    if len(sys.argv) != 3:
        print("Használat: python feladat1_client.py <host> <port>")
        sys.exit(1)

    host = sys.argv[1]
    port = int(sys.argv[2])

    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as sock:
        sock.connect((host, port))

        # 1. SUBMIT
        send_message(sock, NEPTUN, ISBN, "submit")

        elerheto, kolcsonzesi_ido = recv_two_ints(sock)

        # 2. NINCS ELÉRHETŐ PÉLDÁNY
        if elerheto == 0:
            send_message(sock, NEPTUN, ISBN, "cancel")

        else:
            # 3. VAN ELÉRHETŐ PÉLDÁNY
            if kolcsonzesi_ido >= IGENYELT_NAPOK:
                send_message(sock, NEPTUN, ISBN, "borrow")
            else:
                # 4. EXTEND KÉRÉS
                send_message(sock, NEPTUN, ISBN, "extend")

                jovahagyva, uj_ido = recv_two_ints(sock)

                if jovahagyva == 1 and uj_ido >= IGENYELT_NAPOK:
                    send_message(sock, NEPTUN, ISBN, "borrow")
                else:
                    send_message(sock, NEPTUN, ISBN, "cancel")

        # 5. VÉGSŐ AZONOSÍTÓ
        azonosito = recv_string(sock, 12)
        print("Kölcsönzési azonosító:", azonosito)


if __name__ == "__main__":
    main()
