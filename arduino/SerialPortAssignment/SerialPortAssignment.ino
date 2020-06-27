String incomingMessage;
String wakeUpValue = "WakeUp";

void setup() {
  // initialize the serial port
  Serial.begin(9600);
}

void loop() {
  if (Serial.available() > 0){
    incomingMessage = Serial.readStringUntil('\n');
    incomingMessage.trim();

    if(incomingMessage == "WakeUp"){
      Serial.println("ArduinoUno");
    }
  }
}
