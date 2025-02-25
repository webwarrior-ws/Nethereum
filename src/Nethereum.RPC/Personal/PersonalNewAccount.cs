using System;
using System.Threading;
using System.Threading.Tasks;
 
using JsonRpcSharp.Client;

namespace Nethereum.RPC.Personal
{
    /// <Summary>
    ///     Create a new account
    ///     Parameters
    ///     string, passphrase to protect the account
    ///     Return
    ///     string address of the new account
    ///     Example
    ///     personal.newAccount("mypasswd")
    /// </Summary>
    public class PersonalNewAccount : RpcRequestResponseHandler<string>
    {
        public PersonalNewAccount(IClient client) : base(client, ApiMethods.personal_newAccount.ToString())
        {
        }

        public Task<string> SendRequestAsync(string passPhrase,
                                             object id = null,
                                             CancellationToken cancellationToken = default(CancellationToken))
        {
            if (passPhrase == null) throw new ArgumentNullException(nameof(passPhrase));
            return SendRequestAsync(id, cancellationToken, passPhrase);
        }

        public RpcRequest BuildRequest(string passPhrase, object id = null)
        {
            if (passPhrase == null) throw new ArgumentNullException(nameof(passPhrase));
            return base.BuildRequest(id, passPhrase);
        }
    }
}