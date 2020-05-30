# Front Bezel Pinouts

## Front Microphone

|Pin|Meaning|
|-|-|
|Red|+|
|White|-|
|Black|Shield (connect to case GND, etc.)|

## Front Speakers

Notes:
1. The connector is facing you with the notch at the top
1. The numbering is from left to right, top to bottom (crude drawing below)
1. SPK-A is the speaker with the shorter wire
1. SPK-B is the speaker with the longer wire

```
     _
---- --
| 1 2 |
| 3 4 |
-------
```

|Pin|Meaning|
|-|-|
|1 |SPK-B +|
|2 |SPK-B -|
|3|SPK-A +|
|4|SPK-A -|


## Front LED

|Pin|Meaning|
|-|-|
|Blue|+|
|Black|-|

## WiFi Antenna

The WiFi antenna can be removed by removing front bezel and removing one screw that holds it at the top corner of the screen. Follow Apple Service Source instructions.

The front bezel is held by three __small__ screws on left and right sides, and two __small__ screws at the top (total of eight screws). There are no screws at the bottom that hold the front bezel and no plastic tabs that need to be bent.

**DO NOT** remove the large screws or the CRT will be permanently knocked out of alignment.

The front bezel can only be removed once the digital assembly (the entire MLB/HDD/CD container) has been removed fully.

## IVAD

To turn the CRT on or off, 5V needs to be supplied to pins 4 and 10 as per pinout_ivad.png.

My multimeter indicated that the IVAD pulls 874uA on both pins combined; i.e. 0.874mA. This is thankfully way below the maximum pin output current of an Arduino (40mA). It should be okay to drive these pins directly from Arduino output(s), which saves an otherwise unnecessary relay board. To play it super safe, I will put a resistor in series to limit the current draw. R = 5v / 0.874mA = 572.08 Ω. The closest matching resistor I have on hand is 330 Ω. That'll let the IVAD pull up to 15.15mA, which should be way more thant it needs.