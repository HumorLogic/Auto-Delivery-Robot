#include <Servo.h>
#include <NewPing.h>

#define TRIGGER_PIN  30  // Arduino pin tied to trigger pin on the ultrasonic sensor.
#define ECHO_PIN     31  // Arduino pin tied to echo pin on the ultrasonic sensor.
#define MAX_DISTANCE 200

Servo myservo;
const int LedPin = 13;

//Front-left motorB pin
const int motorB_PWM_pin = 4;
const int motorB_IN1_pin = 26;
const int motorB_IN2_pin = 27;

//Front-right motorA pin
const int motorA_PWM_pin = 5;
const int motorA_IN3_pin = 28;
const int motorA_IN4_pin = 29;


//Right-back motorD pin
const int motorD_PWM_pin = 2;
const int motorD_IN1_pin = 22;
const int motorD_IN2_pin = 23;

//Left-back motorD pin
const int motorC_PWM_pin = 3;
const int motorC_IN3_pin = 24;
const int motorC_IN4_pin = 25;

int ledState = 0;
int motorState = 3;
bool isForward = false;
bool isBackward = false;

char inDir = 'S';
int inSpeed = 0;
int steer = 90;

void setup()
{
    Serial1.begin(115200);
    Serial.begin(9600);
    myservo.attach(9);
    myservo.write(steer);
    pinMode(LedPin, OUTPUT);
    

    pinMode(motorA_IN3_pin, OUTPUT);
    pinMode(motorA_IN4_pin, OUTPUT);
    pinMode(motorA_PWM_pin, OUTPUT);

    pinMode(motorB_IN1_pin, OUTPUT);
    pinMode(motorB_IN2_pin, OUTPUT);
    pinMode(motorB_PWM_pin, OUTPUT);

    pinMode(motorC_IN3_pin, OUTPUT);
    pinMode(motorC_IN4_pin, OUTPUT);
    pinMode(motorC_PWM_pin, OUTPUT);

    pinMode(motorD_IN1_pin, OUTPUT);
    pinMode(motorD_IN2_pin, OUTPUT);
    pinMode(motorD_PWM_pin, OUTPUT);

}

void loop()
{
   
    
    if (Serial1.available() > 2)
    {
        inDir = Serial1.read();
        delay(5);
        //Serial.println(inDir);
        inSpeed = Serial1.read();
        delay(5);
        //Serial.println(inSpeed);
        steer = Serial1.read();
        delay(5);
         //Serial.println(steer);
       
    }

    digitalWrite(LedPin, ledState);
  
    drive_motor(inDir,inSpeed);
    set_direction(steer);
    
    //delay(50);
    //delay(50);
   // Serial.println(inSpeed);
}

void drive_motor(char dir,int mSpeed)
{
  switch(dir){
    case 'F':
        motor_forward_test(mSpeed);
        ledState = 1;
        break;
    case 'B':
        motor_backward(mSpeed);
        ledState = 0 ;
        break;
    case 'L':
        motor_Left(mSpeed);
        break;
    case 'R':
        motor_Right(mSpeed);
        break;
    case 'S':
        motor_brake();
        break;
    default:
        motor_brake();
       
      

        
    

    }
  
//    if(state == 0)
//        motor_brake();  
//    else if (state == 1)
//        motor_forward();
//    else if (state == 2)
//        motor_backward();
//    else if (state == 3)
//        motor_neutral(); 
}


void motor_forward_test(int mspeed)
{
    if(isBackward){
        motor_brake();
        delay(1000);
        isForward = true;
        isBackward = false;
    }

    digitalWrite(motorB_IN1_pin, HIGH);
    digitalWrite(motorB_IN2_pin, LOW);
    analogWrite(motorB_PWM_pin, mspeed);
    
    digitalWrite(motorC_IN3_pin, HIGH);
    digitalWrite(motorC_IN4_pin, LOW);
    analogWrite(motorC_PWM_pin, mspeed);

    digitalWrite(motorD_IN1_pin, HIGH);
    digitalWrite(motorD_IN2_pin, LOW);
    analogWrite(motorD_PWM_pin, mspeed);

    digitalWrite(motorA_IN3_pin, HIGH);
    digitalWrite(motorA_IN4_pin, LOW);
    analogWrite(motorA_PWM_pin, mspeed);

    
}
void set_direction(int steer){
  myservo.write(steer);
  }

