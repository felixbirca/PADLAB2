namespace Common
{
    public class SyncResponseDto
    {
        public bool IsLast { get; set; }
        public string NextNodeIpAddress { get; set; } = string.Empty;
    }
}
