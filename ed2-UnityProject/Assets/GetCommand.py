# -*- coding: utf-8 -*-
"""
Created on Mon Jul 20 02:35:07 2020

@author: Dhaval
"""

class GetCommands:
    commands = []
    def __init__(self):
        try:
            with open('Assets/settings.txt', 'r') as file1:
                for line in file1.readlines():
                    line = line.split(',')
                self.commands = line
                file1.close()
        except:
            print('file not found')
            
    def getCommands(self):
        return self.commands


