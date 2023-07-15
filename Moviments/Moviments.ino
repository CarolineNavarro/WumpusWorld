#include <SoftwareSerial.h>

SoftwareSerial mySerial(6, 5); // ports RX, TX 

int time = 500; // time for the engine 

const int PIN_RED = 7; //led RGB ports 
const int PIN_GREEN = 4;
const int PIN_BLUE  = 10;

int a_MotD=13, //entry for the engines a(right) and b(left)
    a_MotE=12,
    b_MotD=8, 
    b_MotE=2; 

void setup()
{
  mySerial.begin(9600); // initialize bluetooth serial communication

  pinMode(a_MotD, OUTPUT); //initialize engines
  pinMode(a_MotE, OUTPUT);
  pinMode(b_MotD, OUTPUT);
  pinMode(b_MotE, OUTPUT);

  pinMode(PIN_RED, OUTPUT); //initialize led rgb
  pinMode(PIN_GREEN, OUTPUT);
  pinMode(PIN_BLUE,  OUTPUT);

}

void loop() {
  
  serialEvent();    
  delay(1000);    //give a delay of 1 ms not to overload the communication with the unity

}

void serialEvent() {
  
  //if the bluetooth serial is available
  if(mySerial.available() > 0)
  {
    char character = mySerial.read(); //get unity variable 

    //depending on the letter will make a movement of the engine and led
    if(character == 'a'){  
      digitalWrite(LED_BUILTIN, HIGH); 

      digitalWrite(a_MotD, 1); //Moviment engine forward 
      digitalWrite(a_MotE, 0);
      digitalWrite(b_MotD, 1);
      digitalWrite(b_MotE, 0);
      
      delay(time);         //time to keep the motor spinning
      StopMovement();     //stop the engine
    }
    if(character == 'b'){
      digitalWrite(LED_BUILTIN,LOW);

      digitalWrite(a_MotD, 0); //Moviment engine back 
      digitalWrite(a_MotE, 1);
      digitalWrite(b_MotD, 0);
      digitalWrite(b_MotE, 1);

      delay(time);      
      StopMovement();  
    }
    if(character == 'c'){ 
     
      digitalWrite(a_MotD, 1); //rotation engine right
      digitalWrite(a_MotE, 0);
      digitalWrite(b_MotD, 0);
      digitalWrite(b_MotE, 1);
      
      delay(time);         
      StopMovement();     
    }
    if(character == 'd'){

      digitalWrite(a_MotD, 0); //rotation engine left
      digitalWrite(a_MotE, 1);
      digitalWrite(b_MotD, 1);
      digitalWrite(b_MotE, 0);

      delay(time);        
      StopMovement();    
    }

    if(character == 'v'){ //moviment to simulate de wind
      
      setColor(0, 102, 255); //set blue color to the Led

      digitalWrite(a_MotD, 0); //Moviment engine back 
      digitalWrite(a_MotE, 1);
      digitalWrite(b_MotD, 0);
      digitalWrite(b_MotE, 1);

      delay(80); 

      digitalWrite(a_MotD, 1); //Moviment engine foward 
      digitalWrite(a_MotE, 0);
      digitalWrite(b_MotD, 1);
      digitalWrite(b_MotE, 0);

      delay(80);

      digitalWrite(a_MotD, 0); //Moviment engine back 
      digitalWrite(a_MotE, 1);
      digitalWrite(b_MotD, 0);
      digitalWrite(b_MotE, 1);

      delay(80);

      digitalWrite(a_MotD, 1); //Moviment engine foward 
      digitalWrite(a_MotE, 0);
      digitalWrite(b_MotD, 1);
      digitalWrite(b_MotE, 0);

      delay(80);

      digitalWrite(a_MotD, 0); //Moviment engine back 
      digitalWrite(a_MotE, 1);
      digitalWrite(b_MotD, 0);
      digitalWrite(b_MotE, 1);
      
      delay(80);

      digitalWrite(a_MotD, 1); //Moviment engine foward 
      digitalWrite(a_MotE, 0);
      digitalWrite(b_MotD, 1);
      digitalWrite(b_MotE, 0);

      delay(80);

      digitalWrite(a_MotD, 0); //Moviment engine back  
      digitalWrite(a_MotE, 1);
      digitalWrite(b_MotD, 0);
      digitalWrite(b_MotE, 1);

      delay(80);

      digitalWrite(a_MotD, 1); //Moviment engine foward 
      digitalWrite(a_MotE, 0);
      digitalWrite(b_MotD, 1);
      digitalWrite(b_MotE, 0);

      StopMovement();   //stop movement
    }

    if(character == 'l'){       //set color red
      setColor(255, 0, 0);
    }
    if(character == 'p'){      //set color green
      setColor(0, 204, 0);
    }
    if(character == 'u'){
      setColor(255, 255, 0);  //set color yellow
    }
  }
}

void setColor(int R, int G, int B) { //receive three integers to simulate the r,g,b channels
  
  analogWrite(PIN_RED, R); //write in the LED 
  analogWrite(PIN_GREEN, G);
  analogWrite(PIN_BLUE, B);

}

void StopMovement(){

  digitalWrite(a_MotD, 0); //Stop movement engines
  digitalWrite(a_MotE, 0);
  digitalWrite(b_MotD, 0);
  digitalWrite(b_MotE, 0);  

}
