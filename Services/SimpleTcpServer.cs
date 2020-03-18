using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace V2RayShell.Services
{
    //https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.socketasynceventargs?view=netframework-4.5
    class SimpleTcpServer : IDisposable
    {
        private volatile bool stopped;
        private readonly int m_numConnections;
        private int m_receiveBufferSize;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        readonly BufferManager m_bufferManager;
        const int opsToPreAlloc = 2;
        Socket listenSocket;
        readonly SocketAsyncEventArgsPool m_readWritePool;
        //int m_totalBytesRead;
        int m_numConnectedSockets;
        readonly Semaphore m_maxNumberAcceptedClients;

        public Func<SocketAsyncEventArgs,byte[]> Process;

        public SimpleTcpServer(int numConnections, int receiveBufferSize)
        {
            //m_totalBytesRead = 0;
            m_numConnectedSockets = 0;
            m_numConnections = numConnections;
            m_receiveBufferSize = receiveBufferSize;
            m_bufferManager = new BufferManager(receiveBufferSize * numConnections * opsToPreAlloc, receiveBufferSize);
            m_readWritePool = new SocketAsyncEventArgsPool(numConnections);
            m_maxNumberAcceptedClients = new Semaphore(numConnections, numConnections);

            m_bufferManager.InitBuffer();
            for (var i = 0; i < m_numConnections; i++)
            {
                var readWriteEventArg = new SocketAsyncEventArgs();
                readWriteEventArg.Completed += IO_Completed;
                readWriteEventArg.UserToken = new AsyncUserToken{BufferManager = m_bufferManager};
                m_bufferManager.SetBuffer(readWriteEventArg);
                m_readWritePool.Push(readWriteEventArg);
            }
        }


        public void Start(IPEndPoint localEndPoint)
        {
            listenSocket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(localEndPoint);
            listenSocket.Listen(m_numConnections);
            stopped = false;
            StartAccept(null);
        }

        public void Stop()
        {
            stopped = true;
            if (listenSocket != null)
            {
                listenSocket.Close();
                listenSocket.Dispose();
                listenSocket = null;
            }
        }

        public void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            if (stopped) return;
            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += AcceptEventArg_Completed;
            }
            else
            {
                // socket must be cleared since the context object is being reused
                acceptEventArg.AcceptSocket = null;
            }

            m_maxNumberAcceptedClients.WaitOne();
            bool willRaiseEvent = listenSocket.AcceptAsync(acceptEventArg);
            if (!willRaiseEvent)
            {
                ProcessAccept(acceptEventArg);
            }
        }

        void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }
        }

        void AcceptEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            Interlocked.Increment(ref m_numConnectedSockets);
            SocketAsyncEventArgs readEventArgs = m_readWritePool.Pop();
            ((AsyncUserToken) readEventArgs.UserToken).Socket = e.AcceptSocket;
            bool willRaiseEvent = e.AcceptSocket.ReceiveAsync(readEventArgs);
            if (!willRaiseEvent) ProcessReceive(readEventArgs);
            StartAccept(e);
        }

        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            AsyncUserToken token = (AsyncUserToken) e.UserToken;
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                //Interlocked.Add(ref m_totalBytesRead, e.BytesTransferred);
                //e.SetBuffer(e.Offset, e.BytesTransferred);
                if (Process != null)
                {
                    var result = Process(e);
                    e.SetBuffer(result, 0, result.Length);
                }
                var willRaiseEvent = token.Socket.SendAsync(e);
                if (!willRaiseEvent) ProcessSend(e);
            }
            else
            {
                CloseClientSocket(e);
            }
        }

        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                var token = (AsyncUserToken) e.UserToken;
                var willRaiseEvent = token.Socket.ReceiveAsync(e);
                if (!willRaiseEvent) ProcessReceive(e);
            }
            else
            {
                CloseClientSocket(e);
            }
        }

        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            var token = e.UserToken as AsyncUserToken;
            try
            {
                token?.Socket.Shutdown(SocketShutdown.Send);
            }
            catch (Exception ex)
            {
                Logging.LogUsefulException(ex);
            }
            token?.Socket.Close();
            Interlocked.Decrement(ref m_numConnectedSockets);
            m_readWritePool.Push(e);
            m_maxNumberAcceptedClients.Release();
        }

        #region IDisposable

        private bool disposed = false;
        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if(!this.disposed)
            {
                if(disposing)
                {
                    Stop();
                    // Dispose managed resources.
                    var saea = m_readWritePool.Pop();
                    while (saea!=null)
                    {
                        m_bufferManager.FreeBuffer(saea);
                        saea.Dispose();
                        saea = m_readWritePool.Pop();
                    }
                }
                disposed = true;
            }
        }
        ~SimpleTcpServer()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }
        #endregion
    }

    #region Token

    internal sealed class AsyncUserToken
    {
        public AsyncUserToken() : this(null)
        {
        }
        public AsyncUserToken(Socket socket)
        {
            Socket = socket;
        }
        public Socket Socket { get; set; }
        public BufferManager BufferManager { get; set; }
    }

    #endregion

    #region BufferManager

    class BufferManager
    {
        int m_numBytes;                 // the total number of bytes controlled by the buffer pool
        byte[] m_buffer;                // the underlying byte array maintained by the Buffer Manager
        Stack<int> m_freeIndexPool;     // 
        int m_currentIndex;
        int m_bufferSize;

        public BufferManager(int totalBytes, int bufferSize)
        {
            m_numBytes = totalBytes;
            m_currentIndex = 0;
            m_bufferSize = bufferSize;
            m_freeIndexPool = new Stack<int>();
        }

        // Allocates buffer space used by the buffer pool
        public void InitBuffer()
        {
            // create one big large buffer and divide that 
            // out to each SocketAsyncEventArg object
            m_buffer = new byte[m_numBytes];
        }

        // Assigns a buffer from the buffer pool to the 
        // specified SocketAsyncEventArgs object
        //
        // <returns>true if the buffer was successfully set, else false</returns>
        public bool SetBuffer(SocketAsyncEventArgs args)
        {

            if (m_freeIndexPool.Count > 0)
            {
                args.SetBuffer(m_buffer, m_freeIndexPool.Pop(), m_bufferSize);
            }
            else
            {
                if ((m_numBytes - m_bufferSize) < m_currentIndex)
                {
                    return false;
                }
                args.SetBuffer(m_buffer, m_currentIndex, m_bufferSize);
                m_currentIndex += m_bufferSize;
            }
            return true;
        }

        // Removes the buffer from a SocketAsyncEventArg object.  
        // This frees the buffer back to the buffer pool
        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            m_freeIndexPool.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }
    }

    #endregion

    #region SocketAsyncEventArgsPool

    class SocketAsyncEventArgsPool
    {
        readonly Stack<SocketAsyncEventArgs> m_pool;
        // Initializes the object pool to the specified size
        //
        // The "capacity" parameter is the maximum number of 
        // SocketAsyncEventArgs objects the pool can hold
        public SocketAsyncEventArgsPool(int capacity)
        {
            m_pool = new Stack<SocketAsyncEventArgs>(capacity);
        }

        // Add a SocketAsyncEventArg instance to the pool
        //
        //The "item" parameter is the SocketAsyncEventArgs instance 
        // to add to the pool
        public void Push(SocketAsyncEventArgs item)
        {
            if (item == null) { throw new ArgumentNullException("Items added to a SocketAsyncEventArgsPool cannot be null"); }
            lock (m_pool)
            {
                m_pool.Push(item);
            }
        }

        // Removes a SocketAsyncEventArgs instance from the pool
        // and returns the object removed from the pool
        public SocketAsyncEventArgs Pop()
        {
            lock (m_pool)
            {
                return m_pool.Pop();
            }
        }

        // The number of SocketAsyncEventArgs instances in the pool
        public int Count
        {
            // ReSharper disable once InconsistentlySynchronizedField
            get { return m_pool.Count; }
        }
    }

    #endregion
}