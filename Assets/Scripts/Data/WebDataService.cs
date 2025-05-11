using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Core;
using Exceptions;
using Newtonsoft.Json;
using Services;

namespace Data
{
    public sealed class WaveDataService : IWaveDataProvider
    {
        private readonly HttpClient _httpClient = new();
        private const string BaseUrl = "http://www.profc.ir/users/shooterWaves/";

        public async Task<List<WaveConfiguration>> GetWaveConfigurationsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(BaseUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var root = JsonConvert.DeserializeObject<RootResponseDto>(json);

                if (root?.data?.waves == null || root.ok != 1)
                {
                    throw new WaveDataException("Invalid response structure");
                }

                return MapWaveDtoToDomain(root.data.waves);
            }
            catch (Exception ex)
            {
                throw new WaveDataException("Failed to load wave configuration", ex);
            }
        }

        private static List<WaveConfiguration> MapWaveDtoToDomain(List<WaveDto> waveDTOs)
        {
            var waveConfigs = new List<WaveConfiguration>();

            foreach (var waveDto in waveDTOs)
            {
                var enemies = new List<EnemyWaveData>();
                foreach (var enemyDTO in waveDto.enemy)
                {
                    var enemyType = enemyDTO.type switch
                    {
                        1 => EnemyType.Green,
                        2 => EnemyType.Yellow,
                        3 => EnemyType.Red,
                        _ => throw new WaveDataException($"Unknown enemy type code: {enemyDTO.type}")
                    };
                    enemies.Add(new EnemyWaveData(enemyType, enemyDTO.count));
                }
                waveConfigs.Add(new WaveConfiguration(enemies));
            }

            return waveConfigs;
        }

        #region DTO Classes
        [Serializable]
        private class RootResponseDto
        {
            public int ok;
            public WaveDataDto data;
        }

        [Serializable]
        private class WaveDataDto
        {
            [JsonProperty("waves")]
            public List<WaveDto> waves;
        }

        [Serializable]
        private class WaveDto
        {
            [JsonProperty("enemy")]
            public List<EnemyDto> enemy;
        }

        [Serializable]
        private class EnemyDto
        {
            public int type;
            public int count;
        }
        #endregion
    }
}