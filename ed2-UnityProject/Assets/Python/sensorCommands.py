
import keyboard
import serial
from time import sleep
from GetCommand import GetCommands

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

gc = GetCommands()#reads file
commands = gc.getCommands()#gets commands from the file
threshold = 800


def pressKey(i, combo):
    if(commands[combo] != ""):
        keyboard.press(commands[combo]) #will keep repeating until released
        print("pressing " + commands[combo])
            
    else:    
        keyboard.press(commands[i]) #will keep repeating until released
        print("pressing " + commands[i])

        
while True:
    if(isConnected):
        #finger 1
        if(getSensorValue(0) > threshold):
            if(getSensorValue(1) > threshold):
                if(getSensorValue(2) > threshold):
                    if(getSensorValue(3) > threshold):
                        if(getSensorValue(4) > threshold):
                            #finger1,2,3,4,5
                            pressKey(0, 30)
                        elif(commands[30] != "" and keyboard.is_pressed(commands[30])):
                            keyboard.release(commands[30])
                        #finger1,2,3,4
                        pressKey(0, 25)
                    elif(commands[25] != "" and keyboard.is_pressed(commands[25])):
                         keyboard.release(commands[25])
                    #finger 1,2,3,5
                    elif(getSensorValue(4) > threshold):
                        pressKey(0, 27)
                    elif(commands[27] != "" and keyboard.is_pressed(commands[27])):
                        keyboard.release[27]
                    #finger1,2,3 
                    pressKey(0,15)
                elif(commands[15] != "" and keyboard.is_pressed(commands[15])):
                    keyboard.release(commands[15])
                elif(getSensorValue(3) > threshold):
                    if(getSensorValue(4) > threshold):
                        #finger 1,2,4,5
                        pressKey(0, 28)
                    elif(commands[28] != "" and keyboard.is_pressed(commands[28])):
                        keyboard.release(commands[28])
                    #finger 1,2,4 
                    pressKey(0,18)
                elif(commands[18] != "" and keyboard.is_pressed(commands[18])):
                    keyboard.release(commands[18])        
                elif(getSensorValue(4) > threshold):
                    #finger 1,2,5
                    pressKey(0,20)
                elif(commands[20] != "" and keyboard.is_pressed(commands[20])):
                    keyboard.release(commands[20])
                #finger 1,2
                pressKey(0, 5)
            elif(commands[5] != "" and keyboard.is_pressed(commands[5])):
                keyboard.release(commands[5])    
                        
            elif(getSensorValue(2) > threshold):
                if(getSensorValue(3) > threshold):
                    if(getSensorValue(4) > threshold):
                        #finger1,3,4,5
                        pressKey(0,29)
                    elif(commands[29] != "" and keyboard.is_pressed(commands[29])):
                        keyboard.release(commands[29])
                    #finger1,3,4
                    pressKey(0, 21)
                elif(commands[21] != "" and keyboard.is_pressed(commands[21])):
                    keyboard.release(commands[21])    
                #finger 1,3,5  
                elif(getSensorValue(4) < threshold):
                    pressKey(0, 23)
                elif(commands[23] != "" and keyboard.is_pressed(commands[23])):
                    keyboard.release(commands[23])    
                #finger1,3
                pressKey(0, 9)
            elif(commands[9] != "" and keyboard.is_pressed(commands[9])):
                keyboard.release(commands[9])

            elif(getSensorValue(3) > threshold):
                if(getSensorValue(4) > threshold):
                    #finger1,4,5
                    pressKey(0,24)
                elif(commands[24] != "" and keyboard.is_pressed(commands[24])):
                    keyboard.release(commands[24])    
                #finger1,4
                pressKey(0, 12)
            elif(commands[12] != "" and keyboard.is_pressed(commands[12])):
                    keyboard.release(commands[12])

            elif(getSensorValue(4) > threshold):
                #finger1,5
                pressKey(0, 14)
            elif(commands[14] != "" and keyboard.is_pressed(commands[14])):
                keyboard.release(commands[14])

            #finger 1    done
            else:
                pressKey(0,0)
        else:
            keyboard.release(commands[0])
            
        #finger 2
        if(getSensorValue(1) > threshold):
            if(getSensorValue(0) > threshold):
                if(getSensorValue(2) > threshold):
                    if(getSensorValue(3) > threshold):
                        if(getSensorValue(4) > threshold):
                            #finger2,1,3,4,5
                            pressKey(1, 30)
                        elif(commands[30] != "" and keyboard.is_pressed(commands[30])):
                            keyboard.release(commands[30])    
                        #finger2,1,3,4
                        pressKey(1, 25)
                    elif(commands[25] != "" and keyboard.is_pressed(commands[25])):
                        keyboard.release(commands[25])    
                    #finger 2,1,3,5
                    elif(getSensorValue(4) > threshold):
                        pressKey(1, 27)
                    elif(commands[27] != "" and keyboard.is_pressed(commands[27])):
                        keyboard.release(commands[27])    
                    #finger2,1,3 
                    pressKey(1,15)
                elif(commands[15] != "" and keyboard.is_pressed(commands[15])):
                    keyboard.release(commands[15])
                    
                elif(getSensorValue(3) > threshold):
                    if(getSensorValue(4) > threshold):
                        #finger 2,1,4,5
                        pressKey(1, 28)
                    elif(commands[28] != "" and keyboard.is_pressed(commands[28])):
                        keyboard.release(commands[28])
                    #finger 2,1,4 
                    pressKey(1,18)
                elif(commands[18] != "" and keyboard.is_pressed(commands[18])):
                    keyboard.release(commands[18])
                    
                elif(getSensorValue(4) > threshold):
                    #finger 2,1,5
                    pressKey(1,20)
                elif(commands[20] != "" and keyboard.is_pressed(commands[20])):
                    keyboard.release(commands[20])
                #finger 2,1
                pressKey(1, 5)
            elif(commands[5] != "" and keyboard.is_pressed(commands[5])):
                    keyboard.release(commands[5])    
                        
            elif(getSensorValue(2) > threshold):
                if(getSensorValue(3) > threshold):
                    if(getSensorValue(4) > threshold):
                        #finger2,3,4,5
                        pressKey(1,26)
                    elif(commands[26] != "" and keyboard.is_pressed(commands[26])):
                        keyboard.release(commands[26])
                    #finger2,3,4
                    pressKey(1, 16)
                elif(commands[16] != "" and keyboard.is_pressed(commands[16])):
                    keyboard.release(commands[16])    
                #finger 2,3,5  
                elif(getSensorValue(4) < threshold):
                    pressKey(1,19)
                elif(commands[19] != "" and keyboard.is_pressed(commands[19])):
                    keyboard.release(commands[19])    
                #finger2,3
                pressKey(1, 6)
            elif(commands[6] != "" and keyboard.is_pressed(commands[6])):
                    keyboard.release(commands[6])

            elif(getSensorValue(3) > threshold):
                if(getSensorValue(4) > threshold):
                    #finger2,4,5
                    pressKey(1,22)
                elif(commands[22] != "" and keyboard.is_pressed(commands[22])):
                    keyboard.release(commands[22])    
                #finger2,4
                pressKey(1, 10)
            elif(commands[10] != "" and keyboard.is_pressed(commands[10])):
                keyboard.release(commands[10])

            elif(getSensorValue(4) > threshold):
                #finger2,5
                pressKey(1, 13)
            elif(commands[13] != "" and keyboard.is_pressed(commands[13])):
                keyboard.release(commands[13])

            #finger 2    done
            else:
                pressKey(1,1)

        else:
            keyboard.release(commands[1])
        #finger 2 done
        
                               
        #finger3  
        if(getSensorValue(2) > threshold):
            if(getSensorValue(1) > threshold):
                if(getSensorValue(0) > threshold):
                    if(getSensorValue(3) > threshold):
                        if(getSensorValue(4) > threshold):
                            #finger3,1,2,4,5
                            pressKey(2, 30)
                        elif(commands[30] != "" and keyboard.is_pressed(commands[30])):
                            keyboard.release(commands[30])   
                        #finger3,1,2,4
                        pressKey(3, 25)
                    elif(commands[25] != "" and keyboard.is_pressed(commands[25])):
                        keyboard.release(commands[25])    
                    #finger 3,1,2,5
                    elif(getSensorValue(4) > threshold):
                        pressKey(2, 27)
                    elif(commands[27] != "" and keyboard.is_pressed(commands[27])):
                        keyboard.release(commands[27])    
                    #finger3,1,2 
                    pressKey(2,15)
                elif(commands[15] != "" and keyboard.is_pressed(commands[15])):
                    keyboard.release(commands[15])        
                elif(getSensorValue(3) > threshold):
                    if(getSensorValue(4) > threshold):
                        #finger 3,2,4,5
                        pressKey(2, 26)
                    elif(commands[26] != "" and keyboard.is_pressed(commands[26])):
                        keyboard.release(commands[26])
                    #finger 3,2,4 
                    pressKey(2,16)
                elif(commands[16] != "" and keyboard.is_pressed(commands[16])):
                    keyboard.release(commands[16])        
                elif(getSensorValue(4) > threshold):
                    #finger 3,2,5
                    pressKey(2, 19)
                elif(commands[19] != "" and keyboard.is_pressed(commands[19])):
                    keyboard.release(commands[19])
                #finger 3,2
                pressKey(2, 6)
                
            elif(commands[6] != "" and keyboard.is_pressed(commands[6])):
                    keyboard.release(commands[6])            
            elif(getSensorValue(0) > threshold):
                if(getSensorValue(3) > threshold):
                    if(getSensorValue(4) > threshold):
                        #finger1,3,4,5
                        pressKey(2,29)
                    elif(commands[29] != "" and keyboard.is_pressed(commands[29])):
                        keyboard.release(commands[29])
                    #finger1,3,4
                    pressKey(2, 21)
                elif(commands[21] != "" and keyboard.is_pressed(commands[21])):
                    keyboard.release(commands[21])   
                #finger 1,3,5  
                elif(getSensorValue(4) < threshold):
                    pressKey(2, 23)
                elif(commands[23] != "" and keyboard.is_pressed(commands[23])):
                    keyboard.release(commands[23])    
                #finger1,3
                pressKey(2, 9)
            elif(commands[9] != "" and keyboard.is_pressed(commands[9])):
                keyboard.release(commands[9])

            elif(getSensorValue(3) > threshold):
                if(getSensorValue(4) > threshold):
                    #finger3,4,5
                    pressKey(2,17)
                elif(commands[17] != "" and keyboard.is_pressed(commands[17])):
                    keyboard.release(commands[17])   
                #finger3,4
                pressKey(2,7)
            elif(commands[7] != "" and keyboard.is_pressed(commands[7])):
                keyboard.release(commands[7])

            elif(getSensorValue(4) > threshold):
                #finger3,5
                pressKey(2, 11)
            elif(commands[11] != "" and keyboard.is_pressed(commands[11])):
                keyboard.release(commands[11])

            #finger 3    done
            else:
                pressKey(2,2)
        else:
            keyboard.release(commands[2])

            
        #finger4
        if(getSensorValue(3) > threshold):
            if(getSensorValue(1) > threshold):
                if(getSensorValue(2) > threshold):
                    if(getSensorValue(0) > threshold):
                        if(getSensorValue(4) > threshold):
                            #finger1,2,3,4,5
                            pressKey(3, 30)
                        elif(commands[30] != "" and keyboard.is_pressed(commands[30])):
                            keyboard.release(commands[30])    
                        #finger1,2,3,4
                        pressKey(3, 25)
                    elif(commands[25] != "" and keyboard.is_pressed(commands[25])):
                        keyboard.release(commands[25])    
                    #finger 4,2,3,5
                    elif(getSensorValue(4) > threshold):
                        pressKey(3, 26)
                    elif(commands[26] != "" and keyboard.is_pressed(commands[26])):
                        keyboard.release(commands[26])    
                    #finger4,2,3 
                    pressKey(3,16)
                elif(commands[16] != "" and keyboard.is_pressed(commands[16])):
                    keyboard.release(commands[16])
                    
                elif(getSensorValue(0) > threshold):
                    if(getSensorValue(4) > threshold):
                        #finger 1,2,4,5
                        pressKey(3, 28)
                    elif(commands[28] != "" and keyboard.is_pressed(commands[28])):
                        keyboard.release(commands[28])
                    #finger 1,2,4 
                    pressKey(3,18)
                elif(commands[18] != "" and keyboard.is_pressed(commands[18])):
                    keyboard.release(commands[18])        
                elif(getSensorValue(4) > threshold):
                    #finger 4,2,5
                    pressKey(3,22)
                elif(commands[22] != "" and keyboard.is_pressed(commands[22])):
                    keyboard.release(commands[22])
                #finger 4,2
                pressKey(3, 10)
            elif(commands[10] != "" and keyboard.is_pressed(commands[10])):
                keyboard.release(commands[10])
                
                        
            elif(getSensorValue(2) > threshold):
                if(getSensorValue(0) > threshold):
                    if(getSensorValue(4) > threshold):
                        #finger1,3,4,5
                        pressKey(3,29)
                    elif(commands[29] != "" and keyboard.is_pressed(commands[29])):
                        keyboard.release(commands[29])
                    #finger1,3,4
                    pressKey(3, 21)
                elif(commands[21] != "" and keyboard.is_pressed(commands[21])):
                    keyboard.release(commands[21])
                    
                #finger 4,3,5  
                elif(getSensorValue(4) < threshold):
                    pressKey(3, 17)
                elif(commands[17] != "" and keyboard.is_pressed(commands[17])):
                    keyboard.release(commands[17])    
                #finger4,3
                pressKey(3, 7)
            elif(commands[7] != "" and keyboard.is_pressed(commands[7])):
                    keyboard.release(commands[7])


            elif(getSensorValue(0) > threshold):
                if(getSensorValue(4) > threshold):
                    #finger1,4,5
                    pressKey(3,24)
                elif(commands[24] != "" and keyboard.is_pressed(commands[24])):
                    keyboard.release(commands[24])
                    
                #finger1,4
                pressKey(3, 12)
            elif(commands[12] != "" and keyboard.is_pressed(commands[12])):
                keyboard.release(commands[12])

            elif(getSensorValue(4) > threshold):
                #finger4,5
                pressKey(3, 8)
            elif(commands[8] != "" and keyboard.is_pressed(commands[8])):
                keyboard.release(commands[8])

            #finger 4    done
            else:
                pressKey(3,3)
        else:
            keyboard.release(commands[3])

            
        #finger5
        if(getSensorValue(4) > threshold):
            if(getSensorValue(1) > threshold):
                if(getSensorValue(2) > threshold):
                    if(getSensorValue(3) > threshold):
                        if(getSensorValue(0) > threshold):
                            #finger1,2,3,4,5
                            pressKey(4, 30)
                        elif(commands[30] != "" and keyboard.is_pressed(commands[30])):
                            keyboard.release(commands[30])   
                        #finger5,2,3,4
                        pressKey(4,26)
                    elif(commands[26] != "" and keyboard.is_pressed(commands[26])):
                        keyboard.release(commands[26])    
                    #finger 1,2,3,5
                    elif(getSensorValue(0) > threshold):
                        pressKey(4, 27)
                    elif(commands[27] != "" and keyboard.is_pressed(commands[27])):
                        keyboard.release(commands[27])    
                    #finger5,2,3 
                    pressKey(4,19)
                elif(commands[19] != "" and keyboard.is_pressed(commands[19])):
                    keyboard.release(commands[19])        
                elif(getSensorValue(3) > threshold):
                    if(getSensorValue(0) > threshold):
                        #finger 1,2,4,5
                        pressKey(4, 28)
                    elif(commands[28] != "" and keyboard.is_pressed(commands[28])):
                        keyboard.release(commands[28])
                    #finger 5,2,4 
                    pressKey(4,22)
                elif(commands[22] != "" and keyboard.is_pressed(commands[22])):
                    keyboard.release(commands[22])        
                elif(getSensorValue(0) > threshold):
                    #finger 1,2,5
                    pressKey(4,20)
                elif(commands[20] != "" and keyboard.is_pressed(commands[20])):
                    keyboard.release(commands[20])
                #finger 5,2
                pressKey(4, 13)
            elif(commands[13] != "" and keyboard.is_pressed(commands[13])):
                    keyboard.release(commands[13])    
                        
            elif(getSensorValue(2) > threshold):
                if(getSensorValue(3) > threshold):
                    if(getSensorValue(0) > threshold):
                        #finger1,3,4,5
                        pressKey(4,29)
                    elif(commands[29] != "" and keyboard.is_pressed(commands[29])):
                        keyboard.release(commands[29])
                    #finger5,3,4
                    pressKey(4, 17)
                elif(commands[17] != "" and keyboard.is_pressed(commands[17])):
                    keyboard.release(commands[17])    
                #finger 1,3,5  
                elif(getSensorValue(0) < threshold):
                    pressKey(4, 23)
                elif(commands[23] != "" and keyboard.is_pressed(commands[23])):
                    keyboard.release(commands[23])    
                #finger5,3
                pressKey(4, 11)
            elif(commands[11] != "" and keyboard.is_pressed(commands[11])):
                    keyboard.release(commands[11])

            elif(getSensorValue(3) > threshold):
                if(getSensorValue(0) > threshold):
                    #finger1,4,5
                    pressKey(4,24)
                elif(commands[24] != "" and keyboard.is_pressed(commands[24])):
                    keyboard.release(commands[24])    
                #finger5,4
                pressKey(4, 8)
            elif(commands[8] != "" and keyboard.is_pressed(commands[8])):
                    keyboard.release(commands[8])

            elif(getSensorValue(0) > threshold):
                #finger1,5
                pressKey(4, 14)

            elif(commands[14] != "" and keyboard.is_pressed(commands[14])):
                    keyboard.release(commands[14])
            #finger 5    done
            else:
                pressKey(4,4)
        else:
            keyboard.release(commands[4])
           

    else:
        wakeUpController()
