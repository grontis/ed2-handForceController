# ed2-handForceController
Engineering Design 1/2 Project for Group 9

TeamMembers:

Lillian Boettcher

Grant Kveton

Dhaval Manojkumar Shirvi


# Serial Communication Between Arduino and Unity C# (as of 6/1/20)

If you do not have Unity installed, install it (The unity hub is a nice way to keep track of projects and install versions, in my opinion):
https://unity3d.com/get-unity/download

If you do not have the Arduino IDE installed, install it:
https://www.arduino.cc/en/main/software

Connect force sensors and Arduino based on circuit diagram:
![proposal_hardware_schematic](readmeimgs/hardwareschem.png)

Clone the project repository to your local machine:
https://github.com/grontis/ed2-handForceController

Open the sensorSerialWriter Arduino sketch in the Arduino IDE and upload the sketch to the Arduino:
File-> Open -> Navigate to the sensorSerialWrite folder in the cloned repo and open the Arduino file.
Make sure your Arduino is connected to USB of computer.
Sketch->Upload 
(You may need to check that your serial port for the Arduino is selected in the IDE correctly, this can be done under Tools->Port:… mine has an option for COM3(Arduino Uno))
(Currently the sketch only reads one sensor and sends that data, but will be updated soon)

Open the Unity project and run it to test communication:
Open the UnityHub, click “Add” and navigate to the cloned repo and select the “ed2-UnityProject” folder and click “select folder”. It is possible you may encounter that you do not have the project’s version of unity installed. If so you can click on “Installs” and install the version listed under the project (it will probably be best for us all to be working on the project in the same version since we are sharing the repository).
Open the project and in the project window in assets in the “Scenes” folder. Double click to open the “SampleScene”
NOTE: You may need to check that the code specifies the correct serial port (same port that was selected for serial in the Arduino IDE earlier. for example: COM3) I noticed that the port numbers were different between my laptop and desktop.

press the play button (or CTRL+P) to start the application.
Once the application is playing, in the bottom select the console window. Values should be output (0 for no force on sensor, up to 999 for max force) 

NOTE: When making a new Unity project (that isn’t the one setup in the repo) that will be using serial port communication, *before* creating your C# scripts you have to go into Edit-> Project Settings-> Player -> Api compatibility level and set it to “.NET 4.x”, and then restart Unity. Otherwise, the System.IO.Ports namespace will not import. From my debugging over this, I think it has something to do with the way Unity creates C# script assemblies. 
