using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ServiceStack.Common.Web;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceModel.Serialization;
using ServiceStack.WebHost.Endpoints;

namespace Bm.Servicestack.Plugins
{
    public class PrettyXmlFormatPlugin : IPlugin
    {
        public const string XmlPrettyText = "application/prettyxml";
        public void Register(IAppHost appHost)
        {
            appHost.ContentTypeFilters.Register(XmlPrettyText,
                 Serialize,
                 Deserialize);

        }

        public static void Serialize(IRequestContext requestContext, object dto, Stream outputStream)
        {
            
            var xml = HttpResponseFilter.Instance.Serialize(ContentType.Xml, dto);

            xml =  PrettyXml(xml);
            byte[] bytes = Encoding.UTF8.GetBytes(xml);

            outputStream.Write(bytes, 0, bytes.Length);
        }

        public static object Deserialize(Type type, Stream fromStream)
        {
            var obj = JsonDataContractDeserializer.Instance.DeserializeFromStream(type, fromStream);
            return obj;
        }

        private static string PrettyXml(string xml)
        {
            var stringBuilder = new StringBuilder();

            var element = XElement.Parse(xml);

            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.NewLineOnAttributes = true;

            using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                element.Save(xmlWriter);
            }

            return stringBuilder.ToString();
        }


    }

    public class PrettyXmlServiceClient : XmlServiceClient
    {
        public PrettyXmlServiceClient()
            : base()
        {
        }

        public PrettyXmlServiceClient(string baseUri)
            : base(baseUri)
        {
        }

        public PrettyXmlServiceClient(string syncReplyBaseUri, string asyncOneWayBaseUri)
            : base(syncReplyBaseUri, asyncOneWayBaseUri)
        {
        }

        public override string Format
        {
            get
            {
                return "prettyxml";
            }
        }
    }
}