// Turn left
void motor_Left(int mSpeed)
{
   myservo.write(130);
    digitalWrite(motorC_IN3_pin, HIGH);
    digitalWrite(motorC_IN4_pin, LOW);
    analogWrite(motorC_PWM_pin, mSpeed);

    digitalWrite(motorA_IN3_pin, HIGH);
    digitalWrite(motorA_IN4_pin, LOW);
    analogWrite(motorA_PWM_pin, mSpeed);
    

    digitalWrite(motorD_IN1_pin, HIGH);
    digitalWrite(motorD_IN2_pin, 0);
    analogWrite(motorD_PWM_pin, mSpeed);

    

    digitalWrite(motorB_IN1_pin, HIGH);
    digitalWrite(motorB_IN2_pin, 0);
    analogWrite(motorB_PWM_pin, mSpeed);

//    delay(2000);
//    inDir='S';
}

// Turn Right
void motor_Right(int mSpeed){

    myservo.write(50);
    digitalWrite(motorC_IN3_pin, HIGH);
    digitalWrite(motorC_IN4_pin, 0);
    analogWrite(motorC_PWM_pin, mSpeed);

    digitalWrite(motorD_IN1_pin, HIGH);
    digitalWrite(motorD_IN2_pin, LOW);
    analogWrite(motorD_PWM_pin, mSpeed);

    digitalWrite(motorA_IN3_pin, HIGH);
    digitalWrite(motorA_IN4_pin, 0);
    analogWrite(motorA_PWM_pin, mSpeed);

    digitalWrite(motorB_IN1_pin, HIGH);
    digitalWrite(motorB_IN2_pin, LOW);
    analogWrite(motorB_PWM_pin, mSpeed);

//    delay(2000);
//    inDir='S';
}

//电机向前
void motor_forward()
{
    digitalWrite(motorC_IN3_pin, HIGH);
    digitalWrite(motorC_IN4_pin, LOW);
    analogWrite(motorC_PWM_pin, 100);

    digitalWrite(motorD_IN1_pin, HIGH);
    digitalWrite(motorD_IN2_pin, LOW);
    analogWrite(motorD_PWM_pin, 100);

    digitalWrite(motorA_IN3_pin, HIGH);
    digitalWrite(motorA_IN4_pin, LOW);
    analogWrite(motorA_PWM_pin, 100);

    digitalWrite(motorB_IN1_pin, HIGH);
    digitalWrite(motorB_IN2_pin, LOW);
    analogWrite(motorB_PWM_pin, 100);
}

void motor_backward(int mspeed)
{
    if(isForward){
        motor_brake();
        delay(1000);
        isBackward = true;
        isForward = false;
    }
        

    digitalWrite(motorC_IN3_pin, LOW);
    digitalWrite(motorC_IN4_pin, HIGH);
    analogWrite(motorC_PWM_pin, mspeed);

    digitalWrite(motorD_IN1_pin, LOW);
    digitalWrite(motorD_IN2_pin, HIGH);
    analogWrite(motorD_PWM_pin, mspeed);

    digitalWrite(motorA_IN3_pin, LOW);
    digitalWrite(motorA_IN4_pin, HIGH);
    analogWrite(motorA_PWM_pin, mspeed);

    digitalWrite(motorB_IN1_pin, LOW);
    digitalWrite(motorB_IN2_pin, HIGH);
    analogWrite(motorB_PWM_pin, mspeed);
}

void motor_brake()
{
    isForward = false;
    isBackward =false;

    digitalWrite(motorC_IN3_pin, LOW);
    digitalWrite(motorC_IN4_pin, LOW);
    analogWrite(motorC_PWM_pin, 0);

    digitalWrite(motorD_IN1_pin, LOW);
    digitalWrite(motorD_IN2_pin, LOW);
    analogWrite(motorD_PWM_pin, 0);

    digitalWrite(motorA_IN3_pin, LOW);
    digitalWrite(motorA_IN4_pin, LOW);
    analogWrite(motorA_PWM_pin, 0);

    digitalWrite(motorB_IN1_pin, LOW);
    digitalWrite(motorB_IN2_pin, LOW);
    analogWrite(motorB_PWM_pin, 0);
}

void motor_neutral()
{
    digitalWrite(motorC_IN3_pin, HIGH);
    digitalWrite(motorC_IN4_pin, HIGH);
    analogWrite(motorC_PWM_pin, 0);

    digitalWrite(motorD_IN1_pin, HIGH);
    digitalWrite(motorD_IN2_pin, HIGH);
    analogWrite(motorD_PWM_pin, 0);

    digitalWrite(motorA_IN3_pin, HIGH);
    digitalWrite(motorA_IN4_pin, HIGH);
    analogWrite(motorA_PWM_pin, 100);

    digitalWrite(motorB_IN1_pin, HIGH);
    digitalWrite(motorB_IN2_pin, HIGH);
    analogWrite(motorB_PWM_pin, 100);
}
