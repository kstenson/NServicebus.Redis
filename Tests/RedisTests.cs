using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NUnit.Framework;
using Redis;

namespace Tests
{
    [TestFixture]
    public class RedisTests
    {
        private IBus bus;
        
        public RedisTests()
        {
            bus = Configure.With()
                .DefineEndpointName("Test")
                .DefaultBuilder()
                .JsonSerializer()
                .RedisTransport()
                .UnicastBus()
                .CreateBus().Start();


        }

        [Test]
        public void Can_Send_Message_To_Redis()
        {
            bus.Send("TestQueue",new TestMessage {name = "Keith"});
            bus.Send("Test",new TestMessage() {name = "TestKeith"});
        }

        public class TestMessage :IMessage
        {
            public string name { get; set; }
        }


    }

    public class TestMessageHandler : IHandleMessages<RedisTests.TestMessage>
    {
        public void Handle(RedisTests.TestMessage message)
        {
            Console.WriteLine(message.name);
        }
    }
}
