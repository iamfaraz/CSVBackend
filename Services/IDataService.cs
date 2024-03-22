public interface IDataService
{
    public IEnumerable<T> ReadCSV<T>(string filePath);
}