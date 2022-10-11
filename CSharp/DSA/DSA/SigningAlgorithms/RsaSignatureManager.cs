using System.Security.Cryptography.X509Certificates;
using DSA.Serialization;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace DSA.SigningAlgorithms;

public static class RsaSignatureManager
{
    /// <summary>
    /// Sign payload
    /// </summary>
    /// <param name="data"></param>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    public static string Sign(object data, string privateKey)
    {
        var bytes = data.GetJsonBytes();

        var keyPair =
            (AsymmetricCipherKeyPair) new PemReader(new StringReader(privateKey)).ReadObject();

        var sig = SignerUtilities.GetSigner("SHA256withRSA");
        sig.Init(true, keyPair.Private);
        sig.BlockUpdate(bytes, 0, bytes.Length);
        return Convert.ToBase64String(sig.GenerateSignature());
    }

    /// <summary>
    /// Verify payload signature
    /// </summary>
    /// <param name="data"></param>
    /// <param name="signature"></param>
    /// <param name="publicKey"></param>
    /// <returns></returns>
    public static bool Verify(object data, string signature, string publicKey)
    {
        var sig = SignerUtilities.GetSigner("SHA256withRSA");

        var keyPair =
            (RsaKeyParameters) new PemReader(new StringReader(publicKey)).ReadObject();

        sig.Init(false, keyPair);

        var bytes = data.GetJsonBytes();

        var decodedSignature = Convert.FromBase64String(signature);

        sig.BlockUpdate(bytes, 0, bytes.Length);
        return sig.VerifySignature(decodedSignature);
    }

    /// <summary>
    /// Sign payload
    /// </summary>
    /// <param name="data"></param>
    /// <param name="certificate"></param>
    /// <returns></returns>
    public static string Sign(object data, X509Certificate2 certificate)
    {
        var bytes = data.GetJsonBytes();

        var keyPair = DotNetUtilities.GetKeyPair(certificate.GetRSAPrivateKey());

        var sig = SignerUtilities.GetSigner("SHA256withRSA");
        sig.Init(true, keyPair.Private);
        sig.BlockUpdate(bytes, 0, bytes.Length);
        return Convert.ToBase64String(sig.GenerateSignature());
    }

    /// <summary>
    /// Verify payload signature
    /// </summary>
    /// <param name="data"></param>
    /// <param name="signature"></param>
    /// <param name="certificate"></param>
    /// <returns></returns>
    public static bool Verify(object data, string signature, X509Certificate2 certificate)
    {
        var sig = SignerUtilities.GetSigner("SHA256withRSA");

        var keyPair = DotNetUtilities.GetKeyPair(certificate.GetRSAPrivateKey());

        sig.Init(false, keyPair.Public);

        var bytes = data.GetJsonBytes();

        var decodedSignature = Convert.FromBase64String(signature);

        sig.BlockUpdate(bytes, 0, bytes.Length);
        return sig.VerifySignature(decodedSignature);
    }
}