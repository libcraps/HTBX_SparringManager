# HTBX_SparringManager - Arduino
 
We can connect some sensor to the HTBX like polar, so we use movuino that we program on arduino in order to connect them to the bag. 

## Folder :

### Movuino_esp32_polar :

Functional project to connect the movuino to the polar via bluetooth and to send to the computer polar's data via OSC message using WIFI
(MPU not functional yet)

The code has to be cleaned up

### Movuino_esp8266 : 

Functional project that send movuin's MPU data via OSC message using WIFI

You must change wifi information in the sketch (password and name) to be able to connect on it.

### Librairies :

It contains some usefull librairies that you might need