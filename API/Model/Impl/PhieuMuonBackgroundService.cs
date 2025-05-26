using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Model.Impl
{
    public class PhieuMuonBackgroundService : BackgroundService
    {
        private readonly ILogger<PhieuMuonBackgroundService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public PhieuMuonBackgroundService(
            ILogger<PhieuMuonBackgroundService> logger,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Thực hiện kiểm tra ngay khi service bắt đầu
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var kiemTraService = scope.ServiceProvider.GetRequiredService<IKiemTraPhieuMuonService>();
                    await kiemTraService.KiemTraPhieuMuonQuaHan();
                    _logger.LogInformation("Đã kiểm tra và cập nhật phiếu mượn quá hạn ngay khi khởi động lúc {time}", DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi kiểm tra phiếu mượn quá hạn lúc khởi động");
            }
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextRun = now.Date.AddHours(12); // 12:00 PM

                if (now > nextRun)
                {
                    nextRun = nextRun.AddDays(1);
                }

                var delay = nextRun - now;
                await Task.Delay(delay, stoppingToken);

                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var kiemTraService = scope.ServiceProvider.GetRequiredService<IKiemTraPhieuMuonService>();
                        await kiemTraService.KiemTraPhieuMuonQuaHan();
                        _logger.LogInformation("Đã kiểm tra và cập nhật phiếu mượn quá hạn lúc {time}", DateTime.Now);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Lỗi khi kiểm tra phiếu mượn quá hạn");
                }
            }
        }
    }
} 