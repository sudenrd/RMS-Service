using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Interfaces.Services;
using Application.Abstractions.Interfaces.Repositories;
using Domain.Entities;

namespace RMS_Service.Presentation.VeriToplama
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IModbusService _modbusService;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IModbusService modbusService, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _modbusService = modbusService;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Veri Toplama Servisi başlatıldı...");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    ushort[] registers = _modbusService.readAllRegister();

                    float pressure = ConvertToFloat(registers[0], registers[1]);
                    float temperature = ConvertToFloat(registers[2], registers[3]);
                    float flowRate = ConvertToFloat(registers[4], registers[5]);
                    float energy = ConvertToFloat(registers[6], registers[7]);

                    _logger.LogInformation($"Okunan: Basınç={pressure:F2}, Sıcaklık={temperature:F2}, Debi={flowRate:F2}");

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var _repository = scope.ServiceProvider.GetRequiredService<IFC_Repository>();

                        var yeniKayit = new FC
                        {
                            Pressure = pressure,
                            Temperature = temperature,
                            FlowRate = flowRate,
                            Energy = energy,
                            Timestamp = DateTime.UtcNow
                        };

                        _repository.create(yeniKayit);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Bağlantı Hatası: {ex.Message}");
                }

                await Task.Delay(10000, stoppingToken);
            }
        }

        private float ConvertToFloat(ushort reg1, ushort reg2)
        {
            byte[] bytes = new byte[4];
            byte[] b1 = BitConverter.GetBytes(reg1);
            byte[] b2 = BitConverter.GetBytes(reg2);

            bytes[0] = b2[0];
            bytes[1] = b2[1];
            bytes[2] = b1[0];
            bytes[3] = b1[1];

            return BitConverter.ToSingle(bytes, 0);
        }
    }
}