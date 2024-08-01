const int PIN_UP = 12;
const int PIN_DOWN = 2;
const int PIN_FORWARD = 7;
const int PIN_BACKWARD = 4;
const int PIN_AVAILABLE = 13;
const char COMMAND_FORWARD = '1';
const char COMMAND_BACKWARD = '2';
const char COMMAND_STOP = '0';
const char COMMAND_RESET = '3';
const char COMMAND_EMPTY = ' ';
const char STATE_UP = 'u';
const char STATE_DOWN = 'd';
const char STATE_MOVING_FORWARD = 'f';
const char STATE_MOVING_BACKWARD = 'b';
const char STATE_STOPPED = 's';
const char STATE_UNKNOWN = 'x';
const bool DEBUG = true;
const bool DEBUG_ALL = false;
const int DELAY = 100;

char command = COMMAND_EMPTY;
char state = STATE_UNKNOWN;
char  output_message[100];
bool state_changed = false;


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
  int switch_up = digitalRead(PIN_UP);
  int switch_down = digitalRead(PIN_DOWN);
  if (Serial.available())
  {    
    data = Serial.read();

    if (DEBUG && DEBUG_ALL){
      Serial.print("data: ");
      Serial.println(data);    
    }
    
    //read command from serial port
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
      case COMMAND_RESET:
        command = data;
        break;
      default:
        command = COMMAND_EMPTY;
        break;
    }
  }     
  
  //initial state
  if (state == STATE_UNKNOWN)
  {
    if (switch_down == HIGH)
      state = STATE_DOWN;
    else if (switch_up == HIGH)
      state = STATE_UP;
    else
      state = STATE_STOPPED;

    state_changed = true;
  }

  if ((switch_down == HIGH) && (state == STATE_DOWN) && (command == COMMAND_FORWARD))
  {
    digitalWrite(PIN_FORWARD, HIGH);
    state = STATE_MOVING_FORWARD;
    state_changed = true;
  }

  if ((switch_up == HIGH) && (state == STATE_MOVING_FORWARD))
  {
    digitalWrite(PIN_FORWARD, LOW);
    digitalWrite(PIN_BACKWARD, LOW);
    state = STATE_UP;
    state_changed = true;
  }

  if ((switch_up == HIGH) && (state == STATE_UP) && (command == COMMAND_BACKWARD))
  {
    digitalWrite(PIN_BACKWARD, HIGH);
    state = STATE_MOVING_BACKWARD;
    state_changed = true;
  }

  if ((switch_down == HIGH) && (state == STATE_MOVING_BACKWARD))
  {
    digitalWrite(PIN_FORWARD, LOW);
    digitalWrite(PIN_BACKWARD, LOW);
    state = STATE_DOWN;
    state_changed = true;
  }

  if (command == COMMAND_STOP)
  {    
    digitalWrite(PIN_FORWARD, LOW);
    digitalWrite(PIN_BACKWARD, LOW);
    state = STATE_STOPPED;
    state_changed = true;
  }

  if ((state == STATE_STOPPED) && (command == COMMAND_RESET))
  {
    digitalWrite(PIN_FORWARD, LOW);
    digitalWrite(PIN_BACKWARD, HIGH);
    state = STATE_MOVING_BACKWARD;
    state_changed = true;
  }

  

  if (state_changed)
  {
    if (DEBUG)
    {
      Serial.print("\n------\ncommand:");
      Serial.println(command);
      Serial.print("\nstate changed: ");
    }
    Serial.print(state);
    state_changed = false;    
  }

  if (DEBUG && DEBUG_ALL){
    Serial.print("\ndata: ");
    Serial.println(data);
    Serial.print("\ncommand: ");
    Serial.println(command);
    Serial.print("switch up: ");
    Serial.println(switch_up);
    Serial.print("switch down: ");
    Serial.println(switch_down);
    Serial.print("state: ");
    Serial.println(state);
  }

  command = COMMAND_EMPTY;

  delay(DELAY);
}
