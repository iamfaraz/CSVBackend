using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

[Route("api/[controller]")]
[ApiController]
public class DataController : ControllerBase
{
    private readonly IDataService _dataService;
    IWebHostEnvironment WebHostEnvironment;

    public DataController(IDataService dataService, IWebHostEnvironment webHostEnvironment)
    {
        _dataService = dataService;
        WebHostEnvironment = webHostEnvironment;
    }

    [HttpGet("customers")]
    public ActionResult<IEnumerable<Customer>> GetCustomers()
    {
        var serverPath = WebHostEnvironment.ContentRootPath + "\\MyAppData\\csv\\Customers.csv";
        var customers = _dataService.ReadCSV<Customer>(serverPath);
        return Ok(customers);
    }

    [HttpGet("orders")]
    public ActionResult<IEnumerable<Order>> GetOrders()
    {
        var serverPath = WebHostEnvironment.ContentRootPath + "\\MyAppData\\csv\\Orders.csv";
        var orders = _dataService.ReadCSV<Order>(serverPath);
        return Ok(orders);
    }

    [HttpGet("orderItems")]
    public ActionResult<IEnumerable<OrderItem>> GetOrderItems()
    {
        var serverPath = WebHostEnvironment.ContentRootPath + "\\MyAppData\\csv\\OrderItem.csv";
        var orderItems = _dataService.ReadCSV<OrderItem>(serverPath);
        return Ok(orderItems);
    }
}