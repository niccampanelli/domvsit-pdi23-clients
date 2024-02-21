namespace Application.Commom.Boundaries
{
    public interface ISortingRequest
    {
        public string? SortField { get; set; }
        public string? SortOrder { get; set; }
    }
}
