#define USB_CFG_DEVICE_NAME     'D','i','g','i','S','h','a','r','p'
#define USB_CFG_DEVICE_NAME_LEN 9
#define LED 1 // any PWM led will do
#include <DigiUSB.h>

int x; // DigiHDDLamp returns byte value 

void setup() {
  DigiUSB.begin();
  pinMode(LED, OUTPUT);

  // Once windows shows Digispark Bootloder, and then
  // start DigiUSB Device on Windows and digispark blinks LED 3 times.
  // Refer to Device Manager.
  
  for ( int i = 0 ; i < 3; i++) {
    analogWrite(LED, 100);
    delay(200);
    digitalWrite(LED, 0);
    delay(200);
  }

}

// DigiHDDLamp returns byte value (0 to 8) to DigiUSB device.

void bright(int a, int b) {
  if (b == 0) {                 // HDD no active time (= 0%)
    digitalWrite(a, 0);         // Lamp off
  }
  else if (b == 8) {            // HDD full active time (= 100%)
    digitalWrite(a, 1);         // Lamp ON
  }
  else {                        // Because polling 1 second, blink 1 second.
    int c = 100 / b ;           // Recommend Performance counter 
    for (int i = 0 ; i < 5 * b ; i++) {
      digitalWrite(a, 0);
      delay(c);
      digitalWrite(a, 1);
      delay(c);
    }

  }

}

// Main loop

void loop() { 
  DigiUSB.refresh();
  if (DigiUSB.available() > 0) {

    x = DigiUSB.read();

    bright(LED, x);

  }
}
