using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using NServiceBus;
using NServiceBus.Unicast.Queuing;
using NServiceBus.Unicast.Transport;
using ServiceStack.Redis;

namespace Redis
{
    public class RedisQueue : ISendMessages, IReceiveMessages
    {
        private Address _address;

        public void Send(TransportMessage message, Address address)
        {
           using(var redisClient = new RedisClient())
           {
               redisClient.Add(string.Format("{0}:{1}", address.Machine, address.Queue), message);
           }
        }

        public void Init(Address address, bool transactional)
        {
            _address = address;
        }

        public bool HasMessage()
        {
            using (var redisClient = new RedisClient())
            {
                return redisClient.ContainsKey(string.Format("{0}:{1}", _address.Machine, _address.Queue));
            }
        }

        public TransportMessage Receive()
        {
            using (var redisClient = new RedisClient())
            {
               
                    var redis = redisClient.As<TransportMessage>();
                    var message = redis.GetValue(string.Format("{0}:{1}", _address.Machine, _address.Queue));
                    return message;
                
            }
        }
    }
}
