// code for Arduino
// #include "pins_controllino.h"
#include "pins_arduino.h"
#include "tester_constants.h"

const bool DEBUG = false;
const bool DEBUG_ALL = false;
const int DELAY = 1;

volatile char command = COMMAND_EMPTY;
volatile char previous_command = COMMAND_EMPTY;
volatile char state = STATE_UNKNOWN;
volatile char previous_state = STATE_UNKNOWN;
volatile char  output_message[100];
volatile bool state_changed = false;


void stop_running()
{
  digitalWrite(PIN_FORWARD, LOW);
  digitalWrite(PIN_BACKWARD, LOW);

  if (command == COMMAND_STOP) {
    state = STATE_STOPPED;
    return;
  }

  int door_opened = !digitalRead(PIN_CLOSED);

  if (door_opened) {
    state = STATE_DOOR_OPEN;
  } else {
    switch (state) {
      case STATE_MOVING_FORWARD:
        state = STATE_UP;
        break;
      case STATE_MOVING_BACKWARD:
        state = STATE_DOWN;
        break;
      default:
        state = STATE_STOPPED;
        break;
    }    
  }
}

char read_data() {
  if (Serial.available() > 0) {
    return Serial.read();
  }
  return COMMAND_EMPTY;
}

void send_state(char state)
{
  Serial.println(state);
}

void move_forward()
{
  digitalWrite(PIN_FORWARD, HIGH);
  digitalWrite(PIN_BACKWARD, LOW);
  state = STATE_MOVING_FORWARD;
}

void move_backward()
{
  digitalWrite(PIN_FORWARD, LOW);
  digitalWrite(PIN_BACKWARD, HIGH);
  state = STATE_MOVING_BACKWARD;
}

char get_command(char data, char previous_command, char state, char previous_state) {
  // Implement your command parsing logic here
  // For now, just return the data as the command
  return previous_command;
}

void perform_command(char command, char state)
{
  switch(command)
  {
    case COMMAND_STOP:
      stop_running();
      break;
    case COMMAND_FORWARD:
      move_forward();
      break;
    case COMMAND_BACKWARD:
    case COMMAND_RESET:
      move_backward();
      break;    
    case COMMAND_GET_STATE:
      send_state(state);
      break;
    default:
      break;
  }
}

void setup() {
  String portContent = "";
  Serial.begin(9600);    
  portContent = Serial.readString();

  pinMode(PIN_UP, INPUT); 
  pinMode(PIN_DOWN, INPUT);
  pinMode(PIN_CLOSED, INPUT);
  pinMode(PIN_FORWARD, OUTPUT);
  pinMode(PIN_BACKWARD, OUTPUT);

  attachInterrupt(digitalPinToInterrupt(PIN_UP), stop_running, FALLING);
  attachInterrupt(digitalPinToInterrupt(PIN_SWITCH), stop_running, RISING);
  previous_state = state;

  stop_running();
  
}

void loop() {
  char data = read_data();
  command = get_command(data, previous_command, state, previous_state);

  if (command != previous_command) {
    perform_command(command, state);
  }

  if (state != previous_state) {
    send_state(state);
  }

  previous_state = state;
  previous_command = command;
}


