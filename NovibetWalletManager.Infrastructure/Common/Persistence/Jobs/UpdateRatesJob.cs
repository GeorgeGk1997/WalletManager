using NovibetWalletManager.Application.Services.ECB.Commands;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovibetWalletManager.Infrastructure.Common.Persistence.Jobs
{
    public class UpdateRatesJob : IJob
    {

        private readonly UpdateCurrencyRatesCommand _updateCurrencyRatesCommand;
        public UpdateRatesJob(UpdateCurrencyRatesCommand updateCurrencyRatesCommand)
        {
             _updateCurrencyRatesCommand = updateCurrencyRatesCommand;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            await _updateCurrencyRatesCommand.UpdateRatesAsync();
        }
    }
}
