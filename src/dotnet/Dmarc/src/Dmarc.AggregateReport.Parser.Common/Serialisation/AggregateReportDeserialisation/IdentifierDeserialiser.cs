using System;
using System.Xml.Linq;
using Dmarc.AggregateReport.Parser.Common.Domain.Dmarc;
using Dmarc.AggregateReport.Parser.Common.Utils;

namespace Dmarc.AggregateReport.Parser.Common.Serialisation.AggregateReportDeserialisation
{
    public interface IIdentifiersDeserialiser
    {
        Identifier Deserialise(XElement identifiers);
    }

    public class IdentifiersDeserialiser : IIdentifiersDeserialiser
    {
        public Identifier Deserialise(XElement identifiers)
        {
            if (identifiers.Name != "identifiers")
            {
                throw new ArgumentException("Root element must be identifiers");
            }

            string envelopeTo = identifiers.SingleOrDefault("envelope_to")?.Value;
            string headerFrom = identifiers.Single("header_from").Value;

            return new Identifier(envelopeTo, headerFrom);
        }
    }
}