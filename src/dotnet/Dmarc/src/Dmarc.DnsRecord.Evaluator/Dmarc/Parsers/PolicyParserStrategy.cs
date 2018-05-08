using System;
using Dmarc.DnsRecord.Evaluator.Dmarc.Domain;
using Dmarc.DnsRecord.Evaluator.Rules;

namespace Dmarc.DnsRecord.Evaluator.Dmarc.Parsers
{
    public class PolicyParserStrategy : ITagParserStrategy
    {
        public Tag Parse(string tag, string value)
        {
            PolicyType policyType;
            if (!Enum.TryParse(value, true, out policyType))
            {
                policyType = PolicyType.Unknown;
            }

            Policy policy = new Policy(tag, policyType);

            if (policyType == PolicyType.Unknown)
            {
                string errorMessage = string.Format(DmarcParserResource.InvalidValueErrorMessage, Tag, value);
                policy.AddError(new Error(ErrorType.Error, errorMessage));
            }

            return policy;
        }

        public string Tag => "p";

        public int MaxOccurences => 1;
    }
}