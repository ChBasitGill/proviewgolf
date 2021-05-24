namespace ProViewGolf.DataLayer.Models
{
    public enum ResponseStatus
    {
        Error = -1,
        NoResult = 0,
        Success = 1
    }

    public class Response
    {
        public Pagination Pagination { get; set; } = null;

        public ResponseStatus Status { get; set; } = ResponseStatus.Success;
        public string Msg { get; set; } = null;
        public dynamic Data { get; set; } = null;
    }

    public class Pagination
    {
        public int PageSize { get; set; } = 25;
        public int Page { get; set; }
        public int TotalPages => (Records + PageSize - 1) / PageSize;

        public int Records { get; set; }
        public int Fetched { get; set; }
    }
}