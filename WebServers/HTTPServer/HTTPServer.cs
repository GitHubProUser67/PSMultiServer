// Copyright (C) 2016 by David Jeske, Barend Erasmus and donated to the public domain

using CustomLogger;
using HTTPServer.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace HTTPServer
{
    public class HttpServer
    {
        #region Fields

        private readonly ConcurrentDictionary<int, TcpListener> _listeners = new();
        private readonly CancellationTokenSource _cts = null!;
        private readonly HttpProcessor Processor;

        #endregion

        #region Public Methods
        public HttpServer(ushort listenerPort, List<Route> routes, HttpProcessor proc, CancellationToken cancellationToken)
        {
            LoggerAccessor.LogWarn($"[HTTP] - HTTP system is initialising on port: {listenerPort}, service will be available when initialized...");

            Processor = proc;

            routes.ForEach(route => Processor.AddRoute(route));

            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            new Thread(() => CreateHTTPPortListener(listenerPort)).Start();
        }

        private void CreateHTTPPortListener(ushort listenerPort)
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    TcpListener listener = new(IPAddress.Any, listenerPort);
                    listener.Start();
                    LoggerAccessor.LogInfo($"[HTTP] - Server initiated on port: {listenerPort}...");
                    _listeners.TryAdd(listenerPort, listener);

                    while (!_cts.Token.IsCancellationRequested)
                    {
                        try
                        {
                            if (_cts.Token.IsCancellationRequested) break;

                            TcpClient? client = await listener.AcceptTcpClientAsync(_cts.Token).ConfigureAwait(false);

                            _ = Task.Run(() => Processor.HandleClient(client, listenerPort));
                        }
                        catch (OperationCanceledException)
                        {
                            LoggerAccessor.LogWarn($"[HTTP] - System requested a server shutdown on port: {listenerPort}...");
                        }
                        catch (IOException ex)
                        {
                            if (ex.InnerException is SocketException socketException && socketException.ErrorCode != 995 &&
                                socketException.SocketErrorCode != SocketError.ConnectionReset && socketException.SocketErrorCode != SocketError.ConnectionAborted
                                && socketException.SocketErrorCode != SocketError.ConnectionRefused)
                                LoggerAccessor.LogError($"[HTTP] - Client loop thrown an IOException: {ex}");
                        }
                        catch (SocketException ex)
                        {
                            if (ex.ErrorCode != 995 && ex.SocketErrorCode != SocketError.ConnectionReset && ex.SocketErrorCode != SocketError.ConnectionAborted && ex.SocketErrorCode != SocketError.ConnectionRefused)
                                LoggerAccessor.LogError($"[HTTP] - Client loop thrown a SocketException: {ex}");
                        }
                        catch (Exception ex)
                        {
                            if (ex.HResult != 995) LoggerAccessor.LogError($"[HTTP] - Client loop thrown an assertion: {ex}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    LoggerAccessor.LogError($"[HTTP] - Listener failed to start with assertion: {ex}");
                }

            }, _cts.Token);
        }

        public void Stop()
        {
            _cts.Cancel();
            _listeners.Values.ToList().ForEach(x => x.Stop());
        }
        #endregion
    }
}
