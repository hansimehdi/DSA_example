package org.example.dsa;

import com.google.common.io.BaseEncoding;
import org.example.DsaUtils;

import java.nio.charset.StandardCharsets;
import java.security.*;
import java.security.interfaces.RSAPrivateKey;
import java.security.interfaces.RSAPublicKey;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.PKCS8EncodedKeySpec;
import java.security.spec.X509EncodedKeySpec;

public final class RsaSignatureManager {

    private static final String PRIVATE_KEY_HEADER = "-----BEGIN PRIVATE KEY-----";
    private static final String RSA_PRIVATE_KEY_HEADER = "-----BEGIN RSA PRIVATE KEY-----";
    private static final String RSA_PRIVATE_KEY_FOOTER = "-----END RSA PRIVATE KEY-----";
    private static final String PRIVATE_KEY_FOOTER = "-----END PRIVATE KEY-----";
    private static final String PUBLIC_KEY_HEADER = "-----BEGIN PUBLIC KEY-----";
    private static final String PUBLIC_KEY_FOOTER = "-----END PUBLIC KEY-----";

    private RsaSignatureManager(){}

    /**
     * Sign message
     */
    public static String sign(String privateKey, String data) throws NoSuchAlgorithmException, InvalidKeySpecException, InvalidKeyException, SignatureException {
        Signature signer = Signature.getInstance("SHA256withRSA");
        signer.initSign(readPrivateKey(privateKey));
        signer.update(data.getBytes(StandardCharsets.UTF_8));
        return DsaUtils.bytesToBase64(signer.sign());
    }

    /**
     * Verify signature
     */
    public static boolean verify(String publicKey, String data, String signature) throws NoSuchAlgorithmException, InvalidKeySpecException, InvalidKeyException, SignatureException {
        Signature signer = Signature.getInstance("SHA256withRSA");
        signer.initVerify(readPublicKey(publicKey));
        signer.update(data.getBytes(StandardCharsets.UTF_8));
        return signer.verify(DsaUtils.decodeFromBase64(signature));
    }

    /**
     * Read RSA public key
     */
    private static RSAPublicKey readPublicKey(String pemPublicKey) throws NoSuchAlgorithmException, InvalidKeySpecException {
        String base64 = pemPublicKey.replace(PUBLIC_KEY_FOOTER, "")
                .replace(PUBLIC_KEY_HEADER, "");
        BaseEncoding encode = BaseEncoding.base64();
        X509EncodedKeySpec spec = new X509EncodedKeySpec(encode.withSeparator("\n", 64).decode(base64));
        KeyFactory keyFactory = KeyFactory.getInstance("RSA");
        return (RSAPublicKey) keyFactory.generatePublic(spec);
    }

    /**
     * Read RSA private key from
     */
    private static RSAPrivateKey readPrivateKey(String pemPrivateKey) throws NoSuchAlgorithmException, InvalidKeySpecException {
        String base64 = pemPrivateKey.replace(RSA_PRIVATE_KEY_HEADER + "\n", "")
                .replace(RSA_PRIVATE_KEY_FOOTER, "")
                .replace(PRIVATE_KEY_HEADER , "")
                .replace(PRIVATE_KEY_FOOTER, "");

        BaseEncoding encode = BaseEncoding.base64();

        PKCS8EncodedKeySpec spec = new PKCS8EncodedKeySpec(encode.withSeparator("\n", 64).decode(base64));

        KeyFactory keyFactory = KeyFactory.getInstance("RSA");
        return (RSAPrivateKey) keyFactory.generatePrivate(spec);
    }
}
