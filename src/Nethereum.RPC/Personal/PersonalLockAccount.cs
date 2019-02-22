using System;
using System.Threading.Tasks;
 
using Nethereum.Hex.HexConvertors.Extensions;
using JsonRpcSharp.Client;
using System.Threading;

namespace Nethereum.RPC.Personal
{
    /// <Summary>
    ///     Removes the private key with given address from memory. The account can no longer be used to send transactions.
    /// </Summary>
    public class PersonalLockAccount : RpcRequestResponseHandler<bool>
    {
        public PersonalLockAccount(IClient client) : base(client, ApiMethods.personal_lockAccount.ToString())
        {
        }

        public Task<bool> SendRequestAsync(string account,
                                           object id = null,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            return base.SendRequestAsync(id, cancellationToken, account.EnsureHexPrefix());
        }

        public RpcRequest BuildRequest(string account, object id = null)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            return base.BuildRequest(id, account.EnsureHexPrefix());
        }
    }
}