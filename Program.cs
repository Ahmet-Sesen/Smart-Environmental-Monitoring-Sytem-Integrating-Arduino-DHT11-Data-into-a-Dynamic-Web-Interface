using System.Data.SqlClient;
using System.IO.Ports;
class Program
{
    static void Main(string[] args)
    {
        // Arduino'dan gelen verileri okumak için SerialPort'u yapılandırın
        SerialPort serialPort = new SerialPort("COM7", 9600); // Arduino'nun bağlı olduğu port ve baud hızını belirtin

        // SQL Server veritabanına bağlanın
        string connectionString = "sql_database_name;Database=Myweb;Integrated Security=True;";
        SqlConnection connection = new SqlConnection(connectionString);

        try
        {
            // SerialPort'u açın
            serialPort.Open();

            while (true)
            {
                // Arduino'dan gelen veriyi satır satır okuyun
                string rawData = serialPort.ReadLine();

                // Veriyi işleyin (sıcaklık ve nem değerlerini ayırarak)
                string cleanedData = ExtractNumbers(rawData);
                string[] values = cleanedData.Split(',');
                Console.WriteLine(string.Join(", ", values));
                DateTime now = DateTime.Now;

                if (values.Length == 2)
                {
                    if (float.TryParse(values[0], out float temperature) && float.TryParse(values[1], out float humidity))
                    {
                        // SQL Server veritabanına veriyi kaydedin
                        string insertQuery = "INSERT INTO NemSicaklikVerileri (Saat, Sicaklik, Nem) VALUES (@Saat, @Sicaklik, @Nem)";
                        SqlCommand cmd = new SqlCommand(insertQuery, connection);
                        cmd.Parameters.AddWithValue("@Saat", now);
                        cmd.Parameters.AddWithValue("@Sicaklik", temperature);
                        cmd.Parameters.AddWithValue("@Nem", humidity);
                        

                        // Bağlantıyı açın ve komutu çalıştırın
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();

                        Console.WriteLine($"Sıcaklık: {temperature} °C, Nem: {humidity} % veritabanına kaydedildi.");
                    }
                    else
                    {
                        Console.WriteLine("Veriyi işlerken hata oluştu.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
        }
        finally
        {
            // SerialPort ve SQL bağlantılarını kapatın
            serialPort.Close();
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
    }

    // Veriyi temizlemek ve sayısal değerleri ayırmak için bir fonksiyon
    static string ExtractNumbers(string input)
    {
        string result = string.Empty;
        foreach (char c in input)
        {
            if (char.IsDigit(c) || c == '.' || c == ',')
            {
                result += c;
            }
        }
        return result;
    }
}



