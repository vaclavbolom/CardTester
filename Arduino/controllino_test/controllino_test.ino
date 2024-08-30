#include <Controllino.h>

void setup() {
  // put your setup code here, to run once:
  pinMode(CONTROLLINO_A0, INPUT);
  Serial.begin(9600); 
}

void loop() {
  // put your main code here, to run repeatedly:
  int myInput = digitalRead(CONTROLLINO_A0);
  Serial.print("Input value: ");
  Serial.println(myInput);
  delay(500);
}
