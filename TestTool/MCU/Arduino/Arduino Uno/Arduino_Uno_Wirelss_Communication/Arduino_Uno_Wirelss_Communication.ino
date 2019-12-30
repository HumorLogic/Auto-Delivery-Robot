#include <SoftwareSerial.h>

SoftwareSerial mySerial(2, 3); //RX TX
const int LedPin = 13;
const int motorA_PWM_pin = 6;
const int motorA_IN1_pin = 7;
const int motorA_IN2_pin = 8;

int ledState = 0;
int motorState = 3;

void setup()
{
    pinMode(LedPin, OUTPUT);
    pinMode(motorA_IN1_pin, OUTPUT);
    pinMode(motorA_IN2_pin, OUTPUT);
    pinMode(motorA_PWM_pin, OUTPUT);

    mySerial.begin(115200);
}

void loop()
{
    char receiveVal;

    if (mySerial.available() > 0)
    {
        receiveVal = mySerial.read();

        if (receiveVal == '1')
        {
            ledState = 1;
            motorState = 1;
        }
        else if (receiveVal == '2')
        {
            ledState = 0;
            motorState = 2;
        }
        else if (receiveVal == '0')
        {
            motorState = 0;
        }

        //    else
        //       ledState = 0;
    }

    digitalWrite(LedPin, ledState);
    drive_motor(motorState);
    

    delay(50);
}

void drive_motor(int state)
{
    if(state == 0)
        motor_brake();  
    else if (state == 1)
        motor_forward();
    else if (state == 2)
        motor_backward();
    else if (state == 3)
        motor_neutral(); 
}


//电机向前
void motor_forward()
{
    digitalWrite(motorA_IN1_pin, HIGH);
    digitalWrite(motorA_IN2_pin, LOW);
    analogWrite(motorA_PWM_pin, 100);
}

void motor_backward()
{
    digitalWrite(motorA_IN1_pin, LOW);
    digitalWrite(motorA_IN2_pin, HIGH);
    analogWrite(motorA_PWM_pin, 100);
}

void motor_brake()
{
    digitalWrite(motorA_IN1_pin, LOW);
    digitalWrite(motorA_IN2_pin, LOW);
    analogWrite(motorA_PWM_pin, 0);
}

void motor_neutral()
{
    digitalWrite(motorA_IN1_pin, HIGH);
    digitalWrite(motorA_IN2_pin, HIGH);
    analogWrite(motorA_PWM_pin, 0);
}
