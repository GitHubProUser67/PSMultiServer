using PSMultiServer.SRC_Addons.MEDIUS.RT.Common;
using PSMultiServer.SRC_Addons.MEDIUS.Server.Common.Stream;

namespace PSMultiServer.SRC_Addons.MEDIUS.RT.Models
{
    [ScertMessage(RT_MSG_TYPE.RT_MSG_CLIENT_CONNECT_READY_TCP)]
    public class RT_MSG_CLIENT_CONNECT_READY_TCP : BaseScertMessage
    {

        public override RT_MSG_TYPE Id => RT_MSG_TYPE.RT_MSG_CLIENT_CONNECT_READY_TCP;

        public byte StartOpt = 0x0E;
        public RT_RECV_FLAG RecvFlag = RT_RECV_FLAG.RECV_BROADCAST;

        public override void Deserialize(MessageReader reader)
        {
            StartOpt = reader.ReadByte();
            RecvFlag = reader.Read<RT_RECV_FLAG>();
        }

        public override void Serialize(MessageWriter writer)
        {
            writer.Write(StartOpt);
            writer.Write(RecvFlag);
        }

        public override string ToString()
        {
            return base.ToString() + " " +
                $"StartOpt: {StartOpt} " +
                $"RecvFlag: {RecvFlag}";
        }
    }
}