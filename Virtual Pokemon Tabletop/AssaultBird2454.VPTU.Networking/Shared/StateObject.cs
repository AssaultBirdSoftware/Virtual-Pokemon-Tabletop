using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Networking.Shared
{
    public class StateObject
    {
        public StateObject()
        {

        }
        public StateObject(Socket _workSocket, int _BUFFER_SIZE)
        {
            workSocket = _workSocket;
            BUFFER_SIZE = _BUFFER_SIZE;
            buffer = new byte[BUFFER_SIZE];
            sb = new StringBuilder();
        }
        public StateObject(Socket _workSocket, int _BUFFER_SIZE, StringBuilder _sb)
        {
            workSocket = _workSocket;
            BUFFER_SIZE = _BUFFER_SIZE;
            buffer = new byte[BUFFER_SIZE];
            sb = _sb;
        }

        public void Reset()
        {
            buffer = new byte[BUFFER_SIZE];
            sb = new StringBuilder();
        }

        public Socket workSocket;
        public int BUFFER_SIZE;
        public byte[] buffer;
        public StringBuilder sb;
    }
}
