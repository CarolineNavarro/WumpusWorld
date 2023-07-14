#include <SoftwareSerial.h>

SoftwareSerial mySerial(6, 5); // Entradas RX, TX do arduino

int time = 500; // tempo que o motor vai ficar rodando

const int PIN_RED = 7; //led RGB portas 
const int PIN_GREEN = 4;
const int PIN_BLUE  = 10;

int a_MotD=13, //entradas motores a(direita) e b(esquerda)
    a_MotE=12,
    b_MotD=8, 
    b_MotE=2; 

void setup()
{
  mySerial.begin(9600); // inicializar comunicaçao serial do bluetooth

  pinMode(a_MotD, OUTPUT); //inicializa os motores
  pinMode(a_MotE, OUTPUT);
  pinMode(b_MotD, OUTPUT);
  pinMode(b_MotE, OUTPUT);

  pinMode(PIN_RED, OUTPUT); //inicializa a led rgb
  pinMode(PIN_GREEN, OUTPUT);
  pinMode(PIN_BLUE,  OUTPUT);

}

void loop() {
  
  serialEvent();    //chama em update o metodo 
  delay(1000);    //dar um delay de 1 ms para nao sobrecarregar a comunicação com a unity

}

void serialEvent() {
  
  //se o serial do bluetooth esta disponivel
  if(mySerial.available() > 0)
  {
    char character = mySerial.read(); //recebe varialvel da unity 

    // dependendo da letra vai fazer um movimento do motor e led
    if(character == 'a'){  
      digitalWrite(LED_BUILTIN, HIGH); 

      digitalWrite(a_MotD, 1); //Movimento motor frente 
      digitalWrite(a_MotE, 0);
      digitalWrite(b_MotD, 1);
      digitalWrite(b_MotE, 0);
      
      delay(time);         //tempo para manter o motor girando 
      StopMovement();     //chama metodo para parar os motores
    }
    if(character == 'b'){
      digitalWrite(LED_BUILTIN,LOW);

      digitalWrite(a_MotD, 0); //Movimento motor re 
      digitalWrite(a_MotE, 1);
      digitalWrite(b_MotD, 0);
      digitalWrite(b_MotE, 1);

      delay(time);      //tempo para manter o motor girando 
      StopMovement();  //chama metodo para parar os motores
    }
    if(character == 'c'){ 
     
      digitalWrite(a_MotD, 1); //rotação motor direita
      digitalWrite(a_MotE, 0);
      digitalWrite(b_MotD, 0);
      digitalWrite(b_MotE, 1);
      
      delay(time);         //tempo para manter o motor girando 
      StopMovement();     //chama metodo para parar os motores
    }
    if(character == 'd'){

      digitalWrite(a_MotD, 0); //rotação motor esquerda
      digitalWrite(a_MotE, 1);
      digitalWrite(b_MotD, 1);
      digitalWrite(b_MotE, 0);

      delay(time);        //tempo para manter o motor girando 
      StopMovement();    //chama metodo para parar os motores
    }

    if(character == 'v'){ //movimento para simular o vento 
      
      setColor(0, 102, 255); //vai setar cor azul na led

      digitalWrite(a_MotD, 0); //Movimento motor re 
      digitalWrite(a_MotE, 1);
      digitalWrite(b_MotD, 0);
      digitalWrite(b_MotE, 1);

      delay(80); 

      digitalWrite(a_MotD, 1); //Movimento motor frente 
      digitalWrite(a_MotE, 0);
      digitalWrite(b_MotD, 1);
      digitalWrite(b_MotE, 0);

      delay(80);

      digitalWrite(a_MotD, 0); //Movimento motor re 
      digitalWrite(a_MotE, 1);
      digitalWrite(b_MotD, 0);
      digitalWrite(b_MotE, 1);

      delay(80);

      digitalWrite(a_MotD, 1); //Movimento motor frente 
      digitalWrite(a_MotE, 0);
      digitalWrite(b_MotD, 1);
      digitalWrite(b_MotE, 0);

      delay(80);

      digitalWrite(a_MotD, 0); //Movimento motor re 
      digitalWrite(a_MotE, 1);
      digitalWrite(b_MotD, 0);
      digitalWrite(b_MotE, 1);
      
      delay(80);

      digitalWrite(a_MotD, 1); //Movimento motor frente 
      digitalWrite(a_MotE, 0);
      digitalWrite(b_MotD, 1);
      digitalWrite(b_MotE, 0);

      delay(80);

      digitalWrite(a_MotD, 0); //Movimento motor re 
      digitalWrite(a_MotE, 1);
      digitalWrite(b_MotD, 0);
      digitalWrite(b_MotE, 1);

      delay(80);

      digitalWrite(a_MotD, 1); //Movimento motor frente 
      digitalWrite(a_MotE, 0);
      digitalWrite(b_MotD, 1);
      digitalWrite(b_MotE, 0);

      StopMovement();   //para movimento motores
    }

    if(character == 'l'){       //setar a cor para vermelho
      setColor(255, 0, 0);
    }
    if(character == 'p'){      //setar a cor para verde
      setColor(0, 204, 0);
    }
    if(character == 'u'){
      setColor(255, 255, 0);  //setar a cor para amarelo
    }
  }
}

void setColor(int R, int G, int B) { //recebe tres inteiros para colocar nos canais RGB
  
  analogWrite(PIN_RED, R); //escreve de forma analogica no LED 
  analogWrite(PIN_GREEN, G);
  analogWrite(PIN_BLUE, B);

}

void StopMovement(){

  digitalWrite(a_MotD, 0); //Para o movimento dos motores
  digitalWrite(a_MotE, 0);
  digitalWrite(b_MotD, 0);
  digitalWrite(b_MotE, 0);  

}
