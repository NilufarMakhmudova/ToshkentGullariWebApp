using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ToshkentGullari {
    public class NetworkHelper {
        public Stream MakeRemoteRequest(String url, String method, String body) {
            // This is safeguarding ourselves from invalid data
            if (String.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException("url");
            if (!"get".Equals(method, StringComparison.OrdinalIgnoreCase) &&
                !"post".Equals(method, StringComparison.OrdinalIgnoreCase) &&
                !"put".Equals(method, StringComparison.OrdinalIgnoreCase) &&
                !"delete".Equals(method, StringComparison.OrdinalIgnoreCase)) {
                throw new InvalidRestMethodException("Invalid method. Must be GET, POST, PUT or DELETE.");
            }
            // First we create the request object that is responsible for making queries.
            // Make sure that you have included 
            // using System.Net;
            // in the namespace references above
            // We also indicating where to make the actual query
            var req = HttpWebRequest.Create(url);

            // Now we need to define the HttpVerb that is used to make the query
            req.Method = method;

            // Remember? The body can be empty, but once it is defined 
            // (should be used with POST or PUT only, otherwise will throw error)
            // it should be included into request body
            if (!String.IsNullOrWhiteSpace(body)) {

                // before putting it to the request body, we must convert it into the bytes.
                var bytes = Encoding.UTF8.GetBytes(body);

                // then we get the Stream of the request body in order to supply our bytes
                using (var requestBody = req.GetRequestStream()) {

                    // here we actually supply the bytes. 
                    // Supply the bytes starting from 0 till the last byte
                    requestBody.Write(bytes, 0, bytes.Length);
                }
            }

            // Now we need to get the response. This method makes the actual request
            // the result will contain all the data along with response body
            using (var result = req.GetResponse()) {

                // Now we are interested in the body itself. The body is located inside the
                // response stream. Let's get it
                using (var response = result.GetResponseStream()) {

                    // This is a memory stream that will contain the received bytes
                    var ms = new MemoryStream();

                    // Let's copy the contents of the incoming stream into our
                    // memory stream
                    response.CopyTo(ms);

                    // Once we have copied our data into the MemoryStream, it internally called
                    // Write method of the destinaion stream, thus its position is at the end of the stream
                    // Let's rewind it
                    ms.Position = 0;

                    // Excellent, we have a clean stream in our memory that we can send to the requesting
                    // method
                    return ms;
                }
            }
        }
    }
}
