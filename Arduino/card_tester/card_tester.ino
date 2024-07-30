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
char command = COMMAND_EMPTY;
char  output_message[100];

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
  Serial.println("-------");
  char data = ' ';
  int state_up = digitalRead(PIN_UP);
  int state_down = digitalRead(PIN_DOWN);
  if (Serial.available())
  {    

    data = Serial.read();

    Serial.print("data: ");
    Serial.println(data);    
    
    switch(data){
      case COMMAND_FORWARD:
        command = data;
        Serial.println("set FORWARD");
        break;       
      case COMMAND_BACKWARD:
        command = data;
        Serial.println("set BACKWARD");
        break;        
      case COMMAND_STOP:
        command = data;
        Serial.println("set STOP");
        break;
      default:
        command = COMMAND_EMPTY;
        Serial.println("set EMPTY");
        break;
    }
  }   

  Serial.print("command: ");
  Serial.println(command);
  Serial.print("state up: ");
  Serial.println(state_up);
  Serial.print("state down: ");
  Serial.println(state_down);
  
  if (command == COMMAND_FORWARD){
    Serial.println("FORWARD");
    if (state_down == HIGH)
      digitalWrite(PIN_FORWARD, HIGH); 
    if (state_up == HIGH){
      digitalWrite(PIN_FORWARD, LOW);
      Serial.println(state_up);
      command = COMMAND_EMPTY;
    }
  }

  if (command == COMMAND_BACKWARD){
    Serial.println("BACKWARD");
    if (state_up == HIGH)
      digitalWrite(PIN_BACKWARD, HIGH);
    if (state_down == HIGH){
      digitalWrite(PIN_BACKWARD, LOW);
      Serial.println(state_down);
      command = COMMAND_EMPTY;
    }
  }

  if (command == COMMAND_STOP)
  {
    Serial.println("STOP");
    digitalWrite(PIN_FORWARD, LOW);
    digitalWrite(PIN_BACKWARD, LOW);
  }

  // if (state_up == HIGH)
  //   digitalWrite(PIN_FORWARD, HIGH);
  // else
  //   digitalWrite(PIN_FORWARD, LOW);

  // if (state_down == HIGH)
  //   digitalWrite(PIN_BACKWARD, HIGH);
  // else
  //   digitalWrite(PIN_BACKWARD, LOW);
  delay(100);
}
