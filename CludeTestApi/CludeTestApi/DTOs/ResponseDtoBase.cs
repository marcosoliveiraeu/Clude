namespace CludeTestApi.DTOs
{
    public abstract class ResponseDtoBase
    {
        public bool Success { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
    }
}
