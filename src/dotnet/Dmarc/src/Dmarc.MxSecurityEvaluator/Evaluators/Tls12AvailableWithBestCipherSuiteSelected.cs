﻿using Dmarc.Common.Interface.Tls.Domain;

namespace Dmarc.MxSecurityEvaluator.Evaluators
{
    public interface ITls12AvailableWithBestCipherSuiteSelected : ITlsEvaluator { }

    public class Tls12AvailableWithBestCipherSuiteSelected : ITls12AvailableWithBestCipherSuiteSelected
    {
        private readonly string intro = "When testing TLS 1.2 with a range of cipher suites";

        public TlsEvaluatorResult Test(TlsConnectionResult tlsConnectionResult)
        {
            switch (tlsConnectionResult.Error)
            {
                case Error.TCP_CONNECTION_FAILED:
                case Error.SESSION_INITIALIZATION_FAILED:
                    return new TlsEvaluatorResult(EvaluatorResult.INCONCLUSIVE, $"{intro} we were unable to create a connection to the mail server. We will keep trying, so please check back later.");

                case null:
                    break;

                default:
                    return new TlsEvaluatorResult(EvaluatorResult.FAIL, $"{intro} the server responded with an error.");
            }

            switch (tlsConnectionResult.CipherSuite)
            {
                case CipherSuite.TLS_ECDHE_ECDSA_WITH_AES_256_GCM_SHA384:
                case CipherSuite.TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256:
                case CipherSuite.TLS_ECDHE_RSA_WITH_AES_256_GCM_SHA384:
                case CipherSuite.TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256:
                case CipherSuite.TLS_DHE_RSA_WITH_AES_256_GCM_SHA384:
                case CipherSuite.TLS_DHE_RSA_WITH_AES_128_GCM_SHA256:
                case CipherSuite.TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA384:
                case CipherSuite.TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA256:
                case CipherSuite.TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA384:
                case CipherSuite.TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA256:
                    return new TlsEvaluatorResult(EvaluatorResult.PASS);

                case CipherSuite.TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA:
                case CipherSuite.TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA:
                case CipherSuite.TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA:
                case CipherSuite.TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA:
                case CipherSuite.TLS_DHE_RSA_WITH_AES_256_CBC_SHA:
                case CipherSuite.TLS_DHE_RSA_WITH_AES_128_CBC_SHA:
                    return new TlsEvaluatorResult(EvaluatorResult.WARNING, $"{intro} the server selected a cipher suite that uses SHA-1.");

                case CipherSuite.TLS_RSA_WITH_AES_256_GCM_SHA384:
                case CipherSuite.TLS_RSA_WITH_AES_128_GCM_SHA256:
                case CipherSuite.TLS_RSA_WITH_AES_256_CBC_SHA256:
                case CipherSuite.TLS_RSA_WITH_AES_128_CBC_SHA256:
                    return new TlsEvaluatorResult(EvaluatorResult.WARNING, $"{intro} the server selected a cipher suite that has no Perfect Forward Secrecy (PFS).");

                case CipherSuite.TLS_RSA_WITH_AES_256_CBC_SHA:
                case CipherSuite.TLS_RSA_WITH_AES_128_CBC_SHA:
                    return new TlsEvaluatorResult(EvaluatorResult.WARNING, $"{intro} the server selected a cipher suite that has no Perfect Forward Secrecy (PFS) and that uses SHA-1.");

                case CipherSuite.TLS_RSA_WITH_3DES_EDE_CBC_SHA:
                case CipherSuite.TLS_DHE_DSS_WITH_3DES_EDE_CBC_SHA:
                    return new TlsEvaluatorResult(EvaluatorResult.WARNING, $"{intro} the server selected a cipher suite that has no Perfect Forward Secrecy (PFS) and that uses 3DES and SHA-1.");

                case CipherSuite.TLS_RSA_WITH_RC4_128_SHA:
                    return new TlsEvaluatorResult(EvaluatorResult.WARNING, $"{intro} the server selected a cipher suite that has no Perfect Forward Secrecy (PFS) and that uses RC4 and SHA-1.");

                case CipherSuite.TLS_RSA_WITH_RC4_128_MD5:
                case CipherSuite.TLS_NULL_WITH_NULL_NULL:
                case CipherSuite.TLS_RSA_WITH_NULL_MD5:
                case CipherSuite.TLS_RSA_WITH_NULL_SHA:
                case CipherSuite.TLS_RSA_EXPORT_WITH_RC4_40_MD5:
                case CipherSuite.TLS_RSA_EXPORT_WITH_RC2_CBC_40_MD5:
                case CipherSuite.TLS_RSA_EXPORT_WITH_DES40_CBC_SHA:
                case CipherSuite.TLS_RSA_WITH_DES_CBC_SHA:
                case CipherSuite.TLS_DH_DSS_EXPORT_WITH_DES40_CBC_SHA:
                case CipherSuite.TLS_DH_DSS_WITH_DES_CBC_SHA:
                case CipherSuite.TLS_DH_RSA_EXPORT_WITH_DES40_CBC_SHA:
                case CipherSuite.TLS_DH_RSA_WITH_DES_CBC_SHA:
                case CipherSuite.TLS_DHE_DSS_EXPORT_WITH_DES40_CBC_SHA:
                case CipherSuite.TLS_DHE_DSS_WITH_DES_CBC_SHA:
                case CipherSuite.TLS_DHE_RSA_EXPORT_WITH_DES40_CBC_SHA:
                case CipherSuite.TLS_DHE_RSA_WITH_DES_CBC_SHA:
                    return new TlsEvaluatorResult(EvaluatorResult.FAIL, $"{intro} the server selected an insecure cipher suite.");
            }

            return new TlsEvaluatorResult(EvaluatorResult.INCONCLUSIVE, $"{intro} there was a problem and we are unable to provide additional information.");
        }
    }
}
