# -*- coding: utf-8 -*-
"""
Created on Mon Sep 25 15:33:08 2023

@author: johnh
"""
import warnings
import serial
import time
import datetime
import streamlit as st
import pandas as pd
import plotly.graph_objects as go
import plotly.express as px
import pymysql
from sqlalchemy import create_engine

warnings.filterwarnings('ignore')
   
# chart_pl = st.empty()
# hum_pl = st.empty()
host="db356.cgalzncqicjj.us-west-2.rds.amazonaws.com"
port=int(3306)
user="admin"
passw="Catdust2001"
database="db356"

try:
    mydb = create_engine("mysql+pymysql://" + user + ':' + passw + "@" + host + ":" + str(port) + '/' + database , echo=False)
except:
    print("Error During Connection to Server: Unable to Establish Engine")
    quit()
    
    

ardunio = serial.Serial(port='COM8',baudrate=9600,parity=serial.PARITY_NONE,stopbits=serial.STOPBITS_ONE,bytesize=serial.EIGHTBITS)
dataframe = pd.DataFrame(columns=['Time',"PH","Conductivity", "Temperature"])


if ardunio.isOpen() == False:
    ardunio.open()
    
i=0
while i < 45:
    
    try:
        temp = ardunio.readline().decode() #Read Serial line
    except: 
        print("Error Reading Serial: Exiting")
        ardunio.close()
        break
    
    
    try:
        ph,con,temp= temp.split(',')    #Parse serial Data
        
        ph = 7 - ((int(ph)-620)/57.14)  #PH Conversion Formula
        
        #Create Dataframe and pass data
        dataframe.loc[i,'Time'] = datetime.datetime.now()
        dataframe.loc[i,'PH'] = ph
        dataframe.loc[i,'Conductivity'] = float(con)
        dataframe.loc[i,'Temperature'] = float(temp)
    except:
        print("Error During Data Interpretation: Exiting - No upload to server")
        break
    try:
        dataframe.to_sql(name='test356', con=mydb, if_exists = 'append', index=False) #Push dataframe to database
    except:
        print("Error During Data Upload to Server - Passing")
        pass
   
    dataframe = dataframe.drop(i,axis=0)
    
    i+=1
    time.sleep(1)   #One second Delay

ardunio.close()

    