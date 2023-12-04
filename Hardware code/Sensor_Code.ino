#include <OneWire.h>
#include <DallasTemperature.h>
#include <LiquidCrystal_I2C.h>

const int SENSOR_PIN = 4; // Arduino pin connected to DS18B20 sensor's DQ pin

OneWire oneWire(SENSOR_PIN);         // setup a oneWire instance
DallasTemperature tempSensor(&oneWire); 
OneWire oneWire2(A1);         // setup a oneWire instance
DallasTemperature tdsSensor(&oneWire2); 
float tempCelsius; //global varibale for temperature
float tds;  //global varibale for raw conductivity
float Voltage;  //global varibale for Voltage
float tdsValue; //global vaiable for output conductivty 
LiquidCrystal_I2C lcd(0x3F,16,2); //global varibale for temperature




void setup() {
  // Set up LCD and Initialize serial communication at 9600 baud
  lcd.init();
  lcd.backlight();
  lcd.setCursor(0,0);
  lcd.print("Monitoring Unit");
  lcd.setCursor(0,1);
  lcd.print("Status:Connected");
  


  Serial.begin(9600);
  pinMode(5, INPUT);  
  pinMode(4, INPUT);
  tempSensor.begin(); 
}

void loop() {
  // Read analog values from A0, A1, and A2
  int sensorValueA0 = analogRead(A0);
  
  int sensorValueA2 = digitalRead(5);

  tempSensor.requestTemperatures();             // send the command to get temperatures
  tempCelsius = tempSensor.getTempCByIndex(0);
  tds = analogRead(A1); //Read raw TDS value

  //Simple calculation for PPM
  Voltage = tds*5/1024.0;
  tdsValue=(133.42/Voltage*Voltage*Voltage - 255.86*Voltage*Voltage + 857.39*Voltage)*0.5;
  // Print the values to the serial monitor
  Serial.print(sensorValueA0);
  Serial.print(",");
  Serial.print(tdsValue);
  Serial.print(",");
  Serial.println(tempCelsius);

  // Delay for a short time to prevent flooding the serial monitor
  delay(1000);
}
  
