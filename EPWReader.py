# -*- coding: utf-8 -*-
"""
Created on Sun Dec  8 09:34:13 2019

@author: user
"""

import pathlib, time

class EPWLocation:
    def __init__(self,LocationData):
        self.City = LocationData.split(',')[1]
        self.State = LocationData.split(',')[2]
        self.Country = LocationData.split(',')[3]
        self.Source = LocationData.split(',')[4]
        self.WMO = LocationData.split(',')[5]
        self.Latitude = LocationData.split(',')[6]
        self.Longitude = LocationData.split(',')[7]
        self.Timezone = LocationData.split(',')[8]
        self.Elevation = LocationData.split(',')[9]

class EPWDataFrameBuilder1:
    #importing the file at once
    def __init__(self,file):
        f = open(file,'r')
        
        content = f.readlines()
        self.Location = EPWLocation(content[0])
        
        self.DryBulb = []
        self.DewPoint = []
        self.RelHum = []
        self.Pressure = []
        
        headerLength = 8
        
        for i in range(headerLength,headerLength+8760):
            line = content[i]
            #print (line)
            self.DryBulb.append(line.split(',')[6])
            self.DewPoint.append(line.split(',')[7])
            self.RelHum.append(line.split(',')[8])
            self.Pressure.append(line.split(',')[9])
        else:
            print ("reached end of file")
            f.close()
            
class EPWDataFrameBuilder2:
    #importing the file line by line
    def __init__(self,file):
        f = open(file,'r')
        
        count = 0
        self.DryBulb = []
        self.DewPoint = []
        self.RelHum = []
        self.Pressure = []
            
        for line in f:
            count+=1
            if count == 1:
                self.Location = EPWLocation(line)
            if count >8:
                self.DryBulb.append(line.split(',')[6])
                self.DewPoint.append(line.split(',')[7])
                self.RelHum.append(line.split(',')[8])
                self.Pressure.append(line.split(',')[9])
        else:
            print ("reached end of file")
            f.close()
            
class EPWDataFrameBuilder3:
    #using pandas to import file as csv
    def __init__(self,file):
        import pandas as pd
        self.data = pd.read_csv(file,skiprows=8)
        #f = open(file,'r')
        
        #self.DryBulb = []
        #self.DewPoint = []
        #self.RelHum = []
        #self.Pressure = []
        
epw_file = pathlib.Path("EPWDemo.App/files/WAW.epw")

if __name__ == '__main__':
    t0 = time.time()
    if epw_file.exists():
        #STEP 1 - IMPORT ALL AT ONCE AND WORK WITH MEMORY
        epw1 = EPWDataFrameBuilder1(epw_file)
        t1 = time.time()-t0
        
        #STEP 2 - IMPORT LINE BY LINE
        epw2 = EPWDataFrameBuilder2(epw_file)
        t2 = time.time()-t0
        
        #STEP 3 - IMPORT USING PANDAS
        epw3 = EPWDataFrameBuilder3(epw_file)
        t3 = time.time()-t0
        
        print ("Elapsed time t1: %f s"%t1)
        print ("Elapsed time t2: %f s"%t2)
        print ("Elapsed time t3: %f s"%t3)
        
