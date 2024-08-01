
const int N = 3;
const int DELAY = 1000;
String commands[N] = {"A", "B", "C"};
int i = 0;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);  
}

void loop() {
  // put your main code here, to run repeatedly:
  Serial.print(commands[i]);

  if (i == (N - 1))
    i = 0;
  else
    i++;

  delay(DELAY);
}
