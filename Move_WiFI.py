
#!/usr/bin/env python
#
# https://www.dexterindustries.com/BrickPi/
# https://github.com/DexterInd/BrickPi3
#

from __future__ import print_function # use python 3 syntax but make it compatible with python 2
from __future__ import division       #


import socket, threading

import time     # import the time library for the sleep function
import brickpi3 # import the BrickPi3 drivers
BP = brickpi3.BrickPi3()

print ("performing inquiry...")
#HOST= '127.0.0.1' #192.168.0.164 172.30.35.145
#PORT= 4444

data = '0'
left = 'Left'
right = 'Right'
forward = 'Forw'
back = 'Back'
stop = 'STOP'
qu = 'Quit'

speed = 100

neckfrw = 'NeckF'
neckbck = 'NeckB'
s=socket.socket(socket.AF_INET, socket.SOCK_STREAM)

try:
    s.bind(('172.30.35.145',4444))
except socket.error as msg:
    print ("Bind failed. error code:" + str(msg[0]) + " Message" + msg[1])
    
    
#is supposed to BIND TO RIGHT SOCKET BUT doesn't
#print("host is [%s]"% (socket.gethostbyname(socket.gethostname())))
s.listen(1)
print ("Server listening")

try:
 
    print("before accept")    
    conn, addr=s.accept()
    
    while 1:

        print("data check")
        data = conn.recv(1024)
        if data:
            print("recieved[%s]"% (data))
            #conn.send(data)
    # Set the motor speed

        if (data.find(left) != -1):
            BP.set_motor_power(BP.PORT_A, speed)
            BP.set_motor_power(BP.PORT_D, -speed)
        if (data.find(right) != -1):
            BP.set_motor_power(BP.PORT_A, -speed)
            BP.set_motor_power(BP.PORT_D, speed)
        if (data.find(forward) != -1):
            BP.set_motor_power(BP.PORT_A, speed)
            BP.set_motor_power(BP.PORT_D, speed)
        if (data.find(back) != -1):
            BP.set_motor_power(BP.PORT_A, -speed)
            BP.set_motor_power(BP.PORT_D, -speed)
        #neck
        if (data.find(neckfrw) != -1):
            BP.set_motor_power(BP.PORT_B, 100)
        if (data.find(neckbck) != -1):
            BP.set_motor_power(BP.PORT_B, -100)
        #stop
        if (data.find(stop) != -1):
            BP.set_motor_power(BP.PORT_B, 0)
            BP.set_motor_power(BP.PORT_D, 0)
            BP.set_motor_power(BP.PORT_A, 0)
        if (data.find(qu) != -1):
            print("closing socket")
            conn.close()
            s.close()
except:
    s.close()
    if not(data is "0"):
       conn.close()

