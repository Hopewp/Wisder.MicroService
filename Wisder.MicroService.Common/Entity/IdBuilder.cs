using System;
using System.Collections.Generic;
using System.Text;
using Wisder.MicroService.Common.Exceptions;

namespace Wisder.MicroService.Common.Entity
{
    public class IdBuilder
    {
        //各部分长度
        private const int TIME_LEN = 41;
        private const int DATACENTER_LEN = 5;
        private const int SERVER_LEN = 5;
        private const int SEQ_LEN = 12;
        //各部分位移长度
        private const int TIME_LEFT_BIT = 64 - 1 - TIME_LEN;
        private const int DATA_LEFT_BIT = TIME_LEFT_BIT - DATACENTER_LEN;
        private const int SERVER_LEFT_BIT = DATA_LEFT_BIT - SERVER_LEN;
        //各部分最大值
        private const long DATACENTER_MAX = ~(-1 << DATACENTER_LEN);
        private const long SERVER_MAX = ~(-1 << SERVER_LEN);
        private const long SEQ_MAX = ~(-1 << SEQ_LEN);

        private DateTime Start_Time = new DateTime(2020, 9, 1);

        private long DataCenter_Id;
        private long Server_Id;
        private long Last_Timestamp = -1L;
        private long Last_Seq = 0L;


        public IdBuilder(long dataCenterId, long serverId)
        {
            DataCenter_Id = dataCenterId;
            Server_Id = serverId;
        }

        private object _locker = new object();
        public long GetNextId(int step = 1)
        {
            lock (_locker)
            {
                long now = GetTimestampFromStartTime();
                if (now < Last_Timestamp)
                {
                    throw new CommonException("-5", "系统时间错误，不允许生成ID");
                }
                if (now == Last_Timestamp)
                {
                    Last_Seq = Last_Seq + step;
                    if (Last_Seq > SEQ_MAX)
                    {
                        now = NextMillis();
                        Last_Seq = step;
                    }
                }
                else
                {
                    Last_Seq = step;
                }
                Last_Timestamp = now;
                return (now << TIME_LEFT_BIT) | (DataCenter_Id << DATA_LEFT_BIT) | (Server_Id << SERVER_LEFT_BIT) | Last_Seq;
            }
        }
        private long NextMillis()
        {
            long now = GetTimestampFromStartTime();
            while (now <= Last_Timestamp)
            {
                now = GetTimestampFromStartTime();
            }
            return now;
        }
        private long GetTimestampFromStartTime()
        {
            return (DateTime.Now.Ticks - Start_Time.Ticks) / 10000; ;
        }
    }
}
