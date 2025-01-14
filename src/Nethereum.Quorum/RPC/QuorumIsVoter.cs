﻿using System;
using System.Threading.Tasks;
using Nethereum.Hex.HexConvertors.Extensions;
using JsonRpcSharp.Client;
using System.Threading;

namespace Nethereum.Quorum.RPC
{
    public class QuorumIsVoter : RpcRequestResponseHandler<bool>
    {
        public QuorumIsVoter(IClient client) : base(client, ApiMethods.quorum_isVoter.ToString())
        {
        }

        public Task<bool> SendRequestAsync(string address, object id = null,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            if (address == null) throw new ArgumentNullException(nameof(address));
            return base.SendRequestAsync(id, cancellationToken, address.EnsureHexPrefix());
        }

        public RpcRequest BuildRequest(string address, object id = null)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));
            return base.BuildRequest(id, address.EnsureHexPrefix());
        }
    }
}