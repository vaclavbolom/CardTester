//Code for Controllino, same as card_tester.ino, pin numbers set with Controllino constants
#include <Controllino.h>

const int PIN_UP = CONTROLLINO_A0;
const int PIN_DOWN = CONTROLLINO_A1;
const int PIN_FORWARD = CONTROLLINO_D2;
const int PIN_BACKWARD = CONTROLLINO_D0;
const int PIN_CLOSED = CONTROLLINO_A2;
const char COMMAND_FORWARD = '1';
const char COMMAND_BACKWARD = '2';
const char COMMAND_STOP = '0';
const char COMMAND_RESET = '3';
const char COMMAND_RESET_UP = '5';
const char COMMAND_GET_STATE = '4';
const char COMMAND_EMPTY = ' ';
const char STATE_UP = 'u';
const char STATE_DOWN = 'd';
const char STATE_MOVING_FORWARD = 'f';
const char STATE_MOVING_BACKWARD = 'b';
const char STATE_STOPPED = 's';
const char STATE_DOOR_OPEN = 'o';
const char STATE_UNKNOWN = 'x';
const bool DEBUG = false;
const bool DEBUG_ALL = false;
const int DELAY = 1;

char command = COMMAND_EMPTY;
char state = STATE_UNKNOWN;
char  output_message[100];
bool state_changed = false;


void setup() {
  String portContent = "";
  Serial.begin(9600);    
  portContent = Serial.readString();

  pinMode(PIN_UP, INPUT); 
  pinMode(PIN_DOWN, INPUT);
  pinMode(PIN_CLOSED, INPUT);
  pinMode(PIN_FORWARD, OUTPUT);
  pinMode(PIN_BACKWARD, OUTPUT);

  if (DEBUG)
  {
    digitalWrite(PIN_FORWARD, HIGH);
    digitalWrite(PIN_BACKWARD, HIGH);
    delay(1000);
    digitalWrite(PIN_FORWARD, LOW);
    digitalWrite(PIN_BACKWARD, LOW);    
  }
}

void loop() {
  // put your main code here, to run repeatedly:
  char data = ' ';
  int switch_up = digitalRead(PIN_UP);
  int switch_down = digitalRead(PIN_DOWN);
  int switch_closed = !digitalRead(PIN_CLOSED);

  if (DEBUG){      
      Serial.println("---- LOOP ----");
      Serial.print("switch closed: ");
      Serial.println(switch_closed);
  }
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
      case COMMAND_BACKWARD:        
      case COMMAND_STOP:       
      case COMMAND_RESET:    
      case COMMAND_RESET_UP:  
      case COMMAND_GET_STATE:
        command = data;
        break;
      default:
        command = COMMAND_EMPTY;
        break;
    }
   
  }    
  
  if (!switch_closed)
  {
    if (command != COMMAND_GET_STATE)
      command = COMMAND_STOP;
    
    if (DEBUG)
      Serial.println("--force closed");
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

  if (command == COMMAND_GET_STATE)
  {
    state_changed = true;
  }

  if (state == STATE_DOWN && !switch_down)
  {
    state = STATE_STOPPED;
    state_changed = true;
  }

  if (state == STATE_UP && !switch_up){
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
    delay(200);
    digitalWrite(PIN_FORWARD, LOW);
    digitalWrite(PIN_BACKWARD, LOW);
    state = STATE_DOWN;
    state_changed = true;
  }

  if ((command == COMMAND_STOP) && (state != STATE_STOPPED) && (state != STATE_DOOR_OPEN))
  {    
    digitalWrite(PIN_FORWARD, LOW);
    digitalWrite(PIN_BACKWARD, LOW);
    state = switch_closed ? STATE_STOPPED : STATE_DOOR_OPEN;
    state_changed = true;
  }

  if ((state == STATE_DOOR_OPEN) && switch_closed)
  {
    state = STATE_STOPPED;
    state_changed = true;
  }

  // reset down
  if (IsStopped(state) && (command == COMMAND_RESET))
  {
    digitalWrite(PIN_FORWARD, LOW);
    digitalWrite(PIN_BACKWARD, HIGH);
    state = STATE_MOVING_BACKWARD;
    state_changed = true;
  }


  //reset up
  if (IsStopped(state) && (command == COMMAND_RESET_UP))
  {
    digitalWrite(PIN_BACKWARD, LOW);
    digitalWrite(PIN_FORWARD, HIGH);
    state = STATE_MOVING_FORWARD;
    state_changed = true;
  }

  

  if (state_changed)
  {
    if (DEBUG)
    {
      Serial.print("\n---\ncommand:");
      Serial.println(command);
      Serial.print("\nstate changed: ");
    }
    Serial.println(state);
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
    Serial.print("switch closed: ");
    Serial.println(switch_closed);
    Serial.print("state: ");
    Serial.println(state);
  }

  command = COMMAND_EMPTY;

  delay(DELAY);
}

bool IsStopped(char state)
{
  switch(state)
  {
    case STATE_DOWN:
    case STATE_UP:
    case STATE_STOPPED:
      return true;
    default:
      return false;
  }
  
}
