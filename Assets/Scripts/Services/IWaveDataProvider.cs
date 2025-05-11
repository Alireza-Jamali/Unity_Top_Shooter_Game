using System.Collections.Generic;
using System.Threading.Tasks;
using Data;

namespace Services
{
    /// <summary>
    /// Bridge pattern interface for wave data providers
    /// </summary>
    public interface IWaveDataProvider
    {
        Task<List<WaveConfiguration>> GetWaveConfigurationsAsync();
    }
}