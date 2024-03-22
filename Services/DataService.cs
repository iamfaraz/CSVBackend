using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

public class DataService : IDataService
{
    private readonly IWebHostEnvironment _hostEnvironment;
    public DataService(IWebHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    public IEnumerable<T> ReadCSV<T>(string filePath)
    {

        var file = Path.Combine(_hostEnvironment.ContentRootPath, filePath);
        var reader = new StreamReader(file);
        var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = csv.GetRecords<T>();
        return records;
    }
}