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

    [HttpGet("customerOrders")]
    public ActionResult<IEnumerable<CustomerOrderDto>> GetAllCustomerOrders()
    {
        var customerPath = WebHostEnvironment.ContentRootPath + "\\MyAppData\\csv\\Customers.csv";
        var orderPath = WebHostEnvironment.ContentRootPath + "\\MyAppData\\csv\\Orders.csv";
        var orderItemPath = WebHostEnvironment.ContentRootPath + "\\MyAppData\\csv\\OrderItems.csv";
        var customers = _dataService.ReadCSV<Customer>(customerPath);
        var orders = _dataService.ReadCSV<Order>(orderPath);
        var orderItems = _dataService.ReadCSV<OrderItem>(orderItemPath);


        var customerOrders = customers.Select(customer => new CustomerOrderDto
        {
            CustomerId = customer.customer_id,
            FirstName = customer.first_name,
            LastName = customer.last_name,
            Orders = orders.Where(order => order.customer_id == customer.customer_id)
                                    .Select(order => new OrderDto
                                    {
                                        OrderId = order.order_id,
                                        CustomerId = order.customer_id,
                                        OrderDate = order.order_date,
                                        ShippedDate = order.shipped_date,
                                        RequiredDate = order.required_date,
                                        OrderItems = orderItems.Where(item => item.order_id == order.order_id)
                                                                .ToList()
                                    }).ToList()
        }).ToList();

        return customerOrders;
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
        var serverPath = WebHostEnvironment.ContentRootPath + "\\MyAppData\\csv\\OrderItems.csv";
        var orderItems = _dataService.ReadCSV<OrderItem>(serverPath);
        return Ok(orderItems);
    }

    public class CustomerOrderDto
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<OrderDto> Orders { get; set; }
    }

    public class OrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public string OrderDate { get; set; }
        public string RequiredDate { get; set; }
        public string ShippedDate { get; set; }
    }
}