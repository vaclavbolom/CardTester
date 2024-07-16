const int PIN_UP = 12;
const int PIN_DOWN = 2;
const int PIN_FORWARD = 7;
const int PIN_BACKWARD = 4;
const int PIN_AVAILABLE = 13;
const char COMMAND_FORWARD = 'F';
const char COMMAND_BACKWARD = 'B';
const char COMMAND_STOP = 'S';
const char COMMAND_EMPTY = ' ';
const char STATE_UP = 'U';
const char STATE_DOWN = 'D';
char command = ' ';

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);    
  pinMode(PIN_UP, INPUT); 
  pinMode(PIN_DOWN, INPUT);
  pinMode(PIN_AVAILABLE, OUTPUT);
  pinMode(PIN_FORWARD, OUTPUT);
  pinMode(PIN_BACKWARD, OUTPUT);

  digitalWrite(PIN_FORWARD, HIGH);
  digitalWrite(PIN_BACKWARD, HIGH);
  delay(1000);
  digitalWrite(PIN_FORWARD, LOW);
  digitalWrite(PIN_BACKWARD, LOW);
  digitalWrite(PIN_AVAILABLE, LOW);
}

void loop() {
  // put your main code here, to run repeatedly:
  char data = ' ';
  int state_up = digitalRead(PIN_UP);
  int state_down = digitalRead(PIN_DOWN);
  if (Serial.available())
  {    
    data = Serial.read();
    // Serial.println(data);
    
    switch(data){
      case COMMAND_FORWARD:
        command = data;
        break;       
      case COMMAND_BACKWARD:
        command = data;
        break;        
      case COMMAND_STOP:
        command = data;
        break;
      default:
        command = COMMAND_EMPTY;
        break;

    }    
  }
  
  if (command == COMMAND_FORWARD){
    if (state_down == HIGH)
      digitalWrite(PIN_FORWARD, HIGH); 
    if (state_up == HIGH){
      digitalWrite(PIN_FORWARD, LOW);
      Serial.println(STATE_UP);
      command = COMMAND_EMPTY;
    }
  }

  if (command == COMMAND_BACKWARD){
    if (state_up == HIGH)
      digitalWrite(PIN_BACKWARD, HIGH);
    if (state_down == HIGH){
      digitalWrite(PIN_BACKWARD, LOW);
      Serial.println(STATE_DOWN);
      command = COMMAND_EMPTY;
    }
  }

  if (command = COMMAND_STOP)
  {
    Serial.println("I");
  }

  // if (state_up == HIGH)
  //   digitalWrite(PIN_FORWARD, HIGH);
  // else
  //   digitalWrite(PIN_FORWARD, LOW);

  // if (state_down == HIGH)
  //   digitalWrite(PIN_BACKWARD, HIGH);
  // else
  //   digitalWrite(PIN_BACKWARD, LOW);
}
