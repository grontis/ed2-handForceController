int loopDelay = 50; //ms
String incomingMessage;
String wakeUpValue = "WakeUp";
String readingsMessage;
bool isConnected = false;
int timeSinceLastReading = 0; //time in ms since last reading
int connectionTimeout = 500; //.5 second

//PIN Definitions for Multiplexer
const int selectPins[3] = {2, 3, 4}; // S0~2, S1~3, S2~4
const int zInput = A5; // Connect common (Z)

const int NUM_OF_SENSORS = 8;
const int NUM_ON_MUX = 3;
int readings[NUM_OF_SENSORS];

void setup() {
  // initialize the serial port
  Serial.begin(9600);

  //Set select pins to output
  for (int i=0; i<3; i++)
  {
    pinMode(selectPins[i], OUTPUT);
    digitalWrite(selectPins[i], HIGH);
  }

  //set z input
  pinMode(zInput, INPUT);
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
    //First, get values from sensors connected to analog pins directly
    // For example if you have NUM_OF_SENSORS=8, NUM_ON_MUX=3, this loop will iterate A0-A4
    // or if you had NUM_OF_SENSORS=5, NUM_ON_MUX=0, it would still iterate A0-A4, and no mux readings necessary
    for(int i = 0; i < NUM_OF_SENSORS - NUM_ON_MUX; i++){
      readings[i] = analogRead(i);
    }
    //Second, get mux readings
    int currentReading = NUM_OF_SENSORS - NUM_ON_MUX;
    for(byte pin = 0; pin < NUM_ON_MUX; pin++){
      selectMuxPin(pin); //enable select pins accordingly
      readings[currentReading] = analogRead(A5); // and read from Z
      currentReading++;
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


void selectMuxPin(byte pin)
{
  for (int i=0; i<3; i++)
  {
    if (pin & (1<<i))
      digitalWrite(selectPins[i], HIGH);
    else
      digitalWrite(selectPins[i], LOW);
  }
}
