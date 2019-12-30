
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


void setup()
{
    Serial1.begin(115200);
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
    char receiveVal;

    if (Serial1.available() > 0)
    {
        receiveVal = Serial1.read();

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
    

    //delay(50);
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

void motor_backward()
{
    digitalWrite(motorC_IN3_pin, LOW);
    digitalWrite(motorC_IN4_pin, HIGH);
    analogWrite(motorC_PWM_pin, 100);

    digitalWrite(motorD_IN1_pin, LOW);
    digitalWrite(motorD_IN2_pin, HIGH);
    analogWrite(motorD_PWM_pin, 100);

    digitalWrite(motorA_IN3_pin, LOW);
    digitalWrite(motorA_IN4_pin, HIGH);
    analogWrite(motorA_PWM_pin, 100);

    digitalWrite(motorB_IN1_pin, LOW);
    digitalWrite(motorB_IN2_pin, HIGH);
    analogWrite(motorB_PWM_pin, 100);
}

void motor_brake()
{
    digitalWrite(motorC_IN3_pin, LOW);
    digitalWrite(motorC_IN4_pin, LOW);
    analogWrite(motorC_PWM_pin, 0);

    digitalWrite(motorD_IN1_pin, LOW);
    digitalWrite(motorD_IN2_pin, LOW);
    analogWrite(motorD_PWM_pin, 0);

    digitalWrite(motorA_IN3_pin, LOW);
    digitalWrite(motorA_IN4_pin, LOW);
    analogWrite(motorA_PWM_pin, 100);

    digitalWrite(motorB_IN1_pin, LOW);
    digitalWrite(motorB_IN2_pin, LOW);
    analogWrite(motorB_PWM_pin, 100);
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
