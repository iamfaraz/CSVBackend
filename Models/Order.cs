public class Order
{
    public int order_id { get; set; }
    public int customer_id { get; set; }
    public int order_status { get; set; }
    public string? order_date { get; set; }
    public string? required_date { get; set; }
    public string? shipped_date { get; set; }
}