void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);    
  pinMode(13, OUTPUT); 
}

void loop() {
  // put your main code here, to run repeatedly:
  if (Serial.available())
  {
    char data = Serial.read();
    Serial.println(data);
    switch(data){
      case 'O':
        digitalWrite(13, HIGH);
        break;
      case 'F':
        digitalWrite(13, LOW);
        break;

    }
  }
}
