using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TdaDigSimClient.HttpRepository
{
    public interface ISimulatorHttpRepository
    {
        Task SendPayload(string Uri, Scenario bvr);
        //Task SendPayload(string Uri, string bvr);
    }
}
