using System.Linq;
using Dmarc.DnsRecord.Evaluator.Rules;
using Dmarc.DnsRecord.Evaluator.Spf.Domain;
using Dmarc.DnsRecord.Evaluator.Spf.Explainers;

namespace Dmarc.DnsRecord.Evaluator.Spf.Rules.Record
{
    public class ShouldHaveHardFailAllEnabled : IRule<SpfRecord>
    {
        private readonly IQualifierExplainer _qualifierExplainer;

        public ShouldHaveHardFailAllEnabled(IQualifierExplainer qualifierExplainer)
        {
            _qualifierExplainer = qualifierExplainer;
        }

        public bool IsErrored(SpfRecord record, out Error error)
        {
            All all = record.Terms.OfType<All>().FirstOrDefault();

            if (all == null || all.Qualifier == Qualifier.Fail || all.Qualifier == Qualifier.SoftFail)
            {
                error = null;
                return false;
            }

            string errorMessage = string.Format(SpfRulesResource.ShouldHaveHardFailAllEnabledErrorMessage,
                _qualifierExplainer.Explain(Qualifier.Fail, true), _qualifierExplainer.Explain(Qualifier.SoftFail, true),
                all.Value, _qualifierExplainer.Explain(Qualifier.Fail, true));

            error = new Error(ErrorType.Warning, errorMessage);
            return true;
        }
    }
}