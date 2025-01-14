﻿using JsonRpcSharp.Client;
//using JsonRpcSharp.IpcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nethereum.RPC.Tests.Testers;

namespace Nethereum.RPC.Tests
{
    public class ClientFactory
    {
        internal static TimeSpan DefaultTimeOutForTests = TimeSpan.FromSeconds(30.0);

        public static IClient GetClient(TestSettings settings)
        {
           var url = settings.GetRPCUrl();
           return new HttpClient(new Uri(url), DefaultTimeOutForTests);
        }
    }
}
