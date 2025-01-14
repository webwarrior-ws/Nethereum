﻿using System;
using System.Net.Http.Headers;
using Common.Logging;
using Nethereum.Contracts;
using Nethereum.Contracts.Services;
using JsonRpcSharp.Client;
using Nethereum.RPC;
using Nethereum.RPC.Accounts;
using Nethereum.RPC.TransactionManagers;
using Nethereum.Signer;
using Nethereum.Util;

namespace Nethereum.Web3
{
    public class Web3
    {
        private static readonly AddressUtil addressUtil = new AddressUtil();
        private static readonly Sha3Keccack sha3Keccack = new Sha3Keccack();

        public Web3(IClient client)
        {
            Client = client;
            InitialiseInnerServices();
            IntialiseDefaultGasAndGasPrice();
        }

        public Web3(IAccount account, IClient client) : this(client)
        {
            TransactionManager = account.TransactionManager;
            TransactionManager.Client = Client;
        }

        public Web3(TimeSpan connTimeout, string url = @"http://localhost:8545/", ILog log = null)
        {
            IntialiseDefaultHttpClient(url, connTimeout, log);
            InitialiseInnerServices();
            IntialiseDefaultGasAndGasPrice();
        }

        public Web3(IAccount account, TimeSpan connTimeout, string url = @"http://localhost:8545/", ILog log = null) : this(connTimeout, url, log)
        {
            TransactionManager = account.TransactionManager;
            TransactionManager.Client = Client;
        }

        public ITransactionManager TransactionManager
        {
            get => Eth.TransactionManager;
            set => Eth.TransactionManager = value;
        }

        public static UnitConversion Convert { get; } = new UnitConversion();

        public static TransactionSigner OfflineTransactionSigner { get; } = new TransactionSigner();

        public IClient Client { get; private set; }

        public EthApiContractService Eth { get; private set; }
        public ShhApiService Shh { get; private set; }

        public NetApiService Net { get; private set; }

        public PersonalApiService Personal { get; private set; }

        private void IntialiseDefaultGasAndGasPrice()
        {
            TransactionManager.DefaultGas = Transaction.DEFAULT_GAS_LIMIT;
            TransactionManager.DefaultGasPrice = Transaction.DEFAULT_GAS_PRICE;
        }

        public static string GetAddressFromPrivateKey(string privateKey)
        {
            return EthECKey.GetPublicAddress(privateKey);
        }

        public static bool IsChecksumAddress(string address)
        {
            return addressUtil.IsChecksumAddress(address);
        }

        public static string Sha3(string value)
        {
            return sha3Keccack.CalculateHash(value);
        }

        public static string ToChecksumAddress(string address)
        {
            return addressUtil.ConvertToChecksumAddress(address);
        }

        public static string ToValid20ByteAddress(string address)
        {
            return addressUtil.ConvertToValid20ByteAddress(address);
        }

        protected virtual void InitialiseInnerServices()
        {
            Eth = new EthApiContractService(Client);
            Shh = new ShhApiService(Client);
            Net = new NetApiService(Client);
            Personal = new PersonalApiService(Client);
        }

        private void IntialiseDefaultHttpClient(string url, TimeSpan connTimeout, ILog log)
        {
            Client = new HttpClient(new Uri(url), connTimeout, null, null, null, log);
        }
    }
}