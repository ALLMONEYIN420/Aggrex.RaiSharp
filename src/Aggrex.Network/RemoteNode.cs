using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aggrex.Common;
using Aggrex.Configuration;
using Aggrex.Network.HandShakes;
using Aggrex.Network.Messages;
using Aggrex.Network.Messages.KeepAlive;
using Aggrex.Network.Requests;
using Autofac;

namespace Aggrex.Network
{
    public class RemoteNode : IRemoteNode
    {
        public delegate RemoteNode Factory(TcpClient client);

        private readonly IMessageDispatcher _messageDispatcher;

        private readonly TcpClient _client;
        private readonly BlockingCollection<BaseMessage> _requestQueue;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private bool _isConncected;
        private ClientSettings _clientSettings;

        public RemoteNode(TcpClient client, IMessageDispatcher messageDispatcher, ClientSettings clientSettings)
        {
            _client = client;
            _requestQueue = new BlockingCollection<BaseMessage>();
            _messageDispatcher = messageDispatcher;
            _isConncected = false;
            _cancellationTokenSource = new CancellationTokenSource();
            _clientSettings = clientSettings;
        }

        //public void ExecuteProtocolHandShake()
        //{
        //    try
        //    {
        //        _isConncected = true;
        //        var networkStream = _client.GetStream();
        //        var binaryReader = new BinaryReader(networkStream);

        //        Task.Run(() => ExecuteSendLoop());

        //        _handShakeProcessor.ProcessHandShake(binaryReader, this);
        //    }
        //    catch (Exception ex)
        //    {
        //        OnDisconnected();
        //    }
        //}

        public void QueueMessage(BaseMessage request)
        {
            if (_isConncected)
            {
                _requestQueue.TryAdd(request, 500);
            }
        }

        public string DNID { get; set; }

        public bool QueueContainsMessageType<T>() where T : BaseMessage
        {
            return _requestQueue.Any(x => x.GetType() == typeof(T));
        }


        public IPEndPoint RemoteEndPoint
        {
            get { return (_client.Client.RemoteEndPoint as IPEndPoint); }
        }

        public IPEndPoint ListenerEndpoint { get; set; }

        public void ExecuteProtocolLoop()
        {
            _isConncected = true;

            Task.Run(() =>
            {
                ExecuteSendLoop();
            });
            _client.ReceiveTimeout = 100000;

            using (var networkStream = _client.GetStream())
            {
                var binaryReader = new BinaryReader(networkStream);

                while (_isConncected)
                {
                    try
                    {
                        MessageHeader header = new MessageHeader();
                        header.ReadFromStream(binaryReader);
                        _messageDispatcher.DispatchTcpProtocolMessage(header.Type, binaryReader, this);
                    }
                    catch (Exception ex)
                    {
                        OnDisconnected();
                    }
                }
            }
        }

        private void OnDisconnected()
        {
            _isConncected = false;
            Disconnected?.Invoke(this, new EventArgs());
            _cancellationTokenSource.Cancel();

            Console.WriteLine("Disconnected from client.");
        }

        private void ExecuteSendLoop()
        {
            var networkStream = _client.GetStream();
            while (_isConncected)
            {
                BaseMessage request = _requestQueue.Take(_cancellationTokenSource.Token);
                StreamHelper.Write(request, networkStream);
            }
        }

        public event EventHandler Disconnected;
    }
}