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
using Aggrex.Network.HandShakes;
using Aggrex.Network.Messages;
using Aggrex.Network.Requests;
using Aggrex.ServiceContainer;
using Autofac;

namespace Aggrex.Network
{
    internal class RemoteNode : IRemoteNode
    {
        private readonly IMessageDispatcher _messageDispatcher;
        private readonly IHandShakeProcessor _handShakeProcessor;

        private readonly TcpClient _client;
        private readonly BlockingCollection<BaseMessage> _requestQueue;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private bool _isConncected;

        public RemoteNode(TcpClient client, IHandShakeProcessor handShakeProcessor)
        {
            _handShakeProcessor = handShakeProcessor;

            _client = client;
            _requestQueue = new BlockingCollection<BaseMessage>();
            _messageDispatcher = AggrexContainer.Container.Resolve<IMessageDispatcher>();
            _isConncected = false;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void ExecuteProtocolHandShake()
        {
            try
            {
                _isConncected = true;
                var networkStream = _client.GetStream();
                var binaryReader = new BinaryReader(networkStream);

                Task.Run(() => ExecuteSendLoop());

                _handShakeProcessor.ProcessHandShake(binaryReader, this);
            }
            catch (Exception ex)
            {
                OnDisconnected();
            }
        }

        public void QueueMessage(BaseMessage request)
        {
            if (_isConncected)
            {
                _requestQueue.TryAdd(request, 500);
            }
        }

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
            _client.ReceiveTimeout = 10000;

            using (var networkStream = _client.GetStream())
            {
                var binaryReader = new BinaryReader(networkStream);

                while (_isConncected)
                {
                    try
                    {
                        MessageType requestType = (MessageType) binaryReader.ReadByte();
                        _messageDispatcher.DispatchProtocolMessage(requestType, binaryReader, this);
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