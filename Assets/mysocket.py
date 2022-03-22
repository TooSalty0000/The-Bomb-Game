import socket
import random
from time import sleep
  
import cv2
import mediapipe as mp
import time

cap = cv2.VideoCapture(0)
cap.set(3, 1280)
cap.set(4, 720)

hostname = socket.gethostname()
local_ip = socket.gethostbyname(hostname)
print(local_ip)


mpHands = mp.solutions.hands
hands = mpHands.Hands(static_image_mode=False,
                      max_num_hands=2,
                      min_detection_confidence=0.5,
                      min_tracking_confidence=0.5)
mpDraw = mp.solutions.drawing_utils

pTime = 0
cTime = 0
testID = 0
palm = [0, 0]
thumb = [0, 0];
mf = [0, 0];
# square = [-9999, -9999, 9999, 9999]

def distance(x, y):
    return ((x[0] - y[0]) ** 2 + (x[1] - y[1]) ** 2) ** 0.5;

def sendData(data):
    s = socket.socket()
    s.connect((local_ip, 1755))
    s.send(data.encode())
    s.close()

# setup Socket server

while True:
    success, img = cap.read()
    img = cv2.flip(img, 1)
    imgRGB = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
    results = hands.process(imgRGB)
    # print(results.multi_hand_landmarks)
    if results.multi_hand_landmarks:
        for handLms in results.multi_hand_landmarks:
            mpDraw.draw_landmarks(img, handLms, mpHands.HAND_CONNECTIONS)
            for id, lm in enumerate(handLms.landmark):
                #print(id,lm)
                h, w, c = img.shape
                cx, cy = int(lm.x *w), int(lm.y*h)
                if (id == 4):
                    thumb = [cx, cy]
                if (id == 12):
                    mf = [cx, cy]
                
                # if (id == testID):
                #     cv2.circle(img, (cx, cy), 5, (0, 0, 255), -1)
                # if (cx > square[0]):
                #     square[0] = cx
                # if (cx < square[2]):
                #     square[2] = cx
                # if (cy > square[1]):
                #     square[1] = cy
                # if (cy < square[3]):
                #     square[3] = cy
                # if (id == 4):
                #     thumb[0] = cx;
                #     thumb[1] = cy;
                # if (id == 8):
                #     index[0] = cx;
                #     index[1] = cy;
                if (id == 0):
                    palm[0] = cx
                    palm[1] = cy

    # cTime = time.time()
    # fps = 1/(cTime-pTime)
    # pTime = cTime
    # cv2.putText(img,str(int(fps), (10,70), cv2.FONT_HERSHEY_PLAIN, 3, (255,0,255), 3)
    cv2.imshow("Image", img)
    try:
        sendString = str(palm[0]) + ", " + str(palm[1]) + ", " + str(int((thumb[0] - mf[0]) ** 2 + (thumb[1] - mf[1]) ** 2))
        # print(int((thumb[0] - mf[0]) ** 2 + (thumb[1] - mf[1]) ** 2))
        sendData(sendString)
    except:
        print("No server")
    
    
    
    pressedKey = cv2.waitKey(1) & 0xFF;
    if pressedKey == ord('e'):
        testID += 1
    if pressedKey == ord('q'):
        break