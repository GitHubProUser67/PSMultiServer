namespace SRVEmu.DirtySocks.Messages
{
    public class PlusPop : AbstractMessage
    {
        public override string _Name { get => "+pop"; }

        public string? Z { get; set; }
    }
}
