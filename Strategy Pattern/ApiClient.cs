using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Strategy_Pattern
{
    public class ApiClient
    {
        private static readonly HttpClient client;
        private static readonly XmlSerializer serializer;

        static ApiClient()
        {
            client = new HttpClient();
            var xRoot = new XmlRootAttribute
            {
                ElementName = "Order Summary"
            };
            serializer = new XmlSerializer(typeof(OrderSummary), xRoot);
        }

        public async Task<HttpStatusCode> SendOrderSummary(string uri, OrderSummary orderSummary)
        {
            using StringWriter textWriter = new StringWriter();
            serializer.Serialize(textWriter, orderSummary);
            string orderSummaryString = textWriter.ToString();

            var httpContent = new StringContent(orderSummaryString, Encoding.UTF8, "application/xml");
            var response = await client.PostAsync(uri, httpContent);

            return response.StatusCode;
        }
    }
}
