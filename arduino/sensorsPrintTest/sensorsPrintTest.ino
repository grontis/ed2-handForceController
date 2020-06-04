int read1;
int read2;
int read3;
int read4;
int read5;


void setup() {
  // initialize the serial port
  Serial.begin(9600);
}

void loop() {
  //read values
  read1 = analogRead(0);
  read2 = analogRead(1);
  read3 = analogRead(2);
  read4 = analogRead(3);
  read5 = analogRead(4);

  //serial print values
  Serial.println(
    "thumb: " + String(read1) + ", " +
    "pointer: " + String(read2) + ", " +
    "middle: " + String(read3) + ", " +
    "ring: " + String(read4) + ", " +
    "pinky: " + String(read5) + ", "
    );

  delay(500);
}
