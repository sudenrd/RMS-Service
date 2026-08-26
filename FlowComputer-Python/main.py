import time
import threading
import json
import VeriUretimSimulasyonu
from pymodbus.server import StartTcpServer
from pymodbus.datastore import ModbusSequentialDataBlock, ModbusSlaveContext, ModbusServerContext

import ModbusKurulum 
import VeriUretimSimulasyonu
import RegisterManager

with open("config.json", "r") as file:
    params = json.load(file)

store = ModbusSequentialDataBlock(4000, [0] * 10)
slave_context = ModbusSlaveContext(hr=store)
context = ModbusServerContext(slaves=slave_context, single=True)

modbus_kurulum = ModbusKurulum.ModBusKurulum()
veri_sistemi = VeriUretimSimulasyonu.Veri()
register_manager = RegisterManager.RegisterManager(context)

def veri_guncelleme_dongusu():
    while True:
        veri_sistemi.update(params)
        for i in params:
            registerlar = modbus_kurulum.float_to_16bit_register(i["value"])
            register_manager.writeFloat(i["address"], registerlar)
            
            print(f"[Adres: {i['address']}] {i['data']} : {i['value']:.2f} {i['unit']}")

        time.sleep(10)
        print("-" * 30)

guncelleme_thread = threading.Thread(target=veri_guncelleme_dongusu)
guncelleme_thread.start()

StartTcpServer(context=context, address=("127.0.0.1", 502))
import logging
logging.basicConfig()
log = logging.getLogger()
log.setLevel(logging.DEBUG)