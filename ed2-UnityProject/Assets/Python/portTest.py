import keyboard
import serial
from time import sleep

NUMBER_OF_SENSORS = 5
readings = [0] * 5

isConnected = False
serialPort = serial.Serial('COM3', timeout=1)
serialPort.flush()


# TODO iterate through available ports and find correct one programmatically
def wakeUpController():
    global isConnected
    serialPort.write("WakeUp\n".encode())  # send wake up message
    response = serialPort.readline().decode().strip()
    print(response)
    if (response == "ArduinoUno"):
        print('response == "ArduinoUno" TRUE')
        isConnected = True

def getSensorValue(sensorId):
    readFromSerial()
    return readings[sensorId]

def readFromSerial():
    serialMessage = serialPort.readline().decode().strip()
    serialPort.write("reading\n".encode())

    if (len(serialMessage) != 0):
        parseMessage(serialMessage)

def parseMessage(serialMessage):
    global readings
    j = 0

    for i in range(NUMBER_OF_SENSORS):
        messageValue = ""

        while serialMessage[j] != ',':
            messageValue += serialMessage[j]
            j = j+1
        readings[i] = int(messageValue)
        j = j+1

while True:
    if(isConnected):
        if(getSensorValue(0) > 800):
            keyboard.press("s")
            print("pressing s")
            sleep(.001)
        else:
            keyboard.release("s")

        if(getSensorValue(1) > 800):
            keyboard.press("a")
            print("pressing a")
            sleep(.001)
        else:
            keyboard.release("a")

        if(getSensorValue(2) > 800):
            keyboard.press("w")
            print("pressing w")
            sleep(.001)
        else:
            keyboard.release("w")

        if(getSensorValue(3) > 800):
            keyboard.press("d")
            print("pressing d")
            sleep(.001)
        else:
            keyboard.release("d")

        if(getSensorValue(4) > 800):
            keyboard.press("space")
            print("pressing space")
            sleep(.001)
        else:
            keyboard.release("space")

    else:
        wakeUpController()
