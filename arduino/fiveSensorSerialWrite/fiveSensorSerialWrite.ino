const int NUM_OF_SENSORS = 5;
int readings[NUM_OF_SENSORS];
String readingsMessage;

void setup() {
  // initialize the serial port
  Serial.begin(9600);
}

void loop() {
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

  delay(50);
}
