# -*- coding: utf-8 -*-


import sys
sys.path.append(r"Assets/Python/IronPython.StdLib.2.7.10.zip/IronPython.StdLib.2.7.10")
import subprocess
import os

path = os.getcwd() + '/Assets/Python/sensorCommands.py'
osName = os.name
if(osName == 'nt'):
    try:
        os.system("python Assets/Python/sensorCommands.py")
    except:
        try:
            os.system("python3 Assets/Python/sensorCommands.py")
        except:
            print('no python or python modules (keyboard, pyserial) found')

else:
    try:
        subprocess.Popen(['/usr/local/bin/python3', path])
    except:
        try:
            subprocess.Popen(['/usr/local/bin/python', path])
        except:
            try:
                subprocess.Popen(['/usr/local/python', path])
            except:
                try:
                    subprocess.Popen(['/usr/bin/python', path])
                except:
                    print('no python or python modules (keyboard, pyserial) found')

