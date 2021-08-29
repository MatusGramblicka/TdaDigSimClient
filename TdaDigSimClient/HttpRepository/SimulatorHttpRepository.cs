using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TdaDigSimClient.HttpRepository
{
    public class SimulatorHttpRepository : ISimulatorHttpRepository
    {
        private readonly HttpClient _client;

        public SimulatorHttpRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task SendPayload(string Uri, Scenario bvr)
            => await _client.PostAsJsonAsync(Uri, bvr);

        //public async Task SendPayload(string Uri, string bvr)
        //    => await _client.PostAsJsonAsync(Uri, bvr);

    }
}
