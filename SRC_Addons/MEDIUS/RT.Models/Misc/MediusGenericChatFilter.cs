using PSMultiServer.SRC_Addons.MEDIUS.RT.Common;
using PSMultiServer.SRC_Addons.MEDIUS.Server.Common.Stream;

namespace PSMultiServer.SRC_Addons.MEDIUS.RT.Models
{
    public class MediusGenericChatFilter : IStreamSerializer
    {
        public byte[] GenericChatFilterBitfield = new byte[Constants.MEDIUS_GENERIC_CHAT_FILTER_BYTES_LEN];

        public void Deserialize(BinaryReader reader)
        {
            // 
            GenericChatFilterBitfield = reader.ReadBytes(Constants.MEDIUS_GENERIC_CHAT_FILTER_BYTES_LEN);
        }

        public void Serialize(BinaryWriter writer)
        {
            // 
            writer.Write(GenericChatFilterBitfield, Constants.MEDIUS_GENERIC_CHAT_FILTER_BYTES_LEN);
        }

        public override string ToString()
        {
            return base.ToString() + " " +
                $"GenericChatFilterBitfield: {BitConverter.ToString(GenericChatFilterBitfield)}";
        }
    }
}