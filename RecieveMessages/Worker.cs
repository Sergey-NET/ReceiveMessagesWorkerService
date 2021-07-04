using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecieveMessages
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISendMessagesService _sendMessagesService;
        private const string ApplicationName = "MessagingWorkerServiceDemo";

        public Worker(ILogger<Worker> logger,
            ISendMessagesService sendMessagesService)
        {
            _logger = logger;
            _sendMessagesService = sendMessagesService;
        }
        public override async Task StartAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Application {ApplicationName} started. ");

            await _sendMessagesService.StartListen();

            await base.StartAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                try
                {
                    await Task.Delay(1000, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }
        public override async Task StopAsync(
           CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Application {ApplicationName} stopped. ");
            await base.StopAsync(cancellationToken);
        }
    }
}
