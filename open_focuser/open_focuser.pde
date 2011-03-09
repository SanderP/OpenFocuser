/*
 * Source for an Arduino+Adafruit motorshield based 
 * focus controller.
 *
 * $Source: \\orion\users\sander\cvsroot/open_focuser/open_focuser/open_focuser.pde,v $
 * $Revision: 1.2 $
 * $Date: 2011/03/03 23:15:49 $
 */

#include <AFMotor.h>
//#include <EEPROM.h>
#include <avr/eeprom.h> 



uint16_t position_e_a __attribute__((section(".eeprom"))) = 0;
uint16_t init_e_a __attribute__((section(".eeprom"))) = 2;
#define INIT_EEPROM_VALUE  0x3287

#define MAXLEN 50
char inByte = 0;         // incoming serial byte
int index = 0;
char  in_buffer[MAXLEN];
int  temperature = 234;
unsigned int	position = 0;
unsigned int	target_position = 0;
int stop = 0;
#define VER "Open Focuser 1.0"

AF_Stepper motor(48, 2);


void setup()
{
	unsigned int temp;
	// start serial port at 9600 bps:
	Serial.begin(9600);
	delay(1000);
	Serial.println(VER);
	motor.setSpeed(60);
	motor.release();

	temp = eeprom_read_word(&init_e_a);
	if (temp != INIT_EEPROM_VALUE) {
		eeprom_write_word(&init_e_a, INIT_EEPROM_VALUE);
		Serial.println("Position initialized to zero");
		write_status();
	} else {
		position = eeprom_read_word(&position_e_a);
		target_position = position; // no movement right out of the gates
		Serial.print("Position read: ");
		Serial.println(position);
	}
	Serial.println("Ready");
}

void loop()
{
	// if we get a valid byte, read analog ins:
	if (Serial.available() > 0) {
		// get incoming byte:
		inByte = Serial.read();
		in_buffer[index++] = inByte;
		if (inByte == 10 || index == MAXLEN) {
			// LF read
			// chop CR LF off the command string
			in_buffer[index - 2] = 0;
			//Serial.print('>');
			//Serial.print(in_buffer);
			//Serial.println('<');
			process_cmd(in_buffer);
			// go again
			index = 0;
		}
	}
	// read temperature
	// adjust focuser if needed based on temp
	// nothing to read, see if the focuser needs to be moved
	move_focuser();
}

void process_cmd(char* cmd) {
	if (strcmp(cmd, "p") == 0) {
		// get position
		Serial.print("P ");
		Serial.println(position);
	} else if (strncmp(cmd, "m ", 2) == 0) {
		// move
		unsigned int temp = atoi(cmd+2);
		Serial.print("M ");
		Serial.println(temp);
		target_position = temp;
	} else if (strcmp(cmd, "i") == 0) {
		// is focuser moving?
		Serial.print("I ");
		if (stop == 1 || (target_position == position))
			// no
			Serial.println(0);
		else
			// yes
			Serial.println(1);
	} else if (strcmp(cmd, "t") == 0) {
		// get temperature
		Serial.print("T ");
		Serial.println(temperature);
	} else if (strcmp(cmd, "a") == 0) {
		// get target position
		Serial.print("A ");
		Serial.println(target_position);
	} else if (strcmp(cmd, "h") == 0) {
		// halt (ASCOM style)
		Serial.println("H");
		target_position = position;
	} else if (strcmp(cmd, "r") == 0) {
		// release
		Serial.println("R");
		stop = 0;
	} else if (strcmp(cmd, "s") == 0) {
		// stop
		Serial.println("S");
		stop = 1;
	} else if (strcmp(cmd, "v") == 0) {
		Serial.println(VER);
	} else if (strcmp(cmd, "z") == 0) {
		// set current as zero, resets target to zero as well
		Serial.println("Z");
		target_position = 0;
		position = 0;
		write_status();
	} else {
		Serial.print('>');
		Serial.print(cmd);
		Serial.println("< not understood.");
	}
}

void move_focuser() {
	if (stop == 1) {
		motor.release();
		return;
	}
	if (target_position > position) {
		position++;
		motor.step(1, FORWARD, MICROSTEP);
		write_status();
	} else if (target_position < position) {
		position--;
		motor.step(1, BACKWARD, MICROSTEP);
		write_status();
	} else {
		// no movement needed, power down stepper
		motor.release();
	}
}

void write_status() {
	eeprom_write_word(&position_e_a, position);
}
