using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Redis
{
    public static class ConfigureRedisQueue
    {
        public static Configure RedisTransport(this Configure config)
        {
            config.Configurer.ConfigureComponent<RedisQueue>(
                                         DependencyLifecycle.SingleInstance);

            return config;
        }
    }
}
