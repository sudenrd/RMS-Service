import struct

class ModBusKurulum:
    @staticmethod
    def float_to_16bit_register(value):
        byte_data = struct.pack('>f', value)
        register1, register2 = struct.unpack('>HH', byte_data)
        return [register1, register2]
