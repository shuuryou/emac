# eMac as a VGA Monitor
![](https://github.com/shuuryou/emac/blob/master/pictures/emac.jpg?raw=true)

Notes about putting a single board computer (SBC) inside an eMac and making the CRT work as a generic VGA monitor. It builds on the foundations by [Rocky Hill](https://github.com/qbancoffee/emac_ivad_board_init).

## What's Here
* A sketch for an Arduino Leonardo (as built into the SBC being used)
* A crude C# program that provides a graphical means to manage the CRT controller
* Some miscellaneous documentation

## Ingredients
* One single board computer: [LattePanda Alpha 864s](https://www.lattepanda.com/products/lattepanda-alpha-864s.html)
* One Arduino compatible relay board with at least three channels
* One 12V stereo amplifier (e.g. a TDA7297 stereo amplifier board)
* One buck converter with variable DC input and 12V 3A output (e.g. a "SZWENGAO WG8-40S1206" takes 8-40 V DC input and outputs 12V 6A with compensation for sudden changes in voltage)
* One HDMI to VGA converter with audio out (the [HDFury Nano GX](https://www.hdfury.com/product/hdf1-nano-gx/) is known to work with the eMac CRT's crazy refresh rates, but is sold out)
* One very short, shielded HDMI cable
* One DB15M VGA to terminal adapter (unless you're great at soldering thin wires to tiny pins, then get a DB15M VGA plug for soldering)
* One suitable USB cable to power the HDMI to VGA converter, if required
* One bag of jumper wires; get a larger set of male-male, male-female and female-female since they're also useful to have around
* Several JST 4p connectors (male and female); you don't need many but these usually come in bags of 10-20pcs
* Several JST 2p connectors (male and female); again, you don't need many but buy a bag since they're useful to have around
* Some automotive tape to hold wires together
* Some cable ties
* Some adhesive PCB stand-off spacers
* Some WAGO COMPACT 221-413 splicing wire connectors to connect wires until you're sure you want to solder them together

Serves 1 eMac.

### Optional Ingredients
### Microphone
The LattePanda has a headphone jack, but in reality, it's a combined headphone and microphone jack. There are two standards for this kind of jack: CTIA and OMTP. They differ on the position of the Microphone and ground pins.

**The LattePanda Alpha uses CTIA**.

Get these things for the microphone:
* One CTIA to headphone/microphone adapter (~20cm wire length so you can route the headphone side out the side)
* One 3,5mm jack extension cord (male to female) approx. 20cm
* One bag of 3,5mm stereo jack to terminal block adapters (you really only need one male adapter but a mixed bag is usually cheaper)

Wired up correctly, it will work out of the box.

#### Donglegate
There's barely any space to properly route wires for LAN and USB, unless you design a custom solution. I therefore decided to do it the Apple way and provide a single USB-C port for external connectivity.

This only requires one ~20cm USB-C male to female extension cable and one USB C hub with Ethernet and whatever else you want (HDMI, card reader, etc.)

It also has the added advantage that you can power the LattePanda via USB-C power delivery to dry-run/debug it without the eMac CRT or the eMac power supply being involved in any way.

Note: Avoid powering the LattePanda via USB-C power delivery **and** internal 12V power at the same time.

## General Layout

![](https://github.com/shuuryou/emac/blob/master/pictures/layout.jpg?raw=true)

I would recommend a layout as in this photo because it has the advantage that the LattePanda board is accessible via the eMac's bottom user access door when the case is closed. It allows you to push its internal power button with a small flat head screwdriver when powering it over USB-C power delivery without the eMac CRT connected to power.

![](https://github.com/shuuryou/emac/blob/master/pictures/useraccess.jpg?raw=true)

## Wiring

These resources may help you:
* LattePanda pinout: http://docs.lattepanda.com/content/delta_edition/io_playability/
* eMac VGA pinout: https://en.wikibooks.org/wiki/How_to_modify_an_eMac_to_use_as_an_external_monitor
* My documented pinouts: https://github.com/shuuryou/emac/tree/master/pinout

### VGA connector
Be very careful when removing the eMac CRT wires from the connector. If you break the cable or its individual wires, your life will turn quite miserable.

The `SDA` (DDE Data) and `SCL` (DDE Clock) lines don't go to the VGA connector, but to jumper wires that are hooked up to the Arduino side of the LattePanda. `SDA` must be on Arduino pin 4 and `SCL` on Arduino pin 5 when using the sketch in this repository, but the pins may be changed in the header file.

Two further jumper wires go from the Arduino side of the LattePanda to the `SDA` and `SCL` pins on the VGA connector if you want VGA EDID support for the OS running on the PC side of the LattePanda. The Arduino will inject a custom EDID. This is optional, since it usually doesn't work will with HDMI to VGA adapters anyway. If you want this, use pins 2 (Arduino SDA) and 3 (Arduino SCL), respectively. Other pins will not work even if you modify the header file.

Pin 4 is CRT-ON and must be connected with a wire so that it can accept DC 5V switched via a relay. This relay will be used to turn on the CRT.

### LattePanda Power State Pins

Patch these power state pins from the PC side of the LattePanda to the Arduino side using jumper wires:
* PC pin `S3` to Arduino pin 10
* PC pin `S4` to Arduino pin 11
* PC pin `S5` to Arduino pin 12

The Arduino pin numbers may be changed in the header file.

### Power Button

The Arduino needs to control the power button of the eMac case because it must turn on the CRT before it turns on the LattePanda. Otherwise the eMac power supply will cut out due to an overload condition. Wire one side of the eMac power button to Arduino ground and put the other side on Arduino pin 9. Again, you can change the pin number in the header file if you must.

### Power LED

Use a 220 Ohm resistor and connect the power LED to the LED_BUILTIN pin on the LattePanda's Arduino side. This is pin 13. This way you will see the LED pulse while the Arduino is busy initializing after receiving power.

### Relay Board

You can power the relay board from the T5V pin of the eMac's blind mate connector and also get a GND connection from that connector.

The relay controlled by Arduino pin 21 must be connected to the LattePanda's power button using a JST 2p connector.

The relay controlled by Arduino pin 22 must be connected to the CRT-ON wire and must provide DC 5V to it when switched. You can grab 5V from the T5V pin on the blind mate connector.

The relay controlled by Arduino pin 23 must be connected to the amplifier's power input and must provide DC 12V to it when switched. You can grab 12V from the CPU12V power pins of the blind mate connector. Use both CPU12V positive pins and both CPU12V ground pins to avoid putting too much current on the wires.

### System Power

Connect the buck converter to both 20V positive pins and both 20V negative pins on the blind mate connector because they will be pulling up to 3A of current. Connect the 12V output of the buck converter to the LattePanda using a JST 4p connector. Be very careful to solder the wires correctly. Refer to the LattePanda pinout above.

When you apply power to the eMac, the LattePanda's LEDs should light up. The LattePanda must not be powered without the CRT powered on first (powered off and sleep is fine) or the eMac power supply will cut out due to an overload condition. It does not allow a high power draw while the CRT is off.

## Usage

There's an experimental C# tool included in this repository that can be used to adjust the displayed image and also save them to the EEPROM so they're used every time the CRT is switched on. It is largely untested though and was largely designed based on the very few bits of information available on the web. 

![](https://github.com/shuuryou/emac/blob/master/pictures/screenshot.png?raw=true)


You won't get an image on the screen unless you are outputting exactly what the CRT expects, which is one of these:

* 640x480 138Hz
* 800x600 112Hz
* 1024x768 89Hz
* 1152x864 80Hz
* 1280x960 72Hz

This sadly means that you will see nothing at boot time and won't be able to change BIOS settings via the CRT. Be sure to set up the BIOS to your needs and configure your OS to accept remote connections so you can fix the display output if necessary.

Remember to configure a screen saver to avoid image burn-in. Note that the CRT does not support power saving (DPMS, etc.). However, the CRT will shut off when the PC side of the LattePanda enters sleep or hibernation mode.

### Possible Issues

The defaults the Arduino sketch writes into the EEPROM and sends to the IVAD may be causing light but visible blooming (black text a light gray background has a white smudge fading out into the right). This was not evident in MacOS X so the values being sent by this sketch may not be optimal. The logic board of my eMac no longer works so I cannot make additional visual comparisons.

# Help Wanted

If you are able to sniff the I²C communication via the original eMac logic board and the CRT controller, please help figure out which commands control brightness and which control the G2 voltage. If you find anything interesting or notice something is missing please file a bug.
