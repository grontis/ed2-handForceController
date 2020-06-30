int loopDelay = 50; //ms

String incomingMessage;
String wakeUpValue = "WakeUp";

const int NUM_OF_SENSORS = 5;
int readings[NUM_OF_SENSORS];
String readingsMessage;

bool isConnected = false;
int timeSinceLastReading = 0; //time in ms since last reading
int connectionTimeout = 500; //.5 second

void setup() {
  // initialize the serial port
  Serial.begin(9600);
}

void loop() {
  if(!isConnected){
    //checks to see if serial data is available to read
    if (Serial.available() > 0){
      incomingMessage = Serial.readStringUntil('\n');
      incomingMessage.trim();
  
      if(incomingMessage == "WakeUp"){
        Serial.println("ArduinoUno");
        isConnected = true;
        timeSinceLastReading = 0;
        delay(loopDelay);
      }
    }
  }
  else{
    timeSinceLastReading += loopDelay;
    if(timeSinceLastReading >= connectionTimeout){
      isConnected = false;
      return;
    }
    
    if (Serial.available() > 0){
      incomingMessage = Serial.readStringUntil('\n');
      incomingMessage.trim();
  
      if(incomingMessage == "reading"){
        timeSinceLastReading = 0;
      }
      else if(incomingMessage == "WakeUp"){
        Serial.println("ArduinoUno");
        isConnected = true;
        timeSinceLastReading = 0;
        delay(loopDelay);
        return;
      }
    }
    
    //read values and store in array
    for(int i = 0; i < NUM_OF_SENSORS; i++){
      readings[i] = analogRead(i);
    }
  
    //create comma separated message to transmit over serial
    for(int i = 0; i < NUM_OF_SENSORS; i++){
      readingsMessage += String(readings[i]) + ",";
    }
  
    //print message to serial port
    Serial.println(readingsMessage);
  
    //reset message string
    readingsMessage = "";

    Serial.flush();
    delay(loopDelay);
  }
}
