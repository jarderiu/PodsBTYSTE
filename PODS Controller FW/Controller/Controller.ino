/****************************************************************************
 ****************************************************************************
 *
 *  PODS Controller Firmware 
 *  Version: v0.9 (September 2017)
 *  Developer: Sean.Wilson@analog.com
 *  
 *  If assembling a new controller board from scratch the you will first
 *  need to burn the Arduino UNO bootloader before uploading this FW. This
 *  only needs to be done one time when the board is first assembled.
 *    Instructions here: https://www.arduino.cc/en/Tutorial/ArduinoISP
 *
 ****************************************************************************
 ****************************************************************************/

#include <Wire.h>
#include "PCA9685.h"
#include "TimerOne.h"

#define STATUS_LED 8
#define LEDS_ENABLE 3

#define RED 0
#define GREEN 1
#define BLUE 2

#define CONTROLLER1_ADDRESS B10100
#define CONTROLLER2_ADDRESS B10101
#define MAX_COMMAND_SIZE 30
#define COMMAND_SEPERATOR ":"

enum state {INIT, READ_COMMAND, SET_COLOUR, SET_BRIGHTNESS, UPDATE_LEDS, READ_SW, UNHANDLED};

void updateLeds(int, int, int, int);
bool pulseLedOnce = false; 
bool pulseLedCont = false; 
char switches[9] = {5,6,7,9,10,15,16,17,2};
PCA9685 pwmController1;                 
PCA9685 pwmController2;                 

void setup() {
    int i;
    
    Serial.begin(115200);

    Wire.begin();                     // Wire must be started first
    Wire.setClock(400000);            // Supported baud rates are 100kHz, 400kHz, and 1000kHz

    pinMode(STATUS_LED, OUTPUT);      
    pinMode(LEDS_ENABLE, OUTPUT);     

    for(i=0; i<9; i++){
      pinMode(switches[i], INPUT);
    }
    
    digitalWrite(STATUS_LED, HIGH);   // Enable the STATUS_LED to show fw is running
    digitalWrite(LEDS_ENABLE, LOW);   // Pull LEDS_ENABLE low to start the PWM controllers

    pwmController1.resetDevices();       // Software resets all PCA9685 devices on Wire line

    pwmController1.init(CONTROLLER1_ADDRESS);        // Address pins A5-A0 set to B000000
    pwmController1.setPWMFrequency(100); // Default is 200Hz, supports 24Hz to 1526Hz
    pwmController2.init(CONTROLLER2_ADDRESS);        // Address pins A5-A0 set to B000000
    pwmController2.setPWMFrequency(100); // Default is 200Hz, supports 24Hz to 1526Hz

    Timer1.initialize(120000);         // initialize timer1, and set a 120mS period
    Timer1.attachInterrupt(ledCallback);  // attaches callback() as a timer overflow interrupt
}

void loop() {
  enum state nextState = INIT;
  char nextCommand[MAX_COMMAND_SIZE];
  char ledState[9][4];
  char LedBrightness = 16;

  int i,j;

  while(1){
    switch(nextState){

      case INIT:
        post();
        for(i=0;i<9;i++){
          for(j=0;j<3;j++){
            ledState[i][j] = 0;
          }
        }
        nextState = UPDATE_LEDS;
        break;
      
      case READ_COMMAND:  
        readCommand(nextCommand);
        nextState = getNextState(nextCommand);
        break;
    
      case SET_COLOUR:
        parseColourCmd(nextCommand, ledState);
        nextState = UPDATE_LEDS;
        break;

      case SET_BRIGHTNESS:
        LedBrightness = parseBrightnessCmd(nextCommand);
        nextState = UPDATE_LEDS;
        break;

     case UPDATE_LEDS:
        updateLeds(ledState, LedBrightness);
        nextState = READ_COMMAND;
        break;

     case READ_SW:
        char switchVal;
        Serial.print("\nSW:");
        for(i=0;i<9; i++){
          switchVal = digitalRead(switches[i]) ? 'T' : 'F';
          Serial.print(switchVal);
        }
        nextState = READ_COMMAND;
        break; 

     case UNHANDLED:
       Serial.print("\nUnhandled State: ");
       Serial.print(nextCommand);
       nextState = READ_COMMAND;
       break; 
      
    }
  }
  
}

/*********************************************
 *   updateLeds
 *   Updates the PWM values on each contollers to match required LED state
 *      
 *   PWM signals are pinned our in the following order
 *      * Channel8 -> Channel0
 *      * RED -> GREEN -> BLUE
 *      * U4 covers Channel8 -> Channel 4 
 *      * U3 covers Channel3 -> Channel0
 *********************************************/
