import random
import json

with open ("config.json", "r") as file:
    params =json.load(file)

class Veri:
    def update(self, params):
        flow=0
        for i in params:
            if "value" not in i:
                i["value"] = 0

            match i["data"].upper():
                case "PRESSURE":
                    i["value"] = random.uniform(18,40)
            
                case "TEMPERATURE":
                    if i["value"] == 0:
                        i["value"] = 20.0

                    degisim = random.uniform(-1.0, 1.0)
                    yeni_sicaklik = i["value"] + degisim

                    if yeni_sicaklik > 40.0:
                        yeni_sicaklik = 40.0
                    elif yeni_sicaklik < 0.0:
                        yeni_sicaklik = 0.0
                    i["value"] = yeni_sicaklik

                case "FLOW RATE":
                    flow = random.uniform(0, 100)
                    i["value"] = flow

                case "ENERGY":
                    katsayi = random.uniform (9.10, 9.35)
                    i["value"] = flow * katsayi

                case _:
                    i["value"] = 0