# AOGTeensyFlasher
A helper to easily flash the Teensy for agOpenGPS

First of all - full credit to https://github.com/luni64/TeensySharp for this excellent library that makes flashing the Teensy super-simple!

## Reason for creation

People have had issues with various versions of TeensyDuino (specifically, versions higher than 1.57 causing boot loops) and also in locating different versions of the firmware for specific situations.

This tool aims to simplify all that. Go to the Releases section and download the big AOGTeensyFlasher zip file, unzip it, and run the AOGTeensyFlasher.exe file.

When you run it, you'll see the following:

![image](https://github.com/lansalot/AOGTeensyFlasher/assets/9885921/06e61fdc-6182-4021-959f-9cdfdb773d7f)

(If you don't have a Teensy plugged in, that section will be blank of course). If you've never ran this before, you should Refresh the list of available firmware, so press that Refresh List button at the top.

![image](https://github.com/lansalot/AOGTeensyFlasher/assets/9885921/f728d099-cf6a-43f5-a733-bbd19f5a117f)

The list is retrieved from github and saved locally. Saving locally means you can experiment with adding other firmwares in of your choice, and see how you get on. If you have any firmwares that should be added to the tool, please log an issue on github or message andyinv/lansalot on discourse/telegram etc.

Now, it's simply a matter of picking the firmware you wish - if it's not present already, it'll be downloaded - and press Program!

![image](https://github.com/lansalot/AOGTeensyFlasher/assets/9885921/ec14d4c4-5bff-428e-812e-7987035d2aef)

If anything goes wrong, make sure you don't have the Arduino IDE open or anything like that. And again, any issues, please let us know.
