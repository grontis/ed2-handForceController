//This program is outputting a single sensor's value over serial


int read1;


void setup() {
  // initialize the serial port
  Serial.begin(9600);
}

void loop() {
  //read values
  read1 = analogRead(0);

  //serial print values
  Serial.println(read1);

  delay(1000);
}
