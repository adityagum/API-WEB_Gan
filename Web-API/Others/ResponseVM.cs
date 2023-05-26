namespace Web_API.Others
{
    public class ResponseVM<TEntity>
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public TEntity? Data { get; set; }
    }
}
