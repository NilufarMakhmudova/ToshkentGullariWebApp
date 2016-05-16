using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToshkentGullari.Services {
    public class RestClient {
        public List<Product> GetProducts() {
            // Let's prepare our helpers for future use
            var helper = new NetworkHelper();
            var serializer = new XmlService();

            // first, let's grab the remote xml
            var data = helper.MakeRemoteRequest("http://localhost:42911/API/Products/", "GET", null);

            // As we know that the stream contains XML text, lets use our extension method
            // to retrieve the text out of the sequences of bytes
            var xmlText = data.ReadAsText();

            // Now we need to convert the incoming XML into our objects. This will be done
            // using our XmlService
            var result = serializer.Deserialize(xmlText);

            // Once we have done this, we are ready to return our objects
            return result;
        }

        public Weather GetWeatherInformation() {
            var helper = new NetworkHelper();
            var serializer = new XmlService();
            var data = helper.MakeRemoteRequest("http://api.openweathermap.org/data/2.5/weather?q=Tashkent&mode=xml", "GET", null);
            var xmlText = data.ReadAsText();
            var result = serializer.DeserializeWeather(xmlText);
            return result;
        }

        
    }
}
