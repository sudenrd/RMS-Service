using Application.Abstractions.Interfaces.Services;
using NModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace External_Service.Modbus
{
    
        public class ModbusService : IModbusService
        {
            public ushort[] readAllRegister()
            {
                string ipAddress = "127.0.0.1";
                int port = 502;
                ushort startAddress = 4000;
                ushort numberOfPoints = 8;

                try
                {
                    using (TcpClient client = new TcpClient(ipAddress, port))
                    {
                        var factory = new ModbusFactory();
                        IModbusMaster master = factory.CreateMaster(client);

                        ushort[] registers = master.ReadHoldingRegisters(1, startAddress, numberOfPoints);
                        return registers;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Modbus simülatörüne bağlanılamadı: " + ex.Message);
                }
            }
        }
    
}
