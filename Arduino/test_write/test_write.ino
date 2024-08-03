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
const bool DEBUG = false;
const bool DEBUG_ALL = false;
const int DELAY = 0;

char command = COMMAND_EMPTY;
char state = STATE_UNKNOWN;
char  output_message[100];
bool state_changed = false;


void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);    
  
}

void loop() {
  // put your main code here, to run repeatedly:
  char data = ' ';
  
  if (Serial.available())
  {    
    data = Serial.read();

    if (DEBUG && DEBUG_ALL){
      Serial.print("data: ");
      Serial.println(data);   
      Serial.print(" ---") ;
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
    state = STATE_STOPPED;

    state_changed = true;
  }

  
  if ((state == STATE_STOPPED) && (command == COMMAND_RESET))
  {
    state = STATE_MOVING_BACKWARD;  
    state_changed = true;
  }

  if (!state_changed && (state == STATE_MOVING_BACKWARD) && (command == COMMAND_RESET))
  {
    state = STATE_STOPPED;  
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
    Serial.println(state);
    state_changed = false;    
  }

  if (DEBUG && DEBUG_ALL){
    Serial.print("\ndata: ");
    Serial.println(data);
    Serial.print("\ncommand: ");
    Serial.println(command);    
    Serial.print("state: ");
  }

  command = COMMAND_EMPTY;

  delay(DELAY);
}
