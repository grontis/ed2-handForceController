# -*- coding: utf-8 -*-
"""
Created on Mon Jul 20 02:35:07 2020

@author: Dhaval
"""
import os

class GetCommands:
    commands = []
    def __init__(self):
        try:
            with open('Assets/Python/settings.txt', 'r') as file1:
                for line in file1.readlines():
                    line = line.split(',')
                self.commands = line
                file1.close()
        except:
            print('file not found')
            
    def getCommands(self):
        osName = os.name
        if(osName == 'posix'):
            for i in self.commands:
                if(i == 'Enter'):
                    i = 'return'
                if(i == 'Ctrl'):
                    i = 'command'
        for x in self.commands:
            if(x == 'comma'):
                x = ','
        return self.commands
        