void updateLeds(char ledState[][4], char brightness){
  word pwms[27];
  unsigned int i = 0; //loopcounter

  // generate a single dimention array of PWM values
  for(i = 0 ; i < 9; i++){
    int redPwmChan = RED + (i * 3);
    int greenPwmChan = GREEN + (i * 3);
    int bluePwmChan = BLUE + (i * 3);
    pwms[redPwmChan] = ledState[8-i][0]*brightness;
    pwms[greenPwmChan] = ledState[8-i][1]*brightness;
    pwms[bluePwmChan] = ledState[8-i][2]*brightness;
  
  }

  // Assign PWM values to channels on U3 & U4
  pwmController1.setChannelsPWM(0,12,&pwms[15]);
  pwmController2.setChannelsPWM(0,15,pwms);
}

/*********************************************
 *   parseBrightnessCmd
 *   parses the command that follows "SET COLOUR"
 *   updates the LED State accordingly
 *      
 *   expected command format: 
 *   "SET_COLOUR:{CHANNEL}:{HEX COLOUR VALUE}"
 *********************************************/
char parseBrightnessCmd(char* command){
  char *strPointer;
  char * e;

  // find pos of COMMAND_SEPERATOR in the command string
  strPointer = strstr(command, COMMAND_SEPERATOR);

  //increment pointer to the start of the brightness value
  strPointer++; 
  return (char)strtol(strPointer, &e, 0);
}

/*********************************************
 *   parseColourCmd
 *   parses the command that follows "SET COLOUR"
 *   updates the LED State accordingly
 *      
 *   expected command format: 
 *   "SET_COLOUR:{CHANNEL}:{HEX COLOUR VALUE}"
 *   Note: Writing to channel 9 will apply to all channels
 *********************************************/
void parseColourCmd(char* command, char ledState[][4]){
  int i;
  char *strPointer;
  char channelStr[2];
  char * e;

  // find pos of COMMAND_SEPERATOR in the command string
  strPointer = strstr(command, COMMAND_SEPERATOR);
  
  //increment pointer to the start of the channel value
  strPointer++;
  channelStr[0] = *strPointer;
  channelStr[1] = '\0';
  int channel = (int)strtol(channelStr, &e, 0);

  //increment pointer to the start of the hex colour value
  strPointer += 2; 
  int colour = (int)strtol(strPointer, &e, 0);
  if(channel == 9){
    for(i=0;i<9;i++){
      ledState[i][0] = (char)((colour & 0xF00) >> 8);
      ledState[i][1] = (char)((colour & 0x0F0) >> 4);
      ledState[i][2] = (char)(colour & 0x00F);
    }
  }else{
    ledState[channel][0] = (char)((colour & 0xF00) >> 8);
    ledState[channel][1] = (char)((colour & 0x0F0) >> 4);
    ledState[channel][2] = (char)(colour & 0x00F);
  }
}

/*********************************************
 *   getNextState
 *   parses the command to find the next state to jump to
 *      
 *   expected command format: 
 *   "{COMMAND}:{SOME_SPECIFIC_COMMAND_DATA}"
 *   any unhandled command will jump to the UNHANDLED state
 *********************************************/

enum state getNextState(char* command){
  enum state nextState = UNHANDLED;
  
  if(strstr(command, "SET_COLOUR") != 0 ){
    nextState = SET_COLOUR;
  }

  if(strstr(command, "SET_BRIGHTNESS") != 0 ){
    nextState = SET_BRIGHTNESS;
  }

   if(strstr(command, "READ_SW") != 0 ){
    nextState = READ_SW;
  }
  return nextState;
}

/*********************************************
 *   readCommand
 *   reads the next command back from UART
 *   commands end in a carriage return
 *      
 *   expected command format: 
 *   "{COMMAND}:{SOME_SPECIFIC_COMMAND_DATA}\r"
 *********************************************/

void readCommand(char* nextCommand){
  int i=0;
  Serial.write("\n>");
  while(nextCommand[i-1] != '\r'){
    while(Serial.available() == 0);
    nextCommand[i] = Serial.read();
    i++;
  }
  nextCommand[i]='\0';
  pulseLedOnce=true;
}


/*********************************************
 *   post
 *   executes a power on self test
 *     
 *   post lasts 4 seconds
 *   status should flash continuously throughout
 *   All led strips should flash Red, then green, then blue
 *********************************************/
void post(){
  int i,j;
  char testLedState[9][4];
  pulseLedCont=true;
  
  // illuminate Red, Green, and blue on all channels with 1 Second delay between each
  for(i=0;i<4;i++){ 
    for(j=0;j<9;j++){
      testLedState[j][0] = 0;  
      testLedState[j][1] = 0;  
      testLedState[j][2] = 0;  
      testLedState[j][i] = 0xf;  
    }
    updateLeds(testLedState, 255);
    delay(1000);
  }
  pulseLedCont=false;
}

void ledCallback()
{
  if(pulseLedCont){
    digitalWrite(STATUS_LED, digitalRead(STATUS_LED) ^ 1);
  } else {
    digitalWrite(STATUS_LED, !pulseLedOnce);
    pulseLedOnce = false;
  }
}


